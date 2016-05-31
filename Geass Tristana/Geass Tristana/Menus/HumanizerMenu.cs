using LeagueSharp.Common;

namespace Geass_Tristana.Menus
{
    internal class HumanizerMenu : Events.HumanizeEvents, GeassLib.Interfaces.Core.Menu
    {
        public Menu GetMenu()
        {
            var menu = new Menu(MenuNameBase, "humanMenu");

            var subMenuDelay = new Menu(MenuNameBase, "delayMenu");
            subMenuDelay.AddItem(
                new MenuItem(ItemBase + "Slider.LevelDelay", "Level Delay").SetValue(new Slider(500, 50, 1000)));
            subMenuDelay.AddItem(
                new MenuItem(ItemBase + "Slider.EventDelay", "Check Delay").SetValue(new Slider(100, 50, 200)));
            subMenuDelay.AddItem(
                new MenuItem(ItemBase + "Slider.ItemDelay", "Item Delay").SetValue(new Slider(200, 50, 1000)));
            subMenuDelay.AddItem(
                new MenuItem(ItemBase + "Slider.TrinketDelay", "Trinket Delay").SetValue(new Slider(500, 50, 2000)));
            subMenuDelay.AddItem(
                new MenuItem(ItemBase + "Slider.MinSeedDelay", "Minimum Random Delay").SetValue(new Slider(0, 0,
                    500)));
            subMenuDelay.AddItem(
                new MenuItem(ItemBase + "Slider.MaxSeedDelay", "Maximum Random Delay").SetValue(new Slider(50, 0,
                    500)));

            menu.AddSubMenu(subMenuDelay);

            return menu;
        }

        public void Load()
        {
            SMenu.AddSubMenu(GetMenu());
            LoadDelays();
        }
    }
}