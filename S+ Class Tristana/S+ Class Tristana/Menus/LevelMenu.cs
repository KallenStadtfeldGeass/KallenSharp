using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Menus
{
    class LevelMenu : Events.LevelEvents
    {

        public void Load()
        {
            SMenu.AddSubMenu(_Menu());
            Game.OnUpdate += OnUpdate;
        }


        private Menu _Menu()
        {
            var menu = new Menu(_MenuNameBase, "levelMenu");
            menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.AutoLevelUp", "Auto level-up abilities").SetValue(true));
            return menu;
        }
    }
}
