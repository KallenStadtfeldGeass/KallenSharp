using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;
using Prediction = _Project_Geass.Functions.Prediction;

namespace _Project_Geass.Module.Champions.Heroes.Menus
{
    internal class Ashe
    {
        public Ashe()
        {
            Static.Objects.ProjectMenu.AddSubMenu(Combo());
            Static.Objects.ProjectMenu.AddSubMenu(Mixed());
            Static.Objects.ProjectMenu.AddSubMenu(Clear());
            Static.Objects.ProjectMenu.AddSubMenu(Auto());
        }

        private readonly string _baseName = Names.ProjectName + Static.Objects.Player.ChampionName + ".";

        private Menu Auto()
        {
            var basename = _baseName + "Auto.";

            var mainMenu = new Menu(nameof(Auto), basename);
            mainMenu.AddItem(new MenuItem($"{basename}.UseW.OnGapClose", "Use W on gapclose").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseR.OnGapClose", "Use R on gapclose").SetValue(false));

            var wMenu = new Menu("GapClose W Settings", basename + "GapCloseW");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                wMenu.AddItem(new MenuItem($"{basename}.UseW.OnGapClose.{enemy.ChampionName}", $"On {enemy.ChampionName}").SetValue(true));
            }

            var rMenu = new Menu("GapClose R Settings", basename + "GapCloseR");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                rMenu.AddItem(new MenuItem($"{basename}.UseR.OnGapClose.{enemy.ChampionName}", $"On {enemy.ChampionName}").SetValue(true));
            }

            mainMenu.AddSubMenu(wMenu);
            mainMenu.AddSubMenu(rMenu);

            return mainMenu;
        }

        private Menu Combo()
        {
            var basename = _baseName + "Combo.";

            var mainMenu = new Menu(nameof(Combo), basename);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "Use W").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Prediction", "W Prediction").SetValue(
                    new StringList(Prediction.GetHitChanceNames())));

            var wMenu = new Menu("W Settings", basename + "W Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                wMenu.AddItem(new MenuItem($"{basename}.UseW.On.{enemy.ChampionName}", $"{enemy.ChampionName}.Enable").SetValue(true));
            }

            mainMenu.AddItem(new MenuItem($"{basename}.UseR", "Use R").SetValue(true));
            mainMenu.AddItem(
                new MenuItem($"{basename}.UseR.Prediction", "R Prediction").SetValue(
                    new StringList(Prediction.GetHitChanceNames())));
            mainMenu.AddItem(new MenuItem($"{basename}.UseR.Range", "R Range").SetValue(new Slider(1000, 500, 1750)));

            var rMenu = new Menu("R Settings", basename + "R Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                var temp = new Menu($"R Settings {enemy.ChampionName}", basename + $"RSettings.{enemy.ChampionName}");
                temp.AddItem(new MenuItem($"{basename}.UseR.On.{enemy.ChampionName}", $"Enable").SetValue(true));
                temp.AddItem(new MenuItem($"{basename}.UseR.On.{enemy.ChampionName}.HpMin", $"Min Hp%").SetValue(new Slider(15)));
                temp.AddItem(new MenuItem($"{basename}.UseR.On.{enemy.ChampionName}.HpMax", $"Max Hp%").SetValue(new Slider(60)));
                rMenu.AddSubMenu(temp);
            }
            mainMenu.AddSubMenu(wMenu);
            mainMenu.AddSubMenu(rMenu);

            return mainMenu;
        }

        private Menu Mixed()
        {
            var basename = _baseName + "Mixed.";
            var mainMenu = new Menu(nameof(Mixed), basename);
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "Use W").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Prediction", "W Prediction").SetValue(
                    new StringList(Prediction.GetHitChanceNames())));

            var wMenu = new Menu("W Settings", basename + "W Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                wMenu.AddItem(new MenuItem($"{basename}.UseW.On.{enemy.ChampionName}", $"{enemy.ChampionName}.Enable").SetValue(true));
            }

            mainMenu.AddSubMenu(wMenu);
            return mainMenu;
        }

        private Menu Clear()
        {
            var basename = _baseName + "Clear.";
            var mainMenu = new Menu(nameof(Clear), basename);

            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(false));
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.Minions", "Minons In AA Range").SetValue(new Slider(4, 3, 10)));
            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "Use W").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Minions", "Minons Hit").SetValue(new Slider(4, 3, 10)));

            return mainMenu;
        }
    }
}