using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders.Champions.Menus
{
    internal class Ezreal
    {
        public Ezreal()
        {
            Static.Objects.ProjectMenu.AddSubMenu(Misc());
            Static.Objects.ProjectMenu.AddSubMenu(Combo());
            Static.Objects.ProjectMenu.AddSubMenu(Mixed());
            Static.Objects.ProjectMenu.AddSubMenu(Clear());
        }

        private readonly string _baseName = Names.ProjectName + Static.Objects.Player.ChampionName + ".";

        private Menu Misc()
        {
            string basename = _baseName + "Misc.";

            var mainMenu = new Menu(basename, "Misc");
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.TearStack", "Use Q to tear stack (when no enemy in range)").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.TearStack.MinMana", "Min Mana%").SetValue(new Slider(70)));
            return mainMenu;
        }

        private Menu Combo()
        {
            string basename = _baseName + "Combo.";

            var mainMenu = new Menu(basename, "Combo");
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "Use W").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseR", "Use R").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.Prediction", "Q Prediction").SetValue(
                 new StringList(Core.Functions.Prediction.GetHitChanceNames())));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Prediction", "W Prediction").SetValue(
                   new StringList(Core.Functions.Prediction.GetHitChanceNames())));

            mainMenu.AddItem(
    new MenuItem($"{basename}.UseR.Prediction", "R Prediction").SetValue(
        new StringList(Core.Functions.Prediction.GetHitChanceNames())));

            var qMenu = new Menu("Q Settings", basename + "Q Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                qMenu.AddItem(new MenuItem($"{basename}.UseQ.On.{enemy.ChampionName}", $"{enemy.ChampionName}.Enable").SetValue(true));
            }

            var wMenu = new Menu("W Settings", basename + "W Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                wMenu.AddItem(new MenuItem($"{basename}.UseW.On.{enemy.ChampionName}", $"{enemy.ChampionName}.Enable").SetValue(true));
            }

            mainMenu.AddItem(new MenuItem($"{basename}.UseR.Range", "R Range").SetValue(new Slider(1000, 500, 1750)));

            var rMenu = new Menu("R KS Settings", basename + "R Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                rMenu.AddItem(new MenuItem($"{basename}.UseR.On.{enemy.ChampionName}", $"{enemy.ChampionName}.Enable").SetValue(true));
            }

            mainMenu.AddSubMenu(qMenu);
            mainMenu.AddSubMenu(wMenu);
            mainMenu.AddSubMenu(rMenu);

            return mainMenu;
        }

        private Menu Mixed()
        {
            string basename = _baseName + "Mixed.";
            var mainMenu = new Menu(basename, "Mixed");

            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW", "Use W").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.Prediction", "Q Prediction").SetValue(
                    new StringList(Core.Functions.Prediction.GetHitChanceNames())));

            mainMenu.AddItem(new MenuItem($"{basename}.UseW.Prediction", "W Prediction").SetValue(
                   new StringList(Core.Functions.Prediction.GetHitChanceNames())));

            var qMenu = new Menu("Q Settings", basename + "Q Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                qMenu.AddItem(new MenuItem($"{basename}.UseQ.On.{enemy.ChampionName}", $"{enemy.ChampionName}.Enable").SetValue(true));
            }

            var wMenu = new Menu("W Settings", basename + "W Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                wMenu.AddItem(new MenuItem($"{basename}.UseW.On.{enemy.ChampionName}", $"{enemy.ChampionName}.Enable").SetValue(true));
            }

            mainMenu.AddSubMenu(qMenu);
            mainMenu.AddSubMenu(wMenu);

            return mainMenu;
        }

        private Menu Clear()
        {
            string basename = _baseName + "Clear.";
            var mainMenu = new Menu(basename, "Clear");

            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q to last hit").SetValue(true));

            return mainMenu;
        }
    }
}