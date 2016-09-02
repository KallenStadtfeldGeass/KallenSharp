using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;
using System.Collections.Generic;

namespace _Project_Geass.Module.PreLoad.Menus
{
    internal sealed class PreLoadMenu
    {
        public PreLoadMenu()
        {
            var core = new Menu("Bootloaders.Core", Names.Menu.BaseItem + "Bootloaders.Core");
            core.AddItem(
                new MenuItem($"{Names.Menu.BaseItem}.Humanizer", "Enable Minimalist Humanizer(Reload Required)")
                    .SetValue(true));
            Static.Objects.SettingsMenu.AddSubMenu(core);

            foreach (var champ in Names.ChampionBundled)
            {
                var temp = new Menu(champ, Names.Menu.BaseItem + champ);

                foreach (var element in GenerateSettingsList(Names.Menu.BaseItem + champ))
                {
                    temp.AddItem(element);
                }

                Static.Objects.SettingsMenu.AddSubMenu(temp);
            }
        }

        private static IEnumerable<MenuItem> GenerateSettingsList(string basename)
        {
            var items = new List<MenuItem>
            {
                new MenuItem($"{basename}.Enable", "Enable Champion").SetValue(true),
                new MenuItem($"{basename}.ManaMenu", "Mana Menu").SetValue(true),
                new MenuItem($"{basename}.ItemMenu", "Item Menu").SetValue(true),
                new MenuItem($"{basename}.OnLevelMenu", "OnLevel Menu").SetValue(true),
                new MenuItem($"{basename}.TrinketMenu", "Trinket Menu").SetValue(true),
                new MenuItem($"{basename}.DrawingMenu", "Drawing Menu").SetValue(true)
            };

            return items;
        }
    }
}