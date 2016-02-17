using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace SIEvade
{
    internal class Program : Core
    {
        // ReSharper disable once UnusedParameter.Local
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

            SMenu.AddSubMenu(new Menu($"v{AssemblyVersion}", $"versionMenu{AssemblyVersion}"));
            SMenu.AddToMainMenu();

            Game.PrintChat("<b> <font color=\"#F88017\">SI</font> Evade</b> - <font color=\"#F88017\">Thank You for choosing me </font>!");
        }
    }
}
