using _Project_Geass.Bootloaders.Core.Functions;
using _Project_Geass.Data;
using _Project_Geass.Drawing.Champions;
using _Project_Geass.Globals;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;

namespace _Project_Geass.Bootloaders.Champions
{
    internal class Tristana : Base.Champion
    {
        private readonly DamageIndicator _damageIndicator = new DamageIndicator(GetDamage, 1000, true);

        public Tristana()
        {
            Q = new Spell(SpellSlot.Q, 550);
            E = new Spell(SpellSlot.W, 625);
            R = new Spell(SpellSlot.R, 517);

            // ReSharper disable once UnusedVariable
            var temp = new Menus.Tristana();

            Game.OnUpdate += OnUpdate;
            Game.OnUpdate += AutoEvents;

            LeagueSharp.Drawing.OnDraw += OnDraw;
            LeagueSharp.Drawing.OnDraw += OnDrawEnemy;

            Orbwalker = new Orbwalking.Orbwalker(Static.Objects.ProjectMenu.SubMenu(".CommonOrbwalker"));
        }

        public void UpdateChampionRange(int level)
        {
            Q.Range = 550 + (9 * (level - 1));
            E.Range = 625 + (9 * (level - 1));
            R.Range = 517 + (9 * (level - 1));
        }

        private void OnUpdate(EventArgs args)
        {
            if (Humanizer.DelayHandler.CheckOrbwalker())
            {
                UpdateChampionRange(Static.Objects.Player.Level);
                switch (Orbwalker.ActiveMode)
                {
                    case Orbwalking.OrbwalkingMode.Combo:
                        {
                            Combo();
                            break;
                        }
                    case Orbwalking.OrbwalkingMode.Mixed:
                        {
                            Mixed();
                            break;
                        }
                    case Orbwalking.OrbwalkingMode.LaneClear:
                        {
                            Clear();
                            break;
                        }
                }
                Humanizer.DelayHandler.UseOrbwalker();
            }
        }

        private void AutoEvents(EventArgs args)
        {
            if (Humanizer.DelayHandler.CheckAutoEvents())
            {
                string basename = BaseName + "Auto.";

                if (Static.Objects.ProjectMenu.Item($"{basename}.UseR").GetValue<bool>())
                {
                    var enemies = Functions.Objects.Heroes.GetEnemies(R.Range);
                    foreach (var enemy in enemies.Where(e => e.IsValidTarget(R.Range)).OrderBy(hp => hp.Health))
                    {
                        if (
                            !Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}")
                                .GetValue<bool>())
                            continue;

                        if (GetDamage(enemy) < enemy.Health) continue;
                        R.Cast(enemy);
                        break;
                    }
                }
                Humanizer.DelayHandler.UseAutoEvent();
            }
        }

