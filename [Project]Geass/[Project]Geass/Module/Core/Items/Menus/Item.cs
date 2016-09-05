﻿using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;

namespace _Project_Geass.Module.Core.Items.Menus
{
    internal sealed class Item
    {
        private Menu Menu()
        {
            var menu = new Menu(Names.Menu.ItemMenuBase, "itemMenu");

            var offensiveMenu = new Menu(Names.Menu.MenuOffensiveItemBase, "offensiveMenu");
            offensiveMenu.AddItem(new MenuItem(Names.Menu.MenuOffensiveItemBase + "Boolean.Bork", "Use BotRK/Cutlass").SetValue(true));
            offensiveMenu.AddItem(new MenuItem(Names.Menu.MenuOffensiveItemBase + "Boolean.Youmuu", "Use Youmuu's").SetValue(true));
            offensiveMenu.AddItem(new MenuItem(Names.Menu.MenuOffensiveItemBase + "Slider.Bork.MinHp", "(BotRK/Cutlass) Min% HP Remaining(Target)").SetValue(new Slider(20)));
            offensiveMenu.AddItem(new MenuItem(Names.Menu.MenuOffensiveItemBase + "Slider.Bork.MaxHp", "(BotRK/Cutlass) Max% HP Remaining(Target)").SetValue(new Slider(55)));
            offensiveMenu.AddItem(new MenuItem(Names.Menu.MenuOffensiveItemBase + "Slider.Bork.MinHp.Player", "(BotRK/Cutlass) Min% HP Remaining(Player)").SetValue(new Slider(20)));
            offensiveMenu.AddItem(new MenuItem(Names.Menu.MenuOffensiveItemBase + "Boolean.ComboOnly", "Only use offensive items in combo").SetValue(true));

            var defensiveMenu = new Menu(Names.Menu.MenuDefensiveNameBase, "defensiveMenu");

            var qssMenu = new Menu(".QSS Menu", "qssMenu");

            qssMenu.AddItem(new MenuItem(Names.Menu.MenuDefensiveItemBase + "Boolean.QSS", "Use QSS").SetValue(true));
            qssMenu.AddItem(new MenuItem(Names.Menu.MenuDefensiveItemBase + "Slider.QSS.Delay", "QSS Delay").SetValue(new Slider(300, 250, 1500)));

            foreach (var buff in Buffs.GetTypes)
            {
                qssMenu.AddItem(new MenuItem(Names.Menu.MenuDefensiveItemBase + "Boolean.QSS." + buff, "On " + buff).SetValue(true));
            }

            var mercMenu = new Menu(".Merc Menu", "MercMenu");

            mercMenu.AddItem(new MenuItem(Names.Menu.MenuDefensiveItemBase + "Boolean.Merc", "Use Merc").SetValue(true));
            mercMenu.AddItem(new MenuItem(Names.Menu.MenuDefensiveItemBase + "Slider.Merc.Delay", "Merc Delay").SetValue(new Slider(300, 250, 1500)));

            foreach (var buff in Buffs.GetTypes)
            {
                mercMenu.AddItem(new MenuItem(Names.Menu.MenuDefensiveItemBase + "Boolean.Merc." + buff, "On " + buff).SetValue(true));
            }

            defensiveMenu.AddSubMenu(qssMenu);
            defensiveMenu.AddSubMenu(mercMenu);
            defensiveMenu.AddItem(new MenuItem(Names.Menu.MenuDefensiveItemBase + "Boolean.ComboOnly", "Only use offensive items in combo").SetValue(true));

            menu.AddSubMenu(offensiveMenu);
            menu.AddSubMenu(defensiveMenu);
            return menu;
        }

        public Item(Menu menu)
        {
            Static.Objects.ProjectLogger.WriteLog("Item Menu and events loaded.");
            menu.AddSubMenu(Menu());
            // ReSharper disable once UnusedVariable
            var items = new Events.Item();
        }
    }
}