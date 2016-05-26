using Geass_Tristana.Other;
using LeagueSharp.Common;
using System;

namespace Geass_Tristana.Events
{
    internal class TrinketEvents : Core
    {
        public const string MenuItemBase = ".Trinket.";
        public const string MenuNameBase = ".Trinket Menu";

        public void OnUpdate(EventArgs args)
        {
            if (!TickManager.CheckTick($"{HumanizeEvents.ItemBase}Slider.TrinketDelay")) return;

            TickManager.UseTick($"{HumanizeEvents.ItemBase}Slider.TrinketDelay");

            if (!SMenu.Item(MenuItemBase + "Boolean.BuyOrb").GetValue<bool>() || Champion.Player.Level < 9) return;
            if (!Champion.Player.InShop() || Items.HasItem(_itemsTrinkets.Orb.Id))
                return;

            _itemsTrinkets.Orb.Buy();
        }

        private readonly Data.Items.Trinkets _itemsTrinkets = new Data.Items.Trinkets();
    }
}