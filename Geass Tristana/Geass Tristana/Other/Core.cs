﻿using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace Geass_Tristana.Other
{
    internal class Core
    {
        private const string AssemblyName = "S+ Class Tristana [BETA]";

        //Hold The Game "ticks"
        public float AssemblyTime() => (float)DateTime.Now.Subtract(_assemblyLoadTime).TotalMilliseconds;

        //Global External Classes and Variables
        public static Orbwalking.Orbwalker CommonOrbwalker { get; set; }

        public static Menu SMenu { get; set; } = new Menu(AssemblyName, AssemblyName, true);
        // //Private Core Crap
        //private static readonly Libaries.Assembly Assembly = new Libaries.Assembly();

        private readonly DateTime _assemblyLoadTime = DateTime.Now;

        //Hold Global Data and Functions
        public static Libaries.Champion Champion = new Libaries.Champion(550f, 900f, 625f, 700f);

        public static Humanizer.TickManager TickManager = new Humanizer.TickManager();

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
    }
}