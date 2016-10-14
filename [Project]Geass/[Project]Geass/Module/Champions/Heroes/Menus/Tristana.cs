using LeagueSharp.Common;
using SharpDX;
using _Project_Geass.Data.Static;

namespace _Project_Geass.Module.Champions.Heroes.Menus
{

    internal class Tristana
    {
        #region Private Fields

        private readonly string _baseName=Names.ProjectName+Objects.Player.ChampionName+".";

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Tristana" /> class.
        /// </summary>
        public Tristana()
        {
            Objects.ProjectMenu.AddSubMenu(Drawing());
            Objects.ProjectMenu.AddSubMenu(Combo());
            Objects.ProjectMenu.AddSubMenu(Mixed());
            Objects.ProjectMenu.AddSubMenu(Clear());
            Objects.ProjectMenu.AddSubMenu(Auto());
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        ///     Automative events
        /// </summary>
        /// <returns>
        /// </returns>
        private Menu Auto()
        {
            var basename=_baseName+"Auto.";

            var mainMenu = new Menu(nameof(Auto), basename).SetFontStyle(System.Drawing.FontStyle.Bold, SharpDX.Color.LightSkyBlue);
            mainMenu.AddItem(new MenuItem($"{basename}.UseR", "R").SetValue(true)).SetTooltip($"Use R", Color.Aqua);

            var rMenu=new Menu("R Settings", basename+"RSettings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                rMenu.AddItem(new MenuItem($"{basename}.UseR.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use On{enemy.ChampionName}", Color.Aqua);

            mainMenu.AddSubMenu(rMenu);

            return mainMenu;
        }

        /// <summary>
        ///     On Clear
        /// </summary>
        /// <returns>
        /// </returns>
        private Menu Clear()
        {
            var basename=_baseName+"Clear.";
            var mainMenu=new Menu(nameof(Clear), basename).SetFontStyle(System.Drawing.FontStyle.Bold, SharpDX.Color.LightSkyBlue);

            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Q").SetValue(true)).SetTooltip($"Use Q", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.OnMinions", "On Minions").SetValue(true)).SetTooltip($"Use Q on minions", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.OnTurrets", "On Turrets").SetValue(true)).SetTooltip($"Use Q on turrets", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.OnJungle", "On Monsters").SetValue(true)).SetTooltip($"Use Q on monsters", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseE", "E").SetValue(true)).SetTooltip($"Use E", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseE.OnMinions", "On Minons").SetValue(true)).SetTooltip($"Use E on minions", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseE.OnTurrets", "On Turrets").SetValue(true)).SetTooltip($"Use E on turrets", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseE.OnJungle", "On Monsters").SetValue(true)).SetTooltip($"Use E on monsters", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.MinionsInRange", "Minons").SetValue(new Slider(4, 3, 10))).SetTooltip($"Min minions in range", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseE.Focus", "Focus E target").SetValue(true)).SetTooltip($"Focus target with E charge on them", Color.Aqua);

            return mainMenu;
        }

        /// <summary>
        ///     On Combo
        /// </summary>
        /// <returns>
        /// </returns>
        private Menu Combo()
        {
            var basename=_baseName+"Combo.";

            var mainMenu = new Menu(nameof(Combo), basename).SetFontStyle(System.Drawing.FontStyle.Bold, SharpDX.Color.LightSkyBlue);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Q").SetValue(true)).SetTooltip($"Use Q", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseE", "E").SetValue(true)).SetTooltip($"Use E", Color.Aqua);

            var eMenu=new Menu("E Settings", basename+"E Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                eMenu.AddItem(new MenuItem($"{basename}.UseE.On.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use E on {enemy.ChampionName}", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseR", "R Killable").SetValue(true)).SetTooltip($"Use R to Kill steal", Color.Aqua);

            var rMenu=new Menu("R Settings", basename+"R Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                rMenu.AddItem(new MenuItem($"{basename}.UseR.On.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use R Kill Steal on {enemy.ChampionName}", Color.Aqua);

            mainMenu.AddSubMenu(eMenu);
            mainMenu.AddSubMenu(rMenu);

            return mainMenu;
        }

        /// <summary>
        ///     Drawings this instance.
        /// </summary>
        /// <returns>
        /// </returns>
        private Menu Drawing()
        {
            var basename=_baseName+"Drawing.";

            var mainMenu = new Menu(nameof(basename), basename).SetFontStyle(System.Drawing.FontStyle.Bold, SharpDX.Color.LightSkyBlue);
            mainMenu.AddItem(new MenuItem($"{basename}.DrawEStacks", "E Stacks").SetValue(true)).SetTooltip($"Draw E Stacks", Color.Aqua);

            return mainMenu;
        }

        /// <summary>
        ///     On Mixed
        /// </summary>
        /// <returns>
        /// </returns>
        private Menu Mixed()
        {
            var basename=_baseName+"Mixed.";
            var mainMenu=new Menu(nameof(Mixed), basename).SetFontStyle(System.Drawing.FontStyle.Bold, SharpDX.Color.LightSkyBlue);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Q").SetValue(true)).SetTooltip($"Use Q", Color.Aqua);

            var eMenu=new Menu("E Settings", basename+"E Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                eMenu.AddItem(new MenuItem($"{basename}.UseE.On.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use E on {enemy.ChampionName}", Color.Aqua);

            mainMenu.AddSubMenu(eMenu);
            return mainMenu;
        }

        #endregion Private Methods
    }

}