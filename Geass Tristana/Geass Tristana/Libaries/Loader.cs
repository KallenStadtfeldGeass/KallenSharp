﻿using Geass_Tristana.Other;
using LeagueSharp;

namespace Geass_Tristana.Libaries
{
    internal class Loader : Core
    {
        public static void LoadAssembly()
        {
            //Initilize Menus
            var humanizerMenu = new Menus.HumanizerMenu();
            var minonsMenu = new Menus.MinonsMenu();
            var itemsMenu = new Menus.ItemsMenu();
            var trinketMenu = new Menus.TrinketMenu();
            var levelMenu = new Menus.LevelMenu();
            var champMenu = new Menus.ChampionMenu();
            var orbwalkerMenu = new Menus.OrbwalkerMenu();
            var antiMenu = new Menus.AntiMenu();

            //Load Data
            Champion.Player = ObjectManager.Player;
            //Load Menus into SMenu
            humanizerMenu.Load();
            antiMenu.Load();
            minonsMenu.Load();
            itemsMenu.Load();
            trinketMenu.Load();
            levelMenu.Load();
            champMenu.Load();
            orbwalkerMenu.Load();
            //_Assembly.CheckVersion();

            //Add SMenu to main menu
            SMenu.AddToMainMenu();
        }
    }
}