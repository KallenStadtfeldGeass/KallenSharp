using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S__Class_Tristana.Libaries
{
    class Damage : Core
    {
        public static bool CheckNoDamageBuffs(Obj_AI_Hero target)
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

        public static string[] ShieldBuffNames = ShieldNames.Split(',');

        public static int GetRendCount(Obj_AI_Base target)
        {
            return target.GetBuffCount("kalistaexpungemarker");
        }

        public static float GetShield(Obj_AI_Base target)
        {
            return ShieldBuffNames.Any(target.HasBuff) ? target.AllShield : 0;
        }

        public static float CalculateDamage(Obj_AI_Base target,float fdamage)
        {
            var defuffer = 1f;

            if (target.HasBuff("FerociousHowl") || target.HasBuff("GarenW"))
                defuffer *= .7f;

            if (target.HasBuff("Medidate"))
                defuffer *= .5f - target.Spellbook.GetSpell(SpellSlot.E).Level * .05f;

            if (target.HasBuff("gragaswself"))
                defuffer *= .9f - target.Spellbook.GetSpell(SpellSlot.W).Level * .02f;

            if (target.Name.Contains("Baron") && Player.HasBuff("barontarget"))
                defuffer *= 0.5f;

            if (target.Name.Contains("Dragon") && Player.HasBuff("s5test_dragonslayerbuff"))
                defuffer *= (1 - (.07f * Player.GetBuffCount("s5test_dragonslayerbuff")));

            if (Player.HasBuff("summonerexhaust"))
                defuffer *= .4f;


            if (!target.IsChampion()) return (fdamage * defuffer);

            var healthDebuffer = 0f;
            var hero = (Obj_AI_Hero)target;

            if (hero.ChampionName == "Blitzcrank" && !target.HasBuff("BlitzcrankManaBarrierCD") && !target.HasBuff("ManaBarrier"))
                healthDebuffer += target.Mana / 2;

            return (fdamage * defuffer) - (healthDebuffer + GetShield(target) + target.FlatHPRegenMod + 15);
        }
    }
}
