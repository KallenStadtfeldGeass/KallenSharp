using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace Geass_Tristana.Menus
{
    internal class MinonsMenu : Drawing.Minons, Interface.IMenu
    {
        public Menu GetMenu()
        {
                var menu = new Menu(MenuNameBase, "minionMenu");
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnMinions", "Draw On Minions").SetValue(false));
                //   menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMinions.MarkerInnerColor", "Inner Marker Color").SetValue(new Circle(true, Color.DeepSkyBlue)));
                //  menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMinions.MakerOuterColor", "Outer Marker Color").SetValue(new Circle(true, Color.Red)));
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnMinions.MarkerKillableColor", "Killable Marker Color").SetValue(new Circle(true, Color.Green)));
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnMinions.Distance", "Render Distance").SetValue(new Slider(1000, 500, 2500)));
                return menu;
            
        }

        public void Load()
        {
            SMenu.AddSubMenu(GetMenu());
            var drawing = new Drawing.Minons();
            LeagueSharp.Drawing.OnDraw += drawing.OnMinionDraw;
        }
    }
}