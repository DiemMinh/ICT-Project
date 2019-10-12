using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        private OleDbConnection loginConnection = new OleDbConnection();
        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\Snack Study Baseline Food Diaries.MDB;
                Persist Security Info=False;";
            loginConnection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\\login.accdb;
                Persist Security Info=False;";
            //check thi part!!!!!!!!!!!!!!!!!!!!!!
            connection.Open();
            
            connection.Close();
            //check thi part!!!!!!!!!!!!!!!!!!!!!!
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            loginConnection.Open();
            OleDbCommand command = new OleDbCommand();
            command.Connection = loginConnection;
            command.CommandText = "select * from login where Username='" + txtBox_Username.Text + "' and Password='" + txtBox_Password.Text + "'";

            OleDbDataReader reader = command.ExecuteReader();
            int count = 0;
            while (reader.Read())
            {
                count++;
            }
            if (count == 1)
            {
                MessageBox.Show("Welcome " + txtBox_Username.Text);
                loginConnection.Close();
                loginConnection.Dispose();
                    this.Hide();
                ImportForm impForm = new ImportForm();
                impForm.ShowDialog();


            }
            else if (count > 1)
            {
                MessageBox.Show("Duplicate username and password");
            }
            else
            {
                MessageBox.Show("Username and password is not correct");
            }
            loginConnection.Close();
        }

        private void ToolTip2_Popup(object sender, PopupEventArgs e)
        {

        }

        private void Check_Click(object sender, EventArgs e)
        {

        }
    }
}
