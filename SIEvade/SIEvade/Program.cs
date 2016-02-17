using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
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

            (new Menu($"v{Core.AssemblyVersion}", $"versionMenu{Core.AssemblyVersion}")).AddToMainMenu();
            Core.SMenu.AddToMainMenu();

            Game.PrintChat("<b> <font color=\"#F88017\">SI</font> Evade</b> - <font color=\"#008080\">Thank You for choosing me </font>!");
        }
    }
}
