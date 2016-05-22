using LeagueSharp;
using LeagueSharp.Common;

namespace S__Class_Tristana.Menus
{
    internal class TrinketMenu : Events.TrinketEvents
    {
        private Menu _Menu
        {
            get
            {
                var menu = new Menu(MenuNameBase, "trinketOptions");
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.BuyOrb", "Auto Buy Orb At Level >= 9").SetValue(true));
                return menu;
            }
        }

        public void Load()
        {
            SMenu.AddSubMenu(_Menu);
            Game.OnUpdate += OnUpdate;
        }
    }
}