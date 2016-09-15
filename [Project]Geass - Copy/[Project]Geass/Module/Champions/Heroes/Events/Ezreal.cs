using _Project_Geass.Data;
using _Project_Geass.Drawing.Champions;
using _Project_Geass.Globals;
using _Project_Geass.Module.Champions.Core;
using _Project_Geass.Module.Core.Mana.Functions;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using Prediction = _Project_Geass.Functions.Prediction;

namespace _Project_Geass.Module.Champions.Heroes.Events
{
    internal class Ezreal : Base
    {
        private readonly DamageIndicator _damageIndicator = new DamageIndicator(GetDamage, 2000);

        public Ezreal()
        {
            Q = new Spell(SpellSlot.Q, 1150);
            W = new Spell(SpellSlot.W, 1000);
            R = new Spell(SpellSlot.R, 2200);

            Q.SetSkillshot(.25f, 60, 2000, true, SkillshotType.SkillshotLine);
            W.SetSkillshot(.25f, 80, 1550, false, SkillshotType.SkillshotLine);
            R.SetSkillshot(1, 160, 2000, false, SkillshotType.SkillshotLine);

            // ReSharper disable once UnusedVariable
            var temp = new Menus.Ezreal();

            Game.OnUpdate += OnUpdate;
            Game.OnUpdate += AutoEvents;
            LeagueSharp.Drawing.OnDraw += OnDraw;
            LeagueSharp.Drawing.OnDraw += OnDrawEnemy;

            //Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;

            Orbwalker = new Orbwalking.Orbwalker(Static.Objects.ProjectMenu.SubMenu(nameof(Orbwalker)));
        }

        //private const float DelayCheck = 8000;
        //private static float _lastTick;
        //private static float _lastMana;
        private static bool _tearFull = false;

        //private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        //{
        //    if (_tearFull) return;
        //    if (!sender.IsMe) return;

        //    if (args.Slot < SpellSlot.Q || args.Slot > SpellSlot.R) return; // 0-3 (Q-R)

        //    if (DelayCheck + _lastTick > Functions.AssemblyTime.CurrentTime())
        //        if (Items.HasItem(LeagueSharp.Common.Data.ItemData.Tear_of_the_Goddess.Id))
        //        {
        //            _tearFull = _lastMana >= Static.Objects.Player.MaxMana;
        //            _lastMana = Static.Objects.Player.MaxMana;
        //        }

        //    _lastTick = Functions.AssemblyTime.CurrentTime();
        //}

        private void AutoEvents(EventArgs args)
        {
            if (!Humanizer.DelayHandler.CheckAutoEvents()) return;
            if (!_tearFull)
            {
                if (!Static.Objects.Player.IsRecalling())
                {
                    var basename = BaseName + "Misc.";

                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ.TearStack").GetValue<bool>() && Mana.GetManaPercent >= Static.Objects.ProjectMenu.Item($"{basename}.UseQ.TearStack.MinMana").GetValue<Slider>().Value)
                    {
                        if (Items.HasItem(LeagueSharp.Common.Data.ItemData.Tear_of_the_Goddess.Id) ||
                            Items.HasItem(LeagueSharp.Common.Data.ItemData.Manamune.Id))
                            if (Functions.Objects.Minions.GetEnemyMinions2(1000).Count < 1 &&
                                Functions.Objects.Heroes.GetEnemies(1000).Count < 1 &&
                               MinionManager.GetMinions(1000, MinionTypes.All, MinionTeam.Neutral).Count < 1)
                                Q.Cast(Game.CursorPos);
                    }
                }
            }
            Humanizer.DelayHandler.UseAutoEvent();
        }

        private void OnUpdate(EventArgs args)
        {
            if (!Humanizer.DelayHandler.CheckOrbwalker()) return;

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

        private void Combo()
        {
            var basename = BaseName + "Combo.";

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
            {
                if (Mana.CheckMixedQ())
                {
                    var minHitChance =
                        Prediction.GetHitChance(
                            Static.Objects.ProjectMenu.Item($"{basename}.UseQ.Prediction")
                                .GetValue<StringList>()
                                .SelectedValue);

                    //Check if the target in target selector is valid (best target)
                    var focusTarget = TargetSelector.GetTarget(Q.Range, Q.DamageType);
                    if (focusTarget != null)
                    {
                        var pred = LeagueSharp.Common.Prediction.GetPrediction(focusTarget, Q.Delay, Q.Speed);

                        if (!pred.CollisionObjects.Any())
                            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ.On.{focusTarget.ChampionName}").GetValue<bool>() && pred.Hitchance >= minHitChance)
                                Q.Cast(pred.UnitPosition);
                    }
                    if (Q.IsReady())
                    {
                        var validChamp = new List<string>();

                        foreach (var enemy in Functions.Objects.Heroes.GetEnemies(Q.Range))
                        {
                            if (!Static.Objects.ProjectMenu.Item($"{basename}.UseQ.On.{enemy.ChampionName}").GetValue<bool>()) continue;
                            validChamp.Add(enemy.ChampionName);
                        }

                        var validOutputs = Functions.Targeting.GetTargetPredictions(Q, minHitChance, validChamp);

                        foreach (var output in validOutputs)
                        {
                            if (output.CollisionObjects.Any()) continue;
                            Q.Cast(output.UnitPosition);
                            break;
                        }
                    }
                }
            }

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
                    var minHitChance =
                        Prediction.GetHitChance(
                            Static.Objects.ProjectMenu.Item($"{basename}.UseR.Prediction")
                                .GetValue<StringList>()
                                .SelectedValue);

                    //Check if the target in target selector is valid (best target)
                    var focusTarget = TargetSelector.GetTarget(Static.Objects.ProjectMenu.Item($"{basename}.UseR.Range").GetValue<Slider>().Value, R.DamageType);
                    if (focusTarget != null && focusTarget.Distance(Static.Objects.Player) > Q.Range)
                    {
                        var pred = LeagueSharp.Common.Prediction.GetPrediction(focusTarget, R.Delay, R.Speed);

                        if (Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{focusTarget.ChampionName}").GetValue<bool>() && pred.Hitchance >= minHitChance)
                        {
                            if (GetRealRDamage(focusTarget) > focusTarget.Health)
                                R.Cast(pred.UnitPosition);
                        }
                    }

                    if (R.IsReady())
                    {
                        var validChamp = new List<string>();

                        foreach (var enemy in Functions.Objects.Heroes.GetEnemies(R.Range))
                        {
                            if (!Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}").GetValue<bool>()) continue;
                            validChamp.Add(enemy.ChampionName);
                        }

                        var validOutputs = Functions.Targeting.GetTargetPredictions2(R, minHitChance, validChamp);

                        foreach (var output in validOutputs)
                        {
                            if (output.Champion.Health > GetRealRDamage(output.Champion)) continue;
                            if (focusTarget.Distance(Static.Objects.Player) < Q.Range) continue;
                            R.Cast(output.Prediction.UnitPosition);
                            break;
                        }
                    }
                }
            }
        }

