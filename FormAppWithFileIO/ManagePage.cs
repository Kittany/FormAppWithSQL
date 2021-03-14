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

namespace FormAppWithFileIO
{
    public partial class ManagePage : Form
    {
        string conStr = @"Data Source=DESKTOP-VSNLTAM\SQLEXPRESS;Initial Catalog=DBProjectManager;Integrated Security=True";
        string email;
        public ManagePage()
        {
            InitializeComponent();
        }
        public void SetEmail(string email) => this.email = email;
        private void btnGoBack_Click(object sender, EventArgs e)
        {
            HomePage homePage = new HomePage();
            homePage.SetEmail(email);
            this.Hide();
            homePage.Show();
        }
        private void ShowTable()
        {
            SqlConnection connection = new SqlConnection(conStr);
            SqlCommand command = new SqlCommand();
            command.CommandText = "SELECT * FROM TBUsers";
            command.Connection = connection;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            DataTable users = new DataTable();
            users.Load(reader);
            dataGridView.DataSource = users;
            connection.Close();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (emailInput.Text == "" && passwordInput.Text == "" && rePasswordInput.Text == "")
                MessageBox.Show("Please fill all boxes");
            else
            {
                if (passwordInput.Text != "" && passwordInput.Text == rePasswordInput.Text)
                {
                    using (SqlConnection connection = new SqlConnection(conStr))
                    {
                        using (SqlCommand command = new SqlCommand())
                        {
                            command.Connection = connection;
                            command.CommandText = $"Select * from TBUsers Where Email = '{emailInput.Text}'";
                            connection.Open();
                            SqlDataReader reader = command.ExecuteReader();
                            bool hasRows = reader.HasRows;
                            connection.Close();

                            if (hasRows)
                            {
                                command.CommandText = $"UPDATE TBUsers " +
                                    $" SET Password = '{passwordInput.Text}', RePassword = '{rePasswordInput.Text}' " +
                                    $" Where Email = '{emailInput.Text}'";
                                connection.Open();
                                int result = command.ExecuteNonQuery();
                                connection.Close();
                                MessageBox.Show(result == 1 ? "One Row Affected" : "Failed");
                                ShowTable();
                            }
                            else
                                MessageBox.Show("Try again...");
                        }
                    }
                }
                else
                    MessageBox.Show("Try again...");
            }
        }

        private void ManagePage_Load(object sender, EventArgs e)
        {
            ShowTable();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (emailInput.Text != "")
                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"Select * from TBUsers Where Email = '{emailInput.Text}'";
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        bool hasRows = reader.HasRows;
                        connection.Close();

                        if (hasRows)
                        {
                            command.CommandText = $"DELETE FROM TBUsers Where Email = '{emailInput.Text}'";
                            connection.Open();
                            int result = command.ExecuteNonQuery();
                            connection.Close();
                            MessageBox.Show(result == 1 ? "One Row Affected" : "Failed");
                            ShowTable();
                        }
                        else
                            MessageBox.Show("Try again...");
                    }
                }
            else
                MessageBox.Show("Please fill email box");
        }
    }
}
