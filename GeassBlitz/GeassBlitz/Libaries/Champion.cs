using LeagueSharp;
using LeagueSharp.Common;

namespace GeassBlitz.Libaries
{
    internal class Champion : GeassLib.Interfaces.Core.Champion
    {
        public Champion(float qRange = 925f, float rRange = 600f)
        {
            GetSpellQ = new Spell(SpellSlot.Q, qRange);
            GetSpellW = new Spell(SpellSlot.W);
            GetSpellE = new Spell(SpellSlot.E);
            GetSpellR = new Spell(SpellSlot.R, rRange);

            GetSpellQ.SetSkillshot(.25f, 70f, 1750f, true, SkillshotType.SkillshotLine);
        }

        public Spell GetSpellE { get; set; }

        public Spell GetSpellQ { get; set; }

        public Spell GetSpellR { get; set; }

        public Spell GetSpellW { get; set; }

        public int GetManaPercent => (int)(Player.Mana / Player.MaxMana * 100);
        public int HealthPercent => (int)(Player.Health / Player.MaxHealth * 100);

        public Obj_AI_Hero Player { get; set; }
    }
}
