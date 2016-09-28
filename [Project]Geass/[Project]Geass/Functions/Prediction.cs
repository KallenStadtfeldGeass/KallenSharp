using _Project_Geass.Functions.Objects;
using LeagueSharp;
using LeagueSharp.Common;
using SPrediction;
using System;
using System.Collections.Generic;
using System.Linq;
using Collision = SPrediction.Collision;

namespace _Project_Geass.Functions
{
    internal class Prediction
    {
        public static readonly int PredictionMethod =StaticObjects.SettingsMenu.Item($"{Names.Menu.BaseItem}.PredictionMethod").GetValue<StringList>().SelectedIndex;

        public static bool CheckTarget(Obj_AI_Hero target, Spell spell,HitChance minHitChance, bool checkColision = false)
        {
            switch (PredictionMethod)
            {
                case 0:
                {
                    var prediction = spell.GetPrediction(target);
                        if(checkColision)
                            return (prediction.Hitchance >= minHitChance && prediction.CollisionObjects.Count(h => h.IsEnemy && !h.IsDead && h is Obj_AI_Minion) < 2);

                        return prediction.Hitchance >= minHitChance;
                }
                case 1:
                {
                    var prediction = spell.GetSPrediction(target);

                        if (checkColision && prediction.CollisionResult.Objects.HasFlag(Collision.Flags.Minions) || prediction.CollisionResult.Objects.HasFlag(Collision.Flags.YasuoWall))
                        return false;
                
                    return prediction.HitChance >= minHitChance;
                }
            }
            var sprediction = SebbyLib.Prediction.Prediction.GetPrediction(target, spell.Delay);

            if (checkColision)
                return ((HitChance)sprediction.Hitchance >= minHitChance && sprediction.CollisionObjects.Count(h => h.IsEnemy && !h.IsDead && h is Obj_AI_Minion) < 2);

            return (HitChance)sprediction.Hitchance >= minHitChance;

        }

        public static void DoCast(Spell spell, Obj_AI_Hero target)
        {
            switch (PredictionMethod)
            {
                case 0: //Common
                        var cPos = spell.GetPrediction(target);
                        spell.Cast(cPos.CastPosition);
                        break;
                case 1: //Sprediction
                        var sPos = spell.GetSPrediction(target);
                        spell.Cast(sPos.CastPosition);
                        break;
                case 2: //Sebby
                        var pos = SebbyLib.Prediction.Prediction.GetPrediction(target, spell.Delay);
                        spell.Cast(pos.CastPosition);
                        break;
                    
            }
        }
        public static void DoCast(Spell spell, IOrderedEnumerable<Obj_AI_Hero> targets , HitChance minHitChance)
        {
            switch (PredictionMethod)
            {
                case 0: //Common
                    foreach (var target in targets)
                    {
                        var pos = spell.GetPrediction(target);
                        if (pos.Hitchance < minHitChance) continue;
                        spell.Cast(pos.CastPosition);
                        break;
                    }

                    break;
                case 1: //Sprediction
                    foreach (var target in targets)
                    {
                        var pos = spell.GetSPrediction(target);
                        if (pos.HitChance < minHitChance) continue;
                        spell.Cast(pos.CastPosition);
                        break;
                    }
                    break;
                case 2: //Sebby
                    foreach (var target in targets)
                    {
                        var pos = SebbyLib.Prediction.Prediction.GetPrediction(target, spell.Delay);
                        if ((HitChance)pos.Hitchance < minHitChance) continue;
                        spell.Cast(pos.CastPosition);
                        break;
                    }
                    break;
            }
        } 

        static bool ValidChampion(Obj_AI_Hero target)
        {
            return !target.HasBuffOfType(BuffType.Invulnerability) && !target.HasBuffOfType(BuffType.SpellImmunity) &&
                   !target.HasBuffOfType(BuffType.SpellShield);
        }
        public static IOrderedEnumerable<Obj_AI_Hero> OrderTargets(Spell spell)
        {
            var damageType = spell.DamageType == TargetSelector.DamageType.Physical;
            return damageType ? Heroes.GetEnemies(spell.Range).Where(ValidChampion).OrderBy(hp => hp.Health/hp.PercentArmorMod) : Heroes.GetEnemies(spell.Range).Where(ValidChampion).OrderBy(hp => hp.Health / hp.PercentMagicReduction);
        }

        public static string[] GetHitChanceNames()
        {
            var names = Enum.GetNames(typeof(HitChance));
            return new[]
            {
                names[(int) HitChance.High],names[(int) HitChance.VeryHigh], names[(int) HitChance.Medium],
                names[(int) HitChance.Low],
                names[(int) HitChance.Immobile], names[(int) HitChance.Dashing]
            };
        }

        public static HitChance GetHitChance(string value) => (HitChance) Enum.Parse(typeof(HitChance), value);

        public struct PredictionWrapper
        {
            public Obj_AI_Hero Champion;
            public PredictionOutput Prediction;
            public SPrediction.Prediction.Result SPrediction;
            public SebbyLib.Prediction.PredictionOutput SebbyPrediction;
        }
    }
}