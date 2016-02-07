using System;
using LeagueSharp;
using LeagueSharp.Common;


namespace S_Plus_Class_Ryze
{
    class Core
    {


        //This Bufftype thing by Hoes
        public static readonly BuffType[] Bufftype =
        {
            BuffType.Snare,
            BuffType.Blind,
            BuffType.Charm,
            BuffType.Stun,
            BuffType.Fear,
            BuffType.Slow,
            BuffType.Taunt,
            BuffType.Suppression
        };

        private const string MenuName = "S+ Class Ryze";
        public static bool MenuLoaded = false;
        public static Menu SMenu { get; set; } = new Menu(MenuName, MenuName, true);
        public static Obj_AI_Hero Player => ObjectManager.Player;

        public static Orbwalking.Orbwalker OrbWalker { get; set; }
    

        public class Time
        {
            private static readonly DateTime AssemblyLoadTime = DateTime.Now;
            public static float LastTick { get; set; } = TickCount;
            public static float TickCount => (int)DateTime.Now.Subtract(AssemblyLoadTime).TotalMilliseconds;
            public static bool CheckLast()
            {
                return TickCount - LastTick > (1000 + Game.Ping / 2);
            }
        }

        public class Champion
        {
            public static Spell Q { get; set; }
            public static Spell QP { get; set; }
            public static Spell W { get; set; }
            public static Spell E { get; set; }
            public static Spell R { get; set; }

            public static void Load()
            {
                //Loads range and shit
                Q = new Spell(SpellSlot.Q, 865);
                QP = new Spell(SpellSlot.Q, 865);
                W = new Spell(SpellSlot.W, 585);
                E = new Spell(SpellSlot.E, 585);
                R = new Spell(SpellSlot.R);

                Champion.Q.SetSkillshot(0.25f, 50f, 1700f, true, SkillshotType.SkillshotLine);
                Champion.QP.SetSkillshot(0.25f, 50f, 1700f, false, SkillshotType.SkillshotLine);
            }
        }

    }
}
