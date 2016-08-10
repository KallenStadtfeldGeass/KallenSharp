using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders.Core.Menus
{
    internal class TrinketMenu
    {
        // ReSharper disable once NotAccessedField.Local

        public Menu GetMenu()
        {
            var menu = new Menu(Names.Menu.TrinketNameBase, "trinketOptions");
            menu.AddItem(new MenuItem(Names.Menu.TrinketItemBase + "Boolean.BuyOrb", "Auto Buy Orb At Level >= 9").SetValue(true));
            return menu;
        }

        public TrinketMenu()
        {
            Static.Objects.ProjectMenu.AddSubMenu(GetMenu());
            // ReSharper disable once UnusedVariable
            var trinket = new Events.OnUpdate.TrinketEvent();
            Static.Objects.ProjectLogger.WriteLog("Trinket Menu and events loaded.");
        }
    }
}