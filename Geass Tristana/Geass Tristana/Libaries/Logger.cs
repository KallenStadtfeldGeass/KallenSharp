using System;
using Geass_Tristana.Other;


namespace Geass_Tristana.Libaries
{
    class Logger : Core
    {
        public static void Write(string s)
        {
            if(UseLogger())
              Console.WriteLine($"Geass Tristana:{s}");
        }

        public static bool UseLogger()
        {
           return SMenu.Item("Geass.Logger.Enable").GetValue<bool>();
        }
    }
}
