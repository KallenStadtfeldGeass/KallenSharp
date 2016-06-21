using System;
using GeassLib.Events;
using Geass_Tristana.Other;
using LeagueSharp;
using LeagueSharp.Common;

namespace Geass_Tristana.Menus
{
    internal class LevelMenu : Core, GeassLib.Interfaces.Core.Menu
    {

        public const string MenuItemBase = ".Level.";
        public const string MenuNameBase = ".Level Menu";
        private readonly OnLevel _onLevel = new OnLevel(Data.Level.AbilitySequence);
        public Menu GetMenu()
        {
                var menu = new Menu(MenuNameBase, "levelMenu");
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.AutoLevelUp", "Auto level-up abilities").SetValue(true));
                return menu;
        }

        public void Load()
        {
            SMenu.AddSubMenu(GetMenu());
            Game.OnUpdate += OnUpdate;
        }

        void OnUpdate(EventArgs args)
        {
            _onLevel.Enabled = SMenu.Item(MenuItemBase + "Boolean.AutoLevelUp").GetValue<bool>();
        }

    }
}