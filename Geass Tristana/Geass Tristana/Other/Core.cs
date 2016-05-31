﻿using LeagueSharp;
using LeagueSharp.Common;
using GeassLib;
namespace Geass_Tristana.Other
{
#pragma warning disable RECS0014 // If all fields, properties and methods members are static, the class can be made static.
    internal class Core
#pragma warning restore RECS0014 // If all fields, properties and methods members are static, the class can be made static.
    {
        private const string AssemblyName = "Geass Tristana [B]";

        //Global External Classes and Variables
        public static Orbwalking.Orbwalker CommonOrbwalker { get; set; }

        public static Menu SMenu { get; set; } = new Menu(AssemblyName, AssemblyName, true);

        //Hold Global Data and Functions
        public static Libaries.Champion Champion = new Libaries.Champion(550f, 900f, 625f, 700f);

        public static GeassLib.Humanizer.TickManager TickManager = new GeassLib.Humanizer.TickManager();

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