        private void Combo()
        {
            string basename = BaseName + "Combo.";

            var enemies = Functions.Objects.Heroes.GetEnemies(E.Range);

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseR").GetValue<bool>())
            {
                if (Mana.CheckComboR())
                {
                    foreach (var enemy in enemies.Where(e => e.IsValidTarget(R.Range)).OrderBy(hp => hp.Health))
                    {
                        if (!Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}").GetValue<bool>())
                            continue;

                        if (GetDamage(enemy) < enemy.Health) continue;
                        R.Cast(enemy);
                        break;
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseE").GetValue<bool>())
            {
                if (Mana.CheckComboE())
                {
                    foreach (var target in enemies.OrderBy(x => x.Health))
                    {
                        if (!Static.Objects.ProjectMenu.Item($"{basename}.UseE.On.{target.ChampionName}").GetValue<bool>())
                            continue;

                        if (target.HasBuffOfType(BuffType.Invulnerability) || target.HasBuffOfType(BuffType.SpellImmunity) ||
                            target.HasBuffOfType(BuffType.SpellShield)) continue;

                        E.Cast(target);
                        break;
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
            {
                var validTargets = enemies.Where(x => x.HasBuff("TristanaECharge"));
                Q.Cast();
                Orbwalker.ForceTarget(validTargets.FirstOrDefault());
            }
        }

        private void Mixed()
        {
            string basename = BaseName + "Mixed.";

            var enemies = Functions.Objects.Heroes.GetEnemies(E.Range);
            if (Static.Objects.ProjectMenu.Item($"{basename}.UseE").GetValue<bool>())
            {
                if (Mana.CheckMixedE())
                {
                    foreach (var target in enemies.OrderBy(x => x.Health))
                    {
                        if (!Static.Objects.ProjectMenu.Item($"{basename}.UseE.On.{target.ChampionName}").GetValue<bool>())
                            continue;

                        if (target.HasBuffOfType(BuffType.Invulnerability) || target.HasBuffOfType(BuffType.SpellImmunity) ||
                            target.HasBuffOfType(BuffType.SpellShield)) continue;

                        E.Cast(target);
                        break;
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
            {
                var validTargets = enemies.Where(x => x.HasBuff("TristanaECharge"));
                Q.Cast();
                Orbwalker.ForceTarget(validTargets.FirstOrDefault());
            }
        }

        private void Clear()
        {
            var basename = BaseName + "Clear.";

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseE").GetValue<bool>())
            {
                if (Mana.CheckClearE())
                {
                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseE.OnTurrets").GetValue<bool>())
                    {
                        var turrets =
                            ObjectManager.Get<Obj_AI_Turret>()
                                .OrderBy(dis => dis.ServerPosition.Distance(Static.Objects.Player.ServerPosition));
                        var validTurrets =
                            turrets.Where(turret => turret.IsEnemy)
                                .Where(turret => !turret.IsDead && turret.IsValidTarget(E.Range));
                        var objAiTurrets = validTurrets as Obj_AI_Turret[] ?? validTurrets.ToArray();
                        if (objAiTurrets.Any())
                        {
                            var target = objAiTurrets.FirstOrDefault();
                            E.Cast(target);

                            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                                if (Static.Objects.ProjectMenu.Item($"{basename}.OnTurrets").GetValue<bool>())
                                    Q.Cast();

                            Orbwalker.ForceTarget(target);
                            return;
                        }
                    }

                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseE.OnJungle").GetValue<bool>())
                    {
                        var monsters = MinionManager.GetMinions(E.Range, MinionTypes.All, MinionTeam.Neutral);

                        var validMonsters =
                            monsters.Where(
                                name =>
                                    !name.Name.ToLower().Contains("mini") && !name.SkinName.ToLower().Contains("mini") &&
                                    name.IsValidTarget(E.Range)).OrderBy(hp => hp.Health);

                        if (validMonsters.Any())
                        {
                            var target = validMonsters.FirstOrDefault();
                            E.Cast(target);

                            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                                if (Static.Objects.ProjectMenu.Item($"{basename}.OnJungle").GetValue<bool>())
                                    Q.Cast();

                            Orbwalker.ForceTarget(target);
                            return;
                        }
                    }

                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseE.OnMinons").GetValue<bool>())
                    {
                        var validMinons = MinionManager.GetMinions(Static.Objects.Player.Position, E.Range - 50, MinionTypes.All, MinionTeam.NotAlly);
                        if (validMinons.Count >= Static.Objects.ProjectMenu.Item($"{basename}.MinionsInRange").GetValue<Slider>().Value)
                        {
                            Obj_AI_Base target = null;
                            var bestInRange = 0;
                            foreach (
                                var minon in
                                    validMinons.Where(minon => minon.IsValidTarget(E.Range)))
                            {
                                var inRange = 1 + validMinons.Count(minon2 => minon.Distance(minon) < 125);
                                if (inRange <= bestInRange) continue;
                                bestInRange = inRange;
                                target = minon;
                            }
                            if (target != null && bestInRange >= Static.Objects.ProjectMenu.Item($"{basename}.MinionsInRange").GetValue<Slider>().Value)
                            {
                                E.Cast(target);
                                if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                                    if (Static.Objects.ProjectMenu.Item($"{basename}.OnMinons").GetValue<bool>())
                                        Q.Cast();

                                Orbwalker.ForceTarget(target);
                            }
                        }
                    }
                }
            }
        }

        private
            void OnDraw(EventArgs args)
        {
            if (Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf").GetValue<bool>())
            {
                if (E.Level > 0)
                    if (Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.EColor").GetValue<Circle>().Active)
                        Render.Circle.DrawCircle(Static.Objects.Player.Position, E.Range, Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.EColor").GetValue<Circle>().Color, 2);
                if (R.Level > 0)
                    if (Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.RColor").GetValue<Circle>().Active)
                        Render.Circle.DrawCircle(Static.Objects.Player.Position, R.Range, Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.RColor").GetValue<Circle>().Color, 2);
            }
        }

        public void OnDrawEnemy(EventArgs args)
        {
            if (!Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnEnemy").GetValue<bool>())
            {
                _damageIndicator.SetFillEnabled(false);
                _damageIndicator.SetKillableEnabled(false);
                return;
            }

            _damageIndicator.SetFillEnabled(Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Active);
            _damageIndicator.SetFill(Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Color);

            _damageIndicator.SetKillableEnabled(false);
        }

        private static float GetDamage(Obj_AI_Hero target)
        {
            var damage = 0f;

            if (R.IsReady())
                if (Static.Objects.Player.Distance(target) < R.Range)
                    damage += R.GetDamage(target);

            if (target.HasBuff("tristanaecharge"))
            {
                var count = target.GetBuffCount("tristanaecharge");
                if (!Static.Objects.Player.IsWindingUp)
                    if (Static.Objects.Player.Distance(target) < Static.Objects.Player.AttackRange) // target in auto range
                        count++;

                damage += (float)(E.GetDamage(target) * (count * 0.30)) + E.GetDamage(target);
            }

            if (E.IsReady())
                if (Static.Objects.Player.Distance(target) < E.Range)
                    damage += (float)(E.GetDamage(target) * 0.30) + E.GetDamage(target); // 1 auto charge

            return Functions.Calculations.Damage.CalcRealDamage(target, damage);
        }
    }
}