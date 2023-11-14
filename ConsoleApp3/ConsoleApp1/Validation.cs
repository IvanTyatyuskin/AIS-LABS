using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Validation
    {
        public static bool IsValidString(string input)
        {
            return !string.IsNullOrEmpty(input) && input.All(c => char.IsLetter(c)) && Char.IsUpper(input[0]);
        }

        public static bool ConvertToBool(string input)
        {
            if (input == "да")
                return true;
            else
                return false;
        }

    }
}
