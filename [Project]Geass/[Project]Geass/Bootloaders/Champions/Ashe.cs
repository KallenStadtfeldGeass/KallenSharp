using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;
using _Project_Geass.Drawing.Champions;

namespace _Project_Geass.Bootloaders.Champions
{
    internal class Ashe : Base.Champion
    {
        private readonly Random _rng;

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        private Orbwalking.Orbwalker Orbwalker { get; set; }

        private readonly string _baseName = Names.ProjectName + Static.Objects.Player.ChampionName + ".";
        private readonly DamageIndicator _damageIndicator = new DamageIndicator(GetDamage, 1000, true);


        public Ashe()
        {
            GetSpellQ = new Spell(SpellSlot.Q);
            GetSpellW = new Spell(SpellSlot.W, 1200);
            GetSpellR = new Spell(SpellSlot.R, 2200);

            GetSpellW.SetSkillshot(.25f, 57.5f, 2000, true, SkillshotType.SkillshotCone);
            GetSpellR.SetSkillshot(.25f, 250, 1600, false, SkillshotType.SkillshotLine);

            // ReSharper disable once UnusedVariable
            var temp = new Menus.Ashe();
            _rng = new Random();
            Game.OnUpdate += OnUpdate;

            LeagueSharp.Drawing.OnDraw += OnDraw;
            LeagueSharp.Drawing.OnDraw += OnDrawEnemy;
            AntiGapcloser.OnEnemyGapcloser += OnGapcloser;

            Interrupter2.OnInterruptableTarget += OnInterruptable;
            Orbwalking.AfterAttack += AfterAttack;

            Orbwalker = new Orbwalking.Orbwalker(Static.Objects.ProjectMenu.SubMenu(".CommonOrbwalker"));
        }

