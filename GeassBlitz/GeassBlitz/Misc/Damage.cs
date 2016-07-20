using LeagueSharp;
using LeagueSharp.Common;

namespace GeassBlitz.Misc
{
    internal class Damage : Core
    {

        private float GetComboDamage(Obj_AI_Base target)
    {
        float damage = 0f;
            float range = 0f;

        if (GeassLib.Functions.Calculations.Damage.CheckNoDamageBuffs((Obj_AI_Hero)target)) return damage;

            if (Champion.GetSpellQ.IsReady() && target.IsValidTarget(Champion.GetSpellQ.Range))
            {
                range = Champion.GetSpellQ.Range;
                damage += Champion.GetSpellQ.GetDamage(target);
            }


            if (Champion.GetSpellE.IsReady())
                if (Champion.Player.Distance(target) - range < Champion.Player.AttackRange)
                    damage += (float)Champion.Player.GetAutoAttackDamage(target) +
                        (float)Champion.Player.CalcDamage(target, LeagueSharp.Common.Damage.DamageType.Physical, (Champion.Player.TotalAttackDamage - Champion.Player.BaseAttackDamage));


            if (Champion.GetSpellR.IsReady())
            if (Champion.Player.Distance(target) - range < Champion.GetSpellR.Range)
                damage += Champion.GetSpellR.GetDamage(target);


            return damage;
    }

        public float CalcDamage(Obj_AI_Base target)
        {
            return GeassLib.Functions.Calculations.Damage.CalcRealDamage(target, GetComboDamage(target));
        }
}
}
