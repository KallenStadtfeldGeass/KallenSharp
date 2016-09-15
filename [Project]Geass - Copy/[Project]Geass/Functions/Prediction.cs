using LeagueSharp.Common;
using System;

namespace _Project_Geass.Functions
{
    internal class Prediction
    {
        public static string[] GetHitChanceNames()
        {
            var names = Enum.GetNames(typeof(HitChance));
            return new[]
            {
                names[(int) HitChance.VeryHigh], names[(int) HitChance.High], names[(int) HitChance.Medium],names[(int) HitChance.Low],
                names[(int) HitChance.Immobile], names[(int) HitChance.Dashing]
            };
        }

        public static HitChance GetHitChance(string value) => (HitChance)Enum.Parse(typeof(HitChance), value);
    }
}