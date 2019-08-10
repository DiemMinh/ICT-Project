using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
        }

        private OleDbConnection connection = new OleDbConnection();

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open Text File";
            openFileDialog.Filter = "Access files|*.MDB;*.accdb";
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {

                    
                    string fileName = openFileDialog.FileName;
                    MessageBox.Show(fileName);

                    connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+fileName+";Persist Security Info=False;";
                    connection.Open();
                    connection.Close();
                    this.Hide();
                    Form2 frm2 = new Form2();
                    frm2.FileName = fileName;
                    frm2.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
        }
    }
}
