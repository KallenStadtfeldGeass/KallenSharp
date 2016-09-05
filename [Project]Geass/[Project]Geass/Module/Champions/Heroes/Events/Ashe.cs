﻿using _Project_Geass.Data;
using _Project_Geass.Drawing.Champions;
using _Project_Geass.Functions.Objects;
using _Project_Geass.Globals;
using _Project_Geass.Humanizer;
using _Project_Geass.Module.Champions.Core;
using _Project_Geass.Module.Core.Mana.Functions;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Damage = _Project_Geass.Functions.Calculations.Damage;
using Prediction = _Project_Geass.Functions.Prediction;

namespace _Project_Geass.Module.Champions.Heroes.Events
{
    internal class Ashe : Base
    {
        private readonly DamageIndicator _damageIndicator = new DamageIndicator(GetDamage, 2000);

        public Ashe()
        {
            Q = new Spell(SpellSlot.Q);
            W = new Spell(SpellSlot.W, 1200);
            R = new Spell(SpellSlot.R, 2200);

            Q.SetSkillshot(.25f, 57.5f, 2000, true, SkillshotType.SkillshotCone);
            R.SetSkillshot(.25f, 250, 1600, false, SkillshotType.SkillshotLine);

            // ReSharper disable once UnusedVariable
            var temp = new Menus.Ashe();

            Game.OnUpdate += OnUpdate;

            LeagueSharp.Drawing.OnDraw += OnDraw;
            LeagueSharp.Drawing.OnDraw += OnDrawEnemy;
            AntiGapcloser.OnEnemyGapcloser += OnGapcloser;

            Orbwalker = new Orbwalking.Orbwalker(Static.Objects.ProjectMenu.SubMenu(nameof(Orbwalker)));
        }

        private void OnUpdate(EventArgs args)
        {
            if (!DelayHandler.CheckOrbwalker()) return;

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
            DelayHandler.UseOrbwalker();
        }

