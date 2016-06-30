using Geass_Tristana.Other;
using LeagueSharp;

namespace Geass_Tristana.Libaries
{
    internal class Loader : Core
    {
        private static GeassLib.Loader _loader;
        public static void LoadAssembly()
        {
         
            //Initilize Menus
            var humanizerMenu = new Menus.HumanizerMenu();
            var champMenu = new Menus.ChampionMenu();
            var orbwalkerMenu = new Menus.OrbwalkerMenu();
            var antiMenu = new Menus.AntiMenu();

            //Load Data
            Champion.Player = ObjectManager.Player;
            //Load Menus into SMenu
            humanizerMenu.Load();
            antiMenu.Load();
            champMenu.Load();
            orbwalkerMenu.Load();

            _loader = new GeassLib.Loader($"{Champion.Player.ChampionName}", true, true, Data.Level.AbilitySequence, true, true);


            //_Assembly.CheckVersion();

            //Add SMenu to main menu


            SMenu.AddToMainMenu();
        }
    }
}