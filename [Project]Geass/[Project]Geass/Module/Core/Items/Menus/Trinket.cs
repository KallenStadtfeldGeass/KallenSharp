using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;

namespace _Project_Geass.Module.Core.Items.Menus
{
    internal sealed class Trinket
    {
        // ReSharper disable once MemberCanBeMadeStatic.Local
        private Menu Menu()
        {
            var menu = new Menu(Names.Menu.TrinketNameBase, "trinketOptions");
            menu.AddItem(new MenuItem(Names.Menu.TrinketItemBase + "Boolean.BuyOrb", "Auto Buy Orb At Level >= 9").SetValue(true));
            return menu;
        }

        public Trinket(Menu menu)
        {
            menu.AddSubMenu(Menu());
            // ReSharper disable once UnusedVariable
            var trinket = new Events.Trinket();
            Static.Objects.ProjectLogger.WriteLog("Trinket Menu and events loaded.");
        }
    }
}