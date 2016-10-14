using System.Drawing;
using LeagueSharp.Common;
using _Project_Geass.Data.Static;

namespace _Project_Geass.Module.Core.Drawing.Menus
{

    internal sealed class Drawing
    {
        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Drawing" /> class.
        /// </summary>
        /// <param name="menu">
        ///     The menu.
        /// </param>
        /// <param name="drawingOptions">
        ///     The drawing options.
        /// </param>
        /// <param name="enabled">
        ///     if set to <c> true </c> [enabled].
        /// </param>
        public Drawing(Menu menu, bool[] drawingOptions, bool enabled)
        {
            if (!enabled)
                return;

            menu.AddSubMenu(Menu(drawingOptions));
            // ReSharper disable once UnusedVariable
            var helper=new Events.Drawing();

            Objects.ProjectLogger.WriteLog("Drawing Menu and events loaded.");
        }

        #endregion Public Constructors

        #region Public Methods

        /// <summary>
        ///     Menus the specified drawing options.
        /// </summary>
        /// <param name="drawingOptions">
        ///     The drawing options.
        /// </param>
        /// <returns>
        /// </returns>
        public Menu Menu(bool[] drawingOptions)
        {
            var menu=new Menu(nameof(Drawing), Names.Menu.DrawingNameBase);

            var enemyMenu=new Menu("Enemys", Names.Menu.DrawingItemBase+"Enemy");
            enemyMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase+Objects.Player.ChampionName+".Boolean.DrawOnEnemy.ComboDamage", "Combo Damage").SetValue(new Circle(true, Color.DarkGray))).SetTooltip("Draw Champion Combo Damage",
                                                                                                                                                                                                             SharpDX.Color.Aqua);
            enemyMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase+Objects.Player.ChampionName+".Boolean.DrawOnEnemy.DrawRange", "Enemy AA").SetValue(new Circle(true, Color.Red))).SetTooltip("Draw Enemies AA Range", SharpDX.Color.Aqua);

            var selfMenu=new Menu("Self", Names.Menu.DrawingItemBase+"Self");
            //selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase + Data.Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf", "Draw On Self").SetValue(true));

            if (drawingOptions[0])
                selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase+Objects.Player.ChampionName+".Boolean.DrawOnSelf.QColor", "Q").SetValue(new Circle(true, Color.LightBlue))).SetTooltip("Draw Q Range", SharpDX.Color.Aqua);
            if (drawingOptions[1])
                selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase+Objects.Player.ChampionName+".Boolean.DrawOnSelf.WColor", "W").SetValue(new Circle(true, Color.LightGreen))).SetTooltip("Draw W Range", SharpDX.Color.Aqua);
            if (drawingOptions[2])
                selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase+Objects.Player.ChampionName+".Boolean.DrawOnSelf.EColor", "E").SetValue(new Circle(true, Color.LightCoral))).SetTooltip("Draw E Range", SharpDX.Color.Aqua);
            if (drawingOptions[03])
                selfMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase+Objects.Player.ChampionName+".Boolean.DrawOnSelf.RColor", "R").SetValue(new Circle(true, Color.LightSlateGray))).SetTooltip("Draw R Range", SharpDX.Color.Aqua);
            ;

            var lastHitMenu=new Menu("Minions", Names.Menu.DrawingItemBase+"Minions");

            lastHitMenu.AddItem(new MenuItem(Names.Menu.DrawingItemBase+".Minion."+"Circle.LastHitHelper", "LastHit").SetValue(new Circle(true, Color.LightGray))).SetTooltip("Draw Last Hit Circle", SharpDX.Color.Aqua);

            menu.AddSubMenu(lastHitMenu);
            menu.AddSubMenu(enemyMenu);
            menu.AddSubMenu(selfMenu);

            return menu;
        }

        #endregion Public Methods
    }

}