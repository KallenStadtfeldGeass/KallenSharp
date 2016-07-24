using LeagueSharp.Common;
using _Project_Geass.Globals;

namespace _Project_Geass.Bootloaders.Champions.Base
{
    class Champion
    {
        public Spell GetSpellE { get; set; }

        public Spell GetSpellQ { get; set; }

        public Spell GetSpellR { get; set; }

        public Spell GetSpellW { get; set; }

        public int GetManaPercent => (int)(Static.Objects.Player.Mana / Static.Objects.Player.MaxMana * 100);
        public int HealthPercent => (int)(Static.Objects.Player.Health / Static.Objects.Player.MaxHealth * 100);

    }
}
