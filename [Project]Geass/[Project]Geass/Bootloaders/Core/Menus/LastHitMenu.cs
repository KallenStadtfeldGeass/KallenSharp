using _Project_Geass.Bootloaders.Core.Events.Drawing.Minions;
using _Project_Geass.Data;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace _Project_Geass.Bootloaders.Core.Menus
{
    internal class LastHitMenu
    {
        private Menu GetMenu()
        {
            var menu = new Menu(Names.Menu.LastHitHelperNameBase, "minionMenu");
            menu.AddItem(
                new MenuItem(Names.Menu.LastHitHelperItemBase + ".Minion." + "Boolean.LastHitHelper", "LastHit Helper").SetValue(
                    false));

            menu.AddItem(
                new MenuItem(Names.Menu.LastHitHelperItemBase + ".Minion." + "Circle.KillableColor", "Killable Color").SetValue(
                    new Circle(true, Color.LightGray)));

            return menu;
        }

        public LastHitMenu()
        {
            Globals.Static.Objects.ProjectMenu.AddSubMenu(GetMenu());
            // ReSharper disable once UnusedVariable
            var helper = new LastHitHelper();

            Globals.Static.Objects.ProjectLogger.WriteLog("LastHitHelper Menu and events loaded.");
        }
    }
}