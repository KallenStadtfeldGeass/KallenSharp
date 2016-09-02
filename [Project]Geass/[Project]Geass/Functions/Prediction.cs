using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project_Geass.Functions
{
    internal class Prediction
    {
        private static readonly HitChance[] HitChanceValues = (HitChance[])Enum.GetValues(typeof(HitChance));

        public static string[] GetHitChanceNames()
        {
            var names = Enum.GetNames(typeof(HitChance));
            return names.Where((t, i) => HitChanceValues[i] >= HitChance.Low && HitChanceValues[i] <= HitChance.Immobile).Reverse().ToArray();
        }

        public static HitChance GetHitChance(string value) => (HitChance)Enum.Parse(typeof(HitChance), value);
    }
}