using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace S_Class_Smite.Libary
{
    class Smite : Core
    {
        readonly static float[] SmiteDamageArr = {390f,410f,430f,450f,480f,510f,540f,570f,600f,640f,720f,760f,800f,850f,900f,950f,1000f};
        public static bool Load()
        {
            foreach (var spell in ObjectManager.Player.Spellbook.Spells)
            {
                if (spell.Name.ToLower().Contains("smite"))
                {
                    SmiteSpell = new Spell(spell.Slot, 500, TargetSelector.DamageType.True);
                    SmiteSlot = spell.Slot;
                }
            }
            return SmiteSpell != null;
        }

        public static float SmiteDamage(Obj_AI_Base target) // for linking
        {
            return Player.Spellbook.GetSpell(SmiteSpell.Slot).State == SpellState.Ready
                      ? (float)Player.GetSummonerSpellDamage(target, Damage.SummonerSpell.Smite)
                      : 0;
        }

    }
}
