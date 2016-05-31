using System;
using LeagueSharp.Common;

namespace Geass_Tristana.Interface
{
    internal interface IChampionDraw
    {
        Utility.HpBarDamageIndicator.DamageToUnitDelegate DamageToEnemy { get; set; }

        void OnDrawEnemy(EventArgs args);
        void OnDrawSelf(EventArgs args);
    }
}