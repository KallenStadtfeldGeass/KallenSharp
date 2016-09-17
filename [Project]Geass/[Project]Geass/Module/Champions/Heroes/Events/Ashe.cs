﻿using _Project_Geass.Drawing.Champions;
using _Project_Geass.Functions.Objects;
using _Project_Geass.Module.Champions.Core;
using _Project_Geass.Module.Core.Mana.Functions;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using _Project_Geass.Functions;
using _Project_Geass.Humanizer.TickTock;
using Damage = _Project_Geass.Functions.Calculations.Damage;
using Prediction = _Project_Geass.Functions.Prediction;

namespace _Project_Geass.Module.Champions.Heroes.Events
{
    internal class Ashe : Base
    {
        private readonly DamageIndicator _damageIndicator;
        private readonly Mana _manaManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="Ashe"/> class.
        /// </summary>
        /// <param name="manaEnabled">if set to <c>true</c> [mana enabled].</param>
        /// <param name="orbwalker">The orbwalker.</param>
        public Ashe(bool manaEnabled, Orbwalking.Orbwalker orbwalker)
        {
            Q = new Spell(SpellSlot.Q);
            W = new Spell(SpellSlot.W, 1200);
            R = new Spell(SpellSlot.R, 2200);

            W.SetSkillshot(.25f, 57.5f, 2000, true, SkillshotType.SkillshotCone);
            R.SetSkillshot(.25f, 250, 1600, false, SkillshotType.SkillshotLine);

            _manaManager = new Mana(Q, W, E, R, manaEnabled);
            // ReSharper disable once UnusedVariable
            var temp = new Menus.Ashe();

            Game.OnUpdate += OnUpdate;

            LeagueSharp.Drawing.OnDraw += OnDraw;
            LeagueSharp.Drawing.OnDraw += OnDrawEnemy;
            AntiGapcloser.OnEnemyGapcloser += OnGapcloser;

            _damageIndicator = new DamageIndicator(GetDamage, 2000);
            Orbwalker = orbwalker;
        }

        /// <summary>
        /// Raises the <see cref="E:Update" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnUpdate(EventArgs args)
        {
            if (!Handler.CheckOrbwalker()) return;

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
            Handler.UseOrbwalker();
        }

