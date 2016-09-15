﻿using _Project_Geass.Data;
using _Project_Geass.Globals;
using _Project_Geass.Humanizer;
using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace _Project_Geass.Module.Core.Items.Events
{
    internal class Trinket
    {
        public Trinket()
        {
            Game.OnUpdate += OnUpdate;
        }

        private void OnUpdate(EventArgs args)
        {
            if (!DelayHandler.CheckTrinket()) return;

            if (!Static.Objects.ProjectMenu.Item(Names.Menu.TrinketItemBase + "Boolean.BuyOrb").GetValue<bool>()) return;
            DelayHandler.UseTrinket();

            if (Static.Objects.Player.Level < 9) return;
            if (!Static.Objects.Player.InShop() || LeagueSharp.Common.Items.HasItem(Data.Items.Trinkets.Orb.Id))
                return;

            Static.Objects.ProjectLogger.WriteLog("Buy Orb");
            Data.Items.Trinkets.Orb.Buy();
        }
    }
}