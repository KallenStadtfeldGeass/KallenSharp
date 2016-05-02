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

        private Data.Items.Trinkets Items_Trinkets = new Data.Items.Trinkets();

        public void OnUpdate(EventArgs args)
        {
            if (!_TickManager.CheckTick($"{Events.HumanizeEvents.ItemBase}Slider.TrinketDelay")) return;

            _TickManager.UseTick($"{Events.HumanizeEvents.ItemBase}Slider.TrinketDelay");

            if (!SMenu.Item(_MenuItemBase + "Boolean.BuyOrb").GetValue<bool>() || _Champion.Player.Level < 9) return;
            if (!_Champion.Player.InShop() || Items.HasItem(Items_Trinkets.Orb.Id))
                return;

            Items_Trinkets.Orb.Buy();
        }
    }
}
