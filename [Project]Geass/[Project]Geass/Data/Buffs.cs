using LeagueSharp;

namespace _Project_Geass.Data
{
    internal static class Buffs
    {
        public static BuffType[] GetTypes { get; } = {
            BuffType.Snare,
            BuffType.Blind,
            BuffType.Charm,
            BuffType.Stun,
            BuffType.Fear,
            BuffType.Slow,
            BuffType.Taunt,
            BuffType.Suppression
        };
    }
}