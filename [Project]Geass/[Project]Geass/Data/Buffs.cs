using LeagueSharp;

namespace _Project_Geass.Data
{
    internal static class Buffs
    {
        private static readonly BuffType[] Bufftype =
        {
            BuffType.Snare,
            BuffType.Blind,
            BuffType.Charm,
            BuffType.Stun,
            BuffType.Fear,
            BuffType.Slow,
            BuffType.Taunt,
            BuffType.Suppression
        };

        public static BuffType[] GetTypes => Bufftype;
    }
}