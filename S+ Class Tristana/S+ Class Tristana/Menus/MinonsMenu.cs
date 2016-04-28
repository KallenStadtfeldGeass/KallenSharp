using LeagueSharp.Common;
using Color = System.Drawing.Color;
namespace S__Class_Tristana.Menus
{
    class MinonsMenu : Drawing.Minons
    {
        public void Load()
        {
            SMenu.AddSubMenu(_Menu());
            Drawing.Minons Drawing = new S__Class_Tristana.Drawing.Minons();
            LeagueSharp.Drawing.OnDraw += Drawing.OnMinionDraw;
        }
        private Menu _Menu()
        {
            var menu = new Menu(_MenuNameBase, "minionMenu");
            menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMinions", "Draw On Minions").SetValue(false));
         //   menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMinions.MarkerInnerColor", "Inner Marker Color").SetValue(new Circle(true, Color.DeepSkyBlue)));
          //  menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMinions.MakerOuterColor", "Outer Marker Color").SetValue(new Circle(true, Color.Red)));
            menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMinions.MarkerKillableColor", "Killable Marker Color").SetValue(new Circle(true, Color.Green)));
            menu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnMinions.Distance", "Render Distance").SetValue(new Slider(1000, 500, 2500)));
            return menu;
        }

    }
}
