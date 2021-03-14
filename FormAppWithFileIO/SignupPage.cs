using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormAppWithFileIO
{
    public partial class SignupPage : Form
    {
        string conStr = @"Data Source=DESKTOP-VSNLTAM\SQLEXPRESS;Initial Catalog=DBProjectManager;Integrated Security=True";
        public SignupPage()
        {
            InitializeComponent();
        }

        private void btnSignup_Click(object sender, EventArgs e)
        {
            if (CheckValidation())
            {
                string firstName = tbFirstName.Text, lastName = tbLastName.Text, email = tbEmail.Text,
                pass = tbPassword.Text, RePass = tbRePassword.Text, state = tbState.Text, role = tbRole.Text;

                using (SqlConnection connection = new SqlConnection(conStr))
                {
                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = $"INSERT INTO TBUsers(FirstName, LastName, State, Role, Email, Password, RePassword) " +
                    $"VALUES('{firstName}','{lastName}','{state}','{role}','{email}','{pass}','{RePass}')";
                        connection.Open();
                        int res = command.ExecuteNonQuery();
                        connection.Close();
                        MessageBox.Show((res == 1 ? "" : "not") + "Succeeded");
                    }
                }
                Form1 LoginPage = new Form1();

                this.Hide();
                LoginPage.Show();
            }
            else
                MessageBox.Show("Error 404");

        }

        private bool CheckValidation()
        {
            string email = tbEmail.Text;
            bool check = (tbEmail.Text != "" && tbFirstName.Text != "" && tbLastName.Text != "" && tbPassword.Text != "" && tbRePassword.Text != "" && tbRole.Text != "" && tbState.Text != "");
            if (!check)
                return false;
            if (tbPassword.Text != tbRePassword.Text)
                return false;
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (match.Success)
                return true;
            else
                return false;
        }

        private void SignupPage_Load(object sender, EventArgs e)
        {

        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            Form1 loginPage = new Form1();
            this.Hide();
            loginPage.Show();
        }
    }
}
