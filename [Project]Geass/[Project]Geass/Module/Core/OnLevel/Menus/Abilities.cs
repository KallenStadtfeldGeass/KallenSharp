using _Project_Geass.Data;
using LeagueSharp.Common;
using _Project_Geass.Functions;
using _Project_Geass.Global;
using _Project_Geass.Global.Data;

namespace _Project_Geass.Module.Core.OnLevel.Menus
{
    internal sealed class Abilities
    {
        private Menu Menu()
        {
            var menu = new Menu(Names.Menu.LevelNameBase, "levelMenu");
            menu.AddItem(new MenuItem(Names.Menu.LevelItemBase + "Boolean.AutoLevelUp", "Auto level-up abilities").SetValue(true));
            return menu;
        }

        public Abilities(Menu menu, int[] abiSeq,bool enabled)
        {
            if (!enabled) return;

            menu.AddSubMenu(Menu());

            // ReSharper disable once UnusedVariable
            var onLevel = new Events.Abilities(abiSeq);

            StaticObjects.ProjectLogger.WriteLog("OnLevel Menu and events loaded.");
        }
    }
}