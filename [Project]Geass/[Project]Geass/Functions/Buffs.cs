using LeagueSharp;

namespace _Project_Geass.Global.Data
{

    internal static class Buffs
    {
        #region Public Properties

        public static BuffType[] GetTypes{get;} = {BuffType.Snare, BuffType.Blind, BuffType.Charm, BuffType.Stun, BuffType.Fear, BuffType.Slow, BuffType.Taunt, BuffType.Suppression};

        #endregion Public Properties
    }

}