using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;

namespace _Project_Geass.Functions
{
    class Targeting
    {

        public static IOrderedEnumerable<Data.Prediction.ChampionPrefiction> GetTargetPredictions(Spell spell, HitChance minHitChance)
        {
            var tempList = new List<Data.Prediction.ChampionPrefiction>();

            foreach (var champion in Objects.Heroes.GetEnemies(spell.Range))
            {
                if (champion.HasBuffOfType(BuffType.Invulnerability) || champion.HasBuffOfType(BuffType.SpellImmunity) || champion.HasBuffOfType(BuffType.SpellShield)) continue;

                var tempPred = Prediction.GetPrediction(champion, spell.Delay, spell.Speed);
                if(tempPred.Hitchance >= minHitChance)
                    tempList.Add(new Data.Prediction.ChampionPrefiction(champion,tempPred));
            }

            return tempList.OrderBy(pred => pred.Prediction.Hitchance);
        }
    }
}
