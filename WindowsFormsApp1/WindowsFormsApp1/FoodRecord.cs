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
        private double legume_protein_serve;
        // Dairy
        private double dairy;
        // Alcohol
        private double alcoholic_drink_sd;

        public FoodRecord(string foodName, string foodGroup, string day, string weight_g, string sugar, string energy_kJ,
            string total_fat_g, string saturated_fat_g, string sodium_mg, string vegetables_serve, string fruit_serve, string fruit_juice_serve, string grain_serve,
            string wholegrains_serve, string protein_foods_serve, string legume_protein_serve, string dairy, string alcoholic_drink_sd)
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
            this.legume_protein_serve = legume_protein_serve == "" ? 0 : Convert.ToDouble(legume_protein_serve);
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
        public double Legume_protein_serve { get => legume_protein_serve; set => legume_protein_serve = value; }

        public override string ToString()
        {
            return "Name: " + "[" + foodName + "]" + ", Food group: " + FoodGroup.ToString() + ", Discretionary: " + Discretionary + ", Weight: " + Weight_g + ", Sugar: " + sugar + ", Energy: " + Energy_kJ + ", Total Fat: " + Total_fat_g + ", Saturated Fat: " +
                Saturated_fat_g + ", Sodium: " + Sodium_mg + ", Vegetables serves: " + Vegetables_serve + ", Fruit serves: " + Fruit_serve + ", Fruit juice serves: " + Fruit_juice_serve + ",Grain serves: " + Grain_serve + ", Wholegrain serves: " + Wholegrains_serve + ", Protein serves: " +
                Protein_foods_serve + ", Dairy: " + Dairy + ", Alcoholic: " + Alcoholic_drink_sd;
        }

        // Vegetables serve
        public void CalculateVegServe(ref double vegJuice, ref double vegTotal)
        {
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

        // Vegetables variety
        public int[] CalculateVegARFS(int foodGroupInt, int[] vegVarietyPoints)
        {
            // Potato
            if (FoodGroup.Equals("24101") || FoodGroup.Equals("24103"))
            {
                Console.WriteLine("Potato- " + FoodGroup + " " + foodName);
                vegVarietyPoints[0] = 1;
            }
            // Pumpkin or turnip or swede 
            if (FoodGroup.Equals("24701"))
            {
                Console.WriteLine("Pumpkin or turnip or swede- " + FoodGroup + " " + foodName);
                vegVarietyPoints[1] = 1;
            }
            // Sweet potato or beetroot
            if (FoodGroup.Equals("24302"))
            {
                Console.WriteLine("Sweet potato or beetroot- " + FoodGroup + " " + foodName);
                vegVarietyPoints[2] = 1;
            }
            // Cauliflower
            if (FoodGroup.Equals("24202") && FoodName.CaseInsensitiveContains("cauliflower"))
            {
                Console.WriteLine("Cauliflower- " + FoodGroup + " " + foodName);
                vegVarietyPoints[3] = 1;
            }
            // Green beans
            if (FoodGroup.Equals("24502"))
            {
                Console.WriteLine("Green beans- " + FoodGroup + " " + foodName);
                vegVarietyPoints[4] = 1;
            }
            // Spinach or kale or rocket
            if (FoodGroup.Equals("24401"))
            {
                Console.WriteLine("Spinach or kale or rocket- " + FoodGroup + " " + foodName);
                vegVarietyPoints[5] = 1;
            }
            // Cabbage or brussel sprouts  or bok choy
            if (FoodGroup.Equals("24201"))
            {
                Console.WriteLine("Cabbage or brussel sprouts or bok choy- " + FoodGroup + " " + foodName);
                vegVarietyPoints[6] = 1;
            }
            // Peas
            if (FoodGroup.Equals("24501"))
            {
                Console.WriteLine("Peas- " + FoodGroup + " " + foodName);
                vegVarietyPoints[7] = 1;
            }
            // Broccoli
            if (FoodGroup.Equals("24202") && FoodName.CaseInsensitiveContains("broccoli"))
            {
                Console.WriteLine("Broccoli- " + FoodGroup + " " + foodName);
                vegVarietyPoints[8] = 1;
            }
            // Carrots
            if (FoodGroup.Equals("24301") && FoodName.CaseInsensitiveContains("Carrot"))
            {
                Console.WriteLine("Carrot- " + FoodGroup + " " + foodName);
                vegVarietyPoints[9] = 1;
            }
            // Zucchini, eggplant, squash
            if ((FoodGroup.Equals("24702") && FoodName.CaseInsensitiveContains("zucchini")) || (FoodGroup.Equals("24705") && FoodName.CaseInsensitiveContains("eggplant"))
                || (FoodGroup.Equals("24702") && FoodName.CaseInsensitiveContains("Squash")))
            {
                Console.WriteLine("Zucchini, eggplant, squash- " + FoodGroup + " " + foodName);
                vegVarietyPoints[10] = 1;
            }
            // Capsicum
            if (FoodGroup.Equals("24705") && FoodName.CaseInsensitiveContains("capsicum"))
            {
                Console.WriteLine("Capsicum- " + FoodGroup + " " + foodName);
                vegVarietyPoints[11] = 1;
            }
            // Corn, sweet corn, corn on the cob
            if (FoodGroup.Equals("24704"))
            {
                Console.WriteLine("Corn- " + FoodGroup + " " + foodName);
                vegVarietyPoints[12] = 1;
            }
            // Mushrooms
            if (FoodGroup.Equals("24703"))
            {
                Console.WriteLine("Mushroom- " + FoodGroup + " " + foodName);
                vegVarietyPoints[13] = 1;
            }
            // Tomatoes
            if (FoodGroup.Equals("24601") || FoodGroup.Equals("24602"))
            {
                Console.WriteLine("Tomato- " + FoodGroup + " " + foodName);
                vegVarietyPoints[14] = 1;
            }
            // Lecttuce, antichoke 
            if (FoodGroup.Equals("24401") && FoodName.CaseInsensitiveContains("lettuce") || FoodGroup.Equals("24402") && FoodName.CaseInsensitiveContains("artichoke"))
            {
                Console.WriteLine("Antichoke- " + FoodGroup + " " + foodName);
                vegVarietyPoints[15] = 1;
            }
            // Celery, cucumber
            if (FoodGroup.Equals("24402") && FoodName.CaseInsensitiveContains("celery") || FoodGroup.Equals("24705") && FoodName.CaseInsensitiveContains("cucumber"))
            {
                Console.WriteLine("Cucumber- " + FoodGroup + " " + foodName);
                vegVarietyPoints[16] = 1;
            }
            // Onion, spring onion, leek, 
            if (FoodGroup.Equals("24802"))
            {
                Console.WriteLine("Onion- " + FoodGroup + " " + foodName);
                vegVarietyPoints[17] = 1;
            }
            // Mixed vegetable dishes(salads, soups)
            string[] mixedVegetables = new string[] { "24901", "24902", "24904", "21102", "21302", "21402", "21502", "21602" };
            if (mixedVegetables.Contains(FoodGroup))
            {
                Console.WriteLine("Mixed- " + FoodGroup + " " + foodName);
                vegVarietyPoints[18] = 2;
            }
            // Other (legumes)
            if (FoodGroup.Equals("25101") || FoodGroup.Equals("25102") || FoodGroup.Equals("25201") || FoodGroup.Equals("25202"))
            {
                Console.WriteLine("Legumes- " + FoodGroup + " " + foodName);
                vegVarietyPoints[19] = 1;
            }
            return vegVarietyPoints;
        }

        //Fruit serve
        public void CalculateFruitServe(int foodGroupInt, ref double fruitJuiceDried, ref double fruitWithoutJuice)
        {
            if (Discretionary == false)
            {
                // Dried fruit
                string[] driedFruit = new string[] { "16801,16802, 16803, 16804" };
                if (driedFruit.Contains(FoodGroup))
                {
                    fruitJuiceDried += Fruit_serve;
                    //Console.WriteLine("Dried" + record.FoodGroup + " " + record.Fruit_serve);
                }
                // Fruit juice
                else if (Fruit_juice_serve != 0)
                {
                    fruitJuiceDried += Fruit_juice_serve;
                    fruitWithoutJuice += Fruit_serve - Fruit_juice_serve;
                    //Console.WriteLine("Fruit juice" + record.FoodGroup + " " + record.Fruit_serve);
                }
                // Fresh fruit
                else if ((16101 <= foodGroupInt && foodGroupInt <= 16901) || FoodGroup.StartsWith("16") || FoodGroup.Equals(16001))
                {
                    fruitWithoutJuice += Fruit_serve;
                    //Console.WriteLine("Fruit" + record.FoodGroup + " " + record.Fruit_serve);
                }
            }
        }

        // Fruit variety
        public int[] CalculateFruitARFS(int foodGroupInt, int[] fruitVarietyPoints)
        {
            // Canned fruit
            string[] cannedFruit = new string[] { "16102", "16104", "16202", "16304", "16402", "16404", "16505", "16702" };
            if (cannedFruit.Contains(FoodGroup))
            {
                //Console.WriteLine("Canned fruit- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[0] = 1;
            }
            // Fresh fruit salad
            if (FoodGroup.Equals("16701"))
            {
                //Console.WriteLine("Fresh fruit salad- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[1] = 1;
            }
            // Dried fruit
            string[] driedFruit = new string[] { "16801,16802, 16803, 16804" };
            if (FoodGroup.Equals("24302"))
            {
                //Console.WriteLine("Dried fruit- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[2] = 1;
            }
            // Apple or pear
            if (FoodGroup.Equals("16101") && FoodName.CaseInsensitiveContains("apple") || FoodGroup.Equals("16103") && FoodName.CaseInsensitiveContains("pear"))
            {
                //Console.WriteLine("Apple or pear- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[3] = 1;
            }
            // Orange, mandarin, grapefruit
            if (FoodGroup.Equals("16301") || FoodGroup.Equals("16302") || FoodGroup.Equals("16303"))
            {
                //Console.WriteLine("Orange, mandarin, grapefruit- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[4] = 1;
            }
            // Banana
            if (FoodGroup.Equals("16501"))
            {
                //Console.WriteLine("Banana- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[5] = 1;
            }
            // Peach, nectarine, plum or apricot
            if (FoodGroup.Equals("16401") || FoodGroup.Equals("16403"))
            {
                //Console.WriteLine("Peach, nectarine, plum or apricot- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[6] = 1;
            }
            // Mango or paw-paw
            if (FoodGroup.Equals("16504"))
            {
                //Console.WriteLine("Mango or Paw-paw- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[7] = 1;
            }
            // Pineapple
            if (FoodGroup.Equals("16502"))
            {
                //Console.WriteLine("Pineapple- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[8] = 1;
            }
            // Grapes, strawberries, blueberries, fig
            // The name is cut short to identify both strawberry or strawberries
            if (FoodGroup.Equals("16601") && FoodName.CaseInsensitiveContains("grape") ||
                FoodGroup.Equals("16201") && (FoodName.CaseInsensitiveContains("strawberr") || FoodName.CaseInsensitiveContains("blueberr") || FoodName.CaseInsensitiveContains("berry"))
                || FoodGroup.Equals("16503") && FoodName.CaseInsensitiveContains("fig"))
            {
                //Console.WriteLine("Grapes, strawberries, blueberries, fig- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[9] = 1;
            }
            // Melon e.g., watermelon, rockmelon
            if (FoodGroup.Equals("16601") && (FoodName.CaseInsensitiveContains("melon") || FoodName.CaseInsensitiveContains("watermelon") || FoodName.CaseInsensitiveContains("rockmelon")))
            {
                //Console.WriteLine("Melon- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[10] = 1;
            }
            // Other
            if (FoodGroup.Equals("16504") && FoodName.CaseInsensitiveContains("passionfruit") || FoodGroup.Equals("16601") && FoodName.CaseInsensitiveContains("rhubarb")
                || (FoodGroup.Equals("16503") && FoodName.CaseInsensitiveContains("persimmon") || FoodGroup.Equals("16504") && FoodName.CaseInsensitiveContains("pomegranate")))
            {
                //Console.WriteLine("Other- " + FoodGroup + " " + foodName);
                fruitVarietyPoints[11] = 1;
            }
            return fruitVarietyPoints;
        }

        // Grain serve
        public void CalculateGrainServe(ref double totalGrain)
        {
            // Grain
            if (Discretionary == false && Grain_serve != 0)
            {
                // Calculating serve
                //Console.WriteLine("Grain " + " " + FoodName + " " + FoodGroup + " " + "Grain Serve " + Grain_serve + " " + "Whole Grain " + " " + wholegrains_serve);
                totalGrain += Grain_serve;
            }
        }

        public void CalculateWholeGrainServe(ref double wholeGrainTotal)
        {
            // Wholegrain
            if (Discretionary == false && Wholegrains_serve != 0)
            {
                // Calculating serve
                wholeGrainTotal += wholegrains_serve;
            }
        }

        // Grain variety
        public int[] CalculateGraintARFS(int foodGroupInt, int[] grainVarietyPoints)
        {
            // Muesli
            if (FoodGroup.Equals("12515") && FoodName.CaseInsensitiveContains("Muesli"))
            {
                //Console.WriteLine("Muesli- " + FoodGroup + " " + foodName);
                grainVarietyPoints[0] = 1;
            }
            // Fresh fruit salad
            if (FoodGroup.Equals("12601") || FoodGroup.Equals("12602"))
            {
                //Console.WriteLine("Porridge- " + FoodGroup + " " + foodName);
                grainVarietyPoints[1] = 1;
            }
            // Breakfast cereal
            string[] breakfastCereal = new string[] { "12501", "12502", "12503", "12504", "12505", "12506", "12507", "12508", "12509", "12510", "12511", "12512", "12513", "12514", "12515", "12516" };
            if (breakfastCereal.Contains(FoodGroup))
            {
                //Console.WriteLine("Breakfast cerealt- " + FoodGroup + " " + foodName);
                grainVarietyPoints[2] = 1;
            }
            // Bread, pita bread, roll or toast
            bool Bread = (12201 <= foodGroupInt && foodGroupInt <= 12214) || foodGroup.Equals("12302") || foodGroup.Equals("12303") || foodGroup.Equals("13503") || foodGroup.Equals("13505");
            if (Bread)
            {
                //Console.WriteLine("Bread, pita bread, roll or toast- " + FoodGroup + " " + foodName);
                grainVarietyPoints[3] = 1;
            }
            // English muffin, bagel, crumpet or scone
            if (FoodGroup.Equals("12301") || FoodGroup.Equals("12305") || FoodGroup.Equals("13307"))
            {
                //Console.WriteLine("English muffin, bagel, crumpet or scone- " + FoodGroup + " " + foodName);
                grainVarietyPoints[4] = 1;
            }
            // Rice
            if (FoodGroup.Equals("12102") || FoodGroup.Equals("13511") || FoodGroup.Equals("13514"))
            {
                //Console.WriteLine("Rice- " + FoodGroup + " " + foodName);
                grainVarietyPoints[5] = 1;
            }
            // Other grains e.g., cous cous, burghul, pearl barley, freekah
            if (FoodGroup.Equals("12101") || (FoodGroup.Equals("12103") && (foodName.CaseInsensitiveContains("cous cous"))) || FoodGroup.Equals("13515"))
            {
                //Console.WriteLine("Other grains- " + FoodGroup + " " + foodName);
                grainVarietyPoints[6] = 1;
            }
            // Noodles
            if (FoodGroup.Equals("12401") || FoodGroup.Equals("12402") || FoodGroup.Equals("12403") || FoodGroup.Equals("12404"))
            {
                //Console.WriteLine("Noodles- " + FoodGroup + " " + foodName);
                grainVarietyPoints[7] = 1;
            }
            // Pasta
            if  (FoodGroup.Equals("13509") && (FoodName.CaseInsensitiveContains("pasta dish") || FoodName.CaseInsensitiveContains("spaghetti") || FoodName.CaseInsensitiveContains("lasagna"))
                || FoodGroup.Equals("13515"))
            {
                //Console.WriteLine("Pasta- " + FoodGroup + " " + foodName);
                grainVarietyPoints[8] = 1;
            }
            // Clear soup with rice or noodles
            if (FoodGroup.Equals("21101") && FoodName.CaseInsensitiveContains("Soup") && FoodName.CaseInsensitiveContains("Noodle"))
            {
                //Console.WriteLine("Grapes, strawberries, blueberries, fig- " + FoodGroup + " " + foodName);
                grainVarietyPoints[9] = 1;
            }
            // Tacos, burios, enchiladas
            if (FoodGroup.Equals("13507"))
            {
                //Console.WriteLine("Tacos, burritos, enchiladas- " + FoodGroup + " " + foodName);
                grainVarietyPoints[10] = 1;
            }
            // Wholegrain crackers
            if (FoodGroup.Equals("13201") || FoodGroup.Equals("13203") || FoodGroup.Equals("13204") || FoodGroup.Equals("13205"))
            {
                //Console.WriteLine("Other- " + FoodGroup + " " + foodName);
                grainVarietyPoints[11] = 1;
            }
            return grainVarietyPoints;
        }

        // Protein
        public void CalculateProteinServe(int foodGroupInt, ref double totalMeat)
        {
            // Legumes and beans
            string[] legumes = new string[] { "25101", "25102", "25201", "25202"};
            // Lean meat and alt
            if (Discretionary == false && !legumes.Contains(FoodGroup) && protein_foods_serve != 0)
            {
                //Calculating serve

                //Console.WriteLine("Meat " + " " + FoodName + " " + FoodGroup + " " + "Meat Serve " + protein_foods_serve);
                // Should I remove legumes from protein
                if (Legume_protein_serve != 0)
                {
                    totalMeat += protein_foods_serve - Legume_protein_serve;
                }
                
                else
                {
                    totalMeat += protein_foods_serve;
                }
            }
        }

        // Protein variety
        public int[] CalculateProteinARFS(int foodGroupInt, int[] proteinVarietyPoints)
        {
            // Mince e.g., spaghetti bolognese, rissoles, lasagna Beef, lamb, veal dishes
            string[] mince = new String[] { "18701", "18702", "18703", "18704", "18705", "18706", "18707", "18711", "18712", "18801", "18802", "18803" };
            if (FoodGroup.Equals("18503") && FoodName.CaseInsensitiveContains("sausage") || FoodGroup.Equals("18101") && FoodName.CaseInsensitiveContains("mince")
               || (FoodGroup.Equals("13509") && FoodName.CaseInsensitiveContains("spaghetti") && FoodName.CaseInsensitiveContains("bolognese") 
               || FoodGroup.Equals("13509") && FoodName.CaseInsensitiveContains("lasagna")) || mince.Contains(FoodGroup))
            {
                //Console.WriteLine("Mince: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[0] = 1;
            }
            // Meat
            string[] meat = new String[] { "18101", "18102", "18104", "18201", "18202" };
            if (meat.Contains(FoodGroup))
            {
                //Console.WriteLine("Meat: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[1] = 1;
            }
            // Chicken
            string[] chicken = new string[] { "18301", "18302", "18303", "18901", "18902", "18903" };
            if (chicken.Contains(FoodGroup))
            {
                //Console.WriteLine("Chicken: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[2] = 1;
            }
            // Pork
            string[] pork = new string[] { "18103", "18708", "18709", "18710" };
            if (pork.Contains(FoodGroup))
            {
                //Console.WriteLine("Pork: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[3] = 1;
            }
            // Fresh fish, not crumbed or battered
            string[] freshFish = new string[] { "15101", "15102", "15601", "15602", "15701" };
            if (freshFish.Contains(FoodGroup))
            {
                //Console.WriteLine("Fresh fish, not crumbed or battered: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[4] = 1;
            }
            // Canned Fish
            string[] cannedFish = new string[] { "15401", "15402"};
            if (cannedFish.Contains(FoodGroup))
            {
                //Console.WriteLine("Rice- " + FoodGroup + " " + foodName);
                proteinVarietyPoints[5] = 1;
            }
            //  Other seafood e.g., prawns, lobster
            string[] seaFood = new string[] { "15101", "15102", "15601", "15602", "15701" };
            if (seaFood.Contains(FoodGroup))
            {
                //Console.WriteLine("Other grains- " + FoodGroup + " " + foodName);
                proteinVarietyPoints[6] = 1;
            }
            // Nuts
            string[] nuts = new string[] { "22201", "22202", "22204", "22205", "22302" };
            if (nuts.Contains(FoodGroup))
            {
                //Console.WriteLine("Nuts: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[7] = 1;
            }
            // Seeds
            string[] seeds = new string[] { "22101", "22102", "22301"};
            if (seeds.Contains(FoodGroup))
            {
                //Console.WriteLine("Seeds: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[8] = 1;
            }
            // Eggs e.g., boiled, scrambled
            string[] eggs = new string[] { "17101", "17102", "17103", "17201", "17401" };
            if (eggs.Contains(FoodGroup))
            {
                //Console.WriteLine("Eggs: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[9] = 1;
            }
            // Soybeans, tofu, meat substitutes
            string[] soybeans = new string[] { "20601", "20701"};
            if (soybeans.Contains(FoodGroup))
            {
                //Console.WriteLine("Tacos, burritos, enchiladas- " + FoodGroup + " " + foodName);
                proteinVarietyPoints[10] = 1;
            }
            return proteinVarietyPoints;
        }

        // Dairy
        public void CalculateDairyServe(int foodGroupInt, ref double totalDairy)
        {
            // Major
            bool Major = 19101 <= foodGroupInt && foodGroupInt <= 19806;
            // Dairy alternative
            bool DairyAlt = 20101 <= foodGroupInt && foodGroupInt <= 20502;
            // Coffee based milk drink unless chocolate based and made up of water
            bool coffeeBased1 = 11801 <= foodGroupInt && foodGroupInt <= 11803 && !(foodName.CaseInsensitiveContains("Chocolate based") && foodName.CaseInsensitiveContains("Water"));
            // Coffee based milk drink 
            bool coffeeBased2 = foodGroup.Equals("11805");

            // Dairy
            if (Discretionary == false && (Major || DairyAlt || coffeeBased1 || coffeeBased2))
            {
                //Calculating serve
                //Console.WriteLine("Dairy " + " " + FoodName + " " + FoodGroup + " " + "Dairy Serve " + Dairy);
                totalDairy += Dairy;
            }
        }

        public void CalculateReducedFatDairyServe(ref double totalReducedFat)
        {
            // Reduced Fat
            string[] reducedFat = new string[] {"19103", "19104", "19105", "19202", "19203", "19207", "19208", "19209", "19402", "19404", "19407", "19602", "19803", "19804", "20103", "20104", "20105", "20202", "20502" };
            if (Discretionary == false && reducedFat.Contains(FoodGroup))
            {
                // Calculating serve
                //Console.WriteLine("Reduced Dairy " + " " + FoodName + " " + FoodGroup + " " + "Dairy Serve " + Dairy);
                totalReducedFat += Dairy;
            }
        }

        // Dairy variety
        public int[] CalculateDairyARFS(int foodGroupInt, int[] dairyPoints)
        {
            // Flavoured milk
            string[] flavouredMilk = new String[] { "19801", "19802", "19803", "19804", "19805", "19806" };
            if (flavouredMilk.Contains(FoodGroup))
            {
                //Console.WriteLine("Flavoured Milk: " + FoodGroup + " " + foodName);
                dairyPoints[0] = 1;
            }
            // Plain milk-glass or with cereal or alternative
            string[] plainMilk = new String[] { "19101", "19102", "19103", "19104", "19105", "19108", "19107", "20101", "20102", "20103",
                "20104", "20105", "20106", "20107", "20201", "20202" };
            if (plainMilk.Contains(FoodGroup))
            {
                //Console.WriteLine("Plain milk-glass or with cereal or alternative: " + FoodGroup + " " + foodName);
                dairyPoints[1] = 1;
            }
            // Yoghust
            bool yoghust = (19201 <= foodGroupInt && foodGroupInt <= 19212) || foodGroup.Equals("20501") || foodGroup.Equals("20502");
            if (yoghust)
            {
                //Console.WriteLine("Yoghust: " + FoodGroup + " " + foodName);
                dairyPoints[2] = 1;
            }
            // Pork
            string[] cheese = new string[] { "19401", "19402", "19403", "19404", "19405", "19406", "19407", "19408", "20301" };
            if (cheese.Contains(FoodGroup))
            {
                //Console.WriteLine("Cheese: " + FoodGroup + " " + foodName);
                dairyPoints[3] = 1;
            }
            // Fresh fish, not crumbed or battered
            string[] dairyDessert = new string[] { "19601", "19602"};
            if (dairyDessert.Contains(FoodGroup))
            {
                //Console.WriteLine("Dairty desserts: " + FoodGroup + " " + foodName);
                dairyPoints[4] = 1;
            }
            return dairyPoints;
        }
    }

}