        private void OnUpdate(EventArgs args)
        {
            if (Humanizer.DelayHandler.CheckOrbwalker())
            {
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

        private void Combo()
        {
            string basename = _baseName + "Combo.";

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
            {
                if (Core.Functions.Mana.CheckComboQ())
                {
                    if (GetSpellQ.IsReady())
                    {
                        if (Functions.Objects.Heroes.GetEnemies(Static.Objects.Player.AttackRange - 30).Count > 0)
                        {
                            GetSpellQ.Cast();
                        }
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
            {
                if (Core.Functions.Mana.CheckComboW())
                {
                    if (GetSpellW.IsReady())
                    {
                        var enemies = Functions.Objects.Heroes.GetEnemies(GetSpellW.Range);

                        var validPos =
                            enemies.Where(
                                x =>
                                    GetSpellW.GetPrediction(x).Hitchance >=
                                    Core.Functions.Prediction.GetHitChance(
                                        Static.Objects.ProjectMenu.Item($"{basename}.UseW.Prediction")
                                            .GetValue<StringList>()
                                            .SelectedValue));

                        foreach (var pos in validPos.OrderBy(x => x.Health))
                        {
                            GetSpellW.Cast(pos.Position);
                            break;
                        }
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseR").GetValue<bool>())
            {
                if (Core.Functions.Mana.CheckComboR())
                {
                    if (GetSpellR.IsReady())
                    {
                        var enemies = Functions.Objects.Heroes.GetEnemies(Static.Objects.ProjectMenu.Item($"{basename}.UseR.Range").GetValue<Slider>().Value);

                        var validEnemies = (enemies.OrderBy(x => x.HealthPercent).Where(enemy => Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}").GetValue<bool>()).Where(enemy => !(Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}.HpMin").GetValue<Slider>().Value > enemy.HealthPercent) && !(Static.Objects.ProjectMenu.Item($"{basename}.UseR.On.{enemy.ChampionName}.HpMax").GetValue<Slider>().Value < enemy.HealthPercent)));

                        var validPos =
                            validEnemies.Where(
                                x =>
                                    GetSpellR.GetPrediction(x).Hitchance >=
                                    Core.Functions.Prediction.GetHitChance(
                                        Static.Objects.ProjectMenu.Item($"{basename}.UseR.Prediction")
                                            .GetValue<StringList>()
                                            .SelectedValue));

                        foreach (var pos in validPos.OrderBy(x => x.Health))
                        {
                            GetSpellR.Cast(pos.Position);
                            break;
                        }
                    }
                }
            }
        }

        private void Mixed()
        {
            string basename = _baseName + "Mixed.";

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
            {
                if (Core.Functions.Mana.CheckMixedW())
                {
                    var enemies = Functions.Objects.Heroes.GetEnemies(GetSpellW.Range);

                    var validPos =
                        enemies.Where(
                            x =>
                                GetSpellW.GetPrediction(x).Hitchance >=
                                Core.Functions.Prediction.GetHitChance(
                                    Static.Objects.ProjectMenu.Item($"{basename}.UseW.Prediction")
                                        .GetValue<StringList>()
                                        .SelectedValue));

                    foreach (var pos in validPos.OrderBy(x => x.Health))
                    {
                        GetSpellW.Cast(pos.Position);
                        break;
                    }
                }
            }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                if (Core.Functions.Mana.CheckMixedQ())
                    if (Functions.Objects.Heroes.GetEnemies(Static.Objects.Player.AttackRange - 50).Count >= 1)
                        GetSpellQ.Cast();
        }

        private void Clear()
        {
            var basename = _baseName + "Clear.";

            var validMinions = Functions.Objects.Minions.GetEnemyMinions2(GetSpellW.Range);

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseW").GetValue<bool>())
                if (Core.Functions.Mana.CheckClearW())
                    if (GetSpellW.IsReady())
                    {
                        var pos = GetSpellW.GetLineFarmLocation(validMinions);

                        if (pos.MinionsHit >= Static.Objects.ProjectMenu.Item($"{basename}.UseW.Minions").GetValue<Slider>().Value)
                            GetSpellW.Cast(pos.Position);
                    }

            if (Static.Objects.ProjectMenu.Item($"{basename}.UseQ").GetValue<bool>())
                if (Core.Functions.Mana.CheckClearQ())
                {
                    var aaMinons = validMinions.Where(x => x.Distance(Static.Objects.Player) < Static.Objects.Player.AttackRange);
                    if (GetSpellQ.IsReady())
                        if (aaMinons.Count() >=
                            Static.Objects.ProjectMenu.Item($"{basename}.UseQ.Minions").GetValue<Slider>().Value)
                            GetSpellQ.Cast();
                }
        }

        private void AfterAttack(AttackableUnit unit, AttackableUnit target)
        {
        }

        /// <summary>
        /// Called when [gapcloser].
        /// </summary>
        /// <param name="gapcloser">The gapcloser.</param>
        private void OnGapcloser(ActiveGapcloser gapcloser)
        {
            var basename = _baseName + "Auto.";

            if (GetSpellR.IsReady())
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
                                Utility.DelayAction.Add(Math.Abs(_rng.Next() * (150 - 50 - Game.Ping) + 50 - Game.Ping),
                                    () =>
                                    {
                                        GetSpellR.Cast(gapcloser.End);
                                    });
                            }
                            else
                                GetSpellR.Cast(gapcloser.End);
                        }
                    }
                }
            }

            if (GetSpellW.IsReady())
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
                                Utility.DelayAction.Add(
                                    Math.Abs(_rng.Next() * (200 - 100 - Game.Ping) + 100 - Game.Ping), () =>
                                      {
                                          GetSpellW.Cast(gapcloser.End);
                                      });
                            }
                            else
                                GetSpellW.Cast(gapcloser.End);
                        }
                    }
                }
            }
        }

        private void OnInterruptable(Obj_AI_Hero sender, Interrupter2.InterruptableTargetEventArgs args)
        {
        }

        private void OnDraw(EventArgs args)
        {
            if (Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf").GetValue<bool>())
            {
                if (Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.WColor").GetValue<Circle>().Active)
                    Render.Circle.DrawCircle(Static.Objects.Player.Position, GetSpellW.Range, Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.WColor").GetValue<Circle>().Color, 2);
                if (Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.RColor").GetValue<Circle>().Active)
                    Render.Circle.DrawCircle(Static.Objects.Player.Position, GetSpellW.Range, Static.Objects.ProjectMenu.Item(Names.Menu.DrawingItemBase + Static.Objects.Player.ChampionName + ".Boolean.DrawOnSelf.RColor").GetValue<Circle>().Color, 2);
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


        static float GetDamage(Obj_AI_Hero target)
        {
            var damage = 0f;
            if (target.Distance(Static.Objects.Player) < Static.Objects.Player.AttackRange - 25 &&
                Static.Objects.Player.CanAttack && !Static.Objects.Player.IsWindingUp)
                damage += (float)Static.Objects.Player.GetAutoAttackDamage(target) - 10;
            

            if (GetSpellW.IsReady())
               damage += damage += (float) GetSpellW.GetDamage(target);

            if (GetSpellR.IsReady())
                damage += damage += (float)GetSpellR.GetDamage(target);

            return Functions.Calculations.Damage.CalcRealDamage(target,damage);

        }
    }
}