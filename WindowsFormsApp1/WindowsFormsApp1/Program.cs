using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public static class Extensions
    {
        public static bool CaseInsensitiveContains(this string text, string value,
            StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }

        public static bool StringCheckKeyWords(this string stringToCheck, string[] stringArray)
        {
            foreach (string keyword in stringArray)
            {
                if (stringToCheck.CaseInsensitiveContains(keyword))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool StringCheckKeyWords(this string stringToCheck, List<string> stringList)
        {
            foreach (string keyword in stringList)
            {
                if (stringToCheck.CaseInsensitiveContains(keyword))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool StringCheckKeyWords(this string stringToCheck, string[] bracket1, string[] bracket2)
        {
            List<string> combination = new List<string>();
            foreach (string i in bracket1)
            {
                foreach (string j in bracket2)
                {
                    combination.Add(i + " " + j);
                }
            }
            foreach (string keyword in combination)
            {
                if (stringToCheck.CaseInsensitiveContains(keyword))
                {
                    return true;
                }
            }
            return false;
        }
    }


    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImportForm());
        }
    }
}
