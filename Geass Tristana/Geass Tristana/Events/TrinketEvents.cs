using Geass_Tristana.Other;
using LeagueSharp.Common;
using System;

namespace Geass_Tristana.Events
{
     class TrinketEvents : Core
    {
        public const string MenuItemBase = ".Trinket.";
        public const string MenuNameBase = ".Trinket Menu";

        public void OnUpdate(EventArgs args)
        {
            if (!TickManager.CheckTick($"{HumanizeEvents.ItemBase}Slider.TrinketDelay")) return;

            TickManager.UseTick($"{HumanizeEvents.ItemBase}Slider.TrinketDelay");

            if (!SMenu.Item(MenuItemBase + "Boolean.BuyOrb").GetValue<bool>()) return;
                if(Champion.Player.Level < 9) return;
            if (!Champion.Player.InShop() || Items.HasItem(GeassLib.Data.Items.Trinkets.Orb.Id))
                return;

           GeassLib.Data.Items.Trinkets.Orb.Buy();
        }
    }
}