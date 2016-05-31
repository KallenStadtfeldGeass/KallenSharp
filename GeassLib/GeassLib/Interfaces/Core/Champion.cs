using LeagueSharp;
using LeagueSharp.Common;

namespace GeassLib.Interfaces.Core
{
    public interface Champion
    {
        int GetManaPercent { get; }
        int HealthPercent { get; }
        Spell GetSpellE { get; set; }
        Spell GetSpellQ { get; set; }
        Spell GetSpellR { get; set; }
        Spell GetSpellW { get; set; }
        Obj_AI_Hero Player { get; set; }

    }
}
