using Geass_Tristana.Other;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;

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
           // Libaries.Logger.Write($"Orbwalker mode {CommonOrbwalker.ActiveMode}");
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
            if (CommonOrbwalker.ActiveMode == Orbwalking.OrbwalkingMode.LaneClear && SMenu.Item(MenuNameBase + "Clear.Boolean.FocusETarget").GetValue<bool>())
            {
                foreach (
                    var minon in
                        MinionManager.GetMinions(Champion.Player.Position, Champion.GetSpellQ.Range,
                            MinionTypes.All, MinionTeam.NotAlly).Where(charge => charge.HasBuff("TristanaECharge") && charge.IsValidTarget(Champion.GetSpellQ.Range)))
                {
                    Libaries.Logger.Write($"Orbwalker Force Target {minon.Name}");
                    CommonOrbwalker.ForceTarget(minon);
                    return;
                }
            }
            else if (CommonOrbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Mixed
               && SMenu.Item(MenuNameBase + "Mixed.Boolean.FocusETarget").GetValue<bool>() ||
               CommonOrbwalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo &&
               SMenu.Item(MenuNameBase + "Combo.Boolean.FocusETarget").GetValue<bool>())
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(target => target.HasBuff("TristanaECharge") && target.IsValidTarget(Champion.GetSpellQ.Range)))
                {
                    Libaries.Logger.Write($"Orbwalker Force Target {enemy.Name}");
                    CommonOrbwalker.ForceTarget(enemy);
                    return;
                }
            }

            OrbwalkModeHandler();
        }
    }
}