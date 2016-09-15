using _Project_Geass.Data;
using _Project_Geass.Globals;
using _Project_Geass.Module.Core.Drawing.Events;
using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace _Project_Geass.Module.Core.Drawing.Menus
{
    internal sealed class Drawing
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Drawing"/> class.
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// <param name="drawingOptions">The drawing options.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        public Drawing(Menu menu, bool[] drawingOptions,bool enabled)
        {
            if (!enabled) return;

            menu.AddSubMenu(Menu(drawingOptions));
            // ReSharper disable once UnusedVariable
            var helper = new LastHitHelper();

            Static.Objects.ProjectLogger.WriteLog("Drawing Menu and events loaded.");
        }

        /// <summary>
        /// Menus the specified drawing options.
        /// </summary>
        /// <param name="drawingOptions">The drawing options.</param>
        /// <returns></returns>
        public Menu Menu(bool[] drawingOptions)
        {
            var menu = new Menu(Names.Menu.DrawingNameBase, "enemyMenu");

            var enemyMenu = new Menu(".Enemys", "enemyMenu");
            enemyMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnEnemy", "Draw On Enemys").SetValue(true));
            enemyMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnEnemy.FillColor", "Combo Damage Fill").SetValue(new Circle(true, Color.DarkGray)));

            var selfMenu = new Menu(".Self", "selfMenu");
            selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf", "Draw On Self").SetValue(true));

            if (drawingOptions[0])
                selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.QColor", "Q Range").SetValue(new Circle(true, Color.LightBlue)));
            if (drawingOptions[1])
                selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.WColor", "W Range").SetValue(new Circle(true, Color.LightGreen)));
            if (drawingOptions[2])
                selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.EColor", "E Range").SetValue(new Circle(true, Color.LightCoral)));
            if (drawingOptions[03])
                selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.RColor", "R Range").SetValue(new Circle(true, Color.LightSlateGray)));

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