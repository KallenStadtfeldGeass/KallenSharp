using LeagueSharp;
using LeagueSharp.Common;
using S__Class_Tristana.Other;

namespace S__Class_Tristana.Libaries
{
    internal class Loader : Core
    {
        public void LoadAssembly()
        {
            //Initilize Menus
            var humanizerMenu = new Menus.HumanizerMenu();
            var minonsMenu = new Menus.MinonsMenu();
            var itemsMenu = new Menus.ItemsMenu();
            var trinketMenu = new Menus.TrinketMenu();
            var levelMenu = new Menus.LevelMenu();
            var champMenu = new Menus.ChampionMenu();
            var orbwalkerMenu = new Menus.OrbwalkerMenu();

            //Load Data
            Champion.Player = ObjectManager.Player;
            //Load Menus into SMenu
            humanizerMenu.Load();
            minonsMenu.Load();
            itemsMenu.Load();
            trinketMenu.Load();
            levelMenu.Load();
            champMenu.Load();
            //_Assembly.CheckVersion();

            //Load Menus + Events
            CommonOrbwalker = new Orbwalking.Orbwalker(SMenu.SubMenu(".CommonOrbwalker"));
            orbwalkerMenu.Load();

            //Add SMenu to main menu
            SMenu.AddToMainMenu();
        }
    }
}