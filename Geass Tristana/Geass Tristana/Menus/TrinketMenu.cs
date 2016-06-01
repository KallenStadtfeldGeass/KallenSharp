using LeagueSharp;
using LeagueSharp.Common;

namespace Geass_Tristana.Menus
{
     class TrinketMenu : Events.TrinketEvents, GeassLib.Interfaces.Core.Menu
    {
        public Menu GetMenu()
        {
                var menu = new Menu(MenuNameBase, "trinketOptions");
                GeassLib.Functions.Menu.AddBool(menu, MenuItemBase + "Boolean.BuyOrb", "Auto Buy Orb At Level >= 9");
                return menu;
        }

        public void Load()
        {
            SMenu.AddSubMenu(GetMenu());
            Game.OnUpdate += OnUpdate;
        }
    }
}