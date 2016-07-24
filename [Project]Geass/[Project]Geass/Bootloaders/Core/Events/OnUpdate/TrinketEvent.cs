using System;
using LeagueSharp;
using LeagueSharp.Common;
using _Project_Geass.Constants;
using _Project_Geass.Globals;
using _Project_Geass.Humanizer;

namespace _Project_Geass.Bootloaders.Core.Events.OnUpdate
{
    class TrinketEvent
    {
        public TrinketEvent()
        {
            if (!DelayHandler.Loaded) DelayHandler.Load();
            Game.OnUpdate += OnUpdate;
        }
        void OnUpdate(EventArgs args)
        {
            if (DelayHandler.CheckTrinket())
            {
                if (!Static.Objects.ProjectMenu.Item(Names.Menu.TrinketItemBase + "Boolean.BuyOrb").GetValue<bool>()) return;
                DelayHandler.UseTrinket();

                if (Static.Objects.Player.Level < 9) return;
                if (!Static.Objects.Player.InShop() || Items.HasItem(Data.Items.Trinkets.Orb.Id))
                    return;

                Static.Objects.ProjectLogger.WriteLog("Buy Orb");
                Data.Items.Trinkets.Orb.Buy();
            }
        }

    }
}
