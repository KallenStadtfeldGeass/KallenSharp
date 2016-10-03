﻿using _Project_Geass.Functions.Objects;
using LeagueSharp;
using LeagueSharp.Common;
using SPrediction;
using System;
using System.Linq;

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
                    if (!checkColision) return prediction.Hitchance >= minHitChance;

                        if (prediction.CollisionObjects.Any())
                            return false;

                    return prediction.Hitchance >= minHitChance;
                }
                case 1:
                {

                    var sebbyPrediction = SebbyLib.Prediction.Prediction.GetPrediction(target, spell.Delay);

                    if (!checkColision) return (HitChance) sebbyPrediction.Hitchance >= minHitChance;


                    if (sebbyPrediction.CollisionObjects.Any())
                        return false;

                    return (HitChance)sebbyPrediction.Hitchance >= minHitChance;
                }
            }


            var sprediction = spell.GetSPrediction(target);

            if (checkColision && sprediction.CollisionResult.Units.Any(a => !a.IsChampion()))
                return false;

            return sprediction.HitChance >= minHitChance;

        }

        public static bool DoCast(Spell spell, Obj_AI_Hero target,bool colisionCheck = false)
        {
            switch (PredictionMethod)
            {

                case 0: //Common
                        var cPos = spell.GetPrediction(target);
                    if (colisionCheck && cPos.CollisionObjects.Any()) return false;
                        spell.Cast(cPos.CastPosition);
                        break;
                case 1: //Sebby
                        var pos = SebbyLib.Prediction.Prediction.GetPrediction(target, spell.Delay);
                    if (colisionCheck && pos.CollisionObjects.Any()) return false;
                    spell.Cast(pos.CastPosition);
                        break;
                case 2: //Sprediction
                    var sPos = spell.GetSPrediction(target);
                    if (colisionCheck && sPos.CollisionResult.Units.Any(a => !a.IsChampion()))
                        spell.Cast(sPos.CastPosition);
                    break;

            }
            return true;
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
    }
}