using LeagueSharp;
using LeagueSharp.Common;
using System.Collections.Generic;
using System.Linq;

namespace _Project_Geass.Functions
{
    internal class Targeting
    {
        public struct PredictionWithChampion
        {
            public PredictionWithChampion(Obj_AI_Hero champ, PredictionOutput prediction)
            {
                Champion = champ;
                Prediction = prediction;
            }

            public PredictionOutput Prediction;
            public Obj_AI_Hero Champion;
        }

        /// <summary>
        /// Gets and orders from best target with champion names
        /// </summary>
        /// <param name="spell">The spell.</param>
        /// <param name="minHitChance">The minimum hit chance.</param>
        /// <param name="validChampNames">The valid champ names.</param>
        /// <returns></returns>
        public static IOrderedEnumerable<PredictionWithChampion> GetTargetPredictions2(Spell spell, HitChance minHitChance, List<string> validChampNames)
        {
            var tempList = new List<PredictionWithChampion>();

            foreach (var champion in Objects.Heroes.GetEnemies(spell.Range).Where(name => validChampNames.Contains(name.ChampionName)))
            {
                if (champion.HasBuffOfType(BuffType.Invulnerability) || champion.HasBuffOfType(BuffType.SpellImmunity) || champion.HasBuffOfType(BuffType.SpellShield)) continue;

                var tempPred = LeagueSharp.Common.Prediction.GetPrediction(champion, spell.Delay, spell.Speed);
                if (tempPred.Hitchance >= minHitChance)
                    tempList.Add(new PredictionWithChampion(champion, tempPred));
            }

            return tempList.OrderBy(pred => pred.Prediction.Hitchance);
        }

        /// <summary>
        /// Gets and orders from best target
        /// </summary>
        /// <param name="spell">The spell.</param>
        /// <param name="minHitChance">The minimum hit chance.</param>
        /// <param name="validChampNames">The valid champ names.</param>
        /// <returns></returns>
        public static IOrderedEnumerable<PredictionOutput> GetTargetPredictions(Spell spell, HitChance minHitChance, List<string> validChampNames)
        {
            var tempList = new List<PredictionOutput>();

            foreach (var champion in Objects.Heroes.GetEnemies(spell.Range).Where(name => validChampNames.Contains(name.ChampionName)))
            {
                if (champion.HasBuffOfType(BuffType.Invulnerability) || champion.HasBuffOfType(BuffType.SpellImmunity) || champion.HasBuffOfType(BuffType.SpellShield)) continue;

                var tempPred = LeagueSharp.Common.Prediction.GetPrediction(champion, spell.Delay, spell.Speed);
                if (tempPred.Hitchance >= minHitChance)
                    tempList.Add(tempPred);
            }

            return tempList.OrderBy(pred => pred.Hitchance);
        }
    }
}