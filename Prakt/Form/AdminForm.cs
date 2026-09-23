using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Prakt
{
    public partial class AdminForm : Form
    {
        private User _currentUser;
        private DataGridView dgvUsers;
        private TextBox txtNewLogin;
        private TextBox txtNewPassword;
        private TextBox txtNewFullName;
        private ComboBox cmbRole;
        private Button btnAddUser;

        public AdminForm(User currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();
            InitializeCustomComponents();
            LoadUsers();
        }

        private void InitializeCustomComponents()
        {
            this.Text = string.Format("Панель администратора - {0}", _currentUser.FullName);
            this.Size = new Size(800, 480);
            this.StartPosition = FormStartPosition.CenterScreen;

            var lblTitle = new Label
            {
                Text = "Управление пользователями",
                AutoSize = true,
                Location = new Point(20, 20),
                Font = new Font("Arial", 14, FontStyle.Bold)
            };

            dgvUsers = new DataGridView
            {
                Location = new Point(20, 50),
                Size = new Size(750, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            var grpAddUser = new GroupBox
            {
                Text = "Добавить нового пользователя",
                Location = new Point(20, 260),
                Size = new Size(750, 180)
            };

            var lblLogin = new Label
            {
                Text = "Логин:",
                Location = new Point(20, 30),
                AutoSize = true
            };

            txtNewLogin = new TextBox
            {
                Location = new Point(20, 55),
                Size = new Size(150, 23)
            };

            var lblPassword = new Label
            {
                Text = "Пароль:",
                Location = new Point(190, 30),
                AutoSize = true
            };

            txtNewPassword = new TextBox
            {
                Location = new Point(190, 55),
                Size = new Size(150, 23),
                PasswordChar = '*'
            };

            var lblFullName = new Label
            {
                Text = "ФИО:",
                Location = new Point(360, 30),
                AutoSize = true
            };

            txtNewFullName = new TextBox
            {
                Location = new Point(360, 55),
                Size = new Size(200, 23)
            };

            var lblRole = new Label
            {
                Text = "Роль:",
                Location = new Point(580, 30),
                AutoSize = true
            };

            cmbRole = new ComboBox
            {
                Location = new Point(580, 55),
                Size = new Size(150, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbRole.Items.Add("Admin");
            cmbRole.Items.Add("Doctor");
            cmbRole.SelectedIndex = 1;

            btnAddUser = new Button
            {
                Text = "Добавить пользователя",
                Location = new Point(20, 100),
                Size = new Size(150, 35)
            };
            btnAddUser.Click += BtnAddUser_Click;

            grpAddUser.Controls.AddRange(new Control[] {
                lblLogin, txtNewLogin, lblPassword, txtNewPassword,
                lblFullName, txtNewFullName, lblRole, cmbRole, btnAddUser
            });


            Controls.AddRange(new Control[] { lblTitle, dgvUsers, grpAddUser,});
        }
        private void LoadUsers()
        {
            try
            {
                string query = @"SELECT u.Id, u.Login, u.FullName, r.RoleName
                             FROM Users u
                             JOIN Roles r ON u.RoleId = r.Id
                             ORDER BY u.FullName";

                DataTable dt = DatabaseHelper.ExecuteQuery(query, null);
                dgvUsers.DataSource = dt;

                if (dgvUsers.Columns.Contains("Id"))
                    dgvUsers.Columns["Id"].HeaderText = "ID";
                if (dgvUsers.Columns.Contains("Login"))
                    dgvUsers.Columns["Login"].HeaderText = "Логин";
                if (dgvUsers.Columns.Contains("FullName"))
                    dgvUsers.Columns["FullName"].HeaderText = "ФИО";
                if (dgvUsers.Columns.Contains("RoleName"))
                    dgvUsers.Columns["RoleName"].HeaderText = "Роль";
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка загрузки пользователей: {0}", ex.Message), "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAddUser_Click(object sender, EventArgs e)
        {
            string login = txtNewLogin.Text.Trim();
            string password = txtNewPassword.Text;
            string fullName = txtNewFullName.Text.Trim();
            string roleName = cmbRole.SelectedItem.ToString();

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(fullName))
            {
                MessageBox.Show("Заполните все поля", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlParameter[] checkParams = new SqlParameter[]
                {
                    new SqlParameter("@Login", login)
                };

                string checkQuery = "SELECT COUNT(*) FROM Users WHERE Login = @Login";
                int count = Convert.ToInt32(DatabaseHelper.ExecuteScalar(checkQuery, checkParams));

                if (count > 0)
                {
                    MessageBox.Show("Пользователь с таким логином уже существует", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int roleId = roleName == "Admin" ? 1 : 2;

                SqlParameter[] insertParams = new SqlParameter[]
                {
                    new SqlParameter("@Login", login),
                    new SqlParameter("@Password", password),
                    new SqlParameter("@FullName", fullName),
                    new SqlParameter("@RoleId", roleId)
                };

                string insertQuery = @"INSERT INTO Users (Login, Password, FullName, RoleId)
                                   VALUES (@Login, @Password, @FullName, @RoleId)";

                DatabaseHelper.ExecuteNonQuery(insertQuery, insertParams);

                MessageBox.Show("Пользователь успешно добавлен", "Успех",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                txtNewLogin.Clear();
                txtNewPassword.Clear();
                txtNewFullName.Clear();
                cmbRole.SelectedIndex = 1;

                LoadUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ошибка добавления пользователя: {0}", ex.Message), "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

