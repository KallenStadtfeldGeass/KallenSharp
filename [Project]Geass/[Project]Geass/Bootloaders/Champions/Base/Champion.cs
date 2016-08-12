using _Project_Geass.Globals;
using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders.Champions.Base
{
    internal class Champion
    {
        public static Spell GetSpellE { get; set; }

        public static Spell GetSpellQ { get; set; }

        public static Spell GetSpellR { get; set; }

        public static Spell GetSpellW { get; set; }

        public static int GetManaPercent => (int)(Static.Objects.Player.Mana / Static.Objects.Player.MaxMana * 100);
        public static int HealthPercent => (int)(Static.Objects.Player.Health / Static.Objects.Player.MaxHealth * 100);
    }
}