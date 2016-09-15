using _Project_Geass.Data;
using _Project_Geass.Module.Core.Drawing.Events;
using LeagueSharp.Common;

namespace _Project_Geass.Module.Core.Mana.Menus
{
    internal sealed class ManaMenu
    {
        public bool Enabled;

        private Menu Menu(int[,] options)
        {
            var menu = new Menu(Names.Menu.ManaNameBase, "ManaManager");
            menu.AddItem(new MenuItem(Names.Menu.ManaItemBase + "Use.ManaManager", "Use ManaManager").SetValue(true));

            for (var index = 0; index < Data.Champions.SettingsBase.ManaModes.Length; index++)
            {
                var subMenu = new Menu($"{Data.Champions.SettingsBase.ManaModes[index]}", $"{Data.Champions.SettingsBase.ManaModes[index]}ManaMenu");
                for (var i = 0; i < Data.Champions.SettingsBase.ManaAbilities.Length; i++)
                {
                    if (options[index, i] != -1)
                        subMenu.AddItem(new MenuItem($"{Names.Menu.ManaItemBase}{Data.Champions.SettingsBase.ManaModes[index]}.Slider.MinMana.{Data.Champions.SettingsBase.ManaAbilities[i]}", $"Min Mana% {Data.Champions.SettingsBase.ManaAbilities[i]}").SetValue(new Slider(options[index, i])));
                }
                menu.AddSubMenu(subMenu);
            }
            return menu;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManaMenu"/> class.
        /// </summary>
        /// <param name="menu">The menu.</param>
        /// <param name="options">The options.</param>
        /// <param name="enabled">if set to <c>true</c> [enabled].</param>
        public ManaMenu(Menu menu, int[,] options,bool enabled)
        {
            Enabled = enabled;
            if (!enabled) return;

            menu.AddSubMenu(Menu(options));
            // ReSharper disable once UnusedVariable
        }
    }
}