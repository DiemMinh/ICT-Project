using Microsoft.Office.Interop.Excel;
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
                

            }
            catch (Exception ex) {
                MessageBox.Show("Failed to import file ! Should be import file Snack Study Baseline Food Diaries.MDF");
            }
            finally
            {
                connection.Close();
            }
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
            using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Excel Workbook|*.xls", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                    Workbook wb = app.Workbooks.Add(XlSheetType.xlWorksheet);
                    Worksheet ws = (Worksheet)app.ActiveSheet;
                    
                    app.Visible = false;
                    ws.Cells[1, 1] = "ID";
                    ws.Cells[1, 2] = "Age";
                    ws.Cells[1, 3] = "Gender";
                    ws.Cells[1, 4] = "Pregnant";
                    ws.Cells[1, 5] = "Lactatating";
                    ws.Cells[1, 6] = "final_DGI";
                    ws.Cells[1, 7] = "veg_Variety";
                    ws.Cells[1, 8] = "fruit_Variety";
                    ws.Cells[1, 9] = "grain_Variety";
                    ws.Cells[1, 10] = "protein_Variety";
                    ws.Cells[1, 11] = "dairy_Variety";
                    ws.Cells[1, 12] = "Variety_DGI";
                    ws.Cells[1, 13] = "avg_Veg";
                    ws.Cells[1, 14] = "Veg_DGI";
                    ws.Cells[1, 15] = "avg_Fruit";
                    ws.Cells[1, 16] = "Fruit_DGI";
                    ws.Cells[1, 17] = "avg_Grain";
                    ws.Cells[1, 18] = "Grain_DGI";
                    ws.Cells[1, 19] = "wholeGrainProportion";
                    ws.Cells[1, 20] = "WholeGrain_DGI";
                    ws.Cells[1, 21] = "avg_Protein";
                    ws.Cells[1, 22] = "Protein_DGI";
                    ws.Cells[1, 23] = "avg_Dairy";
                    ws.Cells[1, 24] = "Dairy_DGI";
                    ws.Cells[1, 25] = "reduceFatProportion";
                    ws.Cells[1, 26] = "ReducedFat_DGI";
                    ws.Cells[1, 27] = "avg_Discretionary";
                    ws.Cells[1, 28] = "Discretionary_DGI";
                    ws.Cells[1, 29] = "avg_Alcohol";
                    ws.Cells[1, 30] = "Alcohol_DGI";
                    ws.Cells[1, 31] = "avg_BeverageWeight";
                    ws.Cells[1, 32] = "fluids_DGI";
                    ws.Cells[1, 33] = "waterProportion";
                    ws.Cells[1, 34] = "WaterProportion_DGI";
                    ws.Cells[1, 35] = "avg_Spread";
                    ws.Cells[1, 36] = "avg_Oil";
                    ws.Cells[1, 37] = "avg_Avocado";
                    ws.Cells[1, 38] = "avg_Unsaturated";
                    ws.Cells[1, 39] = "Unsaturated_DGI";
                    ws.Columns.AutoFit();
                    ws.Rows.AutoFit();
                    int i = 2;
                    foreach (Participant item in participantList)
                    {
                        ws.Cells[i, 1] = item.UID;
                        ws.Cells[i, 2] = item.Age;
                        ws.Cells[i, 3] = item.Gender;
                        ws.Cells[i, 4] = item.Pregnant;
                        ws.Cells[i, 5] = item.Lactating;
                        ws.Cells[i, 6] = item.Final_DGI;
                        ws.Cells[i, 7] = item.Veg_Variety;
                        ws.Cells[i, 8] = item.Fruit_Variety;
                        ws.Cells[i, 9] = item.Grain_Variety;
                        ws.Cells[i, 10] = item.Protein_Variety;
                        ws.Cells[i, 11] = item.Dairy_Variety;
                        ws.Cells[i, 12] = item.Total_Variety;
                        ws.Cells[i, 13] = item.Avg_Veg;
                        ws.Cells[i, 14] = item.Veg_Score;
                        ws.Cells[i, 15] = item.Avg_Fruit;
                        ws.Cells[i, 16] = item.Fruit_Score;
                        ws.Cells[i, 17] = item.Avg_Grain;
                        ws.Cells[i, 18] = item.Grain_Score;
                        ws.Cells[i, 19] = item.WholeGrainProportion;
                        ws.Cells[i, 20] = item.WholeGrainProportion_Score;
                        ws.Cells[i, 21] = item.Avg_Protein;
                        ws.Cells[i, 22] = item.Protein_Score;
                        ws.Cells[i, 23] = item.Avg_Dairy;
                        ws.Cells[i, 24] = item.Dairy_Score;
                        ws.Cells[i, 25] = item.ReduceFatProportion;
                        ws.Cells[i, 26] = item.ReducedFatProportion_Score;
                        ws.Cells[i, 27] = item.Avg_Discretionary;
                        ws.Cells[i, 28] = item.Discretionary_Score;
                        ws.Cells[i, 29] = item.Avg_Alcohol;
                        ws.Cells[i, 30] = item.Alcohol_Score;
                        ws.Cells[i, 31] = item.Avg_BeverageWeight;
                        ws.Cells[i, 32] = item.Fluids_Score;
                        ws.Cells[i, 33] = item.WaterProportion;
                        ws.Cells[i, 34] = item.WaterProportion_Score;
                        ws.Cells[i, 35] = item.Avg_Spread;
                        ws.Cells[i, 36] = item.Avg_Oil;
                        ws.Cells[i, 37] = item.Avg_Avocado;
                        ws.Cells[i, 38] = item.Avg_Unsaturated;
                        ws.Cells[i, 39] = item.Unsaturated_Score;
                        ws.Columns.AutoFit();
                        ws.Rows.AutoFit();
                        i++;
                    }
                    wb.SaveAs(sfd.FileName, XlFileFormat.xlWorkbookDefault, Type.Missing, Type.Missing, true, false, XlSaveAsAccessMode.xlNoChange, XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                    app.Quit();
                    MessageBox.Show("Your data has been sucessfully exported.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

}
