using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BlooodyyBank
{
    public partial class Detail : Form
    {
        // Connection string for SQL Server
        private string connectionString = @"Data Source=YOUR_SERVER;Initial Catalog=YOUR_DB;Integrated Security=True";

        public Detail()
        {
            InitializeComponent();
        }

        // Add button
        private void button1_Click(object sender, EventArgs e)
        {
            // Get the input values
            string hospital = textBox1.Text;
            string name = textBox2.Text;
            string gender = comboBox2.SelectedItem.ToString();
            string email = textBox4.Text;
            string address = textBox5.Text;
            string phoneNumber = textBox3.Text;
            string bloodType = comboBox1.SelectedItem.ToString();

            // Validate input
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Name and Email are required.");
                return;
            }

            // Insert into database
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Donors (Hospital, Name, Gender, Email, Address, PhoneNumber, BloodType) VALUES (@Hospital, @Name, @Gender, @Email, @Address, @PhoneNumber, @BloodType)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Hospital", hospital);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@BloodType", bloodType);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Donor Added Successfully");
                    ResetFields();
                }
            }
        }

        // Delete button
        private void button2_Click(object sender, EventArgs e)
        {
            string email = textBox4.Text;
            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please enter the email of the donor to delete.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Donors WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Donor deleted successfully.");
                        ResetFields();
                    }
                    else
                    {
                        MessageBox.Show("No donor found with this email.");
                    }
                }
            }
        }

        // Reset button
        private void button5_Click(object sender, EventArgs e)
        {
            ResetFields();
        }

        // Show List button
        private void button4_Click(object sender, EventArgs e)
        {
            ShowDonorsList();
        }

        // Method to reset all input fields
        private void ResetFields()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            textBox4.Text = "";
            textBox5.Text = "";
        }

        // Method to show list of donors (can be improved with a DataGridView)
        private void ShowDonorsList()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM Donors";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // Display in a message box (you can use a DataGridView for a better UI)
                    string donorsList = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        donorsList += $"Name: {row["Name"]}, Email: {row["Email"]}, Blood Type: {row["BloodType"]}\n";
                    }

                    MessageBox.Show(donorsList);
                }
            }
        }
    }
}