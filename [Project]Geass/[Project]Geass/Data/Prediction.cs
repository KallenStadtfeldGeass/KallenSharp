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

            public ChampionPrediction(Obj_AI_Hero champ, PredictionOutput pred)
            {
                Champion = champ;
                Prediction = pred;
            }
        }
    }
}