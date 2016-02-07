using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S_Plus_Class_Ryze
{
    class Program : Core
    {
        static void Main(string[] args)
        {
            if (args == null) throw new ArgumentNullException("args");
            // So you can test if it in VS wihout crashes
            #if !DEBUG
                      Load();
            #endif
        }

        static void Load()
        {
            if (Player.ChampionName.ToLower() != "ryze") return;
            Console.WriteLine(@"S+ Class Ryze Loading...");
            Console.WriteLine(@"S+ Class Ryze Loading...Champ Data...");
            Core.Champion.Load();
        }
    }
}
