using LeagueSharp;
using LeagueSharp.Common;

namespace Geass_Tristana.Libaries
{
    internal class Champion
    {
        public Champion(float qRange, float wRange, float eRange, float rRange)
        {
            GetSpellQ = new Spell(SpellSlot.Q, qRange);
            GetSpellW = new Spell(SpellSlot.W, wRange);
            GetSpellE = new Spell(SpellSlot.E, eRange);
            GetSpellR = new Spell(SpellSlot.R, rRange);

            GetSpellW.SetSkillshot(0.35f, 250f, 1400f, false, SkillshotType.SkillshotCircle);
        }

        public Spell GetSpellE { get; }

        public Spell GetSpellQ { get; }

        public Spell GetSpellR { get; }

        public Spell GetSpellW { get; }

        public int GetManaPercent => (int)(Player.Mana / Player.MaxMana * 100);
        public int HealthPercent => (int)(Player.Health / Player.MaxHealth * 100);

        public void UpdateChampionRange(int level)
        {
            GetSpellQ.Range = 550 + (9 * (level - 1));
            GetSpellE.Range = 625 + (9 * (level - 1));
            GetSpellR.Range = 517 + (9 * (level - 1));
        }

        public Obj_AI_Hero Player { get; set; }
    }
}