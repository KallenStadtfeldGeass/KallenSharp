using LeagueSharp;
using LeagueSharp.Common;

namespace Geass_Tristana.Menus
{
    internal class LevelMenu : Events.LevelEvents, Interface.IMenu
    {
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
    }
}