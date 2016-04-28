using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana
{
    class Damage : Core
    {
        public bool CheckNoDamageBuffs(Obj_AI_Hero target)
        {
            foreach (var b in target.Buffs.Where(b => b.IsValidBuff()))
            {
                switch (b.DisplayName)
                {
                    case "Chrono Shift":
                        return true;
                    case "JudicatorIntervention":
                        return true;
                    case "Undying Rage":
                        if (target.ChampionName == "Tryndamere")
                            return true;
                        continue;

                    //Spell Shields
                    case "bansheesveil":
                        return true;
                    case "SivirE":
                        return true;
                    case "NocturneW":
                        return true;
                    case "kindredrnodeathbuff":
                        return true;
                }
            }
            if (target.ChampionName == "Poppy" && HeroManager.Allies.Any(
                o =>
                {
                    return !o.IsMe
                           && o.Buffs.Any(
                               b =>
                                   b.Caster.NetworkId == target.NetworkId && b.IsValidBuff()
                                   && b.DisplayName == "PoppyDITarget");
                }))
            {
                return true;
            }

            return (target.HasBuffOfType(BuffType.Invulnerability)
                    || target.HasBuffOfType(BuffType.SpellImmunity));
            // || target.HasBuffOfType(BuffType.SpellShield));
        }

        private const string ShieldNames = "blindmonkwoneshield,evelynnrshield,EyeOfTheStorm,ItemSeraphsEmbrace,JarvanIVGoeldenAegis,KarmaSolKimShield,lulufarieshield,luxprismaticwaveshieldself,manabarrier,mordekaiserironman,nautiluspiercinggazeshield,orianaredactshield,rumbleshieldbuff,Shenstandunitedshield,SkarnerExoskeleton,summonerbarrier,tahmkencheshield,udyrturtleactivation,UrgotTerrorCapacitorActive2,ViktorPowerTransfer,dianashield,malphiteshieldeffect,RivenFeint,ShenStandUnited,sionwshieldstacks,vipassivebuff";

        public string[] ShieldBuffNames = ShieldNames.Split(',');

        public float GetShield(Obj_AI_Base target)
        {
            return ShieldBuffNames.Any(target.HasBuff) ? target.AllShield : 0;
        }


        private float GetComboDamage(Obj_AI_Base target)
        {
            float damage = 0f;

            if (!_Champion.Player.IsWindingUp) // can auto attack         
                if(_Champion.Player.Distance(target) < _Champion.Player.AttackRange) // target in auto range
                   damage += (float)_Champion.Player.GetAutoAttackDamage(target);
            
            if(_Champion.GetSpellR().IsReady())
                if(_Champion.Player.Distance(target) < _Champion.GetSpellR().Range)
                    damage += (float)_Champion.GetSpellR().GetDamage(target);

            if (target.HasBuff("tristanaecharge"))
            {
                int count = target.GetBuffCount("tristanaecharge");
                if (!_Champion.Player.IsWindingUp)
                    if (_Champion.Player.Distance(target) < _Champion.Player.AttackRange) // target in auto range
                        count++;

                    damage += (float)(_Champion.GetSpellE().GetDamage(target) * (count * 0.30)) + _Champion.GetSpellE().GetDamage(target);

                return damage;
            }

            if (_Champion.GetSpellE().IsReady())
                if (_Champion.Player.Distance(target) < _Champion.GetSpellE().Range)
                    damage += (float)(_Champion.GetSpellE().GetDamage(target) * 0.30) + _Champion.GetSpellE().GetDamage(target); // 1 auto charge

            return damage;
        }

        public float CalculateDamage(Obj_AI_Base target)
        {
            var defuffer = 1f;

            if (target.HasBuff("FerociousHowl") || target.HasBuff("GarenW"))
                defuffer *= .7f;

            if (target.HasBuff("Medidate"))
                defuffer *= .5f - target.Spellbook.GetSpell(SpellSlot.E).Level * .05f;

            if (target.HasBuff("gragaswself"))
                defuffer *= .9f - target.Spellbook.GetSpell(SpellSlot.W).Level * .02f;

            if (target.Name.Contains("Baron") && _Champion.Player.HasBuff("barontarget"))
                defuffer *= 0.5f;

            if (target.Name.Contains("Dragon") && _Champion.Player.HasBuff("s5test_dragonslayerbuff"))
                defuffer *= (1 - (.07f * _Champion.Player.GetBuffCount("s5test_dragonslayerbuff")));

            if (_Champion.Player.HasBuff("summonerexhaust"))
                defuffer *= .4f;


            if (!target.IsChampion()) return (GetComboDamage(target) * defuffer);

            var healthDebuffer = 0f;
            var hero = (Obj_AI_Hero)target;

            if (hero.ChampionName == "Blitzcrank" && !target.HasBuff("BlitzcrankManaBarrierCD") && !target.HasBuff("ManaBarrier"))
                healthDebuffer += target.Mana / 2;

            return (GetComboDamage(target) * defuffer) - (healthDebuffer + GetShield(target) + target.FlatHPRegenMod + 15);
        }
    }
}
