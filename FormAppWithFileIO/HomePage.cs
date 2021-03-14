using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormAppWithFileIO
{
    public partial class HomePage : Form
    {
        string conStr = @"Data Source=DESKTOP-VSNLTAM\SQLEXPRESS;Initial Catalog=DBProjectManager;Integrated Security=True";
        string email;
        public HomePage() // ADD UPDATE DELETE MANAGE
        {
            InitializeComponent();
        }
        public HomePage(string email) => this.email = email;


        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            this.Hide();
            form1.Show();
        }

        private void HomePage_Load(object sender, EventArgs e)
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

        public void SetEmail(string email) => this.email = email;
        private void btnShowCurrentUser_Click(object sender, EventArgs e) // FIX
        {
            SqlConnection connection = new SqlConnection(conStr);
            SqlCommand command = new SqlCommand();
            command.CommandText = $"SELECT * FROM TBUsers where Email = '{email}'";
            command.Connection = connection;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            DataTable users = new DataTable();
            users.Load(reader);
            dataGridView1.DataSource = users;


            connection.Close();

        }

        private void btnManage_Click(object sender, EventArgs e)
        {
            ManagePage managePage = new ManagePage();
            managePage.SetEmail(email);
            this.Hide();
            managePage.Show();
        }
    }
}
