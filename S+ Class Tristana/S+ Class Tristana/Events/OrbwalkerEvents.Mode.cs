using LeagueSharp;
using LeagueSharp.Common;
using System.Linq;

namespace S__Class_Tristana.Events
{
    internal partial class OrbwalkerEvents
    {
        private void Combo()
        {
            if (!SMenu.Item(MenuNameBase + "Combo.Boolean.UseQ").GetValue<bool>() &&
                !SMenu.Item(MenuNameBase + "Combo.Boolean.UseE").GetValue<bool>() &&
                !SMenu.Item(MenuNameBase + "Combo.Boolean.UseR").GetValue<bool>()) return;

            if (SMenu.Item(MenuNameBase + "Combo.Boolean.UseE").GetValue<bool>() && Champion.GetSpellE.IsReady())
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().OrderBy(hp => hp.Health))
                {
                    if (enemy.IsDead) continue;
                    if (!enemy.IsEnemy) continue;
                    if (!SMenu.Item(MenuNameBase + "Combo.Boolean.UseE.On." + enemy.ChampionName).GetValue<bool>()) continue;
                    if (!enemy.IsValidTarget(Champion.GetSpellE.Range)) continue;

                    Champion.GetSpellE.Cast(enemy);
                    break;
                }
            }

            if (SMenu.Item(MenuNameBase + "Combo.Boolean.UseQ").GetValue<bool>() && Champion.GetSpellQ.IsReady())
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().OrderBy(hp => hp.Health))
                {
                    if (enemy.IsDead) continue;
                    if (!enemy.IsEnemy) continue;
                    if (!enemy.IsValidTarget(Champion.GetSpellQ.Range)) continue;

                    Champion.GetSpellQ.Cast();
                    break;
                }
            }

            if (SMenu.Item(MenuNameBase + "Combo.Boolean.FocusETarget").GetValue<bool>() && Champion.GetSpellQ.IsReady())
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().OrderBy(hp => hp.Health))
                {
                    if (enemy.IsDead) continue;
                    if (!enemy.IsEnemy) continue;
                    if (!enemy.IsValidTarget(Champion.GetSpellQ.Range)) continue;
                    if (!enemy.HasBuff("TristanaECharge")) continue;

                    CommonOrbwalker.ForceTarget(enemy);
                    break;
                }
            }

            if (SMenu.Item(MenuNameBase + "Combo.Boolean.UseR").GetValue<bool>() && Champion.GetSpellR.IsReady())
            {
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().OrderBy(hp => hp.Health))
                {
                    if (enemy.IsDead) continue;
                    if (!enemy.IsEnemy) continue;
                    if (!SMenu.Item(MenuNameBase + "Combo.Boolean.UseR.On." + enemy.ChampionName).GetValue<bool>()) continue;
                    if (!enemy.IsValidTarget(Champion.GetSpellR.Range)) continue;
                    if (_damageLib.CalculateDamage(enemy) < enemy.Health) continue;
                    Champion.GetSpellR.Cast(enemy);
                    break;
                }
            }
        }

        private Result JungleClear()
        {
            if (!SMenu.Item(MenuNameBase + "Clear.Boolean.UseQ.Monsters").GetValue<bool>() &&
                !SMenu.Item(MenuNameBase + "Clear.Boolean.UseE.Monsters").GetValue<bool>()) return Result.Invalid;

            var validMonsters = MinionManager.GetMinions(Champion.GetSpellQ.Range, MinionTypes.All, MinionTeam.Neutral);

            if (validMonsters.Count <= 0) return Result.Failure;

            foreach (var monster in validMonsters)
            {
                if (monster.Name.ToLower().Contains("mini") || monster.SkinName.ToLower().Contains("mini")) continue;
                if (!monster.IsValidTarget(Champion.GetSpellE.Range)) continue;

                if (SMenu.Item(MenuNameBase + "Clear.Boolean.UseE.Monsters").GetValue<bool>())
                {
                    Champion.GetSpellE.Cast(monster);
                    CommonOrbwalker.ForceTarget(monster);
                }
                if (SMenu.Item(MenuNameBase + "Clear.Boolean.UseQ.Monsters").GetValue<bool>())
                {
                    Champion.GetSpellQ.Cast();
                    CommonOrbwalker.ForceTarget(monster);
                }
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
                Obj_AI_Base target = null;
                var bestInRange = 0;
                foreach (var minon in validMinons)
                {
                    var inRange = 1;
                    if (!minon.IsValidTarget(Champion.GetSpellE.Range)) continue;
                    foreach (var minon2 in validMinons)
                    {
                        if (minon2.Distance(minon) < 125) inRange++;
                    }
                    if (inRange <= bestInRange) continue;
                    bestInRange = inRange;
                    target = minon;
                }
                if (target != null && bestInRange >= SMenu.Item(MenuNameBase + "Clear.Minons.Slider.MinMinons").GetValue<Slider>().Value)
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
                   // Game.PrintChat("In range");
                    if (!SMenu.Item(MenuNameBase + "Mixed.Boolean.UseE.On." + enemy.ChampionName).GetValue<bool>()) continue;
                   // Game.PrintChat("CAST Check");
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
                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().OrderBy(hp => hp.Health))
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