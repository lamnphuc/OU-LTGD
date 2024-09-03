using BlooodyyBank.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BlooodyyBank
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        public static class UserData
        {
            // Connection string stored in App.config or web.config
            private static readonly string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["MyConnectionString"].ConnectionString;
            // Save user data to SQL Server
            public static void SaveUser(User user)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL Query to insert the user data
                    string query = "INSERT INTO Users (Username, Password, FullName, Email) VALUES (@Username, @Password, @FullName, @Email)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Add parameters to prevent SQL injection
                        cmd.Parameters.AddWithValue("@Username", user.Username);
                        cmd.Parameters.AddWithValue("@Password", user.Password);
                        cmd.Parameters.AddWithValue("@FullName", user.FullName);
                        cmd.Parameters.AddWithValue("@Email", user.Email);

                        cmd.ExecuteNonQuery();
                    }
                }
            }

            // Check if username is already taken in the database
            public static bool IsUsernameTaken(string username)
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // SQL Query to check if username exists
                    string query = "SELECT COUNT(1) FROM Users WHERE Username = @Username";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);

                        return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                    }
                }
            }
        }

        private void registerButton_Click(object sender, EventArgs e)
        {
            string fullName = textBoxFullName.Text.Trim();
            string username = UserName.Text.Trim();
            string password = Password.Text.Trim();
            string email = Email.Text.Trim();

            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(username) ||
                string.IsNullOrEmpty(password) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (UserData.IsUsernameTaken(username))
            {
                MessageBox.Show("Username is already taken. Please choose another one.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            User newUser = new User(fullName, username, password, email)
            {
                Username = username,
                Password = password, 
                FullName = fullName,
                Email = email
            };

            UserData.SaveUser(newUser);

            MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
