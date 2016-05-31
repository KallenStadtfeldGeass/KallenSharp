using LeagueSharp;
using LeagueSharp.Common;

namespace Geass_Tristana.Menus
{
    internal class ItemsMenu : Events.ItemEvents, GeassLib.Interfaces.Core.Menu
    {
        public Menu GetMenu()
        {
                var menu = new Menu(MenuNameBase, "itemMenu");

                var offensiveMenu = new Menu(MenuOffensiveNameBase, "offensiveMenu");
                offensiveMenu.AddItem(new MenuItem(MenuOffensiveItemBase + "Boolean.Bork", "Use BotRK/Cutlass").SetValue(true));
                offensiveMenu.AddItem(new MenuItem(MenuOffensiveItemBase + "Boolean.Youmuu", "Use Youmuu's").SetValue(true));
                offensiveMenu.AddItem(new MenuItem(MenuOffensiveItemBase + "Slider.Bork.MinHp", "(BotRK/Cutlass) Min% HP Remaining(Target)").SetValue(new Slider(20)));
                offensiveMenu.AddItem(new MenuItem(MenuOffensiveItemBase + "Slider.Bork.MaxHp", "(BotRK/Cutlass) Max% HP Remaining(Target)").SetValue(new Slider(55)));
                offensiveMenu.AddItem(new MenuItem(MenuOffensiveItemBase + "Slider.Bork.MinHp.Player", "(BotRK/Cutlass) Min% HP Remaining(Player)").SetValue(new Slider(20)));
                offensiveMenu.AddItem(new MenuItem(MenuOffensiveItemBase + "Boolean.ComboOnly", "Only use offensive items in combo").SetValue(true));

                var defensiveMenu = new Menu(MenuDefensiveNameBase, "defensiveMenu");

                var qssMenu = new Menu(".QSS Menu", "qssMenu");

                qssMenu.AddItem(new MenuItem(MenuDefensiveItemBase + "Boolean.QSS", "Use QSS").SetValue(true));
                qssMenu.AddItem(new MenuItem(MenuDefensiveItemBase + "Slider.QSS.Delay", "QSS Delay").SetValue(new Slider(300, 250, 1500)));

                foreach (var buff in Bufftype)
                {
                    qssMenu.AddItem(new MenuItem(MenuDefensiveItemBase + "Boolean.QSS." + buff, "Use QSS On" + buff).SetValue(true));
                }

                var mercMenu = new Menu(".Merc Menu", "MercMenu");

                mercMenu.AddItem(new MenuItem(MenuDefensiveItemBase + "Boolean.Merc", "Use Merc").SetValue(true));
                mercMenu.AddItem(new MenuItem(MenuDefensiveItemBase + "Slider.Merc.Delay", "Merc Delay").SetValue(new Slider(300, 250, 1500)));

                foreach (var buff in Bufftype)
                {
                    mercMenu.AddItem(new MenuItem(MenuDefensiveItemBase + "Boolean.Merc." + buff, "Use Merc On" + buff).SetValue(true));
                }

                defensiveMenu.AddSubMenu(qssMenu);
                defensiveMenu.AddSubMenu(mercMenu);
                defensiveMenu.AddItem(new MenuItem(MenuDefensiveItemBase + "Boolean.ComboOnly", "Only use offensive items in combo").SetValue(true));

                menu.AddSubMenu(offensiveMenu);
                menu.AddSubMenu(defensiveMenu);
                return menu;
            
        }

        public void Load()
        {
            SMenu.AddSubMenu(GetMenu());
            Orbwalking.AfterAttack += After_Attack;
            Orbwalking.BeforeAttack += Before_Attack;
            Game.OnUpdate += OnUpdate;
        }
    }
}