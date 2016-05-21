using LeagueSharp;
using LeagueSharp.Common;

namespace S__Class_Tristana.Libaries
{
    internal class Champion
    {
        private readonly Spell _q, _w, _e, _r;

        public Champion(float qRange, float wRange, float eRange, float rRange)
        {
            _q = new Spell(SpellSlot.Q, qRange);
            _w = new Spell(SpellSlot.W, wRange);
            _e = new Spell(SpellSlot.E, eRange);
            _r = new Spell(SpellSlot.R, rRange);

            _w.SetSkillshot(0.35f, 250f, 1400f, false, SkillshotType.SkillshotCircle);
        }

        public Spell GetSpellE => _e;

        public Spell GetSpellQ => _q;

        public Spell GetSpellR => _r;

        public Spell GetSpellW => _w;

        public void UpdateChampionRange(int level)
        {
            _q.Range = 550 + 9 * (level - 1);
            _e.Range = 625 + 9 * (level - 1);
            _r.Range = 517 + 9 * (level - 1);
        }

        public Obj_AI_Hero Player { get; set; }
    }
}