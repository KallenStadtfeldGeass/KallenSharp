using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace _Project_Geass.Bootloaders.Champions
{
    class Ashe : Base.Champion
    {

        public Ashe()
        {
           GetSpellQ = new Spell(SpellSlot.Q);
           GetSpellW = new Spell(SpellSlot.W,1200);
           GetSpellR = new Spell(SpellSlot.R,2200);

           GetSpellW.SetSkillshot(.25f,57.5f,2000,true,SkillshotType.SkillshotCone);
           GetSpellR.SetSkillshot(.25f,250,1600,false,SkillshotType.SkillshotLine);

            Game.OnUpdate += OnUpdate;
            LeagueSharp.Drawing.OnDraw += OnDraw;
            AntiGapcloser.OnEnemyGapcloser += OnGapcloser;
            Interrupter2.OnInterruptableTarget += OnInterruptable;
            Orbwalking.AfterAttack += AfterAttack;
        }

        private void OnUpdate(EventArgs args)
        {
            
        }

        private void AfterAttack(AttackableUnit unit, AttackableUnit target)
        {
            
        }

        private void OnGapcloser(ActiveGapcloser gapcloser)
        {
            
        }

        private void OnInterruptable(Obj_AI_Hero sender,Interrupter2.InterruptableTargetEventArgs args)
        {
            
        }

        private void OnDraw(EventArgs args)
        {
            
        }
    }
}
