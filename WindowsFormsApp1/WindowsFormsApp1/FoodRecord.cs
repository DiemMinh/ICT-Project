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
        // To check whether a food is included in Variety
        private bool inVariety;
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
            return "Name: " + "[" + foodName + "]" + ", Food group: " + FoodGroup.ToString() + ", Discretionary: " + Discretionary + ", Variety: " + inVariety + ", Weight: " + Weight_g + ", Sugar: " + sugar + ", Energy: " + Energy_kJ + ", Total Fat: " + Total_fat_g + ", Saturated Fat: " +
                Saturated_fat_g + ", Sodium: " + Sodium_mg + ", Vegetables serves: " + Vegetables_serve + ", Fruit serves: " + Fruit_serve + ", Fruit juice serves: " + Fruit_juice_serve + ",Grain serves: " + Grain_serve + ", Wholegrain serves: " + Wholegrains_serve + ", Protein serves: " +
                Protein_foods_serve + ", Dairy: " + Dairy + ", Alcoholic: " + Alcoholic_drink_sd;
        }

        // Vegetables serve
        public void CalculateVegServe(ref double vegJuice, ref double vegTotal)
        {
            if (Discretionary == false && (vegetables_serve != 0 && !(FoodGroup.StartsWith("24705") && FoodName.CaseInsensitiveContains("Avocado"))))
            {
                // Calculating serve
                if (FoodGroup.Equals("11304") || FoodGroup.Equals("11305"))
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

        //Fruit serve (Fix to be liked vegetable 1 serve/day)
        public void CalculateFruitServe(int foodGroupInt, ref double fruitJuiceDried, ref double fruitTotal)
        {
            if (Discretionary == false && Fruit_serve != 0)
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
                    fruitTotal += Fruit_serve - Fruit_juice_serve;
                    //Console.WriteLine("Fruit juice" + record.FoodGroup + " " + record.Fruit_serve);
                }
                // Fresh fruit
                else
                {
                    fruitTotal += Fruit_serve;
                    //Console.WriteLine("Fruit" + record.FoodGroup + " " + record.Fruit_serve);
                }
                // More than 1 serve of fruit juice and dried fruit per day is counted only as 1
                if (fruitJuiceDried > 1)
                {
                    fruitJuiceDried = 1;
                }
                fruitTotal += fruitJuiceDried;
            }
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

        // Protein
        public void CalculateProteinServe(int foodGroupInt, ref double totalMeat)
        {
            // Legumes and beans
            string[] legumes = new string[] { "25101", "25102", "25201", "25202" };
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

        // Reduced fat
        public void CalculateReducedFatDairyServe(ref double totalReducedFat)
        {
            // Reduced Fat
            string[] reducedFat = new string[] { "19103", "19104", "19105", "19202", "19203", "19207", "19208", "19209", "19402", "19404", "19407", "19602", "19803", "19804", "20103", "20104", "20105", "20202", "20502" };
            if (Discretionary == false && reducedFat.Contains(FoodGroup))
            {
                // Calculating serve
                //Console.WriteLine("Reduced Dairy " + " " + FoodName + " " + FoodGroup + " " + "Dairy Serve " + Dairy);
                totalReducedFat += Dairy;
            }
        }

        // Unsaturated Fat
        public void CalculateUnsaturatedFatServe(int foodGroupInt, ref double oilTotal, ref double avocadoTotal, ref double spreadTotal)
        {
            if (Discretionary == false && ((14301 <= foodGroupInt && foodGroupInt <= 14304)) || (14306 <= foodGroupInt && foodGroupInt <= 14307)
                       || (14401 <= foodGroupInt && foodGroupInt <= 14403) || FoodGroup.Equals("22102") || FoodGroup.Equals("22202") ||
                       FoodGroup.Equals("22204") || ((FoodGroup.Equals("24705")) && FoodName.CaseInsensitiveContains("Avocado")))
            {
                if (14401 <= foodGroupInt && foodGroupInt <= 14403)
                {
                    oilTotal += Weight_g;
                    //Console.WriteLine("Oil " + " " + record.FoodName + record.FoodGroup + " " + record.Weight_g);
                }
                if ((FoodGroup.Equals("24705")) && FoodName.CaseInsensitiveContains("Avocado"))
                {
                    avocadoTotal += Weight_g;
                    //Console.WriteLine("Avocado " + " " + record.FoodName + record.FoodGroup + " " + record.Weight_g);
                }
                else
                {
                    spreadTotal += Weight_g;
                    //Console.WriteLine("Spread " + " " + record.FoodName + record.FoodGroup + " " + record.Weight_g);
                }
            }
        }

        // Alcohol
        public void CalculateAlcoholSD(int foodGroupInt, ref double alcoholTotal)
        {
            if (Discretionary == true && ((29101 <= foodGroupInt && foodGroupInt <= 29505)))
            {
                //Console.WriteLine("Alcohol " + record.FoodGroup + " " + record.Alcoholic_drink_sd);
                alcoholTotal += Alcoholic_drink_sd;
            }
        }

        // Fluid
        public void calculateFluidWeight(int foodGroupInt, ref double beverageTotal, ref double waterTotal)
        {
            if (Discretionary == false && ((11701 <= foodGroupInt && foodGroupInt <= 11703) ||
                       (11101 <= foodGroupInt && foodGroupInt <= 11604) || (11801 <= foodGroupInt && foodGroupInt <= 11806) ||
                       (19101 <= foodGroupInt && foodGroupInt <= 19105) || foodGroupInt == 19109
                       || (19801 <= foodGroupInt && foodGroupInt <= 19806) || (19101 <= foodGroupInt && foodGroupInt <= 19105)
                       || (20101 <= foodGroupInt && foodGroupInt <= 20107) || (20201 <= foodGroupInt && foodGroupInt <= 20202)))
            {
                //Console.WriteLine("Fluid " + record.FoodGroup + " " + record.FoodName + " " + record.Weight_g);
                beverageTotal += Weight_g;
                if (11701 <= foodGroupInt && foodGroupInt <= 11703)
                {
                    waterTotal += Weight_g;
                }
            }
        }

        // Discretionary
        public void CalculateDiscretionaryServe(int foodGroupInt, ref double discretionaryTotalEnergy)
        {
            if (Discretionary == true && (!(29101 <= foodGroupInt && foodGroupInt <= 29505)))
            {
                //Console.WriteLine("Discretionary " + record.FoodGroup + " " + record.Energy_kJ);
                discretionaryTotalEnergy += Energy_kJ;
            }
        }

        // Check variety - look for food code

        public void checkVarietyWithCode(int[] vegVarietyPoints, int[] fruitVarietyPoints, int[] grainVarietyPoints, int[] proteinVarietyPoints, int[] dairyVarietyPoints, int foodGroupInt)
        {
            if (Vegetables_serve > 0)
            {
                vegVarietyPoints = CalculateVegVarieryWithCode(foodGroupInt, vegVarietyPoints);
            }
            if (Fruit_serve > 0)
            {
                fruitVarietyPoints = CalculateFruitVarieryWithCode(foodGroupInt, fruitVarietyPoints);
            }
            if (Grain_serve > 0)
            {
                grainVarietyPoints = CalculateGrainVarieryWithCode(foodGroupInt, grainVarietyPoints);
            }
            if (Protein_foods_serve > 0)
            {
                proteinVarietyPoints = CalculateProteinVarieryWithCode(foodGroupInt, proteinVarietyPoints);
            }
            if (dairy > 0)
            {
                dairyVarietyPoints = CalculateDairyVarieryWithCode(foodGroupInt, dairyVarietyPoints);
            }
        }


        // Rule 1 - If no food code, look for a key word in food description - see updated table for keywords
        public void checkVarietyWithoutCode(int[] vegVarietyPoints, int[] fruitVarietyPoints, int[] grainVarietyPoints, int[] proteinVarietyPoints, int[] dairyVarietyPoints)
        {
            if (Vegetables_serve > 0)
            {
                vegVarietyPoints = CalculateVegVarieryWithoutCode(vegVarietyPoints);
            }
            if (Fruit_serve > 0)
            {
                fruitVarietyPoints = CalculateFruitVarieryWithoutCode(fruitVarietyPoints);
            }
            if (Grain_serve > 0)
            {
                grainVarietyPoints = CalculateGrainVarieryWithoutCode(grainVarietyPoints);
            }
            if (Protein_foods_serve > 0)
            {
                proteinVarietyPoints = CalculateProteinVarieryWithoutCode(proteinVarietyPoints);
            }
            if (dairy > 0)
            {
                dairyVarietyPoints = CalculateDairyVarieryWithoutCode(dairyVarietyPoints);
            }
        }

        // THE FOLLOWING FUNCTIONS CHECK VARIETY WITH FOOD CODE
        // Check vegetables variety with food code
        public int[] CalculateVegVarieryWithCode(int foodGroupInt, int[] vegVarietyPoints)
        {

            //bool pumpkin = (19201 <= foodGroupInt && foodGroupInt <= 19212) || foodGroup.Equals("20501") && foodName.CaseInsensitiveContains(turnip) || foodGroup.Equals("20502"));


            // To check whether food code is in the list
            bool inVarietyVeg = false;

            string[] potatoCode = new string[] { "24101", "24103" };
            bool potato = (potatoCode.Contains(FoodGroup));
            if (potato)
            {
                //Console.WriteLine("Potato by code: " + FoodGroup + " " + foodName);
                vegVarietyPoints[0] = 1;
            }

            // Pumpkin or turnip or swede 
            string[] pumpkinCode = new string[] { "24701" };
            string[] pumpkinKeyWord = new string[] { "pumpkin", "turnip", "swede" };
            bool pumpkin = (pumpkinCode.Contains(FoodGroup) && foodName.StringCheckKeyWords(pumpkinKeyWord));
            if (pumpkin)
                if (FoodGroup.Equals("24701"))
                {
                    //Console.WriteLine("Pumpkin or turnip or swede by code: " + FoodGroup + " " + foodName);
                    vegVarietyPoints[1] = 1;
                }

            // Sweet potato or beetroot
            string[] beetrootCode = new string[] { "24302" };
            string[] beetrootKeyWord = new string[] { "sweet potato", "beetroot" };
            bool beetroot = (beetrootCode.Contains(FoodGroup) && foodName.StringCheckKeyWords(beetrootKeyWord));
            if (beetroot)
            {
                //Console.WriteLine("Sweet potato or beetroot by code: " + FoodGroup + " " + foodName);
                vegVarietyPoints[2] = 1;
            }

            // Cauliflower
            string[] cauliflowerCode = new string[] { "24202" };
            string[] cauliflowerKeyWord = new string[] { "cauliflower" };
            bool cauliflower = (cauliflowerCode.Contains(FoodGroup) && foodName.StringCheckKeyWords(cauliflowerKeyWord));
            if (cauliflower)
            {
                //Console.WriteLine("Cauliflower by  code: " + FoodGroup + " " + foodName);
                vegVarietyPoints[3] = 1;
            }

            // Green beans
            string[] greenBeanCode = new string[] { "24502" };
            bool greenBean = (greenBeanCode.Contains(FoodGroup));
            if (greenBean)
            {
                //Console.WriteLine("Green beans by code: " + FoodGroup + " " + foodName);
                vegVarietyPoints[4] = 1;
            }
            // Spinach or kale or rocket
            string[] spinachCode = new string[] { "24401" };
            string[] spinachKeyWord = new string[] { "spinach", "kale", "rocket", "lettuce" };
            bool spinach = (spinachCode.Contains(FoodGroup) && foodName.StringCheckKeyWords(spinachKeyWord));
            if (FoodGroup.Equals("24401"))
            {
                //Console.WriteLine("Spinach or kale or rocket by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[5] = 1;
            }

            // Cabbage or brussel sprouts  or bok choy
            string[] cabbageCode = new string[] { "24201" };
            string[] cabbageKeyWord = new string[] { "cabbage", "brussel sprout", "bok choy" };
            bool cabbage = (cabbageCode.Contains(FoodGroup) && foodName.StringCheckKeyWords(cabbageKeyWord));
            if (cabbage)
            {
                //Console.WriteLine("Cabbage or brussel sprouts or bok choy by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[6] = 1;
            }

            // Peas
            string[] peasCode = new string[] { "24501" };
            string[] peasKeyWord = new string[] { "pea" };
            bool peas = (peasCode.Contains(FoodGroup) && foodName.StringCheckKeyWords(peasKeyWord));
            if (peas)
            {
                //Console.WriteLine("Peas by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[7] = 1;
            }

            // Broccoli
            if (FoodGroup.Equals("24202") && FoodName.CaseInsensitiveContains("broccoli"))
            {
                //Console.WriteLine("Broccoli by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[8] = 1;
            }
            // Carrots
            if (FoodGroup.Equals("24301") && FoodName.CaseInsensitiveContains("Carrot"))
            {
                //Console.WriteLine("Carrot by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[9] = 1;
            }
            // Zucchini, eggplant, squash
            bool zucchiniEggplant = (FoodGroup.Equals("24702") && FoodName.CaseInsensitiveContains("zucchini")) || (FoodGroup.Equals("24705") && FoodName.CaseInsensitiveContains("eggplant"))
                || (FoodGroup.Equals("24702") && FoodName.CaseInsensitiveContains("Squash"));
            if (zucchiniEggplant)
            {
                //Console.WriteLine("Zucchini, eggplant, squash by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[10] = 1;
            }
            // Capsicum
            bool capsicum = FoodGroup.Equals("24705") && FoodName.CaseInsensitiveContains("capsicum");
            if (capsicum)
            {
                //Console.WriteLine("Capsicum- " + FoodGroup + " " + foodName);
                vegVarietyPoints[11] = 1;
            }
            // Corn, sweet corn, corn on the cob
            bool corn = FoodGroup.Equals("24704") && FoodName.CaseInsensitiveContains("corn");
            if (corn)
            {
                //Console.WriteLine("Corn by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[12] = 1;
            }
            // Mushrooms
            bool mushroom = FoodGroup.Equals("24703") && FoodName.CaseInsensitiveContains("mushroom");
            if (mushroom)
            {
                //Console.WriteLine("Mushroom by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[13] = 1;
            }
            // Tomatoes
            string[] tomatoCode = new string[] { "24601", "24602", "23106" };
            bool tomato = (tomatoCode.Contains(FoodGroup));
            if (tomato)
            {
                //Console.WriteLine("Tomato by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[14] = 1;
            }

            // Lecttuce, antichoke 
            bool artichoke = FoodGroup.Equals("24402") && FoodName.CaseInsensitiveContains("artichoke");
            if (artichoke)
            {
                //Console.WriteLine("Antichoke by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[15] = 1;
            }

            // Celery, cucumber, asparagus
            bool celery = FoodGroup.Equals("24402") && FoodName.CaseInsensitiveContains("celery") || FoodGroup.Equals("24402") && FoodName.CaseInsensitiveContains("asparagus")
                || FoodGroup.Equals("24705") && FoodName.CaseInsensitiveContains("cucumber");
            if (celery)
            {
                //Console.WriteLine("Celery or asparagus or cucumber by code " + FoodGroup + " " + foodName);
                vegVarietyPoints[16] = 1;
            }
            // Onion, spring onion, leek, 
            string[] onionCode = new string[] { "24802" };
            string[] onionKeyWord = new string[] { "onion", "spring onion", "leek" };
            bool onion = (onionCode.Contains(FoodGroup) && foodName.StringCheckKeyWords(onionKeyWord));
            if (onion)
            {
                //Console.WriteLine("Onion- " + FoodGroup + " " + foodName);
                vegVarietyPoints[17] = 1;
            }
            // Mixed vegetable dishes(salads, soups)
            string[] mixedVegetablesCode = new string[] { "24803", "24901", "24902", "24904", "24905", "21102", "21302", "21402", "21502", "21602" };
            bool mixedVegetables = (mixedVegetablesCode.Contains(FoodGroup));
            if (mixedVegetables)
            {
                //Console.WriteLine("Mixed- " + FoodGroup + " " + foodName);
                vegVarietyPoints[18] = 2;
            }
            // Other (legumes)
            if (FoodGroup.Equals("25101") || FoodGroup.Equals("25102") || FoodGroup.Equals("25201") || FoodGroup.Equals("25202"))
            {
                //Console.WriteLine("Legumes- " + FoodGroup + " " + foodName);
                vegVarietyPoints[19] = 1;
            }

            // Rule 3 - If the food has a food code but it is not one that is listed for that core food group, apply rule 3
            // Does not apply if already receive points from mixed vegetable dishes(salads, soups)
            if (inVarietyVeg == false && vegVarietyPoints[18] != 2) // means no food code
            {
                if (vegetables_serve >= 2)
                {
                    vegVarietyPoints[18] = 2;
                }
                else
                {
                    vegVarietyPoints[18] = 1;
                }
            }
            return vegVarietyPoints;
        }

        public int[] CalculateFruitVarieryWithCode(int foodGroupInt, int[] fruitVarietyPoints)
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

        // Grain variety
        public int[] CalculateGrainVarieryWithCode(int foodGroupInt, int[] grainVarietyPoints)
        {
            // Muesli
            if (FoodGroup.Equals("12515") && FoodName.CaseInsensitiveContains("Muesli"))
            {
                //Console.WriteLine("Muesli- " + FoodGroup + " " + foodName);
                grainVarietyPoints[0] = 1;
            }
            // Porridge
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
            if (FoodGroup.Equals("13509") && (FoodName.CaseInsensitiveContains("pasta dish") || FoodName.CaseInsensitiveContains("spaghetti") || FoodName.CaseInsensitiveContains("lasagna"))
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

        // Protein variety
        public int[] CalculateProteinVarieryWithCode(int foodGroupInt, int[] proteinVarietyPoints)
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
            string[] cannedFish = new string[] { "15401", "15402" };
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
            string[] seeds = new string[] { "22101", "22102", "22301" };
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
            string[] soybeans = new string[] { "20601", "20701" };
            if (soybeans.Contains(FoodGroup))
            {
                //Console.WriteLine("Tacos, burritos, enchiladas- " + FoodGroup + " " + foodName);
                proteinVarietyPoints[10] = 1;
            }
            return proteinVarietyPoints;
        }

        // Dairy variety
        public int[] CalculateDairyVarieryWithCode(int foodGroupInt, int[] dairyPoints)
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
            string[] dairyDessert = new string[] { "19601", "19602" };
            if (dairyDessert.Contains(FoodGroup))
            {
                //Console.WriteLine("Dairty desserts: " + FoodGroup + " " + foodName);
                dairyPoints[4] = 1;
            }
            return dairyPoints;
        }

        // THE FOLLOWING FUNCTIONS CHECK VARIETY WITHOUT FOOD CODE

        // Calculate veg variety without code (might or might not contain keyword)
        public int[] CalculateVegVarieryWithoutCode(int[] vegVarietyPoints)
        {
            // To check whether name contains keyWord
            bool inVarietyVeg = false;
            // List of keyword for each types
            string[] potato = new string[] { "potato" };
            string[] pumpkinTurnipSwede = new string[] { "pumpkin", "turnip", "swede" };
            string[] sweetPotatoBeetroot = new string[] { "sweet potato", "beetroot" };
            string[] cauliflower = new string[] { "cauliflower" };
            string[] greenBeans = new string[] { "green bean" };
            string[] spinachKaleLettuce = new string[] { "spinach", "kale", "rocket", "lettuce" };
            string[] cabbage = new string[] { "cabbage", "brussel sprout", "bok choy" };
            string[] pea = new string[] { "pea" };
            string[] broccoli = new string[] { "broccoli" };
            string[] carrot = new string[] { "carrot" };
            string[] zucchiniEggplant = new string[] { "zucchini", "eggplant", "squash" };
            string[] capsicum = new string[] { "capsicum" };
            string[] corn = new string[] { "corn" };
            string[] mushRoom = new string[] { "mushRoom" };
            string[] tomato = new string[] { "tomato" };
            string[] artichoke = new string[] { "artichoke" };
            string[] celery = new string[] { "celery", "asparagus", "cucumber" };
            string[] onion = new string[] { "onion", "spring onion", "leek" };
            string[] legumes = new string[] { "chick pea", "lentil", "kidney bean", "black bean", "butter bean", "navy bean", "haricot bean", "adzuki bean", "cannellini bean",
                "borlotti bean", "edamame bean", "fava bean", "lima bean", "pinto bean", "lupin", "dahl", "split pea" };

            // Potato
            if (FoodName.StringCheckKeyWords(potato))
            {
                Console.WriteLine("Potato by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[0] = 1;
                inVarietyVeg = true;
            }

            // Pumpkin or turnip or swede 

            if (FoodName.StringCheckKeyWords(pumpkinTurnipSwede))
            {
                Console.WriteLine("Pumpkin or turnip or swede by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[1] = 1;
                inVarietyVeg = true;
            }

            // Sweet potato or beetroot
            if (FoodName.StringCheckKeyWords(sweetPotatoBeetroot))
            {
                Console.WriteLine("Sweet potato or beetroot by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[2] = 1;
                inVarietyVeg = true;
            }

            // Cauliflower
            if (FoodName.StringCheckKeyWords(cauliflower))
            {
                Console.WriteLine("Cauliflower by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[3] = 1;
                inVarietyVeg = true;
            }

            // Green beans
            if (FoodName.StringCheckKeyWords(greenBeans))
            {
                Console.WriteLine("Green beans by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[4] = 1;
                inVarietyVeg = true;
            }

            // Spinach or kale or rocket or lettuce
            if (FoodName.StringCheckKeyWords(spinachKaleLettuce))
            {
                Console.WriteLine("Spinach or kale or rocket or lettuce by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[5] = 1;
                inVarietyVeg = true;
            }

            // Cabbage or brussel sprouts  or bok choy
            if (FoodName.StringCheckKeyWords(cabbage))
            {
                Console.WriteLine("Cabbage or brussel sprouts or bok choy by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[6] = 1;
                inVarietyVeg = true;
            }

            // Peas
            if (FoodName.StringCheckKeyWords(pea))
            {
                Console.WriteLine("Peas by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[7] = 1;
                inVarietyVeg = true;
            }

            // Broccoli
            if (FoodName.StringCheckKeyWords(broccoli))
            {
                Console.WriteLine("Broccoli by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[8] = 1;
                inVarietyVeg = true;
            }

            // Carrots
            if (FoodName.StringCheckKeyWords(broccoli))
            {
                Console.WriteLine("Carrot by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[9] = 1;
                inVarietyVeg = true;
            }

            // Zucchini, eggplant, squash
            if (FoodName.StringCheckKeyWords(zucchiniEggplant))
            {
                Console.WriteLine("Zucchini, eggplant, squash by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[10] = 1;
                inVarietyVeg = true;
            }

            // Capsicum
            if (FoodName.StringCheckKeyWords(capsicum))
            {
                Console.WriteLine("Capsicum by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[11] = 1;
                inVarietyVeg = true;
            }

            // Corn, sweet corn, corn on the cob
            if (FoodName.StringCheckKeyWords(corn))
            {
                Console.WriteLine("Corn by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[12] = 1;
                inVarietyVeg = true;
            }

            // Mushrooms
            if (FoodName.StringCheckKeyWords(mushRoom))
            {
                Console.WriteLine("Mushroom by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[13] = 1;
                inVarietyVeg = true;
            }

            // Tomatoes
            if (FoodName.StringCheckKeyWords(tomato))
            {
                Console.WriteLine("Tomato by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[14] = 1;
                inVarietyVeg = true;
            }

            // Lecttuce, antichoke 
            if (FoodName.StringCheckKeyWords(artichoke))
            {
                Console.WriteLine("Antichoke by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[15] = 1;
                inVarietyVeg = true;
            }

            // Celery, cucumber
            if (FoodName.StringCheckKeyWords(celery))
            {
                Console.WriteLine("Cucumber by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[16] = 1;
                inVarietyVeg = true;
            }

            // Onion, spring onion, leek, 
            if (FoodName.StringCheckKeyWords(onion))
            {
                Console.WriteLine("Onion by keyword: " + FoodGroup + " " + foodName);
                vegVarietyPoints[17] = 1;
                inVarietyVeg = true;
            }

            // Other (legumes)
            if (FoodName.StringCheckKeyWords(legumes))
            {
                Console.WriteLine("Legumes- " + FoodGroup + " " + foodName);
                vegVarietyPoints[19] = 1;
                inVarietyVeg = true;
            }

            // Rule 2 - If no food code or keyword = Mixed dish (recipe)
            if (inVarietyVeg == false) // means no keyword
            {
                if (vegetables_serve >= 2)
                {
                    vegVarietyPoints[18] = 2;
                }
                else
                {
                    vegVarietyPoints[18] = 1;
                }
            }

            // Just to record that the food is included in variety but is not mixed dish for at least 1 group
            else
            {
                inVariety = true;
            }
            return vegVarietyPoints;
        }

        public int[] CalculateFruitVarieryWithoutCode(int[] fruitVarietyPoints)
        {
            // To check whether name contains keyWord
            bool inVarietyFruit = false;
            // List of keyword for each types
            string[] freshfruit = new string[] { "fruit" };
            string[] applePear = new string[] { "apple", "pear" };
            string[] orangeMandarin = new string[] { "orange", "mandarin", "lemon", "lime", "grapefruit" };
            string[] banana = new string[] { "banana" };
            string[] peach = new string[] { "peach", "nectarine", "plum", "apricot" };
            string[] mango = new string[] { "mango", "paw paw" };
            string[] pineapple = new string[] { "pineapple" };
            string[] grapes = new string[] { "grape", "berry", "berri", "fig" };
            string[] melon = new string[] { "melon" };
            string[] other = new string[] { "passionfruit", "rhubarb", "persimmon", "pomegranate", "guava", "quince", "wild fruit" };


            //  Fresh fruit salad
            if (FoodName.StringCheckKeyWords(freshfruit))
            {
                Console.WriteLine("Fresh fruit by keyword: " + FoodGroup + " " + foodName);
                fruitVarietyPoints[1] = 1;
                inVarietyFruit = true;
            }

            // Apple or pear

            if (FoodName.StringCheckKeyWords(applePear))
            {
                Console.WriteLine("Apple or pear by keyword: " + FoodGroup + " " + foodName);
                fruitVarietyPoints[3] = 1;
                inVarietyFruit = true;
            }

            // Orange, mandarin, grapefruit
            if (FoodName.StringCheckKeyWords(orangeMandarin))
            {
                Console.WriteLine("Orange or mandarin by keyword: " + FoodGroup + " " + foodName);
                fruitVarietyPoints[4] = 1;
                inVarietyFruit = true;
            }

            // Banana
            if (FoodName.StringCheckKeyWords(banana))
            {
                Console.WriteLine("Banana by keyword: " + FoodGroup + " " + foodName);
                fruitVarietyPoints[5] = 1;
                inVarietyFruit = true;
            }

            // Peach, nectarine, plum or apricot
            if (FoodName.StringCheckKeyWords(peach))
            {
                Console.WriteLine("Peach, nectarine by keyword: " + FoodGroup + " " + foodName);
                fruitVarietyPoints[6] = 1;
                inVarietyFruit = true;
            }

            // Mango, paw-paw
            if (FoodName.StringCheckKeyWords(mango))
            {
                Console.WriteLine("Mango or paw paw: " + FoodGroup + " " + foodName);
                fruitVarietyPoints[7] = 1;
                inVarietyFruit = true;
            }

            // Pineapple
            if (FoodName.StringCheckKeyWords(pineapple))
            {
                Console.WriteLine("Pineapple: " + FoodGroup + " " + foodName);
                fruitVarietyPoints[8] = 1;
                inVarietyFruit = true;
            }

            // Grapes
            if (FoodName.StringCheckKeyWords(grapes))
            {
                Console.WriteLine("Grapes, strawberries by keyword: " + FoodGroup + " " + foodName);
                fruitVarietyPoints[9] = 1;
                inVarietyFruit = true;
            }

            // Melon
            if (FoodName.StringCheckKeyWords(melon))
            {
                Console.WriteLine("Melon by keyword: " + FoodGroup + " " + foodName);
                fruitVarietyPoints[10] = 1;
                inVarietyFruit = true;
            }

            // Other
            if (FoodName.StringCheckKeyWords(other))
            {
                Console.WriteLine("Other by keyword: " + FoodGroup + " " + foodName);
                fruitVarietyPoints[11] = 1;
                inVarietyFruit = true;
            }

            // Rule 2 - If no food code or keyword = Mixed dish (recipe)
            if (inVarietyFruit == false) // means no keyword
            {
                fruitVarietyPoints[1] = 1;
            }

            // Just to record that the food is included in variety but is not mixed dish for at least 1 group
            else
            {
                inVariety = true;
            }
            return fruitVarietyPoints;
        }

        public int[] CalculateGrainVarieryWithoutCode(int[] grainVarietyPoints)
        {
            // To check whether name contains keyWord
            bool inVarietyGrain = false;
            // List of keyword for each types
            string[] muesliGranola = new string[] { "muesli, granola" };
            string[] porridgeOats = new string[] { "porridge", "oat" };
            string[] bread = new string[] { "bread", "roll", "toast", "pita bread", "flat bread", "pizza", "sandwich", "burger" };
            string[] rice = new string[] { "rice", "sushi" };
            string[] muffin = new string[] { "muffin", "bagel", "crumpet", "scone", "bun", "scroll", "pancake" };
            string[] otherGrains = new string[] { "cous cous", "burghul", "pearl barley", "freekeh", "popcorn" };
            string[] noodles = new string[] { "noodle", "rice noodle" };
            string[] pasta = new string[] { "pasta", "spaghetti", "fettucine", "lasagna", "ravioli", "cannelloni" };
            string[] clearSoup = new string[] { "dumpling", "laksa" };
            string[] tacos = new string[] { "taco", "burrito", "enchilada" };
            string[] wholeGrainCrackers = new string[] { "biscuit", "cracker" };


            //   Muesli or granola
            if (FoodName.StringCheckKeyWords(muesliGranola))
            {
                Console.WriteLine("Muesli or granola by keyword: " + FoodGroup + " " + foodName);
                grainVarietyPoints[0] = 1;
                inVarietyGrain = true;
            }

            //  Porridge or oats
            if (FoodName.StringCheckKeyWords(porridgeOats))
            {
                Console.WriteLine("Porridge or oats by keyword: " + FoodGroup + " " + foodName);
                grainVarietyPoints[1] = 1;
                inVarietyGrain = true;
            }

            // Bread, pita bread, roll or toast
            if (FoodName.StringCheckKeyWords(bread))
            {
                Console.WriteLine("Bread by keyword: " + FoodGroup + " " + foodName);
                grainVarietyPoints[3] = 1;
                inVarietyGrain = true;
            }

            // English muffin, bagel, crumpet or scone or pancake 
            if (FoodName.StringCheckKeyWords(muffin))
            {
                Console.WriteLine("Muffin by keyword: " + FoodGroup + " " + foodName);
                grainVarietyPoints[4] = 1;
                inVarietyGrain = true;
            }

            // Rice
            if (FoodName.StringCheckKeyWords(rice))
            {
                Console.WriteLine("Rice or sushi by keyword: " + FoodGroup + " " + foodName);
                grainVarietyPoints[5] = 1;
                inVarietyGrain = true;
            }

            // Other grains e.g., cous cous, burghul, pearl barley, freekeh, popcorn
            if (FoodName.StringCheckKeyWords(otherGrains))
            {
                Console.WriteLine("Other grains by keyword: " + FoodGroup + " " + foodName);
                grainVarietyPoints[6] = 1;
                inVarietyGrain = true;
            }

            // Noodles e.g., egg noodles 
            if (FoodName.StringCheckKeyWords(noodles))
            {
                Console.WriteLine("Noodle or rice noodle: " + FoodGroup + " " + foodName);
                grainVarietyPoints[7] = 1;
                inVarietyGrain = true;
            }

            // Pasta e.g., spaghetti, lasagne, pasta bake
            if (FoodName.StringCheckKeyWords(pasta))
            {
                Console.WriteLine("Pasta by keyword: " + FoodGroup + " " + foodName);
                grainVarietyPoints[8] = 1;
                inVarietyGrain = true;
            }

            // Clear soup with rice or noodles
            if (FoodName.StringCheckKeyWords(clearSoup))
            {
                Console.WriteLine("Clear soup by keyword: " + FoodGroup + " " + foodName);
                grainVarietyPoints[9] = 1;
                inVarietyGrain = true;
            }

            // Tacos, burritos, enchiladas
            if (FoodName.StringCheckKeyWords(tacos))
            {
                Console.WriteLine("Taco by keyword: " + FoodGroup + " " + foodName);
                grainVarietyPoints[10] = 1;
                inVarietyGrain = true;
            }

            // Wholegrain crackers
            if (FoodName.StringCheckKeyWords(wholeGrainCrackers))
            {
                Console.WriteLine("Crackers by keyword: " + FoodGroup + " " + foodName);
                grainVarietyPoints[11] = 1;
                inVarietyGrain = true;
            }

            // Rule 2 - If no food code or keyword = Mixed dish (recipe)
            if (inVarietyGrain == false) // means no keyword
            {
                grainVarietyPoints[12] = 1;
            }

            // Just to record that the food is included in variety but is not mixed dish for at least 1 group
            else
            {
                inVariety = true;
            }
            return grainVarietyPoints;
        }

        public int[] CalculateProteinVarieryWithoutCode(int[] proteinVarietyPoints)
        {
            // To check whether name contains keyWord
            bool inVarietyProten = false;
            // List of keyword for each types
            string[] mince1 = new string[] { "beef", "lamb", "mutton", "veal", "venison", "kangaroo" };
            string[] mince2 = new string[] { "sausage", "mince", "rissole", "patty", "bolognese", "lasagna", "moussaka" };
            string[] meat1 = new string[] { "silverside", "beef", "lamb", "mutton", "veal", "venison", "kangaroo" };
            string[] meat2 = new string[] { "steak", "chop", "roast", "fillet", "schnitzel" };
            string[] chicken = new string[] { "chicken", "turkey", "duck", "quail", "poultry" };
            string[] pork = new string[] { "pork", "pig" };
            string[] fish = new string[] { "fish" };
            string[] otherSeafood = new string[] { "seafood" };
            // Alternative
            string[] nut = new string[] { "nut" };
            string[] seed = new string[] { "seed" };
            string[] egg = new string[] { "egg" };
            string[] soybean = new string[] { "soybean" };


            //   Mince e.g., spaghetti bolognese, rissoles, lasagna, Beef, lamb, veal dishes
            if (FoodName.StringCheckKeyWords(mince1, mince2))
            {
                Console.WriteLine("Mince by keyword: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[0] = 1;
                inVarietyProten = true;
            }

            //  Meat (beef or lamb) roast, chops (with/without sauce), kangaroo
            if (FoodName.StringCheckKeyWords(meat1, meat2))
            {
                Console.WriteLine("Meat by keyword: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[1] = 1;
                inVarietyProten = true;
            }

            // Chicken e.g., BBQ, satay, stir fry (with/without sauce), turkey
            if (FoodName.StringCheckKeyWords(chicken))
            {
                Console.WriteLine("Chicken by keyword: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[2] = 1;
                inVarietyProten = true;
            }

            // Pork e.g., chops, sweet & sour (with/without sauce)
            if (FoodName.StringCheckKeyWords(pork))
            {
                Console.WriteLine("Pork by keyword: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[3] = 1;
                inVarietyProten = true;
            }

            // Fresh fish, not crumbed or battered
            if (FoodName.StringCheckKeyWords(fish))
            {
                Console.WriteLine("Fish by keyword: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[4] = 1;
                inVarietyProten = true;
            }

            // No keyword for Canned tuna, salmon, sardines

            // Other seafood e.g., prawns
            if (FoodName.StringCheckKeyWords(otherSeafood))
            {
                Console.WriteLine("Seafood by keyword: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[6] = 1;
                inVarietyProten = true;
            }

            // Nuts e.g., peanuts, almonds, nut spreads
            if (FoodName.StringCheckKeyWords(nut))
            {
                Console.WriteLine("Nut by keyword: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[7] = 1;
                inVarietyProten = true;
            }

            // Seeds
            if (FoodName.StringCheckKeyWords(seed))
            {
                Console.WriteLine("Seeds by keyword: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[8] = 1;
                inVarietyProten = true;
            }

            // Eggs e.g., boiled, scrambled
            if (FoodName.StringCheckKeyWords(egg))
            {
                Console.WriteLine("Egg by keyword: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[9] = 1;
                inVarietyProten = true;
            }

            // Soybeans, tofu, meat substitutes
            if (FoodName.StringCheckKeyWords(soybean))
            {
                Console.WriteLine("Soybeans by keyword: " + FoodGroup + " " + foodName);
                proteinVarietyPoints[10] = 1;
                inVarietyProten = true;
            }

            // Rule 2 - If no food code or keyword = Mixed dish (recipe)
            if (inVarietyProten == false) // means no keyword
            {
                proteinVarietyPoints[11] = 1;
            }

            // Just to record that the food is included in variety but is not mixed dish for at least 1 group
            else
            {
                inVariety = true;
            }
            return proteinVarietyPoints;
        }

        public int[] CalculateDairyVarieryWithoutCode(int[] dairyVarietyPoints)
        {
            // To check whether name contains keyWord
            bool inVarietyDairy = false;
            // List of keyword for each types
            string[] plainMilk = new string[] { "milk" };
            string[] yoghust = new string[] { "yoghust" };
            string[] cheese = new string[] { "cheese" };
            string[] custard = new string[] { "custard", "creme brulee" };

            //  Plain milk-glass or with cereal or alternative
            if (FoodName.StringCheckKeyWords(plainMilk))
            {
                Console.WriteLine("Plain milk by keyword: " + FoodGroup + " " + foodName);
                dairyVarietyPoints[1] = 1;
                inVarietyDairy = true;
            }

            // Yoghurt (not frozen) plain or flavoured 
            if (FoodName.StringCheckKeyWords(yoghust))
            {
                Console.WriteLine("Yoghust by keyword: " + FoodGroup + " " + foodName);
                dairyVarietyPoints[2] = 1;
                inVarietyDairy = true;
            }

            //  Cheese; hard/soft 
            if (FoodName.StringCheckKeyWords(cheese))
            {
                Console.WriteLine("Cheese by keyword: " + FoodGroup + " " + foodName);
                dairyVarietyPoints[3] = 1;
                inVarietyDairy = true;
            }

            // Dairy desserts; custard, cream brulee
            if (FoodName.StringCheckKeyWords(custard))
            {
                Console.WriteLine("Fish by keyword: " + FoodGroup + " " + foodName);
                dairyVarietyPoints[4] = 1;
                inVarietyDairy = true;
            }

            // Rule 2 - If no food code or keyword = Mixed dish (recipe)
            if (inVarietyDairy == false) // means no keyword
            {
                dairyVarietyPoints[5] = 1;
            }

            // Just to record that the food is included in variety but is not mixed dish for at least 1 group
            else
            {
                inVariety = true;
            }
            return dairyVarietyPoints;
        }


        public bool DiscretionaryCheck(HashSet<string> discretionaryList)
        {
            // Recipe discretionary if 
            const double discreSaturatedFat = 0.05;
            const double discreSugar = 0.15;
            // 125 group discretionary if >30g sugar/100g
            const double discre125 = 0.3;
            // 125 group with fruit discretionary if >35g sugar/100g
            const double discre125Fruit = 0.35;
            // 181 group discretionary if >10g total fat/100g
            const double discre181 = 0.1;
            // 182 group discretionary if >10g total fat/100g
            const double discre182 = 0.1;
            // 183 group discretionary if >10g total fat/100g
            const double discre183 = 0.1;
            // 184 group discretionary if >10g total fat/100g
            const double discre184 = 0.1;
            // 18703 group discretionary if fast food chain or >5g saturated fat/100g
            const double discre18703 = 0.05;
            // 18707 group discretionary if >5g saturated fat/100g
            const double discre18707 = 0.05;
            // 18801 group discretionary if >5g saturated fat/100g
            const double discre18801 = 0.05;
            // 18903 group discretionary if fast food chain or >10g total fat/100g, or >5g saturated fat/100g, or 450mg Na/100g
            const double discre18903TotalFat = 0.1;
            const double discre18903SaturatedFat = 0.05;
            const double discre18903Na = 4.5;

            // 18906 group discretionary if fast food chain or >30g sugar/100g
            double discre18906 = 0.3;
            // 26202 group discretionary if fast food chain or >5g saturated fat/100g, or 450mg Na/100g
            double discre26202SaturatedFat = 0.05;
            double discre26202Na = 4.5;
            // 22203 group discretionary if name is coconut

            if (discretionaryList.Contains(FoodGroup))
            {
                return true;
            }
            // 125 group discretionary if >30g sugar/100g or >35g sugar/100 g with fruit
            if (FoodGroup.StartsWith("125"))
            {
                // Contain fruit if 12508, 12509, 12510, 12514, 12515
                if (FoodGroup.StartsWith("12508") || FoodGroup.StartsWith("12509") || FoodGroup.StartsWith("12510")
                    || FoodGroup.StartsWith("12514") || FoodGroup.StartsWith("12515"))
                {
                    double discretionaryIf = Weight_g * discre125Fruit;
                    if (Sugar > discretionaryIf)
                    {
                        return true;
                    }
                    return false;
                }
                else
                {
                    double discretionaryIf = Weight_g * discre125;
                    if (Sugar > discretionaryIf)
                    {
                        return true;
                    }
                    return false;
                }
            }
            // 181 group discretionary if >10g total fat/100g
            if (FoodGroup.StartsWith("181"))
            {
                double discretionaryIf = Weight_g * discre181;
                if (Total_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 182 group discretionary if >10g total fat/100g
            if (FoodGroup.StartsWith("182"))
            {
                double discretionaryIf = Weight_g * discre182;
                if (Total_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 183 group discretionary if >10g total fat/100g
            if (FoodGroup.StartsWith("183"))
            {
                double discretionaryIf = Weight_g * discre183;
                if (Total_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 184 group discretionary if >10g total fat/100g
            if (FoodGroup.StartsWith("184"))
            {
                double discretionaryIf = Weight_g * discre184;
                if (Total_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 18703 group discretionary if >10g total fat/100g
            if (FoodGroup.StartsWith("18703"))
            {
                double discretionaryIf = Weight_g * discre18703;
                if (Saturated_fat_g > discretionaryIf || FoodName.CaseInsensitiveContains("fast food"))
                {
                    return true;
                }
                return false;
            }
            // 18707 group discretionary if >10g total fat/100g
            if (FoodGroup.StartsWith("18707"))
            {
                double discretionaryIf = Weight_g * discre18707;
                if (Saturated_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 18801 group discretionary if >10g total fat/100g
            if (FoodGroup.StartsWith("18801"))
            {
                double discretionaryIf = Weight_g * discre18801;
                if (Saturated_fat_g > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 18903 group discretionary if fast food chain or >10g total fat/100g, or >5g saturated fat/100g, or 450mg Na/100g
            if (FoodGroup.StartsWith("18903"))
            {
                double discretionaryIf1 = Weight_g * discre18903TotalFat;
                double discretionaryIf2 = Weight_g * discre18903SaturatedFat;
                double discretionaryIf3 = Weight_g * discre18903Na;
                if (Total_fat_g > discretionaryIf1 || Saturated_fat_g > discretionaryIf2 || Sodium_mg > discre18903Na || FoodName.CaseInsensitiveContains("fast food"))
                {
                    return true;
                }
                return false;
            }
            // 18906 group discretionary if fast food chain or >30g sugar/100g
            if (FoodGroup.StartsWith("18906"))
            {
                double discretionaryIf = Weight_g * discre18906;
                if (Sugar > discretionaryIf)
                {
                    return true;
                }
                return false;
            }
            // 26202 group discretionary if fast food chain or >5g saturated fat/100g, or 450mg Na/100g
            if (FoodGroup.StartsWith("26202"))
            {
                double discretionaryIf1 = Weight_g * discre26202SaturatedFat;
                double discretionaryIf2 = Weight_g * discre26202Na;
                if (Saturated_fat_g > discretionaryIf1 || Sodium_mg > discretionaryIf2 || FoodName.CaseInsensitiveContains("fast food"))
                {
                    return true;
                }
                return false;
            }
            // 22203 group discretionary if name is coconut
            if (FoodGroup.StartsWith("22203") && FoodName.CaseInsensitiveContains("coconut"))
            {
                return true;
            }
            // Discretionary without food code
            if (FoodGroup.Equals(""))
            {
                double discretionaryIf1 = Weight_g * discreSaturatedFat;
                double discretionaryIf2 = Weight_g * discreSugar;
                if (Saturated_fat_g > discretionaryIf1 || Sugar > discretionaryIf2)
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }

            // Hot chocolate is discretionary
            // Write later
        }
    }
}
