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
    public partial class Form1 : Form
    {
        string conStr = @"Data Source=DESKTOP-VSNLTAM\SQLEXPRESS;Initial Catalog=DBProjectManager;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (emailInput.Text != "" || passwordInput.Text != "")
            {

                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"Select * from TBUsers Where Email = '{emailInput.Text}' and Password = '{passwordInput.Text}'";
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        bool hasRows = reader.HasRows;
                        connection.Close();

                        if (hasRows)
                        {
                            MessageBox.Show("Successfully loged in");
                            HomePage homePage = new HomePage();
                            homePage.SetEmail(emailInput.Text);
                            this.Hide();
                            homePage.Show();
                        }
                        else
                            MessageBox.Show("Try again...");
                    }
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            SignupPage signupPage = new SignupPage();
            this.Hide();
            signupPage.Show();
        }
    }
}


