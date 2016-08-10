using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders.Champions.Menus
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
            string basename = _baseName + "Auto.";

            var mainMenu = new Menu(basename, "Auto");
            mainMenu.AddItem(new MenuItem($"{basename}.UseW.OnGapClose", "Use W on gapclose").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseR.OnGapClose", "Use R on gapclose").SetValue(false));

            var wMenu = new Menu("GapClose W Settings", basename + "GapCloseW");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                wMenu.AddItem(new MenuItem($"{basename}.UseW.OnGapClose.{enemy}", $"On {enemy}").SetValue(true));
            }

            var rMenu = new Menu("GapClose R Settings", basename + "GapCloseR");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                rMenu.AddItem(new MenuItem($"{basename}.UseR.OnGapClose.{enemy}", $"On {enemy}").SetValue(true));
            }

            mainMenu.AddSubMenu(wMenu);
            mainMenu.AddSubMenu(rMenu);

            return mainMenu;
        }

        private Menu Combo()
        {
            string basename = _baseName + "Combo.";

            var mainMenu = new Menu(basename, "Combo");
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "Use W").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Prediction", "W Prediction").SetValue(
                    new StringList(Core.Functions.Prediction.GetHitChanceNames())));

            mainMenu.AddItem(new MenuItem($"{basename}.UseR", "Use R").SetValue(true));
            mainMenu.AddItem(
                new MenuItem($"{basename}.UseR.Prediction", "R Prediction").SetValue(
                    new StringList(Core.Functions.Prediction.GetHitChanceNames())));
            mainMenu.AddItem(new MenuItem($"{basename}.UseR.Range", "R Range").SetValue(new Slider(1000, 500, 1750)));

            var rMenu = new Menu("R Settings", basename + "R Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                rMenu.AddItem(new MenuItem($"{basename}.UseR.On.{enemy}", $"On {enemy}").SetValue(true));
            }

            mainMenu.AddSubMenu(rMenu);

            return mainMenu;
        }

        private Menu Mixed()
        {
            string basename = _baseName + "Mixed.";
            var mainMenu = new Menu(basename, "Mixed");
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "Use W").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Prediction", "W Prediction").SetValue(
                    new StringList(Core.Functions.Prediction.GetHitChanceNames())));

            return mainMenu;
        }

        private Menu Clear()
        {
            string basename = _baseName + "Clear.";
            var mainMenu = new Menu(basename, "Clear");

            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "Use W").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Minions", "Minons Hit").SetValue(new Slider(4, 3, 10)));

            return mainMenu;
        }
    }
}