        /// <summary>
        /// On Combo
        /// </summary>
        private void Combo()
        {
            var basename = BaseName + "Combo.";

            if (StaticObjects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
            {
                if (_manaManager.CheckComboQ())
                {
                    if (Functions.Objects.Heroes.GetEnemies(StaticObjects.Player.AttackRange - 30).Count > 0)
                        Q.Cast();
                }
            }

            if (StaticObjects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
            {
                if (_manaManager.CheckComboW())
                {
                    var minHitChance =
                        Prediction.GetHitChance(
                            StaticObjects.ProjectMenu.Item($"{basename}.UseW.Prediction")
                                .GetValue<StringList>()
                                .SelectedValue);

                    //Check if the target in target selector is valid (best target)
                    var focusTarget = TargetSelector.GetTarget(W.Range, W.DamageType);
                    if (focusTarget != null)
                    {
                        var pred = LeagueSharp.Common.Prediction.GetPrediction(focusTarget, W.Delay, W.Speed);
                        if (StaticObjects.ProjectMenu.Item($"{basename}.UseW.On.{focusTarget.ChampionName}").GetValue<bool>() && pred.Hitchance >= minHitChance)
                            W.Cast(pred.UnitPosition);
                    }

                    if (W.IsReady())
                    {
                        var validChamp = new List<string>();

                        foreach (var enemy in Functions.Objects.Heroes.GetEnemies(W.Range))
                        {
                            if (!StaticObjects.ProjectMenu.Item($"{basename}.UseW.On.{enemy.ChampionName}").GetValue<bool>()) continue;
                            validChamp.Add(enemy.ChampionName);
                        }

                        var validOutputs = Targeting.GetTargetPredictions(W, minHitChance, validChamp);

                        foreach (var output in validOutputs)
                        {
                            W.Cast(output.UnitPosition);
                            break;
                        }
                    }
                }
            }

            if (StaticObjects.ProjectMenu.Item($"{basename}.UseR").GetValue<bool>())
            {
                if (_manaManager.CheckComboR())
                {
                    if (_manaManager.CheckComboR())
                    {
                        var minHitChance =
                            Prediction.GetHitChance(
                                StaticObjects.ProjectMenu.Item($"{basename}.UseR.Prediction")
                                    .GetValue<StringList>()
                                    .SelectedValue);

                        //Check if the target in target selector is valid (best target)
                        var focusTarget = TargetSelector.GetTarget(StaticObjects.ProjectMenu.Item($"{basename}.UseR.Range").GetValue<Slider>().Value, R.DamageType);
                        if (focusTarget != null)
                        {
                            var pred = LeagueSharp.Common.Prediction.GetPrediction(focusTarget, R.Delay, R.Speed);

                            if (StaticObjects.ProjectMenu.Item($"{basename}.UseR.On.{focusTarget.ChampionName}").GetValue<bool>() && pred.Hitchance >= minHitChance)
                            {
                                if (StaticObjects.ProjectMenu.Item($"{basename}.UseR.On.{focusTarget.ChampionName}.HpMin").GetValue<Slider>().Value > focusTarget.HealthPercent)
                                    if (StaticObjects.ProjectMenu.Item($"{basename}.UseR.On.{focusTarget.ChampionName}.HpMax").GetValue<Slider>().Value < focusTarget.HealthPercent)
                                        R.Cast(pred.UnitPosition);
                            }
                        }

                        if (R.IsReady())
                        {
                            var validChamp = new List<string>();

                            foreach (var enemy in Functions.Objects.Heroes.GetEnemies(R.Range))
                            {
                                if (!StaticObjects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}").GetValue<bool>()) continue;
                                if (StaticObjects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}.HpMin").GetValue<Slider>().Value > enemy.HealthPercent) continue;
                                if (StaticObjects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}.HpMax").GetValue<Slider>().Value < enemy.HealthPercent) continue;
                                validChamp.Add(enemy.ChampionName);
                            }

                            var validOutputs = Targeting.GetTargetPredictions(R, minHitChance, validChamp);

                            foreach (var output in validOutputs)
                            {
                                R.Cast(output.UnitPosition);
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// On Mixed
        /// </summary>
        private void Mixed()
        {
            var basename = BaseName + "Mixed.";

            if (StaticObjects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
            {
                if (_manaManager.CheckMixedW())
                {
                    var minHitChance =
                        Prediction.GetHitChance(
                            StaticObjects.ProjectMenu.Item($"{basename}.UseW.Prediction")
                                .GetValue<StringList>()
                                .SelectedValue);

                    //Check if the target in target selector is valid (best target)
                    var focusTarget = TargetSelector.GetTarget(W.Range, W.DamageType);
                    var found = false;
                    if (focusTarget != null)
                    {
                        var pred = LeagueSharp.Common.Prediction.GetPrediction(focusTarget, W.Delay, W.Speed);
                        if (
                            StaticObjects.ProjectMenu.Item($"{basename}.UseW.On.{focusTarget.ChampionName}")
                                .GetValue<bool>() && pred.Hitchance >= minHitChance)
                        {
                            W.Cast(pred.UnitPosition);
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        var validChamp = new List<string>();

                        foreach (var enemy in Functions.Objects.Heroes.GetEnemies(W.Range))
                        {
                            if (!StaticObjects.ProjectMenu.Item($"{basename}.UseW.On.{enemy.ChampionName}").GetValue<bool>()) continue;
                            validChamp.Add(enemy.ChampionName);
                        }

                        var validOutputs = Targeting.GetTargetPredictions(W, minHitChance, validChamp);

                        foreach (var output in validOutputs)
                        {
                            W.Cast(output.UnitPosition);
                            break;
                        }
                    }
                }
            }

            if (StaticObjects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                if (_manaManager.CheckMixedQ())
                    if (Functions.Objects.Heroes.GetEnemies(StaticObjects.Player.AttackRange - 50).Count >= 1)
                        Q.Cast();
        }

        /// <summary>
        /// On Clear
        /// </summary>
        private int minonsHit;
        private void Clear()
        {
            var basename = BaseName + "Clear.";

            var validMinions = Minions.GetEnemyMinions2(W.Range);

            if (StaticObjects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
                if (_manaManager.CheckClearW())
                {
                    var pos = W.GetLineFarmLocation(validMinions);
                    minonsHit = pos.MinionsHit;
                    if (pos.MinionsHit >=
                        StaticObjects.ProjectMenu.Item($"{basename}.UseW.Minions").GetValue<Slider>().Value)
                        W.Cast(pos.Position);
                }

            if (StaticObjects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())

                if (_manaManager.CheckClearQ())
                {
                    var aaMinons =
                        validMinions.Where(x => x.Distance(StaticObjects.Player) < StaticObjects.Player.AttackRange);

                    if (aaMinons.Count() >=
                        StaticObjects.ProjectMenu.Item($"{basename}.UseQ.Minions").GetValue<Slider>().Value)
                        Q.Cast();
                }
        }

        /// <summary>
        ///     Called when [gapcloser].
        /// </summary>
        /// <param name="gapcloser">The gapcloser.</param>
        private void OnGapcloser(ActiveGapcloser gapcloser)
        {
            var basename = BaseName + "Auto.";

            if (R.IsReady())
            {
                if (StaticObjects.ProjectMenu.Item($"{basename}.UseR.OnGapClose").GetValue<bool>())
                {
                    if (
                        StaticObjects.ProjectMenu.Item($"{basename}.UseR.OnGapClose.{gapcloser.Sender.ChampionName}")
                            .GetValue<bool>())
                    {
                        if (gapcloser.End.Distance(ObjectManager.Player.Position) < 300)
                        {
                            if (StaticObjects.ProjectMenu.Item($"{Names.Menu.BaseItem}.Humanizer").GetValue<bool>())
                            {
                                if (gapcloser.Sender.HasBuffOfType(BuffType.Invulnerability) ||
                                    gapcloser.Sender.HasBuffOfType(BuffType.SpellImmunity) ||
                                    gapcloser.Sender.HasBuffOfType(BuffType.SpellShield)) return;
                                Utility.DelayAction.Add(Math.Abs(Rng.Next() * (150 - 50 - Game.Ping) + 50 - Game.Ping),
                                    () => { R.Cast(gapcloser.End); });
                            }
                            else
                                R.Cast(gapcloser.End);
                        }
                    }
                }
            }

            if (W.IsReady())
            {
                if (StaticObjects.ProjectMenu.Item($"{basename}.UseW.OnGapClose").GetValue<bool>())
                {
                    if (
                        StaticObjects.ProjectMenu.Item($"{basename}.UseW.OnGapClose.{gapcloser.Sender.ChampionName}")
                            .GetValue<bool>())
                    {
                        if (gapcloser.End.Distance(ObjectManager.Player.Position) < 200)
                        {
                            if (StaticObjects.ProjectMenu.Item($"{Names.Menu.BaseItem}.Humanizer").GetValue<bool>())
                            {
                                if (gapcloser.Sender.HasBuffOfType(BuffType.Invulnerability) ||
                                    gapcloser.Sender.HasBuffOfType(BuffType.SpellImmunity) ||
                                    gapcloser.Sender.HasBuffOfType(BuffType.SpellShield)) return;
                                Utility.DelayAction.Add(
                                    Math.Abs(Rng.Next() * (200 - 100 - Game.Ping) + 100 - Game.Ping),
                                    () => { W.Cast(gapcloser.End); });
                            }
                            else
                                W.Cast(gapcloser.End);
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

            var heroPosition = LeagueSharp.Drawing.WorldToScreen(StaticObjects.Player.Position);
            LeagueSharp.Drawing.DrawText(heroPosition.X + 20, heroPosition.Y - 30, System.Drawing.Color.MintCream, minonsHit.ToString());


            if (W.Level > 0)
                if (
                    StaticObjects.ProjectMenu.Item(Names.Menu.DrawingItemBase + StaticObjects.Player.ChampionName +
                                                    ".Boolean.DrawOnSelf.WColor").GetValue<Circle>().Active)
                    Render.Circle.DrawCircle(StaticObjects.Player.Position, W.Range,
                        StaticObjects.ProjectMenu.Item(Names.Menu.DrawingItemBase +
                                                        StaticObjects.Player.ChampionName +
                                                        ".Boolean.DrawOnSelf.WColor").GetValue<Circle>().Color, 2);
            if (R.Level > 0)
                if (
                    StaticObjects.ProjectMenu.Item(Names.Menu.DrawingItemBase + StaticObjects.Player.ChampionName +
                                                    ".Boolean.DrawOnSelf.RColor").GetValue<Circle>().Active)
                    Render.Circle.DrawCircle(StaticObjects.Player.Position, R.Range,
                        StaticObjects.ProjectMenu.Item(Names.Menu.DrawingItemBase +
                                                        StaticObjects.Player.ChampionName +
                                                        ".Boolean.DrawOnSelf.RColor").GetValue<Circle>().Color, 2);
        }

        /// <summary>
        /// Raises the <see cref="E:DrawEnemy" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        public void OnDrawEnemy(EventArgs args)
        {
            if (!StaticObjects.ProjectMenu.Item(Names.Menu.DrawingItemBase + StaticObjects.Player.ChampionName + ".Boolean.DrawOnEnemy.ComboDamage").GetValue<Circle>().Active)
            {
                _damageIndicator.SetFillEnabled(false);
                _damageIndicator.SetKillableEnabled(false);
                return;
            }

            _damageIndicator.SetFillEnabled(true);
            _damageIndicator.SetFill(StaticObjects.ProjectMenu.Item(Names.Menu.DrawingItemBase + StaticObjects.Player.ChampionName + ".Boolean.DrawOnEnemy.ComboDamage").GetValue<Circle>().Color);

            _damageIndicator.SetKillableEnabled(false);
        }

        /// <summary>
        /// Returns estimated damage
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns></returns>
        private float GetDamage(Obj_AI_Hero target)
        {
            var damage = 0f;
            if (target.Distance(StaticObjects.Player) < StaticObjects.Player.AttackRange - 25 &&
                StaticObjects.Player.CanAttack && !StaticObjects.Player.IsWindingUp)
                damage += (float)StaticObjects.Player.GetAutoAttackDamage(target) - 10;

            if (W.IsReady())
                damage += W.GetDamage(target);

            if (R.IsReady())
                damage += R.GetDamage(target);

            return Damage.CalcRealDamage(target, damage);
        }
    }
}