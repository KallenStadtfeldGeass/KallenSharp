using System;
using LeagueSharp.Common;

namespace GeassLib.Interfaces.Drawing
{
    public interface Champion
    {
        Utility.HpBarDamageIndicator.DamageToUnitDelegate DamageToEnemy { get; set; }

        void OnDrawEnemy(EventArgs args);
        void OnDrawSelf(EventArgs args);
    }
}
