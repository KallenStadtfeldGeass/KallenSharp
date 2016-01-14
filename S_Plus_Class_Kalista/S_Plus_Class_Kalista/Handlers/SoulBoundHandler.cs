using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace S_Plus_Class_Kalista.Handlers
{
    internal class SoulBoundHandler : Core
    {
        private const string _MenuNameBase = ".Soulbound Menu";
        private const string _MenuItemBase = ".Soulbound.";

        public static void Load()
        {
            SMenu.AddSubMenu(_Menu());

            Obj_AI_Base.OnProcessSpellCast += OnCast;
        }

        private static Menu _Menu()
        {
            var menu = new Menu(_MenuNameBase, "souldBoundMenu");
            menu.AddItem(new MenuItem(_MenuItemBase + "AutoSave.Boolean.AutoSavePercent", "Auto-Save SoulBound HP%").SetValue(true));
            menu.AddItem(new MenuItem(_MenuItemBase + "AutoSave.Slider.PercentHp","Soulboud HP%").SetValue(new Slider(10, 1, 90)));
            return menu;
        }

        private static void OnCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsEnemy) return;
            if (Player.InFountain()) return;
            if (!Champion.R.IsReady()) return;
            if (!ManaHandler.UseAutoR()) return;
            if (!SMenu.Item(_MenuItemBase + "AutoSave.Boolean.AutoSavePercent").GetValue<bool>()) return; // Dont save soulmate D:

            if (!Humanizer.Limiter.CheckDelay($"{Humanizer.DelayItemBase}Slider.SoulBoundDelay")) return;
            Humanizer.Limiter.UseTick($"{Humanizer.DelayItemBase}Slider.SoulBoundDelay");

            if (!GetValidSoulMate()) return; // Get SoulBound as you can change them and also check if there in R range   

            var healthPercent = SMenu.Item(_MenuItemBase + "AutoSave.Slider.PercentHp").GetValue<Slider>().Value;

            if (SoulBoundHero.CountEnemiesInRange(800) <= 0) return; // No enimies in range

            if (SoulBoundHero.HealthPercent <= healthPercent)
            {
                Champion.R.Cast();
            }
        }

        private static bool GetValidSoulMate()
        {
            foreach (var soulMate in ObjectManager.Get<Obj_AI_Hero>().Where(x => x.IsAlly && !x.IsMe && x.HasBuff("kalistacoopstrikeally") && x.Distance(ObjectManager.Player.Position) < Champion.R.Range && !x.InFountain() && !x.IsRecalling()))
            {
                SoulBoundHero = soulMate;
                return true;
            }
            return false;
        }
    }
}

