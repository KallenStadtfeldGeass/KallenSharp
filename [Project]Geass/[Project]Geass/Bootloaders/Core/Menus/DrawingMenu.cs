using _Project_Geass.Bootloaders.Core.Events.Drawing.Minions;
using _Project_Geass.Data;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace _Project_Geass.Bootloaders.Core.Menus
{
    internal class DrawingMenu
    {
        public DrawingMenu()
        {
            Globals.Static.Objects.ProjectMenu.AddSubMenu(GetDrawingMenu());
            // ReSharper disable once UnusedVariable
            var helper = new LastHitHelper();

            Globals.Static.Objects.ProjectLogger.WriteLog("Drawing Menu and events loaded.");
        }

        public Menu GetDrawingMenu()
        {
            var menu = new Menu(Names.Menu.DrawingNameBase, "enemyMenu");

            var enemyMenu = new Menu(".Enemys", "enemyMenu");
            enemyMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + "Boolean.DrawOnEnemy", "Draw On Enemys").SetValue(true));
            enemyMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + "Boolean.DrawOnEnemy.FillColor", "Combo Damage Fill").SetValue(new Circle(true, Color.DarkGray)));

            var selfMenu = new Menu(".Self", "selfMenu");
            selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf", "Draw On Self").SetValue(true));

            selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.QColor", "Q Range").SetValue(new Circle(true, Color.LightBlue)));
            selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.WColor", "W Range").SetValue(new Circle(true, Color.LightGreen)));
            selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.EColor", "E Range").SetValue(new Circle(true, Color.LightCoral)));
            selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + "Boolean.DrawOnSelf.RColor", "R Range").SetValue(new Circle(true, Color.LightSlateGray)));

            var lastHitMenu = new Menu(Names.Menu.LastHitHelperNameBase, "minionMenu");

            lastHitMenu.AddItem(
                new MenuItem(Names.Menu.LastHitHelperItemBase + ".Minion." + "Boolean.LastHitHelper", "LastHit Helper").SetValue(
                    false));

            lastHitMenu.AddItem(
                new MenuItem(Names.Menu.LastHitHelperItemBase + ".Minion." + "Circle.KillableColor", "Killable Color").SetValue(
                    new Circle(true, Color.LightGray)));

            menu.AddSubMenu(lastHitMenu);
            menu.AddSubMenu(enemyMenu);
            menu.AddSubMenu(selfMenu);

            return menu;
        }
    }
}