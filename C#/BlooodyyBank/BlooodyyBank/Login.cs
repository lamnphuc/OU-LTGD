using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BlooodyyBank
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void PerformLogin()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectDB"].ConnectionString;
            string username = textBoxUsername.Text;
            string password = textBoxPassword.Text;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                // Retrieve UserID along with verifying username and password
                string query = "SELECT UserID FROM Users WHERE Username = @Username AND Password = @Password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password); // Direct comparison for demo purposes

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        int userId = Convert.ToInt32(result);
                        MessageBox.Show("Login successful!");

                        // Open the Detail form and pass the userId
                        Detail detailForm = new Detail(userId);
                        detailForm.Show();
                        this.Hide(); // Optionally hide the login form
                    }
                    else
                    {
                        MessageBox.Show("Invalid username or password!");
                    }
                }
            }
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                PerformLogin();
                e.SuppressKeyPress = true;
            }
        }
    }
}
