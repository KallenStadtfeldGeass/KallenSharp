using LeagueSharp.Common;
using S__Class_Tristana.Other;
using System;

namespace S__Class_Tristana.Events
{
    internal partial class OrbwalkerEvents : Core
    {
        public const string MenuItemBase = ".Orbwalker.";
        public const string MenuNameBase = ".Orbwalker Menu";

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

            TickManager.UseTick($"{MenuNameBase}.OrbwalkDelay");
        }

        public void OnUpdate(EventArgs args)
        {
            if (TickManager.CheckTick($"{MenuNameBase}.OrbwalkDelay")) return;
            OrbwalkModeHandler();
        }
    }
}