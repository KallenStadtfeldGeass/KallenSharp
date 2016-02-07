using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;

namespace S_Class_Smite.Libary
{
    class Core
    {

        private const string MenuName = "S Class Smite";
        public static Menu SMenu { get; set; } = new Menu(MenuName, MenuName, true);
        public static Obj_AI_Hero Player => ObjectManager.Player;
        public static Obj_AI_Hero SoulBoundHero { get; set; }
        public static Spell SmiteSpell;
        public static SpellSlot SmiteSlot;
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
    }
}
