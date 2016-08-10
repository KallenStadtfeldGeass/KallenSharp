using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace _Project_Geass.Bootloaders.Champions
{
    internal class Ashe : Base.Champion
    {
        private readonly Random _rng;
        private Orbwalking.Orbwalker Orbwalker { get; set; }
        private readonly string _baseName = Names.ProjectName + Static.Objects.Player.ChampionName + ".";

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
            AntiGapcloser.OnEnemyGapcloser += OnGapcloser;

            Interrupter2.OnInterruptableTarget += OnInterruptable;
            Orbwalking.AfterAttack += AfterAttack;

            Orbwalker = new Orbwalking.Orbwalker(Static.Objects.ProjectMenu.SubMenu(".CommonOrbwalker"));
        }

        private void OnUpdate(EventArgs args)
        {
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
                            if (Core.Functions.MenuOptions.HumanizerEnabled())
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
                            if (Core.Functions.MenuOptions.HumanizerEnabled())
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
        }
    }
}