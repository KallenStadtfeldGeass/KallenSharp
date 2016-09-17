using LeagueSharp.Common;
using System;
using LeagueSharp;

namespace _Project_Geass.Functions
{
    internal class Prediction
    {
        public struct ChampionPrediction
        {
            public Obj_AI_Hero Champion;
            public PredictionOutput Prediction;

            /// <summary>
            /// Initializes a new instance of the <see cref="ChampionPrediction"/> struct.
            /// </summary>
            /// <param name="champ">The champ.</param>
            /// <param name="pred">The pred.</param>
            public ChampionPrediction(Obj_AI_Hero champ, PredictionOutput pred)
            {
                Champion = champ;
                Prediction = pred;
            }
        }

        public static string[] GetHitChanceNames()
        {
            var names = Enum.GetNames(typeof(HitChance));
            return new[]
            {
                names[(int) HitChance.VeryHigh], names[(int) HitChance.High], names[(int) HitChance.Medium],
                names[(int) HitChance.Low],
                names[(int) HitChance.Immobile], names[(int) HitChance.Dashing]
            };
        }

        public static HitChance GetHitChance(string value) => (HitChance) Enum.Parse(typeof(HitChance), value);
    }
}