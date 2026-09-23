using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;
using Microsoft.VisualStudio.Services.UserAccountMapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;

namespace Prakt
{
    public partial class LoginForm : Form
    {
        private User _currentUser;
        private TextBox txtLogin;
        private TextBox txtPassword;
        private Button btnLogin;
        public LoginForm()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }
        private void InitializeCustomComponents()
        {
            this.Text = "Вход в систему";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            var lblTitle = new Label
            {
                Text = "Автоматизированное Рабочее Место\nмедицинского сотрудника",
                AutoSize = false,
                Size = new Size(350, 60),
                Location = new Point(25, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Arial", 12, FontStyle.Bold)
            };

            var lblLogin = new Label
            {
                Text = "Логин:",
                Location = new Point(50, 90),
                AutoSize = true
            };

            txtLogin = new TextBox
            {
                Location = new Point(50, 110),
                Size = new Size(280, 23),
                Name = "txtLogin"
            };

            var lblPassword = new Label
            {
                Text = "Пароль:",
                Location = new Point(50, 140),
                AutoSize = true
            };

            txtPassword = new TextBox
            {
                Location = new Point(50, 160),
                Size = new Size(280, 23),
                PasswordChar = '*',
                Name = "txtPassword"
            };

            btnLogin = new Button
            {
                Text = "Войти",
                Location = new Point(50, 200),
                Size = new Size(120, 35),
                Name = "btnLogin"
            };
            btnLogin.Click += BtnLogin_Click;

            var btnExit = new Button
            {
                Text = "Выход",
                Location = new Point(210, 200),
                Size = new Size(120, 35),
                Name = "btnExit"
            };
            btnExit.Click += (s, e) => Application.Exit();

            Controls.AddRange(new Control[] {
                lblTitle, lblLogin, txtLogin, lblPassword, txtPassword, btnLogin, btnExit
            });
        }
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string login = txtLogin.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Введите логин и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlParameter[] parameters = new SqlParameter[]
                {
                    new SqlParameter("@Login", login),
                    new SqlParameter("@Password", password)
                };

                string query = @"SELECT u.Id, u.Login, u.FullName, r.RoleName
                             FROM Users u
                             JOIN Roles r ON u.RoleId = r.Id
                             WHERE u.Login = @Login AND u.Password = @Password";

                DataTable dt = DatabaseHelper.ExecuteQuery(query, parameters);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    _currentUser = new User
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Login = row["Login"].ToString(),
                        FullName = row["FullName"].ToString(),
                        Role = row["RoleName"].ToString() == "Admin" ? UserRole.Admin : UserRole.Doctor
                    };

                    MessageBox.Show(string.Format("Добро пожаловать, {0}!", _currentUser.FullName), "Успех",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Hide();

                    Form nextForm;
                    if (_currentUser.Role == UserRole.Admin)
                    {
                        nextForm = new AdminForm(_currentUser);
                    }
                    else if (_currentUser.Role == UserRole.Doctor)
                    {
                        string connectionString = "Server=ZVERDVD\\MYBLATNOISERVER;Database=MedicalDB;Integrated Security=True;"; 
                        nextForm = new DoctorForm(_currentUser, connectionString);
                    }
                    else
                    {
                        MessageBox.Show("Неизвестная роль пользователя", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtLogin.Clear();
                        txtPassword.Clear();
                        this.Show();
                        return;
                    }

                    nextForm.ShowDialog();

                    txtLogin.Clear();
                    txtPassword.Clear();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка подключения к базе данных: {0}", ex.Message), "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
