using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class FoodRecord
    {
        private string foodName;
        private string foodGroup;
        private string day;
        private bool discretionary;
        private double weight_g;
        // To identify discretionary beyond 5-digit code
        private double sugar;
        private double energy_kJ;
        private double total_fat_g;
        private double saturated_fat_g;
        private double sodium_mg;
        // vegetables
        private double vegetables_serve;
        // fruit
        private double fruit_serve;
        private double fruit_juice_serve;
        // Grain
        private double grain_serve;
        private double wholegrains_serve;
        // Meat
        private double protein_foods_serve;
        // Dairy
        private double dairy;
        // Alcohol
        private double alcoholic_drink_sd;

        public FoodRecord(string foodName, string foodGroup, string day, string weight_g, string sugar, string energy_kJ,
            string total_fat_g, string saturated_fat_g, string sodium_mg, string vegetables_serve, string fruit_serve, string fruit_juice_serve, string grain_serve,
            string wholegrains_serve, string protein_foods_serve, string dairy, string alcoholic_drink_sd)
        {
            this.foodName = foodName;
            this.foodGroup = foodGroup;
            this.day = day;
            this.discretionary = false;
            this.weight_g = weight_g == "" ? 0 : Convert.ToDouble(weight_g);
            this.sugar = sugar == "" ? 0 : Convert.ToDouble(sugar); 
            this.energy_kJ = energy_kJ == "" ? 0 : Convert.ToDouble(energy_kJ);
            this.total_fat_g = total_fat_g == "" ? 0 : Convert.ToDouble(total_fat_g);
            this.saturated_fat_g = saturated_fat_g == "" ? 0 : Convert.ToDouble(saturated_fat_g);
            this.sodium_mg = saturated_fat_g == "" ? 0 : Convert.ToDouble(sodium_mg);
            this.vegetables_serve = vegetables_serve == "" ? 0 : Convert.ToDouble(vegetables_serve);
            this.fruit_serve = fruit_serve == "" ? 0 : Convert.ToDouble(fruit_serve);
            this.fruit_juice_serve = fruit_juice_serve == "" ? 0 : Convert.ToDouble(fruit_juice_serve);
            this.grain_serve = grain_serve == "" ? 0 : Convert.ToDouble(grain_serve); ;
            this.wholegrains_serve = wholegrains_serve == "" ? 0 : Convert.ToDouble(wholegrains_serve); 
            this.protein_foods_serve = protein_foods_serve == "" ? 0 : Convert.ToDouble(protein_foods_serve); 
            this.dairy = dairy == "" ? 0 : Convert.ToDouble(dairy); ;
            this.alcoholic_drink_sd = alcoholic_drink_sd == "" ? 0 : Convert.ToDouble(alcoholic_drink_sd); ;
        }

        public string FoodName { get => foodName; set => foodName = value; }

        public string Day { get => day; set => day = value; }
        public bool Discretionary { get => discretionary; set => discretionary = value; }
        public double Weight_g { get => weight_g; set => weight_g = value; }
        public double Sugar { get => sugar; set => sugar = value; }
        public double Energy_kJ { get => energy_kJ; set => energy_kJ = value; }
        public double Total_fat_g { get => total_fat_g; set => total_fat_g = value; }
        public double Saturated_fat_g { get => saturated_fat_g; set => saturated_fat_g = value; }
        public double Sodium_mg { get => sodium_mg; set => sodium_mg = value; }
        public double Vegetables_serve { get => vegetables_serve; set => vegetables_serve = value; }
        public double Fruit_serve { get => fruit_serve; set => fruit_serve = value; }
        public double Fruit_juice_serve { get => fruit_juice_serve; set => fruit_juice_serve = value; }
        public double Grain_serve { get => grain_serve; set => grain_serve = value; }
        public double Wholegrains_serve { get => wholegrains_serve; set => wholegrains_serve = value; }
        public double Protein_foods_serve { get => protein_foods_serve; set => protein_foods_serve = value; }
        public double Dairy { get => dairy; set => dairy = value; }
        public double Alcoholic_drink_sd { get => alcoholic_drink_sd; set => alcoholic_drink_sd = value; }
        public string FoodGroup { get => foodGroup; set => foodGroup = value; }

        public override string ToString()
        {
            return "Name: " + "[" + foodName+ "]" + ", Food group: " + FoodGroup.ToString() + ", Discretionary: " +Discretionary + ", Weight: " +Weight_g + ", Sugar: " + sugar + ", Energy: " + Energy_kJ + ", Total Fat: " + Total_fat_g + ", Saturated Fat: " +
                Saturated_fat_g + ", Sodium: " + Sodium_mg + ", Vegetables serves: " + Vegetables_serve + ", Fruit serves: " + Fruit_serve + ", Fruit juice serves: " + Fruit_juice_serve + ",Grain serves: " + Grain_serve + ", Wholegrain serves: " + Wholegrains_serve + ", Protein serves: " +
                Protein_foods_serve + ", Dairy: " + Dairy + ", Alcoholic: " + Alcoholic_drink_sd;
        }

        public void CalculateVegServe(ref double vegJuice, ref double vegTotal)
        {
            // Vegetables
            if (Discretionary == false && (!FoodGroup.StartsWith("143")) && (!FoodGroup.StartsWith("144")) &&
                (!FoodGroup.StartsWith("22102")) && (!FoodGroup.StartsWith("22202")) &&
                (!(FoodGroup.StartsWith("24705") && FoodName.CaseInsensitiveContains("Avocado"))) && (!FoodGroup.StartsWith("22204")))
            {
                // Calculating serve
                if (FoodGroup.Equals("11304") || FoodGroup.Equals("11305") || FoodGroup.Equals("11306"))
                {
                    vegJuice += Vegetables_serve;
                }
                else
                {
                    vegTotal += Vegetables_serve;
                }
                // More than 1 serve of veg juice per day is counted only as 1
                if (vegJuice > 1)
                {
                    vegJuice = 1;
                }
                vegTotal += vegJuice;
            }
        }

        public int[] CalculateVegVariety(int[] vegVarietyPoints)
        {
            // Potato
            if (FoodGroup.Equals("24101") || FoodGroup.Equals("24103"))
            {
                vegVarietyPoints[0] = 1;
            }
            // Pumpkin or turnip or swede 
            if (FoodGroup.Equals("24701"))
            {
                vegVarietyPoints[1] = 1;
            }
            // Sweet potato or beetroot
            if (FoodGroup.Equals("24302"))
            {
                vegVarietyPoints[2] = 1;
            }
            // Cauliflower
            if (FoodGroup.Equals("24202") && FoodName.CaseInsensitiveContains("cauliflower"))
            {
                vegVarietyPoints[3] = 1;
            }
            // Green beans
            if (FoodGroup.Equals("24502"))
            {
                vegVarietyPoints[4] = 1;
            }
            // Spinach or kale or rocket
            if (FoodGroup.Equals("24401"))
            {
                vegVarietyPoints[5] = 1;
            }
            // Cabbage or brussel sprouts  or bok choy
            if (FoodGroup.Equals("24201"))
            {
                vegVarietyPoints[6] = 1;
            }
            // Peas
            if (FoodGroup.Equals("24501"))
            {
                vegVarietyPoints[7] = 1;
            }
            // Broccoli
            if (FoodGroup.Equals("24202") && FoodName.CaseInsensitiveContains("broccoli"))
            {
                vegVarietyPoints[8] = 1;
            }
            // Carrots
            if (FoodGroup.Equals("24301") && FoodName.CaseInsensitiveContains("Carrots"))
            {
                vegVarietyPoints[9] = 1;
            }
            // Zucchini, eggplant, squash
            if ((FoodGroup.Equals("24702") && FoodName.CaseInsensitiveContains("zucchini")) || (FoodGroup.Equals("24705") && FoodName.CaseInsensitiveContains("eggplant"))
                || (FoodGroup.Equals("24702") && FoodName.CaseInsensitiveContains("Squash ")))
            {
                vegVarietyPoints[10] = 1;
            }
            // Capsicum
            if (FoodGroup.Equals("24705") && FoodName.CaseInsensitiveContains("capsicum"))
            {
                vegVarietyPoints[11] = 1;
            }
            // Corn, sweet corn, corn on the cob
            if (FoodGroup.Equals("24704"))
            {
                vegVarietyPoints[12] = 1;
            }
            // Mushrooms
            if (FoodGroup.Equals("24703"))
            {
                vegVarietyPoints[13] = 1;
            }
            // Tomatoes
            if (FoodGroup.Equals("24601") || FoodGroup.Equals("24602"))
            {
                vegVarietyPoints[14] = 1;
            }
            // Lecttuce, antichoke 
            if (FoodGroup.Equals("24401") && FoodName.CaseInsensitiveContains("lettuce") || FoodGroup.Equals("24402") && FoodName.CaseInsensitiveContains("artichoke"))
            {
                vegVarietyPoints[15] = 1;
            }
            // Celery, cucumber
            if (FoodGroup.Equals("24402") && FoodName.CaseInsensitiveContains("celery") || FoodGroup.Equals("24705") && FoodName.CaseInsensitiveContains("cucumber"))
            {
                vegVarietyPoints[16] = 1;
            }
            // Onion, spring onion, leek, 
            if (FoodGroup.Equals("24802"))
            {
                vegVarietyPoints[17] = 1;
            }
            // Mixed vegetable dishes(salads, soups)
            if (FoodGroup.Equals("24802"))
            {
                vegVarietyPoints[18] = 1;
            }
            return vegVarietyPoints;
        }


        public void CalculateGrainServe(ref double grainTotal)
        {
            // Grain
            if (Discretionary == false)
            {
                // Calculating serve
                grainTotal += Grain_serve;
            }
        }

        public void CalculateWholeGrainServe(ref double wholeGrainTotal)
        {
            // Wholegrain
            if (Discretionary == false)
            {
                // Calculating serve
                wholeGrainTotal += wholegrains_serve;
            }
        }

    }
}
