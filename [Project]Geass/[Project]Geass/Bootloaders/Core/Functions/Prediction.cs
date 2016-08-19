using LeagueSharp.Common;
using System;

namespace _Project_Geass.Bootloaders.Core.Functions
{
    internal class Prediction
    {
        public static string[] GetHitChanceNames()
        {
            return Enum.GetNames(typeof(LeagueSharp.Common.HitChance));
        }

        public static HitChance GetHitChance(string value) => (HitChance)Enum.Parse(typeof(HitChance), value);
    }
}