using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prakt
{
    public partial class DoctorForm : Form
    {
        private User _currentDoctor;
        private string _connectionString;

        public DoctorForm(User doctor, string connectionString)
        {
            InitializeComponent();
            _currentDoctor = doctor;
            _connectionString = connectionString;
            LoadPatients();
        }

        private void LoadPatients()
        {
            cmbPatients.Items.Clear();
            cmbPatients.Items.Add("-- Новый пациент --");

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT Id, FullName FROM Patients ORDER BY FullName";
                SqlCommand cmd = new SqlCommand(query, conn);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var patientItem = new PatientListItem
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        DisplayName = reader["FullName"].ToString()
                    };
                    cmbPatients.Items.Add(patientItem);
                }
                reader.Close();
            }
            cmbPatients.SelectedIndex = 0;
        }

        private void cmbPatients_SelectedIndexChanged(object sender, EventArgs e)
        {
            EnablePatientFields(true);
            if (cmbPatients.SelectedItem is PatientListItem selectedPatient)
            {
                if (selectedPatient == null) 
                { 
                    EnablePatientFields(true);
                    ClearPatientFields();
                }
                else
                {
                    LoadPatientDetails(selectedPatient.Id);
                    EnablePatientFields(true);
                }
            }
        }

        private void LoadPatientDetails(int patientId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string query = "SELECT FullName, BirthDate, PhoneNumber, Address, PolicyNumber FROM Patients WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Id", patientId);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    txtFullName.Text = reader["FullName"].ToString();

                    DateTime birthDate;
                    if (DateTime.TryParse(reader["BirthDate"].ToString(), out birthDate))
                        dtpBirthDate.Value = birthDate;
                    else
                        dtpBirthDate.Value = DateTime.Now;

                    txtPhone.Text = reader["PhoneNumber"].ToString();
                    txtAddress.Text = reader["Address"].ToString();
                    txtPolicy.Text = reader["PolicyNumber"].ToString();
                }
                reader.Close();
            }
        }

        private void EnablePatientFields(bool enabled)
        {
            txtFullName.Enabled = enabled;
            dtpBirthDate.Enabled = enabled;
            txtPhone.Enabled = enabled;
            txtAddress.Enabled = enabled;
            txtPolicy.Enabled = enabled;
        }

        private void ClearPatientFields()
        {
            txtFullName.Clear();
            dtpBirthDate.Value = DateTime.Now;
            txtPhone.Clear();
            txtAddress.Clear();
            txtPolicy.Clear();
            txtDiagnosis.Clear();
            txtMedication.Clear();
            txtDosage.Clear();
            txtInstructions.Clear();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ClearPatientFields();
            cmbPatients.SelectedIndex = 0; 
            LoadPatients(); 
            
        }

        private void btnSaveAppointment_Click(object sender, EventArgs e)
        {
            ///
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtPolicy.Text))
            {
                MessageBox.Show("Заполните ФИО и Номер полиса пациента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtDiagnosis.Text) || string.IsNullOrWhiteSpace(txtMedication.Text))
            {
                MessageBox.Show("Заполните Диагноз и Лекарство.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            int patientId = 0;

                            if (cmbPatients.SelectedItem is PatientListItem selectedItem && selectedItem.Id > 0)
                            {
                                patientId = selectedItem.Id;
                                //Обновление данных пациента
                                UpdatePatient(conn, transaction, patientId);
                            }
                            else
                            {
                                // Новый пациент
                                patientId = InsertNewPatient(conn, transaction);
                            }

                            //Сохранение Appointment
                            int appointmentId = SaveAppointmentOnly(conn, transaction, patientId, _currentDoctor.Id, txtDiagnosis.Text);

                            //Сохранение Prescription
                            SavePrescription(conn, transaction, appointmentId, txtMedication.Text, txtDosage.Text, txtInstructions.Text);

                            transaction.Commit();

                            MessageBox.Show($"Прием успешно сохранен!\nID приема: {appointmentId}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Очистка и обновление
                            ClearPatientFields();
                            cmbPatients.SelectedIndex = 0;
                            LoadPatients();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            throw; 
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Прием и возврат ID
        private int SaveAppointmentOnly(SqlConnection conn, SqlTransaction transaction, int patientId, int doctorId, string diagnosis)
        {
            string sql = @"INSERT INTO Appointments (PatientId, DoctorId, VisitDate, Diagnosis) 
                   VALUES (@PatientId, @DoctorId, GETDATE(), @Diagnosis); 
                   SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@PatientId", patientId);
                cmd.Parameters.AddWithValue("@DoctorId", doctorId);
                cmd.Parameters.AddWithValue("@Diagnosis", diagnosis);

                object result = cmd.ExecuteScalar();

                // Защита от Null
                if (result == null || result == DBNull.Value)
                {
                    throw new Exception("Не удалось получить ID созданного приема.");
                }

                return Convert.ToInt32(result);
            }
        }

        //Вставка назначения по ID приема
        private void SavePrescription(SqlConnection conn, SqlTransaction transaction, int appointmentId, string medication, string dosage, string instructions)
        {
            string sql = @"INSERT INTO Prescriptions (AppointmentId, MedicationName, Dosage, Instructions) 
                   VALUES (@AppointmentId, @MedicationName, @Dosage, @Instructions)";

            using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@AppointmentId", appointmentId);
                cmd.Parameters.AddWithValue("@MedicationName", medication);

                // Обработка NULL
                cmd.Parameters.AddWithValue("@Dosage", string.IsNullOrWhiteSpace(dosage) ? (object)DBNull.Value : dosage);
                cmd.Parameters.AddWithValue("@Instructions", string.IsNullOrWhiteSpace(instructions) ? (object)DBNull.Value : instructions);

                cmd.ExecuteNonQuery();
            }
        }

        //Создание нового пациента
        private int InsertNewPatient(SqlConnection conn, SqlTransaction transaction)
        {
            string sql = @"INSERT INTO Patients (FullName, BirthDate, PhoneNumber, Address, PolicyNumber) 
                   VALUES (@FullName, @BirthDate, @Phone, @Address, @Policy); 
                   SELECT SCOPE_IDENTITY();";

            using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);

                DateTime bDate = dtpBirthDate.Value;
                cmd.Parameters.AddWithValue("@BirthDate", bDate.Year > 1900 ? (object)bDate : DBNull.Value);

                cmd.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text);
                cmd.Parameters.AddWithValue("@Policy", txtPolicy.Text);

                object result = cmd.ExecuteScalar();
                if (result == null || result == DBNull.Value)
                    throw new Exception("Не удалось создать пациента.");

                return Convert.ToInt32(result);
            }
        }

        //Обновление существующего пациента
        private void UpdatePatient(SqlConnection conn, SqlTransaction transaction, int patientId)
        {
            string sql = @"UPDATE Patients SET 
                   FullName = @FullName, 
                   BirthDate = @BirthDate, 
                   PhoneNumber = @Phone, 
                   Address = @Address, 
                   PolicyNumber = @Policy 
                   WHERE Id = @Id";

            using (SqlCommand cmd = new SqlCommand(sql, conn, transaction))
            {
                cmd.Parameters.AddWithValue("@Id", patientId);
                cmd.Parameters.AddWithValue("@FullName", txtFullName.Text);

                DateTime bDate = dtpBirthDate.Value;
                cmd.Parameters.AddWithValue("@BirthDate", bDate.Year > 1900 ? (object)bDate : DBNull.Value);

                cmd.Parameters.AddWithValue("@Phone", string.IsNullOrWhiteSpace(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text);
                cmd.Parameters.AddWithValue("@Address", string.IsNullOrWhiteSpace(txtAddress.Text) ? (object)DBNull.Value : txtAddress.Text);
                cmd.Parameters.AddWithValue("@Policy", txtPolicy.Text);

                cmd.ExecuteNonQuery();
            }
        }

        private void btnRefresh_Paint(object sender, PaintEventArgs e)
        {
            GraphicsPath graphPath = new GraphicsPath();
            graphPath.AddEllipse(0, 0, btnRefresh.Width - 1, btnRefresh.Height - 1);
            btnRefresh.Region = new Region(graphPath);
        }

        private void btnPrintReport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtDiagnosis.Text))
            {
                MessageBox.Show("Нет данных для формирования отчета. Заполните информацию о пациенте и диагнозе.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                DataTable reportData = new DataTable();
                reportData.Columns.Add("PatientFullName", typeof(string));
                reportData.Columns.Add("BirthDate", typeof(string));
                reportData.Columns.Add("PolicyNumber", typeof(string));
                reportData.Columns.Add("Diagnosis", typeof(string));
                reportData.Columns.Add("MedicationName", typeof(string));
                reportData.Columns.Add("Dosage", typeof(string));
                reportData.Columns.Add("Instructions", typeof(string));
                reportData.Columns.Add("DoctorName", typeof(string));
                reportData.Columns.Add("VisitDate", typeof(string));

                reportData.Rows.Add(
                    txtFullName.Text,
                    dtpBirthDate.Value.ToShortDateString(),
                    txtPolicy.Text,
                    txtDiagnosis.Text,
                    txtMedication.Text,
                    string.IsNullOrWhiteSpace(txtDosage.Text) ? "—" : txtDosage.Text,
                    string.IsNullOrWhiteSpace(txtInstructions.Text) ? "—" : txtInstructions.Text,
                    _currentDoctor.FullName,
                    DateTime.Now.ToString("dd.MM.yyyy HH:mm")
                );

                using (Form repForm = new Form())
                {
                    repForm.Text = "Печать отчета";
                    repForm.Width = 800;
                    repForm.Height = 600;
                    repForm.StartPosition = FormStartPosition.CenterScreen;

                    Microsoft.Reporting.WinForms.ReportViewer reportViewer = new Microsoft.Reporting.WinForms.ReportViewer();
                    reportViewer.Dock = DockStyle.Fill;
                    reportViewer.LocalReport.ReportPath = "PatientReport.rdlc"; 

                    Microsoft.Reporting.WinForms.ReportDataSource rds = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", reportData);
                    reportViewer.LocalReport.DataSources.Clear();
                    reportViewer.LocalReport.DataSources.Add(rds);

                    reportViewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                    reportViewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent;
                    reportViewer.ZoomPercent = 100;

                    repForm.Controls.Add(reportViewer);

                    reportViewer.RefreshReport();
                    repForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при формировании отчета: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    

    //Показ в cmb имен
    public class PatientListItem
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }

        public override string ToString()
        {
            return DisplayName;
        }
    }
}
