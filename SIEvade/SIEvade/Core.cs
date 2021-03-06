﻿using System;
using System.Reflection;
using LeagueSharp;
using LeagueSharp.Common;
using Version = System.Version;

namespace SIEvade
{
    internal class Core
    {
        private const string MenuName = "Should I Evade";
        public static bool MenuLoaded = false;
        public static Menu SMenu { get; set; } = new Menu(MenuName, MenuName, true);
        public static Obj_AI_Hero Player = ObjectManager.Player;
        public static Version AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;

        public static Menu EzEvadeMenu;// = Menu.GetMenu("ezEvade", "ezEvade");
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