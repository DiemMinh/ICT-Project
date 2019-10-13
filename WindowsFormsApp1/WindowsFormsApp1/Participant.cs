using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Participant : IComparer<Participant>
    {
        private string uID;
        private int age;
        private string gender;
        private string pregnant;
        private string lactating;
        private Dictionary<string, List<FoodRecord>> foodDict = new Dictionary<string, List<FoodRecord>>();
        // Component score
        private double veg_DGI;
        private double fruit_DGI;
        private double alcohol_DGI;
        private double discretionary_DGI;
        private double unsaturated_DGI;
        private double fluids_DGI;
        private double waterProportion_DGI;
        private double protein_DGI;
        private double dairy_DGI;
        private double grain_DGI;
        private double wholeGrain_DGI;
        private double reducedFat_DGI;
        private double final_DGI;

        //Variety score
        private double veg_Variety;
        private double fruit_Variety;
        private double grain_Variety;
        private double protein_Variety;
        private double dairy_Variety;
        private double total_Variety;

        // Serves
        private double avg_Veg = 0;

        // Alcohol
        private double avg_Alcohol = 0;
        // Fruit
        private double avg_Fruit = 0;

        // Grain
        private double avg_Grain = 0;
        private double wholeGrainProportion = 0;

        // Lean meat
        private double avg_Protein = 0;

        // Dairy
        private double avg_Dairy = 0;
        private double reduceFatProportion = 0;

        // Water
        private double avg_Water = 0;
        private double avg_BeverageWeight = 0;

        // Unsaturated
        private double avg_Spread = 0;
        private double avg_Oil = 0;
        private double avg_Avocado = 0;
        private double avg_Unsaturated = 0;

        // Discretionary total
        private double avg_Discretionary = 0;

        public Participant(string uID, int age, string gender, string pregnant, string lactating)
        {
            this.uID = uID;
            this.age = age;
            this.gender = gender;
            this.pregnant = pregnant;
            this.lactating = lactating;
        }

        public void Reset()
        {
            Avg_Veg = 0;
            Avg_Alcohol = 0;
            Avg_Fruit = 0;
            Avg_Grain = 0;
            WholeGrainProportion = 0;
            Avg_Protein = 0;
            Avg_Dairy = 0;
            ReduceFatProportion = 0;
            Avg_Water = 0;
            avg_BeverageWeight = 0;
            Avg_Spread = 0;
            Avg_Oil = 0;
            Avg_Avocado = 0;
            Avg_Discretionary = 0;
            FoodDict.Clear();
        }

        public string UID { get => uID; set => uID = value; }
        public int Age { get => age; set => age = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Pregnant { get => pregnant; set => pregnant = value; }
        public string Lactating { get => lactating; set => lactating = value; }
        public Dictionary<string, List<FoodRecord>> FoodDict { get => foodDict; set => foodDict = value; }
        public double Veg_Score { get => veg_DGI; set => veg_DGI = value; }
        public double Fruit_Score { get => fruit_DGI; set => fruit_DGI = value; }
        public double Alcohol_Score { get => alcohol_DGI; set => alcohol_DGI = value; }
        private double Discretionary_Score { get => discretionary_DGI; set => discretionary_DGI = value; }
        public double Unsaturated_Score { get => unsaturated_DGI; set => unsaturated_DGI = value; }
        public double WaterProportion_Score { get => waterProportion_DGI; set => waterProportion_DGI = value; }
        public double Fluids_Score { get => fluids_DGI; set => fluids_DGI = value; }
        public double Grain_Score { get => grain_DGI; set => grain_DGI = value; }
        public double WholeGrainProportion_Score { get => wholeGrain_DGI; set => wholeGrain_DGI = value; }
        public double Veg_Variety { get => veg_Variety; set => veg_Variety = value; }
        public double Fruit_Variety { get => fruit_Variety; set => fruit_Variety = value; }
        public double Grain_Variety { get => grain_Variety; set => grain_Variety = value; }
        public double Protein_Variety { get => protein_Variety; set => protein_Variety = value; }
        public double Dairy_Variety { get => dairy_Variety; set => dairy_Variety = value; }
        public double Protein_Score { get => protein_DGI; set => protein_DGI = value; }
        public double Dairy_Score { get => dairy_DGI; set => dairy_DGI = value; }
        public double ReducedFatProportion_Score { get => reducedFat_DGI; set => reducedFat_DGI = value; }
        public double Avg_Veg { get => avg_Veg; set => avg_Veg = value; }
        public double Avg_Alcohol { get => avg_Alcohol; set => avg_Alcohol = value; }
        public double Avg_Fruit { get => avg_Fruit; set => avg_Fruit = value; }
        public double Avg_Grain { get => avg_Grain; set => avg_Grain = value; }
        public double WholeGrainProportion { get => wholeGrainProportion; set => wholeGrainProportion = value; }
        public double Avg_Protein { get => avg_Protein; set => avg_Protein = value; }
        public double Avg_Dairy { get => avg_Dairy; set => avg_Dairy = value; }
        public double ReduceFatProportion { get => reduceFatProportion; set => reduceFatProportion = value; }
        public double Avg_Water { get => avg_Water; set => avg_Water = value; }
        public double Avg_BeverageWeight { get => avg_BeverageWeight; set => avg_BeverageWeight = value; }
        public double Avg_Spread { get => avg_Spread; set => avg_Spread = value; }
        public double Avg_Oil { get => avg_Oil; set => avg_Oil = value; }
        public double Avg_Avocado { get => avg_Avocado; set => avg_Avocado = value; }
        public double Avg_Discretionary { get => avg_Discretionary; set => avg_Discretionary = value; }
        public double Final_DGI { get => final_DGI; set => final_DGI = value; }
        public double Total_Variety { get => total_Variety; set => total_Variety = value; }
        public double Avg_Unsaturated { get => avg_Unsaturated; set => avg_Unsaturated = value; }

        public override string ToString()
        {
            return "ID: " + UID + ", Age: " + Age.ToString() + ", Gender: " + Gender + ", Pregnant: " + Pregnant + ", Lactation :" + "" + Lactating + ", Vegetables: " + Veg_Score + ", Veg Variety: " + Veg_Variety +
                       ", Fruit: " + Fruit_Score + ", Fruit Variety: " + Fruit_Variety + ", Grain: " + Grain_Score + ", Whole Grain: " + WholeGrainProportion_Score + ", Grain Variety: " + Grain_Variety +
                       ", Protein: " + Protein_Score + ", Protein Variety: " + Protein_Variety + ", Dairy: " + Dairy_Score + ", Reduced Fat: " + ReducedFatProportion_Score + ", Dairy Variety: " + Dairy_Variety +
                       ", Alcohol: " + Alcohol_Score + ", Discretionary: " + Discretionary_Score +
                        ", Unsaturated: " + Unsaturated_Score + ", Fluid: " + Fluids_Score + ", Water Proportion: " + WaterProportion_Score;
        }

        // For testing
        public string ToString2()
        {

            return UID + ", Age: " + Age.ToString();
        }

        public int Compare(Participant x, Participant y)
        {
            if (x.uID == "" || y.uID == "")
            {
                return 0;
            }

            // CompareTo() method 
            return x.uID.CompareTo(y.uID);

        }

        // Vegetables
        public void CalculateScore()
        {
            // Variety
            // Maximum variety points is 21 for veg
            int[] vegVarietyPoints = new int[21];
            int[] fruitVarietyPoints = new int[12];
            int[] grainVarietyPoints = new int[13];
            int[] proteinVarietyPoints = new int[12];
            int[] dairyVarietyPoints = new int[6];

            // Serves
            double vegTotal = 0;

            // Alcohol
            double alcoholTotal = 0;
            // Fruit
            double fruitTotal = 0;

            // Grain
            double grainTotal = 0;
            double wholeGrainProportion = 0;

            // Lean meat
            double proteinTotal = 0;

            // Dairy
            double dairyTotal = 0;
            double reduceFatProportion = 0;

            // Water
            double waterTotal = 0;
            double beverageTotal = 0;

            // Unsaturated
            double spreadTotal = 0;
            double oilTotal = 0;
            double avocadoTotal = 0;

            // Discretionary total
            double discretionaryTotalEnergy = 0;

            List<String> abc = new List<String>();
            foreach (KeyValuePair<string, List<FoodRecord>> item in FoodDict)
            {
                // Maximum 1 serve of vegetables juice and fruit juice and dried fruit per day
                double fruitJuiceDried = 0;
                double vegJuice = 0;
                foreach (FoodRecord record in item.Value)
                {

                    int foodGroupInt = 0;
                    // If food group is not empty then they are not mixed dishes and food group can be converted to integer to set limit ranges
                    if (record.FoodGroup != "")
                    {
                        foodGroupInt = Int32.Parse(record.FoodGroup);
                    }

                    // Variety
                    // General RULES if no relevant food group codes (discretionary is not included in variety scoring):
                    if (record.Discretionary == false)
                    {
                        if (record.FoodGroup == "")
                        {
                            // If no food code, look for a key word in food description
                            record.checkVarietyWithoutCode(vegVarietyPoints, fruitVarietyPoints, grainVarietyPoints, proteinVarietyPoints, dairyVarietyPoints);
                        }
                        else
                        {
                            // If has food code, look for food code
                            record.checkVarietyWithCode(vegVarietyPoints, fruitVarietyPoints, grainVarietyPoints, proteinVarietyPoints, dairyVarietyPoints, foodGroupInt);
                        }
                    }

                    // Vegetables 
                    // Calculate serve
                    record.CalculateVegServe(ref vegJuice, ref vegTotal);

                    // Fruit
                    record.CalculateFruitServe(foodGroupInt, ref fruitJuiceDried, ref fruitTotal);

                    // Grain and wholegrain
                    record.CalculateGrainServe(ref grainTotal);
                    // Proportion of wholegrain
                    record.CalculateWholeGrainServe(ref wholeGrainProportion);

                    // Meat
                    record.CalculateProteinServe(foodGroupInt, ref proteinTotal);

                    // Dairy
                    record.CalculateDairyServe(foodGroupInt, ref dairyTotal);
                    // Reduced fat
                    record.CalculateReducedFatDairyServe(ref reduceFatProportion);

                    // Unsaturated spread and oil
                    record.CalculateUnsaturatedFatServe(foodGroupInt, ref oilTotal, ref avocadoTotal, ref spreadTotal);


                    // Alcohol
                    record.CalculateAlcoholSD(foodGroupInt, ref alcoholTotal);

                    // Discretionary
                    record.CalculateDiscretionaryServe(foodGroupInt, ref discretionaryTotalEnergy);

                    // Water and fluid
                    record.calculateFluidWeight(foodGroupInt, ref beverageTotal, ref waterTotal);

                }
            }
            /*
            abc.Sort();
            foreach (String record in abc)
            {
                Console.WriteLine(record);
            }
            */
            //Fruit DGI
            Fruit_Score = CalculateFruitScore(fruitTotal);
            // Veg DGI
            Veg_Score = CalculateVegScore(vegTotal);
            //Console.WriteLine("")
            // Protein DGI
            Protein_Score = CalculateTotalProteinScore(proteinTotal);
            // Grain DGI
            Grain_Score = CalculateTotalGrainScore(grainTotal);
            WholeGrainProportion_Score = CalculateWholeGrainProportionScore(CalculateProportion(wholeGrainProportion, grainTotal));
            // Dairy DGI
            Dairy_Score = CalculateTotalDairyScore(dairyTotal);
            ReducedFatProportion_Score = CalculateReducedFatProportionScore(CalculateProportion(reduceFatProportion, dairyTotal));
            // Fluid DGI
            Fluids_Score = CalculateTotalBeverageScore(beverageTotal);
            waterProportion_DGI = CalculateWaterProportionScore(CalculateProportion(waterTotal, beverageTotal));
            // Alcohol DGI
            Alcohol_Score = CalculateAlcoholScore(alcoholTotal);
            // Discretionary DGI
            Discretionary_Score = CalculateDiscretionaryScore(discretionaryTotalEnergy / 600);
            // USFA DGI
            Unsaturated_Score = CalculateUnsaturatedScore(oilTotal / 7 + spreadTotal / 10 + avocadoTotal / 30);
            // Variety DGI
            Veg_Variety = CalculateVarietyScore(vegVarietyPoints);
            Protein_Variety = CalculateVarietyScore(fruitVarietyPoints);
            Fruit_Variety = CalculateVarietyScore(fruitVarietyPoints);
            Dairy_Variety = CalculateVarietyScore(fruitVarietyPoints);
            Grain_Variety = CalculateVarietyScore(fruitVarietyPoints);
            Total_Variety = Veg_Variety + Protein_Variety + Fruit_Variety + Dairy_Variety + Grain_Variety;
            // final DGI 
            Final_DGI = Total_Variety + Veg_Score + Fruit_Score + Grain_Score + Dairy_Score + Protein_Score + Discretionary_Score + Unsaturated_Score + Alcohol_Score;

        }

        private double CalculateProportion(double part, double total)
        {
            return (part / total) * 100;
        }

        private double CalculateVarietyScore(int[] VarietyPoints)
        {
            return (double)VarietyPoints.Sum() / VarietyPoints.Length * 2;
        }

        private double CalculateVegScore(double vegTotal)
        {
            Avg_Veg = vegTotal / FoodDict.Count;
            Console.WriteLine("Average serve for vegetables:" + uID + ":" + Avg_Veg);
            double recDailyServe = 1;
            if (string.Equals(Gender, "Male", StringComparison.OrdinalIgnoreCase))
            {
                if (19 <= Age && Age <= 50)
                {
                    recDailyServe = 6;
                }
                else if (51 <= Age && Age <= 70)
                {
                    recDailyServe = 5.5;
                }
                else if (Age > 70)
                {
                    recDailyServe = 5;
                }
            }
            else if (string.Equals(Gender, "Female", StringComparison.OrdinalIgnoreCase))
            {
                // If pregnant
                if (!(string.Equals(Pregnant, "Not Pregnant", StringComparison.OrdinalIgnoreCase)))
                {
                    recDailyServe = 5;
                }
                else if (string.Equals(Lactating, "true", StringComparison.OrdinalIgnoreCase))
                {
                    recDailyServe = 7.5;
                }
                else if (19 <= Age && Age <= 50)
                {
                    recDailyServe = 5;
                }
                else if (51 <= Age && Age <= 70)
                {
                    recDailyServe = 5;
                }
                else if (Age > 70)
                {
                    recDailyServe = 5;
                }
            }
            if (Avg_Veg >= recDailyServe)
            {
                return 10;
            }
            return Avg_Veg * 10 / recDailyServe;
        }

        private double CalculateFruitScore(double fruitTotal)
        {
            Avg_Fruit = fruitTotal / FoodDict.Count;
            Console.WriteLine("Average serve for fruit:" + uID + ":" + avg_Fruit);
            double recDailyServe = 2;
            if (Avg_Fruit >= recDailyServe)
            {
                return 10;
            }
            return Avg_Fruit * 10 / recDailyServe;
        }

        private double CalculateTotalGrainScore(double totalGrain)
        {
            Avg_Grain = totalGrain / FoodDict.Count;
            Console.WriteLine("Average weight for grain:" + uID + ":" + Avg_Grain);
            double recDailyServe = 1;
            if (string.Equals(Gender, "Male", StringComparison.OrdinalIgnoreCase))
            {
                if (19 <= Age && Age <= 50)
                {
                    recDailyServe = 6;
                }
                else if (51 <= Age && Age <= 70)
                {
                    recDailyServe = 6;
                }
                else if (Age > 70)
                {
                    recDailyServe = 4.5;
                }
            }
            else if (string.Equals(Gender, "Female", StringComparison.OrdinalIgnoreCase))
            {
                // If pregnant
                if (!(string.Equals(Pregnant, "Not Pregnant", StringComparison.OrdinalIgnoreCase)))
                {
                    recDailyServe = 8.5;
                }
                else if (string.Equals(Lactating, "true", StringComparison.OrdinalIgnoreCase))
                {
                    recDailyServe = 9;
                }
                else if (19 <= Age && Age <= 50)
                {
                    recDailyServe = 6;
                }
                else if (51 <= Age && Age <= 70)
                {
                    recDailyServe = 4;
                }
                else if (Age > 70)
                {
                    recDailyServe = 3;
                }
            }
            if (Avg_Grain >= recDailyServe)
            {
                return 10;
            }
            return Avg_Grain * 5 / recDailyServe;
        }

        private double CalculateWholeGrainProportionScore(double proportion)
        {
            Console.WriteLine("Proportion of wholegrain:" + uID + ":" + proportion);
            if (proportion > 50)
            {
                return 5;
            }
            return 0;
        }

        private double CalculateTotalProteinScore(double totalMeat)
        {
            Avg_Protein = totalMeat / FoodDict.Count;
            Console.WriteLine("Average weight for meat:" + uID + ":" + Avg_Protein);
            double recDailyServe = 1;
            if (string.Equals(Gender, "Male", StringComparison.OrdinalIgnoreCase))
            {
                if (19 <= Age && Age <= 50)
                {
                    recDailyServe = 3;
                }
                else if (51 <= Age && Age <= 70)
                {
                    recDailyServe = 2.5;
                }
                else if (Age > 70)
                {
                    recDailyServe = 2.5;
                }
            }
            else if (string.Equals(Gender, "Female", StringComparison.OrdinalIgnoreCase))
            {
                // If pregnant
                if (!(string.Equals(Pregnant, "Not Pregnant", StringComparison.OrdinalIgnoreCase)))
                {
                    recDailyServe = 3.5;
                }
                else if (string.Equals(Lactating, "true", StringComparison.OrdinalIgnoreCase))
                {
                    recDailyServe = 2.5;
                }
                else if (19 <= Age && Age <= 50)
                {
                    recDailyServe = 2.5;
                }
                else if (51 <= Age && Age <= 70)
                {
                    recDailyServe = 2;
                }
                else if (Age > 70)
                {
                    recDailyServe = 2;
                }
            }
            if (Avg_Protein >= recDailyServe)
            {
                return 10;
            }
            return Avg_Protein * 10 / recDailyServe;
        }

        private double CalculateTotalDairyScore(double totalDairy)
        {
            double Avg_Dairy = totalDairy / FoodDict.Count;
            Console.WriteLine("Average weight for dairy:" + uID + ":" + Avg_Dairy);
            double recDailyServe = 1;
            if (string.Equals(Gender, "Male", StringComparison.OrdinalIgnoreCase))
            {
                if (19 <= Age && Age <= 50)
                {
                    recDailyServe = 3;
                }
                else if (51 <= Age && Age <= 70)
                {
                    recDailyServe = 2.5;
                }
                else if (Age > 70)
                {
                    recDailyServe = 3.5;
                }
            }
            else if (string.Equals(Gender, "Female", StringComparison.OrdinalIgnoreCase))
            {
                // If pregnant
                if (!(string.Equals(Pregnant, "Not Pregnant", StringComparison.OrdinalIgnoreCase)))
                {
                    recDailyServe = 2.5;
                }
                else if (string.Equals(Lactating, "true", StringComparison.OrdinalIgnoreCase))
                {
                    recDailyServe = 2.5;
                }
                else if (19 <= Age && Age <= 50)
                {
                    recDailyServe = 2.5;
                }
                else if (51 <= Age && Age <= 70)
                {
                    recDailyServe = 4;
                }
                else if (Age > 70)
                {
                    recDailyServe = 4;
                }
            }
            if (Avg_Dairy >= recDailyServe)
            {
                return 10;
            }
            return Avg_Dairy * 5 / recDailyServe;
        }

        private double CalculateReducedFatProportionScore(double proportion)
        {
            Console.WriteLine("Proportion of reduced fat:" + uID + ":" + proportion);
            if (proportion > 100)
            {
                return 5;
            }
            return 0;
        }

        private double CalculateTotalBeverageScore(double totalBeverage)
        {
            Avg_BeverageWeight = totalBeverage / FoodDict.Count;
            double totalBeverageServe = totalBeverage / 250;
            double avg_BeverageServe = totalBeverageServe / FoodDict.Count;
            Console.WriteLine("Average weight for fluid:" + uID + ":" + Avg_BeverageWeight);
            if (string.Equals(Gender, "Male", StringComparison.OrdinalIgnoreCase) && avg_BeverageServe >= 10)
            {
                return 5;
            }
            if (string.Equals(Gender, "Female", StringComparison.OrdinalIgnoreCase) && avg_BeverageServe >= 8)
            {
                return 5;
            }
            // 250 or 2000?
            return Avg_BeverageWeight * 5 / 2000;
        }

        private double CalculateWaterProportionScore(double proportion)
        {
            Console.WriteLine("Proportion of water:" + uID + ":" + proportion);
            if (proportion > 50)
            {
                return 5;
            }
            return 0;
        }

        private double CalculateUnsaturatedScore(double unsaturatedServe)
        {
            Avg_Unsaturated = unsaturatedServe / FoodDict.Count;
            Console.WriteLine("Average serve for Unsaturated Fat:" + uID + ":" + Avg_Unsaturated);
            if (Age > 70)
            {
                if (Avg_Unsaturated < 1 || Avg_Unsaturated > 2)
                {
                    return 0;
                }
                return 10;
            }
            if (string.Equals(Gender, "Male", StringComparison.OrdinalIgnoreCase))
            {
                if (Avg_Unsaturated > 4 || Avg_Unsaturated < 1)
                {
                    return 0;
                }
                return 10;
            }
            if (string.Equals(Gender, "Female", StringComparison.OrdinalIgnoreCase))
            {
                if (Avg_Unsaturated > 2 || Avg_Unsaturated < 1)
                {
                    return 0;
                }
                return 10;
            }
            return 0;
        }

        private double CalculateAlcoholScore(double alcoholTotal)
        {
            Avg_Alcohol = alcoholTotal / FoodDict.Count;
            Console.WriteLine("Average serve for Alcohol:" + uID + ":" + Avg_Alcohol);

            if (Avg_Alcohol > 2)
            {
                return 0;
            }
            return 10;
        }

        private double CalculateDiscretionaryScore(double discretionaryServe)
        {
            Avg_Discretionary = discretionaryServe / FoodDict.Count;
            double discretionaryScore = 0;
            Console.WriteLine("Average serve for Discretionary:" + uID + ":" + Avg_Discretionary);
            double recDailyServe = 1;
            if (string.Equals(Gender, "Male", StringComparison.OrdinalIgnoreCase))
            {
                if (Avg_Discretionary > 3)
                {
                    return 0;
                }
                else
                {
                    recDailyServe = 3;
                }
            }
            else if (string.Equals(Gender, "Female", StringComparison.OrdinalIgnoreCase))
            {
                if (Avg_Discretionary > 2.5)
                {
                    return 0;
                }
                else
                {
                    recDailyServe = 2.5;
                }
            }
            discretionaryScore = 30 - (Avg_Discretionary * 30 / recDailyServe);
            return discretionaryScore;
        }
    }
}
