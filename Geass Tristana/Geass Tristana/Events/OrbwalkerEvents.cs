﻿using Geass_Tristana.Other;
using LeagueSharp.Common;
using System;
using LeagueSharp;

namespace Geass_Tristana.Events
{
    internal partial class OrbwalkerEvents : Core
    {
        public const string MenuItemBase = ".Orbwalker.";
        public const string MenuNameBase = ".Orbwalker Menu";

        public const string ManaMenuItemBase = ".ManaManager.";
        public const string ManaMenuNameBase = ".ManaManager Menu";
        private readonly Other.Damage _damageLib = new Other.Damage();

        private void OrbwalkModeHandler()
        {
            switch (CommonOrbwalker.ActiveMode)
            {
                case Orbwalking.OrbwalkingMode.Combo:
                    Combo();
                    break;

                case Orbwalking.OrbwalkingMode.Mixed:
                    Mixed();
                    break;

                case Orbwalking.OrbwalkingMode.LaneClear:
                    LaneClear();
                    break;

                case Orbwalking.OrbwalkingMode.LastHit:
                    LastHit();
                    break;
            }
        }

        public void OnUpdate(EventArgs args)
        {
            if (!TickManager.CheckTick($"{MenuNameBase}.OrbwalkDelay")) return;
   
            TickManager.UseTick($"{MenuNameBase}.OrbwalkDelay");
            OrbwalkModeHandler();
        }
    }
}