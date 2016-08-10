using System;

namespace _Project_Geass.Bootloaders.Core.Functions
{
    internal class Prediction
    {
        public static string[] GetHitChanceNames()
        {
            var names = Enum.GetNames(typeof(LeagueSharp.Common.HitChance));
            return new[] { names[2], names[3], names[4], names[0] };
        }
    }
}