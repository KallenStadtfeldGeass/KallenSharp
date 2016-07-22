using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders.Base.Menus
{
    internal class InitializerMenu
    {
        public InitializerMenu()
        {
            var nMenu = new Menu(Constants.Names.Menu.BaseName, Constants.Names.Menu.BaseItem);

            foreach (var champ in Constants.Names.ChampionBundled)
            {
                nMenu.AddItem(new MenuItem(Constants.Names.Menu.BaseName + champ, $"Use {champ}").SetValue(true));
            }

            Globals.Static.Objects.SettingsMenu.AddSubMenu(nMenu);
        }
    }
}