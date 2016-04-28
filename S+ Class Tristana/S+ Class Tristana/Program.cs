using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Common;
using LeagueSharp;

namespace S__Class_Tristana
{
    class Program : Core
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
     
           if (ObjectManager.Player.ChampionName != "Tristana")
               return;

            Libaries.Loader loader = new Libaries.Loader();
            loader.LoadAssembly();
        }
    }
}
