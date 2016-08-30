using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders.Core.Menus
{
    internal sealed class OnLevelMenu
    {
        private Menu GetMenu()
        {
            var menu = new Menu(Names.Menu.LevelNameBase, "levelMenu");
            menu.AddItem(new MenuItem(Names.Menu.LevelItemBase + "Boolean.AutoLevelUp", "Auto level-up abilities").SetValue(true));
            return menu;
        }

        public OnLevelMenu(int[] abiSeq)
        {
            Static.Objects.ProjectMenu.AddSubMenu(GetMenu());

            // ReSharper disable once UnusedVariable
            var onLevel = new Events.OnUpdate.OnLevelEvents(abiSeq);

            Static.Objects.ProjectLogger.WriteLog("OnLevel Menu and events loaded.");
        }
    }
}