using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Common;

namespace SIEvade
{
    internal class Program : Core
    {
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
            if (Player.ChampionName == null)// No champ loaded?
                return;
            
            //Create Menu
                MenuBuilder.Load(Player.ChampionName);

            Core.SMenu.AddToMainMenu();
        }
    }
}
