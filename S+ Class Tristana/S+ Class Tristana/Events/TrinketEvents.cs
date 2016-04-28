using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using ItemData = LeagueSharp.Common.Data.ItemData;

namespace S__Class_Tristana.Events
{
    class TrinketEvents : Core
    {
        public const string _MenuNameBase = ".Trinket Menu";
        public const string _MenuItemBase = ".Trinket.";

        public static void OnUpdate(EventArgs args)
        {
            if (!Humanizer.Delay.Limiter.CheckDelay($"{Humanizer.Delay.DelayItemBase}Slider.TrinketDelay")) return;

            Humanizer.Delay.Limiter.UseTick($"{Humanizer.Delay.DelayItemBase}Slider.TrinketDelay");
            if (!SMenu.Item(_MenuItemBase + "Boolean.BuyOrb").GetValue<bool>() || Player.Level < 9) return;
            if (!Player.InShop() || Items.HasItem(Items_Trinkets.Orb.Id))
                return;

            Items_Trinkets.Orb.Buy();
        }
    }
}
