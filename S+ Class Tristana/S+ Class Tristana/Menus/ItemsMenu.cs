using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Menus
{
    class ItemsMenu : Events.ItemEvents
    {
        public void Load()
        {
            SMenu.AddSubMenu(Menu());
            Orbwalking.AfterAttack += After_Attack;
            Orbwalking.BeforeAttack += Before_Attack;
            Game.OnUpdate += OnUpdate;
        }
        private Menu Menu()
        {
            var _Menu = new Menu(_MenuNameBase, "itemMenu");

            var OffensiveMenu = new Menu(_MenuOffensiveNameBase, "offensiveMenu");
            OffensiveMenu.AddItem(new MenuItem(_MenuOffensiveItemBase + "Boolean.Bork", "Use BotRK/Cutlass").SetValue(true));
            OffensiveMenu.AddItem(new MenuItem(_MenuOffensiveItemBase + "Boolean.Youmuu", "Use Youmuu's").SetValue(true));
            OffensiveMenu.AddItem(new MenuItem(_MenuOffensiveItemBase + "Slider.Bork.MinHp", "(BotRK/Cutlass) Min% HP Remaining(Target)").SetValue(new Slider(20)));
            OffensiveMenu.AddItem(new MenuItem(_MenuOffensiveItemBase + "Slider.Bork.MaxHp", "(BotRK/Cutlass) Max% HP Remaining(Target)").SetValue(new Slider(55)));
            OffensiveMenu.AddItem(new MenuItem(_MenuOffensiveItemBase + "Slider.Bork.MinHp.Player", "(BotRK/Cutlass) Min% HP Remaining(Player)").SetValue(new Slider(20)));
            OffensiveMenu.AddItem(new MenuItem(_MenuOffensiveItemBase + "Boolean.ComboOnly", "Only use offensive items in combo").SetValue(true));

            var DefensiveMenu = new Menu(_MenuDefensiveNameBase, "defensiveMenu");

            var qssMenu = new Menu(".QSS Menu", "qssMenu");

            qssMenu.AddItem(new MenuItem(_MenuDefensiveItemBase + "Boolean.QSS", "Use QSS").SetValue(true));
            qssMenu.AddItem(new MenuItem(_MenuDefensiveItemBase + "Slider.QSS.Delay", "QSS Delay").SetValue(new Slider(300, 250, 1500)));


            foreach (var buff in Bufftype)
            {
                qssMenu.AddItem(new MenuItem(_MenuDefensiveItemBase + "Boolean.QSS." + buff, "Use QSS On" + buff).SetValue(true));
            }

            var mercMenu = new Menu(".Merc Menu", "MercMenu");

            mercMenu.AddItem(new MenuItem(_MenuDefensiveItemBase + "Boolean.Merc", "Use Merc").SetValue(true));
            mercMenu.AddItem(new MenuItem(_MenuDefensiveItemBase + "Slider.Merc.Delay", "Merc Delay").SetValue(new Slider(300, 250, 1500)));


            foreach (var buff in Bufftype)
            {
                mercMenu.AddItem(new MenuItem(_MenuDefensiveItemBase + "Boolean.Merc." + buff, "Use Merc On" + buff).SetValue(true));
            }

            DefensiveMenu.AddSubMenu(qssMenu);
            DefensiveMenu.AddSubMenu(mercMenu);
            DefensiveMenu.AddItem(new MenuItem(_MenuDefensiveItemBase + "Boolean.ComboOnly", "Only use offensive items in combo").SetValue(true));

            _Menu.AddSubMenu(OffensiveMenu);
            _Menu.AddSubMenu(DefensiveMenu);
            return _Menu;
        }
    }
}
