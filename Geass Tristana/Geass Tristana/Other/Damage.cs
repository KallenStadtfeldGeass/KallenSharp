using LeagueSharp;
using LeagueSharp.Common;
using System.Linq;

namespace Geass_Tristana.Other
{
    internal class Damage : Core
    {
        
        private float GetComboDamage(Obj_AI_Base target)
        {
            float damage = 0f;

            if (GeassLib.Functions.Calculations.Damage.CheckNoDamageBuffs((Obj_AI_Hero)target))return damage;
            
            //if (!Champion.Player.IsWindingUp) // can auto attack
            //    if (Champion.Player.Distance(target) < Champion.Player.AttackRange) // target in auto range
            //        damage += (float)Champion.Player.GetAutoAttackDamage(target) - 50;

            if (Champion.GetSpellR.IsReady())
                if (Champion.Player.Distance(target) < Champion.GetSpellR.Range)
                    damage += Champion.GetSpellR.GetDamage(target);

            if (target.HasBuff("tristanaecharge"))
            {
                int count = target.GetBuffCount("tristanaecharge");
                if (!Champion.Player.IsWindingUp)
                    if (Champion.Player.Distance(target) < Champion.Player.AttackRange) // target in auto range
                        count++;

                damage += (float)(Champion.GetSpellE.GetDamage(target) * (count * 0.30)) + Champion.GetSpellE.GetDamage(target);

                return damage;
            }

            if (Champion.GetSpellE.IsReady())
                if (Champion.Player.Distance(target) < Champion.GetSpellE.Range)
                    damage += (float)(Champion.GetSpellE.GetDamage(target) * 0.30) + Champion.GetSpellE.GetDamage(target); // 1 auto charge

            return damage;
        }

        public float CalculateDamage(Obj_AI_Base target)
        {
            if (GeassLib.Functions.Calculations.Damage.CheckNoDamageBuffs((Obj_AI_Hero)target)) return 0f;

            var defuffer = 1f;

            if (target.HasBuff("FerociousHowl") || target.HasBuff("GarenW"))
                defuffer *= .7f;

            if (target.HasBuff("Medidate"))
                defuffer *= .5f - target.Spellbook.GetSpell(SpellSlot.E).Level * .05f;

            if (target.HasBuff("gragaswself"))
                defuffer *= .9f - target.Spellbook.GetSpell(SpellSlot.W).Level * .02f;

            if (target.Name.Contains("Baron") && Champion.Player.HasBuff("barontarget"))
                defuffer *= 0.5f;

            if (target.Name.Contains("Dragon") && Champion.Player.HasBuff("s5test_dragonslayerbuff"))
                defuffer *= (1 - (.07f * Champion.Player.GetBuffCount("s5test_dragonslayerbuff")));

            if (Champion.Player.HasBuff("summonerexhaust"))
                defuffer *= .4f;

            if (!target.IsChampion()) return (GetComboDamage(target) * defuffer);

            var healthDebuffer = 0f;
            var hero = (Obj_AI_Hero)target;

            if (hero.ChampionName == "Blitzcrank" && !target.HasBuff("BlitzcrankManaBarrierCD") && !target.HasBuff("ManaBarrier"))
                healthDebuffer += target.Mana / 2;

            return (GetComboDamage(target) * defuffer) - (healthDebuffer + GeassLib.Functions.Calculations.Damage.GetShield(target) + target.FlatHPRegenMod + 15);
        }
    }
}