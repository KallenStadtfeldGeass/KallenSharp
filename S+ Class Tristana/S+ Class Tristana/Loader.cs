using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Libaries
{
    class Loader : Core
    {
        public void LoadAssembly()
        {
            //Initilize Menus
            Menus.HumanizerMenu _HumanizerMenu = new Menus.HumanizerMenu();
            Menus.MinonsMenu _MinonsMenu = new Menus.MinonsMenu();
            Menus.ItemsMenu _ItemsMenu = new Menus.ItemsMenu();
            Menus.TrinketMenu _TrinketMenu = new Menus.TrinketMenu();
            Menus.LevelMenu _LevelMenu = new Menus.LevelMenu();

            //Load Data
            Player = ObjectManager.Player;
            //Load Menus into SMenu
            _HumanizerMenu.Load();
            _MinonsMenu.Load();
            _ItemsMenu.Load();
            _TrinketMenu.Load();
            _LevelMenu.Load();

            //_Assembly.CheckVersion();

            //Load Menus + Events
            //Add Orbwalker to menu
            SMenu.AddSubMenu(new Menu(".Orbwalker", ".Orbwalker"));
            CommonOrbwalker = new Orbwalking.Orbwalker(SMenu.SubMenu(".CommonOrbwalker"));

            //Add SMenu to main menu
            SMenu.AddToMainMenu();
        }
    }
}
