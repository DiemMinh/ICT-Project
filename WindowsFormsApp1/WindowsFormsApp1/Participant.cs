using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Participant
    {
        private string uID;
        private int age;
        private string gender;
        private string pregnant;
        private string lactating;
        private Dictionary<string, List<FoodRecord>> foodDict = new Dictionary<string, List<FoodRecord>>();
        // Component score
        private double veg_Score;
        private double fruit_Score;
        private double alcohol_Score;
        private double discretionary_Score;
        private double unsaturated_Score;
        private double fluids_Score;
        private double waterProportion_Score;
        private double protein_Score;
        private double dairy_Score;
        private double grain_Score;
        private double wholeGrainProportion_Score;
        private double reducedFatProportion_Score;
        //Variety score
        private double veg_Variety;
        private double fruit_Variety;
        private double grain_Variety;
        private double protein_Variety;
        private double dairy_Variety;


        public Participant(string uID, int age, string gender, string pregnant, string lactating)
        {
            this.uID = uID;
            this.age = age;
            this.gender = gender;
            this.pregnant = pregnant;
            this.lactating = lactating;
        }

        public string UID { get => uID; set => uID = value; }
        public int Age { get => age; set => age = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Pregnant { get => pregnant; set => pregnant = value; }
        public string Lactating { get => lactating; set => lactating = value; }
        public Dictionary<string, List<FoodRecord>> FoodDict { get => foodDict; set => foodDict = value; }
        public double Veg_Score { get => veg_Score; set => veg_Score = value; }
        public double Fruit_Score { get => fruit_Score; set => fruit_Score = value; }
        public double Alcohol_Score { get => alcohol_Score; set => alcohol_Score = value; }
        private double Discretionary_Score { get => discretionary_Score; set => discretionary_Score = value; }
        public double Unsaturated_Score { get => unsaturated_Score; set => unsaturated_Score = value; }
        public double WaterProportion_Score { get => waterProportion_Score; set => waterProportion_Score = value; }
        public double Fluids_Score { get => fluids_Score; set => fluids_Score = value; }
        public double Grain_Score { get => grain_Score; set => grain_Score = value; }
        public double WholeGrainProportion_Score { get => wholeGrainProportion_Score; set => wholeGrainProportion_Score = value; }
        public double Veg_Variety { get => veg_Variety; set => veg_Variety = value; }
        public double Fruit_Variety { get => fruit_Variety; set => fruit_Variety = value; }
        public double Grain_Variety { get => grain_Variety; set => grain_Variety = value; }
        public double Protein_Variety { get => protein_Variety; set => protein_Variety = value; }
        public double Dairy_Variety { get => dairy_Variety; set => dairy_Variety = value; }
        public double Protein_Score { get => protein_Score; set => protein_Score = value; }
        public double Dairy_Score { get => dairy_Score; set => dairy_Score = value; }
        public double ReducedFatProportion_Score { get => reducedFatProportion_Score; set => reducedFatProportion_Score = value; }

        public override string ToString()
        {
            return "ID: " + UID + ", Age: " + Age.ToString() + ", Gender: " + Gender + ", Pregnant: " + Pregnant + ", Lactation :" + "" + Lactating + ", Vegetables: " + Veg_Score + ", Veg Variety: " + Veg_Variety +
               ", Fruit: " + Fruit_Score + ", Grain: " + Grain_Score + ", Whole Grain: " + WholeGrainProportion_Score +", Protein: " + Protein_Score + ", Dairy: " + Dairy_Score + ", Reduced Fat: " + ReducedFatProportion_Score + ", Alcohol: " + Alcohol_Score + ", Discretionary: " + Discretionary_Score +
                ", Unsaturated: " + Unsaturated_Score + ", Fluid: " + Fluids_Score + ", Water Proportion: " + WaterProportion_Score;
        }

        // For testing
        public string ToString2()
        {
            return "ID:" + uID + ", " + "Age:" + age.ToString() + ", " + gender + ", " + pregnant + ", " + "Lactation:" + lactating;
        }

        // Vegetables
        public void CalculateScore()
        {
            // Veg
            double vegTotal = 0;
            int[] vegVarietyPoints = new int[21];  // Maximum variety points is 21
            // Alcohol
            double alcoholTotal = 0;
            // Fruit
            double fruitJuiceDried = 0;
            double fruitWithoutJuice = 0;
            int[] fruitVarietyPoints = new int[12];
            // Grain
            double totalGrain = 0;
            double totalWholeGrain = 0;
            int[] grainVarietyPoints = new int[12];
            // Lean meat
            double totalMeat = 0;
            // Dairy
            double totalDairy = 0;
            double totalReducedFat = 0;
            // Water
            double totalWater = 0;
            double totalBeverage = 0;
            // Unsaturated
            double totalSpread = 0;
            double totalOil = 0;
            // Discretionary total
            double discretionaryTotalEnergy = 0;

            List<String> abc = new List<String>();
            foreach (KeyValuePair<string, List<FoodRecord>> item in FoodDict)
            {
                // Maximum 1 serve of vegetables juice per day
                double vegJuice = 0;
                foreach (FoodRecord record in item.Value)
                {
     
                    int foodGroupInt = 0;
                    if (record.FoodGroup != "")
                    {
                        foodGroupInt = Int32.Parse(record.FoodGroup);
                    }
                    
                    // Vegetables 
                    // Calculate serve
                    record.CalculateVegServe(ref vegJuice, ref vegTotal);
                    // Calculate veg variety points
                    vegVarietyPoints = record.CalculateVegARFS(vegVarietyPoints);

                    // Fruit
                    record.CalculateFruitServe(foodGroupInt, ref fruitJuiceDried, ref fruitWithoutJuice);
                    // Calculate fruit variety points
                    fruitVarietyPoints = record.CalculateFruitARFS(fruitVarietyPoints);

                    // Grain and wholegrain
                    record.CalculateGrainServe(ref totalGrain);
                    // Proportion of wholegrain
                    record.CalculateWholeGrainServe(ref totalWholeGrain);

                    // Meat
                    record.CalculateProteinServe(foodGroupInt, ref totalMeat);
                    // Calculate meat variety points
                    //fruitVarietyPoints = record.CalculateFruitARFS(fruitVarietyPoints);

                    // Dairy
                    record.CalculateDairyServe(foodGroupInt, ref totalDairy);
                    record.CalculateReducedFatDairyServe(ref totalReducedFat);


                    // Unsaturated spread and oil
                    if (record.Discretionary == false && ((14301 <= foodGroupInt && foodGroupInt <= 14304)) || (14306 <= foodGroupInt && foodGroupInt <= 14307)
                        || (14401 <= foodGroupInt && foodGroupInt <= 14403) || record.FoodGroup.Equals("22102") || record.FoodGroup.Equals("22202") ||
                        record.FoodGroup.Equals("22204") || ((record.FoodGroup.Equals("24705")) && record.FoodName.CaseInsensitiveContains("Avocado")))
                    {
                        if (14401 <= foodGroupInt && foodGroupInt <= 14403)
                        {
                            totalOil += record.Weight_g;
                            //Console.WriteLine("Oil " + " " + record.FoodName + record.FoodGroup + " " + record.Weight_g);
                        }
                        else
                        {
                            totalSpread += record.Weight_g;
                            //Console.WriteLine("Spread " + " " + record.FoodName + record.FoodGroup + " " + record.Weight_g);
                        }
                    }


                    // Alcohol
                    if (record.Discretionary == true && ((29101 <= foodGroupInt && foodGroupInt <= 29505)))
                    {
                        //Console.WriteLine("Alcohol " + record.FoodGroup + " " + record.Alcoholic_drink_sd);
                        alcoholTotal += record.Alcoholic_drink_sd;
                    }

                    // Discretionary
                    if (record.Discretionary == true && (!(29101 <= foodGroupInt && foodGroupInt <= 29505)))
                    {
                        //Console.WriteLine("Discretionary " + record.FoodGroup + " " + record.Energy_kJ);
                        discretionaryTotalEnergy += record.Energy_kJ;
                    }

                    // Water and fluid
                    if (record.Discretionary == false && ((11701 <= foodGroupInt && foodGroupInt <= 11703) ||
                        (11101 <= foodGroupInt && foodGroupInt <= 11604) ||   (11801 <= foodGroupInt && foodGroupInt <= 11806) ||
                        (19101 <= foodGroupInt && foodGroupInt <= 19105) || foodGroupInt == 19109 
                        || (19801 <= foodGroupInt && foodGroupInt <= 19806) || (19101 <= foodGroupInt && foodGroupInt <= 19105) 
                        || (20101 <= foodGroupInt && foodGroupInt <= 20107) || (20201 <= foodGroupInt && foodGroupInt <= 20202)))
                    {
                        //Console.WriteLine("Fluid " + record.FoodGroup + " " + record.FoodName + " " + record.Weight_g);
                        totalBeverage += record.Weight_g;
                        if (11701 <= foodGroupInt && foodGroupInt <= 11703)
                        {
                            totalWater += record.Weight_g;
                        }
                    }
                    // 250 or 2000?
                    
                }
            }

            abc.Sort();
            foreach (String record in abc)
            {
                Console.WriteLine(record);
            }
            if (fruitJuiceDried > 1)
            {
                fruitJuiceDried = 1;
            }
            //Fruit DGI
            Fruit_Score = CalculateFruitScore(fruitWithoutJuice + fruitJuiceDried);
            Fruit_Variety = CalculateVariety(fruitVarietyPoints);
            // Veg DGI
            Veg_Score = CalculateVegScore(vegTotal);
            Veg_Variety = CalculateVariety(vegVarietyPoints);
            //Console.WriteLine("")
            // Protein DGI
            Protein_Score = CalculateTotalProteinScore(totalMeat);
            // Grain DGI
            Grain_Score = CalculateTotalGrainScore(totalGrain);
            WholeGrainProportion_Score = CalculateWholeGrainProportionScore(CalculateProportion(totalWholeGrain, totalGrain));
            // Dairy DGI
            Dairy_Score = CalculateTotalDairyScore(totalDairy);
            ReducedFatProportion_Score = CalculateReducedFatProportionScore(CalculateProportion(totalReducedFat, totalDairy));
            // Fluid DGI
            Fluids_Score = CalculateTotalBeverageScore(totalBeverage);
            waterProportion_Score = CalculateWaterProportionScore(CalculateProportion(totalWater, totalBeverage));
            // Alcohol DGI
            Alcohol_Score = CalculateAlcoholScore(alcoholTotal);
            // Discretionary DGI
            Discretionary_Score = CalculateDiscretionaryScore(discretionaryTotalEnergy / 600);
            // USFA DGI
            Unsaturated_Score = CalculateUnsaturatedScore(totalOil / 7 + totalSpread/10);
        }

        private double CalculateProportion(double part, double total)
        {
            return (part / total) * 100;
        }

        private double CalculateVariety(int[] VarietyPoints)
        {
            return (double)VarietyPoints.Sum() / VarietyPoints.Length * 2;
        }

        private double CalculateVegScore(double vegTotal)
        {
            double avg_Veg = vegTotal / FoodDict.Count;
            Console.WriteLine("Average serve for vegetables:" + uID + ":" + avg_Veg);
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
            if (avg_Veg >= recDailyServe)
            {
                return 10;
            }
            return avg_Veg * 10 / recDailyServe;
        }

        private double CalculateFruitScore(double fruitTotal)
        {
            double avg_Fruit = fruitTotal / FoodDict.Count;
            Console.WriteLine("Average serve for fruit:" + uID + ":" + avg_Fruit);
            double recDailyServe = 2;
            if (avg_Fruit >= recDailyServe)
            {
                return 10;
            }
            return avg_Fruit * 10 / recDailyServe;
        }

        private double CalculateTotalGrainScore(double totalGrain)
        {
            double avg_Grain = totalGrain / FoodDict.Count;
            Console.WriteLine("Average weight for grain:" + uID + ":" + avg_Grain);
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
            if (avg_Grain >= recDailyServe)
            {
                return 10;
            }
            return avg_Grain * 5 / recDailyServe;
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
            double avg_Meat = totalMeat / FoodDict.Count;
            Console.WriteLine("Average weight for meat:" + uID + ":" + avg_Meat);
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
            if (avg_Meat >= recDailyServe)
            {
                return 10;
            }
            return avg_Meat * 10 / recDailyServe;
        }

        private double CalculateTotalDairyScore(double totalDairy)
        {
            double avg_Dairy = totalDairy / FoodDict.Count;
            Console.WriteLine("Average weight for dairy:" + uID + ":" + avg_Dairy);
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
            if (avg_Dairy >= recDailyServe)
            {
                return 10;
            }
            return avg_Dairy * 5 / recDailyServe;
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
            double avg_BeverageWeight = totalBeverage / FoodDict.Count;
            double totalBeverageServe = totalBeverage / 250;
            double avg_BeverageServe = totalBeverageServe / FoodDict.Count;
            Console.WriteLine("Average weight for fluid:" + uID + ":" + avg_BeverageWeight);
            if (string.Equals(Gender, "Male", StringComparison.OrdinalIgnoreCase) && avg_BeverageServe >= 10)
            {
                return 5;
            }
            if (string.Equals(Gender, "Female", StringComparison.OrdinalIgnoreCase) && avg_BeverageServe >= 8)
            {
                return 5;
            }
            return avg_BeverageWeight * 5 / 2000;
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
            double avg_Unsaturated = unsaturatedServe / FoodDict.Count;
            Console.WriteLine("Average serve for Unsaturated Fat:" + uID + ":" + avg_Unsaturated);
            if (Age > 70)
            {
                if (avg_Unsaturated < 1 || avg_Unsaturated > 2)
                {
                    return 0;
                }
                return 10;
            }
            if (string.Equals(Gender, "Male", StringComparison.OrdinalIgnoreCase))
            {
                if (avg_Unsaturated > 4 || avg_Unsaturated < 1)
                {
                    return 0;
                }
                return 10;
            }
            if (string.Equals(Gender, "Female", StringComparison.OrdinalIgnoreCase))
            {
                if (avg_Unsaturated > 2 || avg_Unsaturated < 1)
                {
                    return 0;
                }
                return 10;
            }
            return 0;
        }

        private double CalculateAlcoholScore(double alcoholTotal)
        {
            double avg_Alcohol = alcoholTotal / FoodDict.Count;
            Console.WriteLine("Average serve for Alcohol:" + uID + ":" + avg_Alcohol);
           
                if (avg_Alcohol > 2)
                {
                    return 0;
                }
                return 10;
        }

        private double CalculateDiscretionaryScore(double discretionaryServe)
        {
            double avg_Discretionary = discretionaryServe / FoodDict.Count;
            double discretionaryScore = 0;
            Console.WriteLine("Average serve for Discretionary:" + uID + ":" + avg_Discretionary);
            double recDailyServe = 1;
            if (string.Equals(Gender, "Male", StringComparison.OrdinalIgnoreCase))
            {
                if (avg_Discretionary > 3)
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
                if (avg_Discretionary > 2.5)
                {
                    return 0;
                }
                else
                {
                    recDailyServe = 2.5;
                }
            }
            discretionaryScore = 30 - (avg_Discretionary * 30 / recDailyServe);
            return discretionaryScore;
        }
    }
}
