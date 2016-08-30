using _Project_Geass.Bootloaders.Core.Functions;
using _Project_Geass.Data;
using _Project_Geass.Drawing.Champions;
using _Project_Geass.Globals;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project_Geass.Bootloaders.Champions
{
    internal class Ezreal : Base.Champion
    {
        private readonly DamageIndicator _damageIndicator = new DamageIndicator(GetDamage, 1000);

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

            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;

            Orbwalker = new OrbwalkingEx.Orbwalker(Static.Objects.ProjectMenu.SubMenu(".CommonOrbwalker"));
        }

        private const float DelayCheck = 8000;
        private static float _lastTick;
        private static float _lastMana;
        private static bool _tearFull;

        private static void OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (_tearFull) return;
            if (!sender.IsMe) return;

            if (args.Slot < SpellSlot.Q || args.Slot > SpellSlot.R) return; // 0-3 (Q-R)

            if (DelayCheck + _lastTick > Functions.AssemblyTime.CurrentTime())
                if (Items.HasItem(LeagueSharp.Common.Data.ItemData.Tear_of_the_Goddess.Id))
                {
                    _tearFull = _lastMana >= Static.Objects.Player.MaxMana;
                    _lastMana = Static.Objects.Player.MaxMana;
                }

            _lastTick = Functions.AssemblyTime.CurrentTime();
        }

        private void AutoEvents(EventArgs args)
        {
            if (!Humanizer.DelayHandler.CheckAutoEvents()) return;

            if (!_tearFull)
            {
                if (!Static.Objects.Player.IsRecalling())
                {
                    var basename = BaseName + "Misc.";

                    if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ.TearStack").GetValue<bool>() &&
                        Static.Objects.ProjectMenu.Item($"{basename}.UseQ.TearStack.MinMana")
                            .GetValue<Slider>()
                            .Value > Mana.GetManaPercent)
                        if (Items.HasItem(LeagueSharp.Common.Data.ItemData.Tear_of_the_Goddess.Id) ||
                            Items.HasItem(LeagueSharp.Common.Data.ItemData.Manamune.Id))
                            if (Functions.Objects.Minions.GetEnemyMinions2(1000).Count < 1 &&
                                Functions.Objects.Heroes.GetEnemies(1000).Count < 1 && Mana.GetManaPercent > 75)
                                Q.Cast();
                }
            }
            Humanizer.DelayHandler.UseAutoEvent();
        }

        private void OnUpdate(EventArgs args)
        {
            if (!Humanizer.DelayHandler.CheckOrbwalker()) return;

            switch (Orbwalker.ActiveMode)
            {
                case OrbwalkingEx.OrbwalkingMode.Combo:
                    {
                        Combo();
                        break;
                    }
                case OrbwalkingEx.OrbwalkingMode.Mixed:
                    {
                        Mixed();
                        break;
                    }
                case OrbwalkingEx.OrbwalkingMode.LaneClear:
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

            var enemies = Functions.Objects.Heroes.GetEnemies(Q.Range);
            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
            {
                if (Mana.CheckMixedQ())
                {
                    var validPos =
                        enemies.Where(
                            x =>
                                Q.GetPrediction(x).Hitchance >=
                                Core.Functions.Prediction.GetHitChance(
                                    Static.Objects.ProjectMenu.Item($"{basename}.UseQ.Prediction")
                                        .GetValue<StringList>()
                                        .SelectedValue));

                    foreach (var pos in validPos.OrderBy(x => x.Health))
                    {
                        if (!Static.Objects.ProjectMenu.Item($"{basename}.UseQ.On.{pos.ChampionName}").GetValue<bool>()) continue;

                        if (pos.HasBuffOfType(BuffType.Invulnerability) || pos.HasBuffOfType(BuffType.SpellImmunity) || pos.HasBuffOfType(BuffType.SpellShield)) continue;

                        Q.Cast(pos.Position);
                        break;
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
            {
                if (Mana.CheckMixedW())
                {
                    var validPos =
                        enemies.Where(
                            x =>
                                W.GetPrediction(x).Hitchance >=
                                Core.Functions.Prediction.GetHitChance(
                                    Static.Objects.ProjectMenu.Item($"{basename}.UseW.Prediction")
                                        .GetValue<StringList>()
                                        .SelectedValue));

                    foreach (var pos in validPos.OrderBy(x => x.Health))
                    {
                        if (!Static.Objects.ProjectMenu.Item($"{basename}.UseW.On.{pos.ChampionName}").GetValue<bool>()) continue;

                        if (pos.HasBuffOfType(BuffType.Invulnerability) || pos.HasBuffOfType(BuffType.SpellImmunity) || pos.HasBuffOfType(BuffType.SpellShield)) continue;

                        W.Cast(pos.Position);
                        break;
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseR").GetValue<bool>())
            {
                enemies = Functions.Objects.Heroes.GetEnemies(Static.Objects.ProjectMenu.Item($"{basename}.UseR.Range").GetValue<Slider>().Value);
                var validPos =
                        enemies.Where(
                            x =>
                                R.GetPrediction(x).Hitchance >=
                                Core.Functions.Prediction.GetHitChance(
                                    Static.Objects.ProjectMenu.Item($"{basename}.UseR.Prediction")
                                        .GetValue<StringList>()
                                        .SelectedValue));
                if (Mana.CheckComboR())
                {
                    if (R.IsReady())
                    {
                        foreach (var pos in validPos)
                        {
                            if (!Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{pos.ChampionName}").GetValue<bool>()) continue;

                            if (pos.HasBuffOfType(BuffType.Invulnerability) || pos.HasBuffOfType(BuffType.SpellImmunity) || pos.HasBuffOfType(BuffType.SpellShield)) continue;

                            if (GetRealRDamage(pos) < pos.Health) continue;
                            R.Cast(pos.Position);
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
            var enemies = Functions.Objects.Heroes.GetEnemies(Q.Range);
            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
            {
                if (Mana.CheckMixedQ())
                {
                    var validPos =
                        enemies.Where(
                            x =>
                                Q.GetPrediction(x).Hitchance >=
                                Core.Functions.Prediction.GetHitChance(
                                    Static.Objects.ProjectMenu.Item($"{basename}.UseQ.Prediction")
                                        .GetValue<StringList>()
                                        .SelectedValue));

                    foreach (var pos in validPos.OrderBy(x => x.Health))
                    {
                        if (!Static.Objects.ProjectMenu.Item($"{basename}.UseQ.On.{pos.ChampionName}").GetValue<bool>()) continue;

                        if (pos.HasBuffOfType(BuffType.Invulnerability) || pos.HasBuffOfType(BuffType.SpellImmunity) || pos.HasBuffOfType(BuffType.SpellShield)) continue;

                        Q.Cast(pos.Position);
                        break;
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
            {
                if (Mana.CheckMixedW())
                {
                    var validPos =
                        enemies.Where(
                            x =>
                                W.GetPrediction(x).Hitchance >=
                                Core.Functions.Prediction.GetHitChance(
                                    Static.Objects.ProjectMenu.Item($"{basename}.UseW.Prediction")
                                        .GetValue<StringList>()
                                        .SelectedValue));

                    foreach (var pos in validPos.OrderBy(x => x.Health))
                    {
                        if (!Static.Objects.ProjectMenu.Item($"{basename}.UseW.On.{pos.ChampionName}").GetValue<bool>()) continue;

                        if (pos.HasBuffOfType(BuffType.Invulnerability) || pos.HasBuffOfType(BuffType.SpellImmunity) || pos.HasBuffOfType(BuffType.SpellShield)) continue;

                        W.Cast(pos.Position);
                        break;
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
                    var validMinions = Functions.Objects.Minions.GetEnemyMinions2(Q.Range).Where(x => x.Health < Q.GetDamage(x) && x.Health > 30).OrderBy(hp => hp.Health);
                    if (Static.Objects.Player.IsWindingUp) // can not auto minon
                        if (Q.IsReady())
                            Q.Cast(validMinions.FirstOrDefault());
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