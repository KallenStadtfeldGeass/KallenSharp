using LeagueSharp.Common;
using _Project_Geass.Bootloaders.Core.Events.Drawing.Minions;
using _Project_Geass.Data;

namespace _Project_Geass.Bootloaders.Core.Menus
{
    class ManaMenu
    {
        private Menu GetMenu(int[,] options)
        {

            var menu = new Menu(Names.Menu.ManaNameBase, "ManaManager");
            menu.AddItem(new MenuItem(Names.Menu.ManaItemBase + "Use.ManaManager", "Use ManaManager").SetValue(true));

            for (var index = 0; index < Data.Champions.Base.ManaModes.Length; index++)
            {
                var subMenu = new Menu($"{Data.Champions.Base.ManaModes[index]}", $"{Data.Champions.Base.ManaModes[index]}ManaMenu");
                for (var i = 0; i < Data.Champions.Base.ManaAbilities.Length; i++)
                {
                    if (options[index, i] != -1)
                        subMenu.AddItem(new MenuItem($"{Names.Menu.ManaItemBase}{Data.Champions.Base.ManaModes[index]}.Slider.MinMana.{Data.Champions.Base.ManaAbilities[i]}", $"Min Mana% {Data.Champions.Base.ManaAbilities[i]}").SetValue(new Slider(options[index, i])));

                }
                menu.AddSubMenu(subMenu);
            }
            return menu;
        }

        public ManaMenu(int[,] options)
        {
            Globals.Static.Objects.ProjectMenu.AddSubMenu(GetMenu(options));
            // ReSharper disable once UnusedVariable
            var helper = new LastHitHelper();

            Globals.Static.Objects.ProjectLogger.WriteLog("LastHitHelper Menu and events loaded.");
        }
    }
}
