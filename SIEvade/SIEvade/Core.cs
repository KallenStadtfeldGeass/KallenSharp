using System;
using System.Reflection;
using LeagueSharp;
using LeagueSharp.Common;
using Version = System.Version;

namespace SIEvade
{ 
    class Core
    {
        private static string MenuName = "Should I Evade";
        public static Menu SMenu { get; set; } = new Menu(MenuName, MenuName, true);
        public static Obj_AI_Hero Player;
        public static Version AssemblyVersion;
        public static Menu EzEvadeMenu;

        public static void Load()
        {
            Player = ObjectManager.Player;
            AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
            EzEvadeMenu = Menu.GetMenu("ezEvade", "ezEvade");
        }
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
