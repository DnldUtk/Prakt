namespace Prakt
{
    partial class DoctorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPatientSelect = new System.Windows.Forms.Label();
            this.cmbPatients = new System.Windows.Forms.ComboBox();
            this.grpPatientInfo = new System.Windows.Forms.GroupBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.dtpBirthDate = new System.Windows.Forms.DateTimePicker();
            this.lblBirthDate = new System.Windows.Forms.Label();
            this.txtPolicy = new System.Windows.Forms.TextBox();
            this.lblPolicy = new System.Windows.Forms.Label();
            this.txtFullName = new System.Windows.Forms.TextBox();
            this.lblFullName = new System.Windows.Forms.Label();
            this.grpAppointment = new System.Windows.Forms.GroupBox();
            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.txtDosage = new System.Windows.Forms.TextBox();
            this.lblDosage = new System.Windows.Forms.Label();
            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.txtMedication = new System.Windows.Forms.TextBox();
            this.lblMedication = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnPrintReport = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grpPatientInfo.SuspendLayout();
            this.grpAppointment.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(252, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Рабочее место врача";
            // 
            // lblPatientSelect
            // 
            this.lblPatientSelect.AutoSize = true;
            this.lblPatientSelect.Location = new System.Drawing.Point(20, 60);
            this.lblPatientSelect.Name = "lblPatientSelect";
            this.lblPatientSelect.Size = new System.Drawing.Size(110, 13);
            this.lblPatientSelect.TabIndex = 1;
            this.lblPatientSelect.Text = "Выберите пациента:";
            // 
            // cmbPatients
            // 
            this.cmbPatients.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPatients.FormattingEnabled = true;
            this.cmbPatients.Location = new System.Drawing.Point(20, 80);
            this.cmbPatients.Name = "cmbPatients";
            this.cmbPatients.Size = new System.Drawing.Size(300, 21);
            this.cmbPatients.TabIndex = 2;
            this.cmbPatients.SelectedIndexChanged += new System.EventHandler(this.cmbPatients_SelectedIndexChanged);
            // 
            // grpPatientInfo
            // 
            this.grpPatientInfo.Controls.Add(this.txtAddress);
            this.grpPatientInfo.Controls.Add(this.lblAddress);
            this.grpPatientInfo.Controls.Add(this.txtPhone);
            this.grpPatientInfo.Controls.Add(this.lblPhone);
            this.grpPatientInfo.Controls.Add(this.dtpBirthDate);
            this.grpPatientInfo.Controls.Add(this.lblBirthDate);
            this.grpPatientInfo.Controls.Add(this.txtPolicy);
            this.grpPatientInfo.Controls.Add(this.lblPolicy);
            this.grpPatientInfo.Controls.Add(this.txtFullName);
            this.grpPatientInfo.Controls.Add(this.lblFullName);
            this.grpPatientInfo.Location = new System.Drawing.Point(20, 115);
            this.grpPatientInfo.Name = "grpPatientInfo";
            this.grpPatientInfo.Size = new System.Drawing.Size(560, 140);
            this.grpPatientInfo.TabIndex = 4;
            this.grpPatientInfo.TabStop = false;
            this.grpPatientInfo.Text = "Информация о пациенте";
            // 
            // txtAddress
            // 
            this.txtAddress.Enabled = false;
            this.txtAddress.Location = new System.Drawing.Point(410, 45);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(130, 20);
            this.txtAddress.TabIndex = 9;
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(410, 25);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(41, 13);
            this.lblAddress.TabIndex = 8;
            this.lblAddress.Text = "Адрес:";
            // 
            // txtPhone
            // 
            this.txtPhone.Enabled = false;
            this.txtPhone.Location = new System.Drawing.Point(240, 95);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(150, 20);
            this.txtPhone.TabIndex = 7;
            // 
            // lblPhone
            // 
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new System.Drawing.Point(240, 75);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(55, 13);
            this.lblPhone.TabIndex = 6;
            this.lblPhone.Text = "Телефон:";
            // 
            // dtpBirthDate
            // 
            this.dtpBirthDate.Enabled = false;
            this.dtpBirthDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBirthDate.Location = new System.Drawing.Point(15, 95);
            this.dtpBirthDate.Name = "dtpBirthDate";
            this.dtpBirthDate.Size = new System.Drawing.Size(200, 20);
            this.dtpBirthDate.TabIndex = 5;
            // 
            // lblBirthDate
            // 
            this.lblBirthDate.AutoSize = true;
            this.lblBirthDate.Location = new System.Drawing.Point(15, 75);
            this.lblBirthDate.Name = "lblBirthDate";
            this.lblBirthDate.Size = new System.Drawing.Size(89, 13);
            this.lblBirthDate.TabIndex = 4;
            this.lblBirthDate.Text = "Дата рождения:";
            // 
            // txtPolicy
            // 
            this.txtPolicy.Enabled = false;
            this.txtPolicy.Location = new System.Drawing.Point(240, 45);
            this.txtPolicy.Name = "txtPolicy";
            this.txtPolicy.Size = new System.Drawing.Size(150, 20);
            this.txtPolicy.TabIndex = 3;
            // 
            // lblPolicy
            // 
            this.lblPolicy.AutoSize = true;
            this.lblPolicy.Location = new System.Drawing.Point(240, 25);
            this.lblPolicy.Name = "lblPolicy";
            this.lblPolicy.Size = new System.Drawing.Size(62, 13);
            this.lblPolicy.TabIndex = 2;
            this.lblPolicy.Text = "№ Полиса:";
            // 
            // txtFullName
            // 
            this.txtFullName.Enabled = false;
            this.txtFullName.Location = new System.Drawing.Point(15, 45);
            this.txtFullName.Name = "txtFullName";
            this.txtFullName.Size = new System.Drawing.Size(200, 20);
            this.txtFullName.TabIndex = 1;
            // 
            // lblFullName
            // 
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new System.Drawing.Point(15, 25);
            this.lblFullName.Name = "lblFullName";
            this.lblFullName.Size = new System.Drawing.Size(37, 13);
            this.lblFullName.TabIndex = 0;
            this.lblFullName.Text = "ФИО:";
            // 
            // grpAppointment
            // 
            this.grpAppointment.Controls.Add(this.txtInstructions);
            this.grpAppointment.Controls.Add(this.lblInstructions);
            this.grpAppointment.Controls.Add(this.txtDosage);
            this.grpAppointment.Controls.Add(this.lblDosage);
            this.grpAppointment.Controls.Add(this.txtDiagnosis);
            this.grpAppointment.Controls.Add(this.lblDiagnosis);
            this.grpAppointment.Controls.Add(this.txtMedication);
            this.grpAppointment.Controls.Add(this.lblMedication);
            this.grpAppointment.Location = new System.Drawing.Point(20, 270);
            this.grpAppointment.Name = "grpAppointment";
            this.grpAppointment.Size = new System.Drawing.Size(560, 245);
            this.grpAppointment.TabIndex = 5;
            this.grpAppointment.TabStop = false;
            this.grpAppointment.Text = "Диагноз и назначения";
            // 
            // txtInstructions
            // 
            this.txtInstructions.Location = new System.Drawing.Point(277, 204);
            this.txtInstructions.Multiline = true;
            this.txtInstructions.Name = "txtInstructions";
            this.txtInstructions.Size = new System.Drawing.Size(260, 20);
            this.txtInstructions.TabIndex = 5;
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Location = new System.Drawing.Point(277, 184);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(70, 13);
            this.lblInstructions.TabIndex = 4;
            this.lblInstructions.Text = "Инструкции:";
            // 
            // txtDosage
            // 
            this.txtDosage.Location = new System.Drawing.Point(12, 204);
            this.txtDosage.Multiline = true;
            this.txtDosage.Name = "txtDosage";
            this.txtDosage.Size = new System.Drawing.Size(250, 20);
            this.txtDosage.TabIndex = 3;
            // 
            // lblDosage
            // 
            this.lblDosage.AutoSize = true;
            this.lblDosage.Location = new System.Drawing.Point(12, 184);
            this.lblDosage.Name = "lblDosage";
            this.lblDosage.Size = new System.Drawing.Size(67, 13);
            this.lblDosage.TabIndex = 2;
            this.lblDosage.Text = "Дозировка:";
            // 
            // txtDiagnosis
            // 
            this.txtDiagnosis.Location = new System.Drawing.Point(15, 44);
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(525, 20);
            this.txtDiagnosis.TabIndex = 1;
            // 
            // lblDiagnosis
            // 
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Location = new System.Drawing.Point(15, 25);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(54, 13);
            this.lblDiagnosis.TabIndex = 0;
            this.lblDiagnosis.Text = "Диагноз:";
            // 
            // txtMedication
            // 
            this.txtMedication.Location = new System.Drawing.Point(15, 93);
            this.txtMedication.Multiline = true;
            this.txtMedication.Name = "txtMedication";
            this.txtMedication.Size = new System.Drawing.Size(525, 75);
            this.txtMedication.TabIndex = 7;
            // 
            // lblMedication
            // 
            this.lblMedication.AutoSize = true;
            this.lblMedication.Location = new System.Drawing.Point(12, 77);
            this.lblMedication.Name = "lblMedication";
            this.lblMedication.Size = new System.Drawing.Size(119, 13);
            this.lblMedication.TabIndex = 6;
            this.lblMedication.Text = "Лекарства / Лечение:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.White;
            this.btnSave.Location = new System.Drawing.Point(20, 521);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(150, 40);
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Сохранить";
            this.btnSave.UseVisualStyleBackColor = false;
            this.btnSave.Click += new System.EventHandler(this.btnSaveAppointment_Click);
            // 
            // btnPrintReport
            // 
            this.btnPrintReport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(100)))), ((int)(((byte)(200)))));
            this.btnPrintReport.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.btnPrintReport.ForeColor = System.Drawing.Color.White;
            this.btnPrintReport.Location = new System.Drawing.Point(190, 521);
            this.btnPrintReport.Name = "btnPrintReport";
            this.btnPrintReport.Size = new System.Drawing.Size(150, 40);
            this.btnPrintReport.TabIndex = 9;
            this.btnPrintReport.Text = "Печать";
            this.btnPrintReport.UseVisualStyleBackColor = false;
            this.btnPrintReport.Click += new System.EventHandler(this.btnPrintReport_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(540, 68);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(40, 40);
            this.btnRefresh.TabIndex = 3;
            this.btnRefresh.Text = "↻";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnRefresh.Paint += new System.Windows.Forms.PaintEventHandler(this.btnRefresh_Paint);
            // 
            // DoctorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 574);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblPatientSelect);
            this.Controls.Add(this.cmbPatients);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.grpPatientInfo);
            this.Controls.Add(this.grpAppointment);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnPrintReport);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "DoctorForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "АРМ Врача";
            this.grpPatientInfo.ResumeLayout(false);
            this.grpPatientInfo.PerformLayout();
            this.grpAppointment.ResumeLayout(false);
            this.grpAppointment.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPatientSelect;
        private System.Windows.Forms.ComboBox cmbPatients;
        private System.Windows.Forms.GroupBox grpPatientInfo;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.DateTimePicker dtpBirthDate;
        private System.Windows.Forms.Label lblBirthDate;
        private System.Windows.Forms.TextBox txtPolicy;
        private System.Windows.Forms.Label lblPolicy;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.GroupBox grpAppointment;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.TextBox txtDosage;
        private System.Windows.Forms.Label lblDosage;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.Label lblDiagnosis;

        // Исправленные поля
        private System.Windows.Forms.TextBox txtMedication;
        private System.Windows.Forms.Label lblMedication;

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnPrintReport;
        private System.Windows.Forms.Button btnRefresh;
    }
}