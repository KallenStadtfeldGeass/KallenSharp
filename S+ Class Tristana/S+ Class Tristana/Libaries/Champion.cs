using LeagueSharp;
using LeagueSharp.Common;


namespace S__Class_Tristana.Libaries
{
    class Champion
    {
        private Spell _Q, _W, _E, _R;

        public Champion(float _qRange,float _wRange,float _eRange,float _rRange)
        {
            _Q = new Spell(SpellSlot.Q, _qRange);
            _W = new Spell(SpellSlot.W, _wRange);
            _E = new Spell(SpellSlot.E, _eRange);
            _R = new Spell(SpellSlot.R, _rRange);

            _W.SetSkillshot(0.35f, 250f, 1400f, false, SkillshotType.SkillshotCircle);
        }

        public Spell GetSpellQ() => _Q;
        public Spell GetSpellW() => _W;
        public Spell GetSpellE() => _E;
        public Spell GetSpellR() => _R;

        public void UpdateChampionRange(int level)
        {
                _Q.Range = 550 + 9 * (level - 1);
                _E.Range = 625 + 9 * (level - 1);
                _R.Range = 517 + 9 * (level - 1);
        }
        
    }
}