        private void Combo()
        {
            var basename = BaseName + "Combo.";

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
            {
                if (Mana.CheckComboQ())
                {
                    if (Functions.Objects.Heroes.GetEnemies(Static.Objects.Player.AttackRange - 30).Count > 0)
                        Q.Cast();
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
            {
                if (Mana.CheckComboW())
                {
                    var minHitChance =
                        Prediction.GetHitChance(
                            Static.Objects.ProjectMenu.Item($"{basename}.UseW.Prediction")
                                .GetValue<StringList>()
                                .SelectedValue);

                    //Check if the target in target selector is valid (best target)
                    var focusTarget = TargetSelector.GetTarget(W.Range, W.DamageType);
                    if (focusTarget != null)
                    {
                        var pred = LeagueSharp.Common.Prediction.GetPrediction(focusTarget, W.Delay, W.Speed);
                        if (Static.Objects.ProjectMenu.Item($"{basename}.UseW.On.{focusTarget.ChampionName}").GetValue<bool>() && pred.Hitchance >= minHitChance)
                            W.Cast(pred.UnitPosition);
                    }

                    if (W.IsReady())
                    {
                        var validChamp = new List<string>();

                        foreach (var enemy in Functions.Objects.Heroes.GetEnemies(W.Range))
                        {
                            if (!Static.Objects.ProjectMenu.Item($"{basename}.UseW.On.{enemy.ChampionName}").GetValue<bool>()) continue;
                            validChamp.Add(enemy.ChampionName);
                        }

                        var validOutputs = Functions.Targeting.GetTargetPredictions(W, minHitChance, validChamp);

                        foreach (var output in validOutputs)
                        {
                            W.Cast(output.UnitPosition);
                            break;
                        }
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseR").GetValue<bool>())
            {
                if (Mana.CheckComboR())
                {
                    if (Mana.CheckComboR())
                    {
                        var minHitChance =
                            Prediction.GetHitChance(
                                Static.Objects.ProjectMenu.Item($"{basename}.UseR.Prediction")
                                    .GetValue<StringList>()
                                    .SelectedValue);

                        //Check if the target in target selector is valid (best target)
                        var focusTarget = TargetSelector.GetTarget(Static.Objects.ProjectMenu.Item($"{basename}.UseR.Range").GetValue<Slider>().Value, R.DamageType);
                        if (focusTarget != null)
                        {
                            var pred = LeagueSharp.Common.Prediction.GetPrediction(focusTarget, R.Delay, R.Speed);

                            if (Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{focusTarget.ChampionName}").GetValue<bool>() && pred.Hitchance >= minHitChance)
                            {
                                if (Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{focusTarget.ChampionName}.HpMin").GetValue<Slider>().Value > focusTarget.HealthPercent)
                                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{focusTarget.ChampionName}.HpMax").GetValue<Slider>().Value < focusTarget.HealthPercent)
                                        R.Cast(pred.UnitPosition);
                            }
                        }

                        if (R.IsReady())
                        {
                            var validChamp = new List<string>();

                            foreach (var enemy in Functions.Objects.Heroes.GetEnemies(R.Range))
                            {
                                if (!Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}").GetValue<bool>()) continue;
                                if (Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}.HpMin").GetValue<Slider>().Value > enemy.HealthPercent) continue;
                                if (Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}.HpMax").GetValue<Slider>().Value < enemy.HealthPercent) continue;
                                validChamp.Add(enemy.ChampionName);
                            }

                            var validOutputs = Functions.Targeting.GetTargetPredictions(R, minHitChance, validChamp);

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

        private void Mixed()
        {
            var basename = BaseName + "Mixed.";

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
            {
                if (Mana.CheckMixedW())
                {
                    var minHitChance =
                        Prediction.GetHitChance(
                            Static.Objects.ProjectMenu.Item($"{basename}.UseW.Prediction")
                                .GetValue<StringList>()
                                .SelectedValue);

                    //Check if the target in target selector is valid (best target)
                    var focusTarget = TargetSelector.GetTarget(W.Range, W.DamageType);
                    if (focusTarget != null)
                    {
                        var pred = LeagueSharp.Common.Prediction.GetPrediction(focusTarget, W.Delay, W.Speed);
                        if (Static.Objects.ProjectMenu.Item($"{basename}.UseW.On.{focusTarget.ChampionName}").GetValue<bool>() && pred.Hitchance >= minHitChance)
                            W.Cast(pred.UnitPosition);
                    }
                    if (W.IsReady())
                    {
                        if (W.IsReady())
                        {
                            var validChamp = new List<string>();

                            foreach (var enemy in Functions.Objects.Heroes.GetEnemies(W.Range))
                            {
                                if (!Static.Objects.ProjectMenu.Item($"{basename}.UseW.On.{enemy.ChampionName}").GetValue<bool>()) continue;
                                validChamp.Add(enemy.ChampionName);
                            }

                            var validOutputs = Functions.Targeting.GetTargetPredictions(W, minHitChance, validChamp);

                            foreach (var output in validOutputs)
                            {
                                W.Cast(output.UnitPosition);
                                break;
                            }
                        }
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                if (Mana.CheckMixedQ())
                    if (Functions.Objects.Heroes.GetEnemies(Static.Objects.Player.AttackRange - 50).Count >= 1)
                        Q.Cast();
        }

        private void Clear()
        {
            var basename = BaseName + "Clear.";

            var validMinions = Minions.GetEnemyMinions2(W.Range);

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
                if (Mana.CheckClearW())
                    if (W.IsReady())
                    {
                        var pos = W.GetLineFarmLocation(validMinions);

                        if (pos.MinionsHit >=
                            Static.Objects.ProjectMenu.Item($"{basename}.UseW.Minions").GetValue<Slider>().Value)
                            W.Cast(pos.Position);
                    }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                if (Mana.CheckClearQ())
                {
                    var aaMinons =
                        validMinions.Where(x => x.Distance(Static.Objects.Player) < Static.Objects.Player.AttackRange);
                    if (Q.IsReady())
                        if (aaMinons.Count() >=
                            Static.Objects.ProjectMenu.Item($"{basename}.UseQ.Minions").GetValue<Slider>().Value)
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
                if (Static.Objects.ProjectMenu.Item($"{basename}.UseR.OnGapClose").GetValue<bool>())
                {
                    if (
                        Static.Objects.ProjectMenu.Item($"{basename}.UseR.OnGapClose.{gapcloser.Sender.ChampionName}")
                            .GetValue<bool>())
                    {
                        if (gapcloser.End.Distance(ObjectManager.Player.Position) < 300)
                        {
                            if (Static.Objects.ProjectMenu.Item($"{Names.Menu.BaseItem}.Humanizer").GetValue<bool>())
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
                if (Static.Objects.ProjectMenu.Item($"{basename}.UseW.OnGapClose").GetValue<bool>())
                {
                    if (
                        Static.Objects.ProjectMenu.Item($"{basename}.UseW.OnGapClose.{gapcloser.Sender.ChampionName}")
                            .GetValue<bool>())
                    {
                        if (gapcloser.End.Distance(ObjectManager.Player.Position) < 200)
                        {
                            if (Static.Objects.ProjectMenu.Item($"{Names.Menu.BaseItem}.Humanizer").GetValue<bool>())
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

        private void OnDraw(EventArgs args)
        {
            if (!Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                 ".Boolean.DrawOnSelf").GetValue<bool>()) return;

            if (W.Level > 0)
                if (
                    Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                    ".Boolean.DrawOnSelf.WColor").GetValue<Circle>().Active)
                    Render.Circle.DrawCircle(Static.Objects.Player.Position, W.Range,
                        Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase +
                                                        Static.Objects.Player.ChampionName +
                                                        ".Boolean.DrawOnSelf.WColor").GetValue<Circle>().Color, 2);
            if (R.Level > 0)
                if (
                    Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                    ".Boolean.DrawOnSelf.RColor").GetValue<Circle>().Active)
                    Render.Circle.DrawCircle(Static.Objects.Player.Position, R.Range,
                        Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase +
                                                        Static.Objects.Player.ChampionName +
                                                        ".Boolean.DrawOnSelf.RColor").GetValue<Circle>().Color, 2);
        }

        public void OnDrawEnemy(EventArgs args)
        {
            if (
                !Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                 ".Boolean.DrawOnEnemy").GetValue<bool>())
            {
                _damageIndicator.SetFillEnabled(false);
                _damageIndicator.SetKillableEnabled(false);
                return;
            }

            _damageIndicator.SetFillEnabled(
                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                ".Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Active);
            _damageIndicator.SetFill(
                Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                ".Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Color);

            _damageIndicator.SetKillableEnabled(false);
        }

        private static float GetDamage(Obj_AI_Hero target)
        {
            var damage = 0f;
            if (target.Distance(Static.Objects.Player) < Static.Objects.Player.AttackRange - 25 &&
                Static.Objects.Player.CanAttack && !Static.Objects.Player.IsWindingUp)
                damage += (float)Static.Objects.Player.GetAutoAttackDamage(target) - 10;

            if (W.IsReady())
                damage += W.GetDamage(target);

            if (R.IsReady())
                damage += R.GetDamage(target);

            return Damage.CalcRealDamage(target, damage);
        }
    }
}