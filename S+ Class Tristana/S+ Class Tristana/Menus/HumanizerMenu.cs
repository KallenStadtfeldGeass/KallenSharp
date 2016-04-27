using LeagueSharp.Common;


namespace S__Class_Tristana.Menus
{
    class HumanizerMenu : Humanizer.Delay
    {
        public void Load()
        {
            SMenu.AddSubMenu(_Menu());
        }

        private Menu _Menu()
        {
            var menu = new Menu(MenuNameBase, "humanMenu");

            var subMenuDelay = new Menu(DelayMenuNameBase, "delayMenu");
            subMenuDelay.AddItem(
                new MenuItem(DelayItemBase + "Slider.LevelDelay", "Level Delay").SetValue(new Slider(500, 50, 1000)));
            subMenuDelay.AddItem(
                new MenuItem(DelayItemBase + "Slider.EventDelay", "Check Delay").SetValue(new Slider(100, 50, 200)));
            subMenuDelay.AddItem(
                new MenuItem(DelayItemBase + "Slider.ItemDelay", "Item Delay").SetValue(new Slider(200, 50, 1000)));
            subMenuDelay.AddItem(
                new MenuItem(DelayItemBase + "Slider.TrinketDelay", "Trinket Delay").SetValue(new Slider(500, 50, 2000)));
            subMenuDelay.AddItem(
                new MenuItem(DelayItemBase + "Slider.MinSeedDelay", "Minimum Random Delay").SetValue(new Slider(0, 0,
                    500)));
            subMenuDelay.AddItem(
                new MenuItem(DelayItemBase + "Slider.MaxSeedDelay", "Maximum Random Delay").SetValue(new Slider(50, 0,
                    500)));

            menu.AddSubMenu(subMenuDelay);


            return menu;
        }
    }
}
