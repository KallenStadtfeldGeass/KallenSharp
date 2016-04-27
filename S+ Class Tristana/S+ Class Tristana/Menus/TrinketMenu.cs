using LeagueSharp;
using LeagueSharp.Common;


namespace S__Class_Tristana.Menus
{
    class TrinketMenu : Events.TrinketEvents
    {
        public void Load()
        {
            SMenu.AddSubMenu(_Menu());
            Game.OnUpdate += OnUpdate;
        }

        private Menu _Menu()
        {
            var menu = new Menu(_MenuNameBase, "trinketOptions");
            menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.BuyOrb", "Auto Buy Orb At Level >= 9").SetValue(true));
            return menu;
        }
    }
}
