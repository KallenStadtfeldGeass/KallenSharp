using LeagueSharp;
using LeagueSharp.Common;

namespace Geass_Tristana.Menus
{
    internal class TrinketMenu : Events.TrinketEvents, Interface.IMenu
    {
        public Menu GetMenu()
        {
                var menu = new Menu(MenuNameBase, "trinketOptions");
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.BuyOrb", "Auto Buy Orb At Level >= 9").SetValue(true));
                return menu;
        }

        public void Load()
        {
            SMenu.AddSubMenu(GetMenu());
            Game.OnUpdate += OnUpdate;
        }
    }
}