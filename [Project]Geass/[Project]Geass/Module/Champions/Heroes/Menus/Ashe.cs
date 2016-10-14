using LeagueSharp.Common;
using SharpDX;
using _Project_Geass.Data.Static;
using Prediction = _Project_Geass.Functions.Prediction;

namespace _Project_Geass.Module.Champions.Heroes.Menus
{

    internal class Ashe
    {
        #region Private Fields

        private readonly string _baseName=Names.ProjectName+Objects.Player.ChampionName+".";

        #endregion Private Fields

        #region Public Constructors

        /// <summary>
        ///     Initializes a new instance of the <see cref="Ashe" /> class.
        /// </summary>
        public Ashe()
        {
            Objects.ProjectMenu.AddSubMenu(Combo());
            Objects.ProjectMenu.AddSubMenu(Mixed());
            Objects.ProjectMenu.AddSubMenu(Clear());
            Objects.ProjectMenu.AddSubMenu(Auto());
        }

        #endregion Public Constructors

        #region Private Methods

        /// <summary>
        ///     Automated events
        /// </summary>
        /// <returns>
        /// </returns>
        private Menu Auto()
        {
            var basename=_baseName+"Auto.";

            var mainMenu = new Menu(nameof(Auto), basename).SetFontStyle(System.Drawing.FontStyle.Bold, SharpDX.Color.LightSkyBlue);
            mainMenu.AddItem(new MenuItem($"{basename}.UseW.OnGapClose", "W").SetValue(true)).SetTooltip("Use W on gapclose", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseR.OnGapClose", "R").SetValue(false)).SetTooltip("Use R on gapclose", Color.Aqua);

            var wMenu=new Menu("GapClose W Settings", basename+"GapCloseW");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                wMenu.AddItem(new MenuItem($"{basename}.UseW.OnGapClose.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use on {enemy.ChampionName}", Color.Aqua);

            var rMenu=new Menu("GapClose R Settings", basename+"GapCloseR");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                rMenu.AddItem(new MenuItem($"{basename}.UseR.OnGapClose.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use on {enemy.ChampionName}", Color.Aqua);

            mainMenu.AddSubMenu(wMenu);
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

            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Q").SetValue(false)).SetTooltip($"Use Q", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.Minions", "Minions").SetValue(new Slider(4, 3, 10))).SetTooltip($"Minions in AA range", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "Use W").SetValue(true)).SetTooltip($"Use W", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Minions", "Minons Hit").SetValue(new Slider(4, 3, 10))).SetTooltip($"Min Minions Hit With W", Color.Aqua);

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

            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Prediction", "W Prediction").SetValue(new StringList(Prediction.GetHitChanceNames())));

            var wMenu=new Menu("W Settings", basename+"W Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                wMenu.AddItem(new MenuItem($"{basename}.UseW.On.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use on {enemy.ChampionName}", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseR", "R").SetValue(true)).SetTooltip($"Use R", Color.Aqua);

            mainMenu.AddItem(new MenuItem($"{basename}.UseR.Prediction", "R Prediction").SetValue(new StringList(Prediction.GetHitChanceNames()))).SetTooltip($"Min Hit Chance", Color.Aqua);
            mainMenu.AddItem(new MenuItem($"{basename}.UseR.Range", "R Range").SetValue(new Slider(1000, 500, 1750))).SetTooltip($"Max R range", Color.Aqua);

            var rMenu=new Menu("R Settings", basename+"R Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                var temp=new Menu($"R Settings {enemy.ChampionName}", basename+$"RSettings.{enemy.ChampionName}");
                temp.AddItem(new MenuItem($"{basename}.UseR.On.{enemy.ChampionName}", $"Enable").SetValue(true)).SetTooltip($"Use on {enemy.ChampionName}", Color.Aqua);
                temp.AddItem(new MenuItem($"{basename}.UseR.On.{enemy.ChampionName}.HpMin", $"Min Hp").SetValue(new Slider(15))).SetTooltip($"Min HP%", Color.Aqua);
                temp.AddItem(new MenuItem($"{basename}.UseR.On.{enemy.ChampionName}.HpMax", $"Max Hp").SetValue(new Slider(60))).SetTooltip($"Max HP%", Color.Aqua);
                rMenu.AddSubMenu(temp);
            }

            mainMenu.AddSubMenu(wMenu);
            mainMenu.AddSubMenu(rMenu);

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
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "Use W").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Prediction", "W Prediction").SetValue(new StringList(Prediction.GetHitChanceNames()))).SetTooltip($"Min HitChance", Color.Aqua);

            var wMenu=new Menu("W Settings", basename+"W Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
                wMenu.AddItem(new MenuItem($"{basename}.UseW.On.{enemy.ChampionName}", $"{enemy.ChampionName}").SetValue(true)).SetTooltip($"Use on {enemy.ChampionName}", Color.Aqua);

            mainMenu.AddSubMenu(wMenu);
            return mainMenu;
        }

        #endregion Private Methods
    }

}