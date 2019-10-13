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
        private OleDbConnection discretionaryConnection = new OleDbConnection();

        HashSet<string> discretionaryList = new HashSet<string>();
        List<string> chosenDate = new List<string>();
        HashSet<Participant> participantList = new HashSet<Participant>();
        private string fileName;
        public ImportForm()
        {
            InitializeComponent();
        }
        public string FileName { get => fileName; set => fileName = value; }


        public String UID = "";
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

                    connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Persist Security Info=False;";
                    checkedListBox1.Items.Clear();
                    importUser();
                    richTextBox1.Show();
                    pictureBox2.Show();
                    checkedListBox1.Show();
                    button1.Show();
                    pictureBox3.Show();
                    calculateBtn.Show();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
            }
        }

        private void ImportForm_Load(object sender, EventArgs e)
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            discretionaryConnection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\discretionary food.accdb; Persist Security Info=False;";
            discretionaryImport();
        }

        public void importUser()
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;



                String query = "SELECT * FROM Documents where name like 'sfc%' and FolderName Like 'BASE%' and age Is Not Null and gender Is Not Null ORDER BY name;";

                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {

                    Participant userID = new Participant(reader["Name"].ToString(), Int32.Parse(reader["Age"].ToString()), reader["Gender"].ToString(), reader["Pregnancy"].ToString(), reader["Lactation"].ToString());
                    checkedListBox1.Items.Add(userID);
                }
                connection.Close();

            }
            catch (Exception ex) { MessageBox.Show("opps" + ex); }
        }



        private void CalculateBtn_Click(object sender, EventArgs e)
        {

            label1.Show();
            if (calculateBtn.Text.Equals("Calculate") && participantList.Count == 0) {
                try
                {
                    foreach (Participant userID in checkedListBox1.CheckedItems)
                    {
                        participantList.Add(userID);
                    }
                    //dayCount = checkedListBox1.CheckedItems.Count;
                    //test1.Text = dayCount.ToString();
                    //test1.Text = foodGroup[18];
                    foreach (Participant person in participantList)
                    {
                        connection.Open();
                        OleDbCommand command1 = new OleDbCommand();

                        command1.Connection = connection;
                        String query1 = "SELECT DISTINCT Day FROM DocFoods WHERE DocName = '" + person.UID + "'";
                        command1.CommandText = query1;
                        OleDbDataReader reader1 = command1.ExecuteReader();
                        while (reader1.Read())
                        {
                            OleDbCommand command2 = new OleDbCommand();
                            command2.Connection = connection;

                            String query2 = "SELECT * FROM DocFoods WHERE (((DocName)='" + person.UID + "') AND ((Day) = '" + reader1["Day"].ToString() + "') AND (Not(ErrorCode) = 2))";
                            command2.CommandText = query2;
                            OleDbDataReader reader2 = command2.ExecuteReader();
                            List<FoodRecord> foodList = new List<FoodRecord>();
                            while (reader2.Read())
                            {
                                FoodRecord record = new FoodRecord(reader2["FoodName"].ToString(), reader2["FoodGroup"].ToString(), reader2["Day"].ToString(), reader2["Weight_g"].ToString(), reader2["Sugars_g"].ToString()
                                , reader2["EnergyDF_kJ"].ToString(), reader2["Total_fat_g"].ToString(), reader2["Saturated_fat_g"].ToString(), reader2["Sodium_mg"].ToString(),
                                reader2["Vegetables_serve"].ToString(), reader2["Fruit_serve"].ToString(), reader2["Fruit_juice_serve"].ToString(), reader2["Grains_serve"].ToString(), reader2["Wholegrains_serve"].ToString()
                                , reader2["Protein_foods_serve"].ToString(), reader2["Legumes_protein_serve"].ToString(), reader2["Dairy_serve"].ToString(), reader2["Alcoholic_drinks_sd"].ToString());
                                record.Discretionary = record.DiscretionaryCheck(discretionaryList);
                                foodList.Add(record);
                            }
                            person.FoodDict.Add(reader1["Day"].ToString(), foodList);
                        }
                        connection.Close();


                        person.CalculateScore();


                        Console.WriteLine();
                        Console.WriteLine(person.ToString());
                        Console.WriteLine("Number of day: " + person.FoodDict.Count);
                    }
                    pictureBox4.Show();
                    label1.Text = "The file is Ready to be Exported";
                    calculateBtn.Text = "Reset";
                    button2.Show();
                    pictureBox5.Show();
                }
                catch (Exception ex) { MessageBox.Show("opps" + ex); }
            }
            
            else if (calculateBtn.Text.Equals("Reset"))
            {
                foreach (Participant person in participantList)
                {
                    person.Reset();
                }
                participantList.Clear();
                calculateBtn.Text = "Calculate";
                label1.Text = "Please wait...";
                label1.Hide();
                pictureBox4.Hide();
                button2.Hide();
                pictureBox5.Hide();
            }
        }


        //calc
        public void discretionaryImport()
        {
            discretionaryConnection.Open();

            OleDbCommand command1 = new OleDbCommand();
            command1.Connection = discretionaryConnection;
            String query1 = "SELECT * FROM [Major to minor food groups] WHERE ((([Major to minor food groups].Flag)='1'))";

            command1.CommandText = query1;
            OleDbDataReader reader1 = command1.ExecuteReader();
            while (reader1.Read())
            {
                discretionaryList.Add(reader1["FoodGroup"].ToString());
            }
            discretionaryConnection.Close();

        }


        private void PictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void CircularProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Label1_Click(object sender, EventArgs e)
        {
        }

        private void CheckedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                String query = "SELECT * FROM Documents where Name = '" + checkedListBox1.Text + "'";
                UID = checkedListBox1.Text;
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();


                connection.Close();

            }
            catch (Exception ex) { MessageBox.Show("opps" + ex); }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;

                OleDbDataReader reader = command.ExecuteReader();


                OleDbCommand command1 = new OleDbCommand();
                command1.Connection = connection;
                String query1 = "SELECT DISTINCT Day FROM DocFoods WHERE DocName = '" + UID + "'";

                command1.CommandText = query1;
                OleDbDataReader reader1 = command1.ExecuteReader();


                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                String query2 = "SELECT DISTINCT Day FROM DocFoods WHERE DocName = '" + UID + "'";

                command2.CommandText = query2;
                OleDbDataReader reader2 = command2.ExecuteReader();


                connection.Close();

            }
            catch (Exception ex) { MessageBox.Show("opps" + ex); }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("Select All"))
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
                button1.Text = "Unselect All";
            }
            else
            {
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
                button1.Text = "Select All";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }

}
