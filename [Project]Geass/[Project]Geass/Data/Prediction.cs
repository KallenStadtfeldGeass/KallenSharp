using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace _Project_Geass.Data
{
    class Prediction
    {
        public struct ChampionPrefiction
        {
            public Obj_AI_Hero Champion;
            public PredictionOutput Prediction;

            public ChampionPrefiction(Obj_AI_Hero champ, PredictionOutput pred)
            {
                Champion = champ;
                Prediction = pred;
            }
        }
    }
}
