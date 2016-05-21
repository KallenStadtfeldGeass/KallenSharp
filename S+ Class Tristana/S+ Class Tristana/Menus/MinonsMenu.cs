using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace S__Class_Tristana.Menus
{
    internal class MinonsMenu : Drawing.Minons
    {

        private Menu _Menu
        {
            get
            {
                var menu = new Menu(MenuNameBase, "minionMenu");
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnMinions", "Draw On Minions").SetValue(false));
                //   menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMinions.MarkerInnerColor", "Inner Marker Color").SetValue(new Circle(true, Color.DeepSkyBlue)));
                //  menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMinions.MakerOuterColor", "Outer Marker Color").SetValue(new Circle(true, Color.Red)));
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnMinions.MarkerKillableColor", "Killable Marker Color").SetValue(new Circle(true, Color.Green)));
                menu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnMinions.Distance", "Render Distance").SetValue(new Slider(1000, 500, 2500)));
                return menu;
            }
        }

        public void Load()
        {
            SMenu.AddSubMenu(_Menu);
            var drawing = new Drawing.Minons();
            LeagueSharp.Drawing.OnDraw += drawing.OnMinionDraw;
        }
    }
}