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
    public partial class Form2 : Form
    {
        private string fileName;
        private OleDbConnection connection = new OleDbConnection();

        private OleDbConnection loginConnection = new OleDbConnection();

        private OleDbConnection discretionaryConnection = new OleDbConnection();
        public String UID = "";
        // 125 group discretionary if >30g sugar/100g
        public const double discre125 = 0.3;
        // 125 group with fruit discretionary if >35g sugar/100g
        public const double discre125Fruit = 0.35;
        // 181 group discretionary if >10g total fat/100g
        public const double discre181 = 0.1;
        // 182 group discretionary if >10g total fat/100g
        public const double discre182 = 0.1;
        // 183 group discretionary if >10g total fat/100g
        public const double discre183 = 0.1;
        // 184 group discretionary if >10g total fat/100g
        public const double discre184 = 0.1;
        // 18703 group discretionary if fast food chain or >5g saturated fat/100g
        public const double discre18703 = 0.05;
        // 18707 group discretionary if >5g saturated fat/100g
        public const double discre18707 = 0.05;
        // 18801 group discretionary if >5g saturated fat/100g
        public const double discre18801 = 0.05;
        // 18903 group discretionary if fast food chain or >10g total fat/100g, or >5g saturated fat/100g, or 450mg Na/100g
        public const double discre18903TotalFat = 0.1;
        public const double discre18903SaturatedFat = 0.05;
        public const double discre18903Na = 4.5;

        // 18906 group discretionary if fast food chain or >30g sugar/100g
        public const double discre18906 = 0.3;
        // 26202 group discretionary if fast food chain or >5g saturated fat/100g, or 450mg Na/100g
        public const double discre26202SaturatedFat = 0.05;
        public const double discre26202Na = 4.5;
        // 22203 group always not discretionary

        DateTime start;
        DateTime end;
        public string gender;
        public int age;



        HashSet<string> discretionaryList = new HashSet<string>();
        List<string> chosenDate = new List<string>();
        HashSet<Participant> participantList = new HashSet<Participant>();


        public int dayCount;

        public string FileName { get => fileName; set => fileName = value; }

        public Form2()
        {
            InitializeComponent();  
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            string executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
            string path = (System.IO.Path.GetDirectoryName(executable));
            AppDomain.CurrentDomain.SetData("DataDirectory", path);


            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Persist Security Info=False;";

            discretionaryConnection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=|DataDirectory|\discretionary food.accdb; Persist Security Info=False;";

            connection.Open();
            //to be deleted
            test.Text = "Wolfgang";
            connection.Close();

            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                discretionaryImport();


                String query = "SELECT * FROM Documents where name like 'sfc%' and FolderName Like 'BASE%' and age Is Not Null and gender Is Not Null ;";
                
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox.Items.Add(reader["Name"].ToString());
                    Participant userID = new Participant(reader["Name"].ToString(), Int32.Parse(reader["Age"].ToString()), reader["Gender"].ToString(), reader["Pregnancy"].ToString(), reader["Lactation"].ToString());
                    participantList.Add(userID);
                    /*foreach(Participant i in participantList)
                    {
                        Console.WriteLine(i.ToString2());
                    }*/
                }
                connection.Close();

            }
            catch (Exception ex) { MessageBox.Show("opps" + ex); }

        }

        private void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                String query = "SELECT * FROM Documents where Name = '" + comboBox.Text + "'";
                UID = comboBox.Text;
                command.CommandText = query;
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    genderLabel.Text = reader["Gender"].ToString();
                    gender = genderLabel.Text;
                    ageLabel.Text = reader["Age"].ToString();
                    age = Int32.Parse(reader["Age"].ToString());
                    test1.Text = age.ToString();
                    checkedListBox1.Items.Clear();
                    StartDateComboBox.Items.Clear();
                    EndDateComboBox.Items.Clear();
                }

                OleDbCommand command1 = new OleDbCommand();
                command1.Connection = connection;
                String query1 = "SELECT DISTINCT Day FROM DocFoods WHERE DocName = '" + UID + "'";

                command1.CommandText = query1;
                OleDbDataReader reader1 = command1.ExecuteReader();
                while (reader1.Read())
                {
                    StartDateComboBox.Items.Add(reader1["Day"].ToString());
                    checkedListBox1.Items.Add(reader1["Day"].ToString());
                }

                OleDbCommand command2 = new OleDbCommand();
                command2.Connection = connection;
                String query2 = "SELECT DISTINCT Day FROM DocFoods WHERE DocName = '" + UID + "'";

                command2.CommandText = query2;
                OleDbDataReader reader2 = command2.ExecuteReader();
                while (reader2.Read())
                {
                    EndDateComboBox.Items.Add(reader2["Day"].ToString());
                }

                connection.Close();

            }
            catch (Exception ex) { MessageBox.Show("opps" + ex); }
        }

        private void StartDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            start = Convert.ToDateTime(StartDateComboBox.Text);
            test1.Text = start.ToLongDateString();
        }

        private void test1_Click(object sender, EventArgs e)
        {
        }

        private void EndDateComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            end = Convert.ToDateTime(EndDateComboBox.Text);
            test1.Text = end.ToLongDateString();
            if (end < start)
            {
                MessageBox.Show("opsss, it seams that the end date is before the start date!!");
                calculateBtn.Hide();

            }
            else
            {
                calculateBtn.Show();
            }
        }

        private void calculateBtn_Click(object sender, EventArgs e)
        {
            try
            {
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
                    HashSet<String> dayList = new HashSet<string>();
                    while (reader1.Read())
                    {
                        dayList.Add(reader1["Day"].ToString());
                    }
                    connection.Close();

                    foreach (String day in dayList) {
                        connection.Open();
                        OleDbCommand command2 = new OleDbCommand();
                        command2.Connection = connection;

                        String query2 = "SELECT * FROM DocFoods WHERE (((DocName)='" + person.UID + "') AND ((Day) = '" + day + "') AND (Not(ErrorCode) = 2))";
                        command2.CommandText = query2;
                        OleDbDataReader reader2 = command2.ExecuteReader();
                        List<FoodRecord> foodList = new List<FoodRecord>();
                        while (reader2.Read())
                        {
                            FoodRecord record = new FoodRecord(reader2["FoodName"].ToString(), reader2["FoodGroup"].ToString(), reader2["Day"].ToString(), reader2["Weight_g"].ToString(), reader2["Sugars_g"].ToString()
                            , reader2["EnergyDF_kJ"].ToString(), reader2["Total_fat_g"].ToString(), reader2["Saturated_fat_g"].ToString(), reader2["Sodium_mg"].ToString(),
                            reader2["Vegetables_serve"].ToString(), reader2["Fruit_serve"].ToString(), reader2["Fruit_juice_serve"].ToString(), reader2["Grains_serve"].ToString(), reader2["Wholegrains_serve"].ToString()
                            , reader2["Protein_foods_serve"].ToString(), reader2["Legumes_protein_serve"].ToString(), reader2["Dairy_serve"].ToString(), reader2["Alcoholic_drinks_sd"].ToString());
                            record.Discretionary = DiscretionaryCheck(record);
                            foodList.Add(record);
                        }
                        person.FoodDict.Add(day, foodList);
                        connection.Close();
                    }
                    person.CalculateScore();

                    if (person.UID == "SFC059")
                    {
                        int count = 0;
                        Console.WriteLine(person.UID.ToString());
                        foreach (KeyValuePair<string, List<FoodRecord>> item in person.FoodDict)
                        {
                            count++;
                            Console.WriteLine("Day" + count + ": {0}", item.Key);
                            foreach (FoodRecord record in item.Value)
                            {
                                Console.WriteLine(record.ToString());
                            }
                        }
                    }
                    Console.WriteLine();
                    Console.WriteLine(person.ToString());
                    Console.WriteLine("Number of day: " + person.FoodDict.Count);
                }
            }
            catch (Exception ex) { MessageBox.Show("opps" + ex); }
        }

        private void CheckedListBox1_ItemCheck(Object sender, ItemCheckEventArgs e)
        {
            // Get the current number checked.
            int num_checked = checkedListBox1.CheckedItems.Count;

            // See if the item is being checked or unchecked.
            if ((e.CurrentValue != CheckState.Checked) &&
                (e.NewValue == CheckState.Checked))
                num_checked++;
            if ((e.CurrentValue == CheckState.Checked) &&
                (e.NewValue != CheckState.Checked))
                num_checked--;

            // Display the count.
            test1.Text = checkedListBox1.Items.Count + " items, " +
                num_checked + " selected";
        }

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

        private bool DiscretionaryCheck(FoodRecord record) {
            if (discretionaryList.Contains(record.FoodGroup))
            {
                return true;
            }
            // 125 group discretionary if >30g sugar/100g or >35g sugar/100 g with fruit
            if (record.FoodGroup.StartsWith("125"))
            {
                // Contain fruit if 12508, 12509, 12510, 12514, 12515
                if (record.FoodGroup.StartsWith("12508") || record.FoodGroup.StartsWith("12509") || record.FoodGroup.StartsWith("12510") 
                    || record.FoodGroup.StartsWith("12514") || record.FoodGroup.StartsWith("12515")){
                    double discretionaryIf = record.Weight_g * discre125Fruit;
                    if (record.Sugar > discretionaryIf)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    double discretionaryIf = record.Weight_g * discre125;
                    if (record.Sugar > discretionaryIf)
                    {
                        return true;
                    }
                    return false;
                }
            }
            // 181 group discretionary if >10g total fat/100g
            if (record.FoodGroup.StartsWith("181"))
            {
                double discretionaryIf = record.Weight_g * discre181;
                if (record.Total_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 182 group discretionary if >10g total fat/100g
            if (record.FoodGroup.StartsWith("182"))
            {
                double discretionaryIf = record.Weight_g * discre182;
                if (record.Total_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 183 group discretionary if >10g total fat/100g
            if (record.FoodGroup.StartsWith("183"))
            {
                double discretionaryIf = record.Weight_g * discre183;
                if (record.Total_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 184 group discretionary if >10g total fat/100g
            if (record.FoodGroup.StartsWith("184"))
            {
                double discretionaryIf = record.Weight_g * discre184;
                if (record.Total_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 18703 group discretionary if >10g total fat/100g
            if (record.FoodGroup.StartsWith("18703"))
            {
                double discretionaryIf = record.Weight_g * discre18703;
                if (record.Saturated_fat_g > discretionaryIf || record.FoodName.Contains("fast food chain"))
                {
                    return true;
                }
                return false;
            }
            // 18707 group discretionary if >10g total fat/100g
            if (record.FoodGroup.StartsWith("18707"))
            {
                double discretionaryIf = record.Weight_g * discre18707;
                if (record.Saturated_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 18801 group discretionary if >10g total fat/100g
            if (record.FoodGroup.StartsWith("18801"))
            {
                double discretionaryIf = record.Weight_g * discre18801;
                if (record.Saturated_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 18707 group discretionary if >10g total fat/100g
            if (record.FoodGroup.StartsWith("18903"))
            {
                double discretionaryIf1 = record.Weight_g * discre18903TotalFat;
                double discretionaryIf2 = record.Weight_g * discre18903SaturatedFat;
                double discretionaryIf3 = record.Weight_g * discre18903Na;
                if (record.Total_fat_g > discretionaryIf1 || record.Saturated_fat_g > discretionaryIf2 || record.Sodium_mg > discre18903Na || record.FoodName.Contains("fast food chain"))
                {
                    return true;
                }
                return false;
            }
            if (record.FoodGroup.StartsWith("18906"))
            {
                double discretionaryIf = record.Weight_g * discre18906;
                if (record.Sugar > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            if (record.FoodGroup.StartsWith("26202"))
            {
                double discretionaryIf1 = record.Weight_g * discre26202SaturatedFat;
                double discretionaryIf2 = record.Weight_g * discre26202Na;
                if (record.Saturated_fat_g > discretionaryIf1 || record.Sodium_mg > discretionaryIf2)
                {
                    return true;
                }
                return false;
            }
            if (record.FoodGroup.StartsWith("22203") && record.FoodName.CaseInsensitiveContains("coconut"))
            {
                return true;
            }
            else
            {
                return false;
            }

            // Hot chocolate is discretionary
            // Write later
        }

        private void CheckedListBox_ItemCheck(Object sender, ItemCheckEventArgs e)
        {

        }


        private void checkedListBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void test2_Click(object sender, EventArgs e)
        {

        }
    }
}

