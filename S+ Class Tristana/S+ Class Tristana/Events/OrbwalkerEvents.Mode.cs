using LeagueSharp;
using LeagueSharp.Common;
using System.Linq;

namespace S__Class_Tristana.Events
{
    internal partial class OrbwalkerEvents
    {
        private void Combo()
        {
        }

        private Result JungleClear()
        {
            if (!SMenu.Item(MenuNameBase + "Clear.Boolean.UseQ.Monsters").GetValue<bool>() &&
                !SMenu.Item(MenuNameBase + "Clear.Boolean.UseE.Monsters").GetValue<bool>()) return Result.Invalid;

            var validMonsters = MinionManager.GetMinions(Champion.GetSpellQ.Range, MinionTypes.All, MinionTeam.Neutral);

            if (validMonsters.Count <= 0) return Result.Failure;

            var target = validMonsters.OrderBy(hp => hp.MaxHealth).First();
            if (target == null) return Result.Failure;

            if (SMenu.Item(MenuNameBase + "Clear.Boolean.UseE.Monsters").GetValue<bool>())
            {
                Champion.GetSpellE.Cast(target);
                CommonOrbwalker.ForceTarget(target);
            }

            if (SMenu.Item(MenuNameBase + "Clear.Boolean.UseQ.Monsters").GetValue<bool>())
            {
                Champion.GetSpellQ.Cast();
                CommonOrbwalker.ForceTarget(target);
            }
            return Result.Success;
        }

        private void LaneClear()
        {
            if (!Champion.GetSpellE.IsReady() && !Champion.GetSpellQ.IsReady()) return;

            if (TurretClear() == Result.Success) { }
            else if (JungleClear() == Result.Success) { }
            else LaneClearE();
        }

        // ReSharper disable once UnusedMethodReturnValue.Local
        private Result LaneClearE()
        {
            if (!SMenu.Item(MenuNameBase + "Clear.Boolean.UseE.Minons").GetValue<bool>()
                && !SMenu.Item(MenuNameBase + "Clear.Boolean.UseQ.Minons").GetValue<bool>()) return Result.Invalid;

            var validMinons = MinionManager.GetMinions(Champion.Player.Position, Champion.GetSpellQ.Range - 50, MinionTypes.All, MinionTeam.NotAlly);
            if (validMinons.Count < SMenu.Item(MenuNameBase + "Clear.Minons.Slider.MinMinons").GetValue<Slider>().Value) return Result.Failure;

            if (Champion.GetSpellE.IsReady() && SMenu.Item(MenuNameBase + "Clear.Boolean.UseE.Minons").GetValue<bool>())
            {
                var target = validMinons.First(hp => hp.Health > 100);

                if (target != null)
                {
                    Champion.GetSpellE.Cast(target);
                    CommonOrbwalker.ForceTarget(target);
                }
            }

            if (SMenu.Item(MenuNameBase + "Clear.Boolean.UseQ.Minons").GetValue<bool>() && Champion.GetSpellQ.IsReady())
            {
                Champion.GetSpellQ.Cast();

                var focusMinions = validMinons.Where(charge => charge.HasBuff("TristanaECharge"));
                var minon = focusMinions.First();
                if (minon != null)
                    CommonOrbwalker.ForceTarget(minon);
            }

            return Result.Success;
        }

        private void LastHit()
        {
        }

        private void Mixed()
        {
            if (SMenu.Item(MenuNameBase + "Mixed.Boolean.UseE").GetValue<bool>() && Champion.GetSpellE.IsReady())
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().OrderBy(hp => hp.HealthPercent))
                {
                    if (enemy.IsDead) continue;
                    if (!enemy.IsEnemy) continue;
                    if (!enemy.IsValidTarget(Champion.GetSpellE.Range - SMenu.Item(MenuNameBase + "Mixed.Slider.MaxDistance").GetValue<Slider>().Value)) continue;
                    if (!SMenu.Item(MenuItemBase + "Mixed.Boolean.UseE.On." + enemy.ChampionName).GetValue<bool>()) continue;

                    Champion.GetSpellE.Cast(enemy);
                    CommonOrbwalker.ForceTarget(enemy);

                    if (SMenu.Item(MenuNameBase + "Mixed.Boolean.UseQ").GetValue<bool>())
                    {
                        if (Champion.GetSpellQ.IsReady())
                            Champion.GetSpellQ.Cast();

                        return;
                    }
                }
            }
            else if (SMenu.Item(MenuNameBase + "Mixed.Boolean.UseQ").GetValue<bool>() && Champion.GetSpellQ.IsReady())
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().OrderBy(hp => hp.HealthPercent))
                {
                    if (enemy.IsDead) continue;
                    if (!enemy.IsEnemy) continue;
                    if (!enemy.IsValidTarget(Champion.GetSpellQ.Range - SMenu.Item(MenuNameBase + "Mixed.Slider.MaxDistance").GetValue<Slider>().Value)) continue;
                    Champion.GetSpellQ.Cast();
                    CommonOrbwalker.ForceTarget(enemy);
                }
            }
        }

        private Result TurretClear()
        {
            if (!SMenu.Item(MenuNameBase + "Clear.Boolean.UseQ.Turret").GetValue<bool>() &&
                !SMenu.Item(MenuNameBase + "Clear.Boolean.UseE.Turret").GetValue<bool>()) return Result.Invalid;

            var validTurets = ObjectManager.Get<Obj_AI_Turret>().OrderBy(dis => dis.ServerPosition.Distance(Champion.Player.ServerPosition));

            var target = validTurets.Where(turret => turret.IsEnemy).Where(turret => !turret.IsDead).FirstOrDefault(turret => turret.IsValidTarget(Champion.GetSpellQ.Range));
            if (target == null) return Result.Failure;

            if (SMenu.Item(MenuNameBase + "Clear.Boolean.UseE.Turret").GetValue<bool>())
            {
                Champion.GetSpellE.Cast(target);
                CommonOrbwalker.ForceTarget(target);
            }

            if (SMenu.Item(MenuNameBase + "Clear.Boolean.UseQ.Turret").GetValue<bool>())
            {
                Champion.GetSpellQ.Cast();
                CommonOrbwalker.ForceTarget(target);
            }

            return Result.Success;
        }
    }

    public enum Result
    {
        Success,
        Failure,
        Invalid
    }
}