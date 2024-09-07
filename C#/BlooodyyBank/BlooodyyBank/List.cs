using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Forms;

namespace BlooodyyBank
{
    public partial class List : Form
    {
        public List()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            // Connection string to your database
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectDB"].ConnectionString;

            // SQL query to retrieve data
            string query = "SELECT * FROM Donors";

            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Use SqlConnection and SqlDataAdapter to fill the DataTable
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
            }

            // Bind the DataTable to the DataGridView
            dataGridView1.DataSource = dataTable;
        }
    }
}