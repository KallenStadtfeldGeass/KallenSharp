using _Project_Geass.Data;
using _Project_Geass.Drawing.Champions;
using _Project_Geass.Globals;
using _Project_Geass.Module.Champions.Core;
using _Project_Geass.Module.Core.Mana.Functions;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;

namespace _Project_Geass.Module.Champions.Heroes.Events
{
    internal class Tristana : Base
    {
        private readonly DamageIndicator _damageIndicator;
        private readonly Mana _manaManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Tristana"/> class.
        /// </summary>
        /// <param name="manaEnabled">if set to <c>true</c> [mana enabled].</param>
        /// <param name="orbwalker">The orbwalker.</param>
        public Tristana(bool manaEnabled,Orbwalking.Orbwalker orbwalker)
        {
            Q = new Spell(SpellSlot.Q, 550);
            E = new Spell(SpellSlot.E, 625);
            R = new Spell(SpellSlot.R, 517);

            _manaManager = new Mana(Q,W,E,R,manaEnabled);
            // ReSharper disable once UnusedVariable
            var temp = new Menus.Tristana();

            Game.OnUpdate += OnUpdate;
            Game.OnUpdate += AutoEvents;

            LeagueSharp.Drawing.OnDraw += OnDraw;
            LeagueSharp.Drawing.OnDraw += OnDrawEnemy;
            _damageIndicator = new DamageIndicator(GetDamage, 2000);
            Orbwalker = orbwalker;
        }

        /// <summary>
        /// Updates the champion ranges.
        /// </summary>
        /// <param name="level">The level.</param>
        protected virtual void UpdateChampionRange(int level)
        {
            Q.Range = 550 + (9 * (level - 1));
            E.Range = 625 + (9 * (level - 1));
            R.Range = 517 + (9 * (level - 1));
        }

