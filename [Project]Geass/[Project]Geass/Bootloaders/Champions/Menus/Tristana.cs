using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders.Champions.Menus
{
    internal class Tristana
    {
        public Tristana()
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

            var mainMenu = new Menu(basename, "Auto KS(R)");
            mainMenu.AddItem(new MenuItem($"{basename}.UseR", "Use R").SetValue(false));

            var rMenu = new Menu("R Settings", basename + "RSettings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                rMenu.AddItem(new MenuItem($"{basename}.UseR.{enemy.ChampionName}", $"On {enemy.ChampionName}").SetValue(true));
            }

            mainMenu.AddSubMenu(rMenu);

            return mainMenu;
        }

        private Menu Combo()
        {
            var basename = _baseName + "Combo.";

            var mainMenu = new Menu(basename, nameof(Combo));
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseE", "Use E").SetValue(true));

            var eMenu = new Menu("E Settings", basename + "E Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                eMenu.AddItem(new MenuItem($"{basename}.UseE.On.{enemy.ChampionName}", $"On {enemy.ChampionName}").SetValue(true));
            }

            mainMenu.AddItem(new MenuItem($"{basename}.UseR", "Use R (kill)").SetValue(true));

            var rMenu = new Menu("R Settings", basename + "R Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                rMenu.AddItem(new MenuItem($"{basename}.UseR.On.{enemy.ChampionName}", $"On {enemy.ChampionName}").SetValue(true));
            }

            mainMenu.AddSubMenu(eMenu);
            mainMenu.AddSubMenu(rMenu);

            return mainMenu;
        }

        private Menu Mixed()
        {
            var basename = _baseName + "Mixed.";
            var mainMenu = new Menu(basename, nameof(Mixed));
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(true));

            var eMenu = new Menu("E Settings", basename + "E Settings");

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies())
            {
                eMenu.AddItem(new MenuItem($"{basename}.UseE.On.{enemy.ChampionName}", $"On {enemy.ChampionName}").SetValue(true));
            }

            mainMenu.AddSubMenu(eMenu);
            return mainMenu;
        }

        private Menu Clear()
        {
            var basename = _baseName + "Clear.";
            var mainMenu = new Menu(basename, nameof(Clear));

            mainMenu.AddItem(new MenuItem($"{basename}.UseQ", "Use Q").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.OnMinions", "Use Q On Minons").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.OnTurrets", "Use Q On Turrets").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseQ.OnJungle", "Use Q On Monsters").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.UseE", "Use E").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseE.OnMinions", "Use E On Minons").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseE.OnTurrets", "Use E On Turrets").SetValue(true));
            mainMenu.AddItem(new MenuItem($"{basename}.UseE.OnJungle", "Use E On Monsters").SetValue(true));

            mainMenu.AddItem(new MenuItem($"{basename}.MinionsInRange", "Minons In Range").SetValue(new Slider(4, 3, 10)));
            mainMenu.AddItem(new MenuItem($"{basename}.UseE.Focus", "Focus E target").SetValue(true));

            return mainMenu;
        }
    }
}