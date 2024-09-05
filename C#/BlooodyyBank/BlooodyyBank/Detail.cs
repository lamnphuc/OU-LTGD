using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using BlooodyyBank.Models;

namespace BlooodyyBank
{
    public partial class Detail : Form
    {
        private int _userId;

        public Detail(int userId)
        {
            InitializeComponent();
            _userId = userId; // Store the userId
        }

        private void AddDonor()
        {
            try
            {
                string hospital = textBox1.Text;
                string name = textBox2.Text;
                string gender = comboBox2.SelectedItem?.ToString();
                string email = textBox4.Text;
                string address = textBox5.Text;
                string phoneNumber = textBox3.Text;
                string bloodType = comboBox1.SelectedItem?.ToString();

                Donor donor = new Donor(hospital, name, gender, email, address, phoneNumber, bloodType, _userId);

                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectDB"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO Donors (Hospital, Name, Gender, Email, Address, PhoneNumber, BloodType, UserID) " +
                                   "VALUES (@Hospital, @Name, @Gender, @Email, @Address, @PhoneNumber, @BloodType, @UserID)";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Hospital", donor.Hospital);
                        cmd.Parameters.AddWithValue("@Name", donor.Name);
                        cmd.Parameters.AddWithValue("@Gender", donor.Gender);
                        cmd.Parameters.AddWithValue("@Email", donor.Email);
                        cmd.Parameters.AddWithValue("@Address", donor.Address);
                        cmd.Parameters.AddWithValue("@PhoneNumber", donor.PhoneNumber);
                        cmd.Parameters.AddWithValue("@BloodType", donor.BloodType);
                        cmd.Parameters.AddWithValue("@UserID", donor.UserID);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show($"{rowsAffected} row(s) inserted.");

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Donor added successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No rows were inserted. Please check the data.");
                        }

                        ResetForm();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }



        private void ResetForm()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            AddDonor();
        }
    }
}
