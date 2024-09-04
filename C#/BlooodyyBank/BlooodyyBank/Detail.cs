using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace BlooodyyBank
{
    public partial class Detail : Form
    {
        private int _userId; // Private field to store the userId

        // Constructor that accepts a userId
        public Detail(int userId)
        {
            InitializeComponent();
            _userId = userId; // Store the userId
        }

        private void Detail_Load(object sender, EventArgs e)
        {
            // Use _userId to load relevant data or perform actions on form load
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddDonor();
        }

        private void AddDonor()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectDB"].ConnectionString;
            string hospital = textBox1.Text;
            string name = textBox2.Text;
            string gender = comboBox2.SelectedItem.ToString();
            string email = textBox4.Text;
            string address = textBox5.Text;
            string phoneNumber = textBox3.Text;
            string bloodType = comboBox1.SelectedItem.ToString();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO Donors (Hospital, Name, Gender, Email, Address, PhoneNumber, BloodType, UserID) " +
                               "VALUES (@Hospital, @Name, @Gender, @Email, @Address, @PhoneNumber, @BloodType, @UserID)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Hospital", hospital);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                    cmd.Parameters.AddWithValue("@BloodType", bloodType);
                    cmd.Parameters.AddWithValue("@UserID", _userId); // Use the stored userId

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Donor added successfully!");
                }
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
    }
}
