using _Project_Geass.Data;
using _Project_Geass.Humanizer;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using _Project_Geass.Data.Items;
using _Project_Geass.Functions;
using _Project_Geass.Global;
using _Project_Geass.Global.Data;

namespace _Project_Geass.Module.Core.Items.Events
{
    internal class Trinket
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Trinket"/> class.
        /// </summary>
        private readonly Trinkets _trinket;
        public Trinket()
        {
            _trinket = new Trinkets();
            Game.OnUpdate += OnUpdate;
        }

        /// <summary>
        /// Raises the <see cref="E:Update" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnUpdate(EventArgs args)
        {
            if (!DelayHandler.CheckTrinket()) return;

            if (!StaticObjects.ProjectMenu.Item(Names.Menu.TrinketItemBase + "Boolean.BuyOrb").GetValue<bool>()) return;
            DelayHandler.UseTrinket();

            if (StaticObjects.Player.Level < 9) return;
            if (!StaticObjects.Player.InShop() || LeagueSharp.Common.Items.HasItem(_trinket.Orb.Id))
                return;

            StaticObjects.ProjectLogger.WriteLog("Buy Orb");
            _trinket.Orb.Buy();
        }
    }
}