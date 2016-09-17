using System.Collections.Generic;
using LeagueSharp.Common;

namespace _Project_Geass.Functions
{
    internal sealed class SettingsMenuGenerater
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsMenuGenerater"/> class.
        /// </summary>
        public SettingsMenuGenerater()
        {

            foreach (var champ in Names.ChampionBundled)
            {
                var temp = new Menu(champ, Names.Menu.BaseItem + champ);

                foreach (var element in GenerateSettingsList(Names.Menu.BaseItem + champ))
                {
                    temp.AddItem(element);
                }

                StaticObjects.SettingsMenu.AddSubMenu(temp);
            }
        }
        /// <summary>
        /// Generates the settings list.
        /// </summary>
        /// <param name="basename">The basename.</param>
        /// <returns></returns>
        private IEnumerable<MenuItem> GenerateSettingsList(string basename)
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