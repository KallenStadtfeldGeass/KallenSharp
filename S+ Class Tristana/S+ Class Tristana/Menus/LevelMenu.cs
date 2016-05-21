using LeagueSharp;
using LeagueSharp.Common;

namespace S__Class_Tristana.Menus
{
    internal class LevelMenu : Events.LevelEvents
    {

        private Menu _Menu
        {
            get
            {
                var menu = new Menu(MenuNameBase, "levelMenu");
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.AutoLevelUp", "Auto level-up abilities").SetValue(true));
                return menu;
            }
        }

        public void Load()
        {
            SMenu.AddSubMenu(_Menu);
            Game.OnUpdate += OnUpdate;
        }
    }
}