        public static double GetRealRDamage(Obj_AI_Base target)
        {
            var damage = Static.Objects.Player.GetSpellDamage(target, SpellSlot.R);
            var hits = R.GetCollision(Static.Objects.Player.ServerPosition.To2D(), new List<SharpDX.Vector2> { target.ServerPosition.To2D() }).Count;
            var debuff = hits > 7 ? .7 : hits * .1;
            return damage * (1 - (debuff * 10));
        }

        private void Mixed()
        {
            var basename = BaseName + "Mixed.";

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
            {
                var minHitChance =
                                       Prediction.GetHitChance(
                                           Static.Objects.ProjectMenu.Item($"{basename}.UseQ.Prediction")
                                               .GetValue<StringList>()
                                               .SelectedValue);

                //Check if the target in target selector is valid (best target)
                var focusTarget = TargetSelector.GetTarget(Q.Range, Q.DamageType);
                if (focusTarget != null)
                {
                    var pred = LeagueSharp.Common.Prediction.GetPrediction(focusTarget, Q.Delay, Q.Speed);

                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ.On.{focusTarget.ChampionName}").GetValue<bool>() && pred.Hitchance >= minHitChance)
                        if (!pred.CollisionObjects.Any())
                            Q.Cast(pred.UnitPosition);
                }

                if (Q.IsReady())
                {
                    var validChamp = new List<string>();

                    foreach (var enemy in Functions.Objects.Heroes.GetEnemies(Q.Range))
                    {
                        if (!Static.Objects.ProjectMenu.Item($"{basename}.UseQ.On.{enemy.ChampionName}").GetValue<bool>()) continue;
                        validChamp.Add(enemy.ChampionName);
                    }

                    var validOutputs = Functions.Targeting.GetTargetPredictions(Q, minHitChance, validChamp);

                    foreach (var output in validOutputs)
                    {
                        if (output.CollisionObjects.Any()) continue;
                        Q.Cast(output.UnitPosition);
                        break;
                    }
                }
            }

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

        private void Clear()
        {
            var basename = BaseName + "Clear.";

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                if (Mana.CheckClearQ())
                {
                    foreach (var target in Functions.Objects.Minions.GetEnemyMinions2(Q.Range).Where(x => x.Health < Q.GetDamage(x) && x.Health > 30).OrderBy(hp => hp.Health))
                    {
                        Q.Cast(target);
                        return;
                    }
                    if (!Static.Objects.ProjectMenu.Item($"{basename}.UseQ.OnJungle").GetValue<bool>()) return;
                    foreach (var target in MinionManager.GetMinions(Q.Range, MinionTypes.All, MinionTeam.Neutral).Where(x => x.IsValidTarget(Q.Range)).OrderBy(hp => hp.MaxHealth / hp.Health))
                    {
                        Q.Cast(target);
                        return;
                    }
                }
        }

        private void OnDraw(EventArgs args)
        {
            if (!Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName +
                                                 ".Boolean.DrawOnSelf").GetValue<bool>()) return;
            if (Q.Level > 0)
                if (Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.QColor").GetValue<Circle>().Active)
                    Render.Circle.DrawCircle(Static.Objects.Player.Position, Q.Range, Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.QColor").GetValue<Circle>().Color, 2);
            if (W.Level > 0)
                if (Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.WColor").GetValue<Circle>().Active)
                    Render.Circle.DrawCircle(Static.Objects.Player.Position, W.Range, Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.WColor").GetValue<Circle>().Color, 2);
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
            if (target.Distance(Static.Objects.Player) < Static.Objects.Player.AttackRange - 25 &&
                Static.Objects.Player.CanAttack && !Static.Objects.Player.IsWindingUp)
                damage += (float)Static.Objects.Player.GetAutoAttackDamage(target) - 10;

            if (W.IsReady())
                damage += W.GetDamage(target);

            if (R.IsReady())
                damage += R.GetDamage(target);

            return Functions.Calculations.Damage.CalcRealDamage(target, damage);
        }
    }
}