using Geass_Tristana.Events;
using LeagueSharp;
using LeagueSharp.Common;

namespace Geass_Tristana.Menus
{
    internal class AntiMenu : AntiEvents, GeassLib.Interfaces.Core.Menu
    {
        public Menu GetMenu()
        {
                var menu = new Menu(MenuNameBase, "antiMenu");

                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.Interruption.Use", "Enable Interruption").SetValue(false));
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.AntiGapClose.Use", "Enable Anti-GapCloser").SetValue(false));

                var interruptionMenu = new Menu(".Interruption", "interruptionMenu");

                var enemies = GeassLib.Functions.Objects.Heroes.GetEnemies();

            foreach (var enemy in enemies)
                {
                    interruptionMenu.AddItem(new MenuItem(MenuItemBase + "Boolean.Interruption.Use.On." + enemy.ChampionName, "Use R Interruption On " + enemy.ChampionName).SetValue(true));
                }

                menu.AddSubMenu(interruptionMenu);

                var antigapclosemenu = new Menu(".Anti-GapClose", "Antigapclosemenu");

            foreach (var enemy in enemies)
                {
                    antigapclosemenu.AddItem(new MenuItem(MenuItemBase + "Boolean.AntiGapClose.Use.On." + enemy.ChampionName, "Use R Antigapclose On " + enemy.ChampionName).SetValue(true));
                }

                menu.AddSubMenu(antigapclosemenu);

                return menu;
        }

        public void Load()
        {
            SMenu.AddSubMenu(GetMenu());
            TickManager.AddTick($"{MenuNameBase}.AntiGapCloseDelay", 25, 50);
            TickManager.AddTick($"{MenuNameBase}.AutoInterrupterDelay", 25, 50);

            var antis = new AntiEvents();
            AntiGapcloser.OnEnemyGapcloser += antis.AntiGapClose;
            Interrupter2.OnInterruptableTarget += antis.AutoInterrupter;
        }
    }
}