using Geass_Tristana.Other;
using LeagueSharp;
using LeagueSharp.Common;

namespace Geass_Tristana.Events
{
    internal class AntiEvents : Core
    {
        public const string MenuItemBase = ".Anti.";
        public const string MenuNameBase = ".Anti Menu";

        public void AntiGapClose(ActiveGapcloser user)
        {
            if (!TickManager.CheckTick($"{MenuNameBase}.AntiGapCloseDelay")) return;
            TickManager.UseTick($"{MenuNameBase}.AntiGapCloseDelay");
            //  Game.PrintChat($"{user.Sender.ChampionName}");

            if (!SMenu.Item(MenuItemBase + "Boolean.Interruption.Use.On." + user.Sender.ChampionName).GetValue<bool>()) return;
            if (!Champion.GetSpellR.IsReady()) return;

            if (user.Sender.Distance(Champion.Player) > Champion.GetSpellR.Range) return;

            Libaries.Logger.Write($"Interrupt Gap R on : {user.Sender}");
            Champion.GetSpellR.Cast(user.Sender);
        }

        public void AutoInterrupter(Obj_AI_Hero sender, Interrupter2.InterruptableTargetEventArgs args)
        {
            if (!TickManager.CheckTick($"{MenuNameBase}.AutoInterrupterDelay")) return;
            TickManager.UseTick($"{MenuNameBase}.AutoInterrupterDelay");

            // Game.PrintChat($"{sender.ChampionName}");

            if (!SMenu.Item(MenuItemBase + "Boolean.AntiGapClose.Use.On." + sender.ChampionName).GetValue<bool>()) return;

            if (!Champion.GetSpellR.IsReady()) return;

            if (sender.Distance(Champion.Player) > Champion.GetSpellR.Range) return;

            Libaries.Logger.Write($"Interrupt Cast R on : {sender}");
            Champion.GetSpellR.Cast(sender);
        }
    }
}