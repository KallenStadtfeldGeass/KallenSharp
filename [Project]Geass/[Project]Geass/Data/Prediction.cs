using LeagueSharp;
using LeagueSharp.Common;

namespace _Project_Geass.Data
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
    }
}