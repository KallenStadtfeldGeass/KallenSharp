using _Project_Geass.Globals;
using LeagueSharp.Common;
using System.Collections.Generic;

namespace _Project_Geass.Bootloaders.Base.Menus
{
    internal sealed class SettingsMenu
    {
        public SettingsMenu()
        {
            foreach (var champ in Constants.Names.ChampionBundled)
            {
                var temp = new Menu(champ, Constants.Names.Menu.BaseItem + champ);

                foreach (var element in GenerateSettingsList(Constants.Names.Menu.BaseItem + champ))
                {
                    Static.Objects.ProjectLogger.WriteLog(element.Name);
                    temp.AddItem(element);
                }

                Static.Objects.SettingsMenu.AddSubMenu(temp);
            }
        }

        private IEnumerable<MenuItem> GenerateSettingsList(string basename)
        {
            var items = new List<MenuItem>
            {
                new MenuItem($"{basename}.Enable", "Enable Champion").SetValue(true),
                new MenuItem($"{basename}.ManaMenu", "Mana Manager").SetValue(true),
                new MenuItem($"{basename}.ItemMenu", "Item Manager").SetValue(true),
                new MenuItem($"{basename}.OnLevelMenu", "OnLevel Manager").SetValue(true),
                new MenuItem($"{basename}.TrinketMenu", "Trinket Menu").SetValue(true),
                new MenuItem($"{basename}.LastHitHelperMenu", "LastHit Helper").SetValue(true),
            };

            return items;
        }
    }
}