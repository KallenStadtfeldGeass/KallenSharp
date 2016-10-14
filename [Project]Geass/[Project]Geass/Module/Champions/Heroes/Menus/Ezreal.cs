using LeagueSharp.Common;
using SharpDX;
using _Project_Geass.Data.Static;
using Prediction = _Project_Geass.Functions.Prediction;

namespace _Project_Geass.Module.Champions.Heroes.Menus
{

    internal class Ezreal
    {
        #region Private Fields

        private readonly string _baseName=Names.ProjectName+Objects.Player.ChampionName+".";

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Ezreal" /> class.
        /// </summary>
        public Ezreal()
        {
            Objects.ProjectMenu.AddSubMenu(Misc());
            Objects.ProjectMenu.AddSubMenu(Combo());
            Objects.ProjectMenu.AddSubMenu(Mixed());
            Objects.ProjectMenu.AddSubMenu(Clear());
        }

        #endregion Public Constructors

        #region Private Methods

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
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.Minon.LastHit", "On Minions").SetValue(true)).SetTooltip($"Use Q on minions", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.OnJungle", "On Mosters").SetValue(true)).SetTooltip($"Use Q on monsters", Color.Aqua);

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

            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "W").SetValue(true)).SetTooltip($"Use W", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseR", "R").SetValue(true)).SetTooltip($"Use R", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.Prediction", "Q Prediction").SetValue(new StringList(Prediction.GetHitChanceNames()))).SetTooltip($"Min Q HitChance", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Prediction", "W Prediction").SetValue(new StringList(Prediction.GetHitChanceNames()))).SetTooltip($"Min W HitChance", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseR.Prediction", "R Prediction").SetValue(new StringList(Prediction.GetHitChanceNames()))).SetTooltip($"Min R HitChance", Color.Aqua);

            var qMenu=new Menu("Q Settings", basename+"Q Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                qMenu.AddItem(new MenuItem($"{basename}.UseQ.On.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use Q on {enemy.ChampionName}", Color.Aqua);

            var wMenu=new Menu("W Settings", basename+"W Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                wMenu.AddItem(new MenuItem($"{basename}.UseW.On.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use W on {enemy.ChampionName}", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseR.Range", "R Range").SetValue(new Slider(1000, 500, 1750))).SetTooltip($"Max R range", Color.Aqua);

            var rMenu=new Menu("R KS Settings", basename+"R Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                rMenu.AddItem(new MenuItem($"{basename}.UseR.On.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use R on {enemy.ChampionName}", Color.Aqua);

            mainMenu.AddSubMenu(qMenu);
            mainMenu.AddSubMenu(wMenu);
            mainMenu.AddSubMenu(rMenu);

            return mainMenu;
        }

        /// <summary>
        ///     Miscs
        /// </summary>
        /// <returns>
        /// </returns>
        private Menu Misc()
        {
            var basename=_baseName+"Misc.";

            var mainMenu = new Menu(nameof(Misc), basename).SetFontStyle(System.Drawing.FontStyle.Bold, SharpDX.Color.LightSkyBlue);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.TearStack", "Stack Tear").SetValue(true)).SetTooltip($"Use Q to stack tear (no enemies in view)", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.TearStack.MinMana", "Min Mana%").SetValue(new Slider(70))).SetTooltip($"Min Man% to stack tear", Color.Aqua);
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

            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "W").SetValue(true)).SetTooltip($"Use W", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.Prediction", "Q Prediction").SetValue(new StringList(Prediction.GetHitChanceNames()))).SetTooltip($"Min Q HitChance", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Prediction", "W Prediction").SetValue(new StringList(Prediction.GetHitChanceNames()))).SetTooltip($"Min W HitChance", Color.Aqua);

            var qMenu=new Menu("Q Settings", basename+"Q Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                qMenu.AddItem(new MenuItem($"{basename}.UseQ.On.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use Q on {enemy.ChampionName}", Color.Aqua);

            var wMenu=new Menu("W Settings", basename+"W Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                wMenu.AddItem(new MenuItem($"{basename}.UseW.On.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use W on {enemy.ChampionName}", Color.Aqua);

            mainMenu.AddSubMenu(qMenu);
            mainMenu.AddSubMenu(wMenu);

            return mainMenu;
        }

        #endregion Private Methods
    }

}