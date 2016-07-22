using _Project_Geass.Bootloaders.Core.Events.Drawing.Minions;
using _Project_Geass.Constants;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace _Project_Geass.Bootloaders.Core.Menus
{
    internal class Drawing
    {
        private Events.Drawing.Minions.LastHitHelper _helper;

        public Menu MinonMenu()
        {
            var menu = new Menu(Names.Menu.DrawingNameBase + "Minons", "minionMenu");
            menu.AddItem(
                new MenuItem(Names.Menu.DrawingItemBase + ".Minion." + "Boolean.LastHitHelper", "LastHit Helper").SetValue(
                    false));

            menu.AddItem(
                new MenuItem(Names.Menu.DrawingItemBase + ".Minion." + "Circle.KillableColor", "Killable Color").SetValue(
                    new Circle(true, Color.Green)));
            menu.AddItem(
                new MenuItem(Names.Menu.DrawingItemBase + ".Minion." + "Slider.RenderDistance", "Render Distance").SetValue(
                    new Slider(1000, 500, 2500)));
            return menu;
        }

        public Drawing()
        {
            Globals.Static.Objects.ProjectLogger.WriteLog("Create Drawing Menu");
            Globals.Static.Objects.ProjectMenu.AddToMainMenu();
            _helper = new LastHitHelper();
        }
    }
}