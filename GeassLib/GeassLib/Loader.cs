using System;
using LeagueSharp;

namespace GeassLib
{
    public class Loader
    {
        public static Obj_AI_Hero Player { get; set; }
        public static void Load()
        {
            AssemblyLoadTime = DateTime.Now;
            Player = ObjectManager.Player;
        }

        public static DateTime AssemblyLoadTime;

        public static float AssemblyTime() => (float)DateTime.Now.Subtract(AssemblyLoadTime).TotalMilliseconds;
    }
}