        /// <summary>
        /// Raises the <see cref="E:Update" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnUpdate(EventArgs args)
        {
            if (!Humanizer.DelayHandler.CheckOrbwalker()) return;

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

        /// <summary>
        /// Automated events.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AutoEvents(EventArgs args)
        {
            if (!Humanizer.DelayHandler.CheckAutoEvents()) return;

            var basename = BaseName + "Auto.";

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseR").GetValue<bool>())
            {
                foreach (var enemy in Functions.Objects.Heroes.GetEnemies(R.Range).OrderBy(hp => hp.Health))
                {
                    if (GetChargeDamage(enemy) > enemy.Health)
                        continue;

                    if (Static.Objects.Player.GetAutoAttackDamage(enemy) > enemy.Health && !Static.Objects.Player.IsWindingUp)
                        continue;

                    if (!Static.Objects.ProjectMenu.Item($"{basename}.UseR.{enemy.ChampionName}").GetValue<bool>())
                        continue;

                    if (GetDamage(enemy) < enemy.Health) continue;
                    R.Cast(enemy);
                    break;
                }
            }
            Humanizer.DelayHandler.UseAutoEvent();
        }

        /// <summary>
        /// On Combo
        /// </summary>
        private void Combo()
        {
            var basename = BaseName + "Combo.";

            var enemies = Functions.Objects.Heroes.GetEnemies(E.Range).OrderBy(hp => hp.Health);

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseR").GetValue<bool>())
            {
                if (_manaManager.CheckComboR())
                {
                    foreach (var enemy in enemies.Where(e => e.IsValidTarget(R.Range)))
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
                if (_manaManager.CheckComboE())
                {
                    foreach (var target in enemies)
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
                foreach (var target in enemies.Where(x => x.HasBuff("TristanaECharge")))
                {
                    Q.Cast();
                    Orbwalker.ForceTarget(target);
                    break;
                }
            }
        }

        /// <summary>
        /// On Mixed
        /// </summary>
        private void Mixed()
        {
            var basename = BaseName + "Mixed.";

            var enemies = Functions.Objects.Heroes.GetEnemies(E.Range).OrderBy(x => x.Health);
            if (Static.Objects.ProjectMenu.Item($"{basename}.UseE").GetValue<bool>())
            {
                if (_manaManager.CheckMixedE())
                {
                    foreach (var target in enemies)
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
                foreach (var target in enemies.Where(x => x.HasBuff("TristanaECharge")))
                {
                    Q.Cast();
                    Orbwalker.ForceTarget(target);
                }
            }
        }

        /// <summary>
        /// On Clear
        /// </summary>
        private void Clear()
        {
            var basename = BaseName + "Clear.";

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseE").GetValue<bool>())
            {
                if (_manaManager.CheckClearE())
                {
                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseE.OnTurrets").GetValue<bool>())
                    {
                        var turrets = ObjectManager.Get<Obj_AI_Turret>().OrderBy(dis => dis.ServerPosition.Distance(Static.Objects.Player.ServerPosition));

                        foreach (var turret in turrets.Where(turret => turret.IsEnemy).Where(turret => !turret.IsDead && turret.IsValidTarget(E.Range)))
                        {
                            E.Cast(turret);

                            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                                if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ.OnTurrets").GetValue<bool>())
                                    Q.Cast();

                            Orbwalker.ForceTarget(turret);
                            return;
                        }
                    }

                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseE.OnJungle").GetValue<bool>())
                    {
                        var bigmonsters = MinionManager.GetMinions(E.Range, MinionTypes.All, MinionTeam.Neutral).Where(name => !name.Name.ToLower().Contains("mini") && !name.SkinName.ToLower().Contains("mini")).OrderBy(hp => hp.Health);

                        foreach (var monster in bigmonsters.Where(target => target.IsValidTarget(E.Range)))
                        {
                            E.Cast(monster);

                            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                                if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ.OnJungle").GetValue<bool>())
                                    Q.Cast();

                            Orbwalker.ForceTarget(monster);
                            return;
                        }
                    }

                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseE.OnMinions").GetValue<bool>())
                    {
                        var validMinons = MinionManager.GetMinions(Static.Objects.Player.Position, E.Range - 50, MinionTypes.All, MinionTeam.NotAlly);
                        if (validMinons.Count >= Static.Objects.ProjectMenu.Item($"{basename}.MinionsInRange").GetValue<Slider>().Value)
                        {
                            Obj_AI_Base target = null;
                            var bestInRange = 0;
                            foreach (var minon in validMinons.Where(minon => minon.IsValidTarget(E.Range)))
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
                                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ.OnMinions").GetValue<bool>())
                                        Q.Cast();

                                Orbwalker.ForceTarget(target);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Raises the <see cref="E:Draw" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnDraw(EventArgs args)
        {
            var basename = BaseName + "Drawing.";

            if (Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf").GetValue<bool>())
            {
                if (E.Level > 0)
                    if (
                        Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                        ".Boolean.DrawOnSelf.EColor").GetValue<Circle>().Active)
                        Render.Circle.DrawCircle(Static.Objects.Player.Position, E.Range,
                            Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase +
                                                            Static.Objects.Player.ChampionName +
                                                            ".Boolean.DrawOnSelf.EColor").GetValue<Circle>().Color, 2);
                if (R.Level > 0)
                    if (
                        Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                        ".Boolean.DrawOnSelf.RColor").GetValue<Circle>().Active)
                        Render.Circle.DrawCircle(Static.Objects.Player.Position, R.Range,
                            Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase +
                                                            Static.Objects.Player.ChampionName +
                                                            ".Boolean.DrawOnSelf.RColor").GetValue<Circle>().Color, 2);
            }
            var heroPosition = LeagueSharp.Drawing.WorldToScreen(Static.Objects.Player.Position);

            if (Static.Objects.ProjectMenu.Item($"{basename}.DrawEStacks").GetValue<bool>())
            {
                var display = Functions.Objects.Heroes.GetEnemies(1000).Where(x => x.HasBuff("TristanaECharge")).Aggregate("Stacks On:", (current, target) => current + $"{target.ChampionName}:{target.GetBuffCount("tristanaecharge")}");
                LeagueSharp.Drawing.DrawText(heroPosition.X + 20, heroPosition.Y - 30, System.Drawing.Color.MintCream, display);
            }
        }

        /// <summary>
        /// Raises the <see cref="E:DrawEnemy" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
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


        /// <summary>
        /// Get the E damage.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        public float GetChargeDamage(Obj_AI_Hero target)
        {
            if (target.HasBuff("tristanaecharge"))
            {
                var count = target.GetBuffCount("tristanaecharge");
                if (Static.Objects.Player.IsWindingUp)
                    return (float)(E.GetDamage(target) * (count * 0.30)) + E.GetDamage(target);

                if (Static.Objects.Player.Distance(target) < Static.Objects.Player.AttackRange) // target in auto range
                    count++;

                return (float)(E.GetDamage(target) * (count * 0.30)) + E.GetDamage(target);
            }
            if (!E.IsReady()) return 0f;

            if (Static.Objects.Player.Distance(target) < E.Range)
                return (float)(E.GetDamage(target) * 0.30) + E.GetDamage(target); // 1 auto charge

            return 0f;
        }

        /// <summary>
        /// Gets estimated damage
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        private float GetDamage(Obj_AI_Hero target)
        {
            var damage = 0f;
            if (target.Distance(Static.Objects.Player) < Static.Objects.Player.AttackRange - 25 &&
                Static.Objects.Player.CanAttack && !Static.Objects.Player.IsWindingUp)
                damage += (float)Static.Objects.Player.GetAutoAttackDamage(target) - 10;

            if (R.IsReady())
                damage += R.GetDamage(target);

            damage += GetChargeDamage(target);

            return Functions.Calculations.Damage.CalcRealDamage(target, damage);
        }
    }
}