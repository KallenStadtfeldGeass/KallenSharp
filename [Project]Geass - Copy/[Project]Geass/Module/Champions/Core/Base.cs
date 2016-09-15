using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;

namespace _Project_Geass.Module.Champions.Core
{
    internal class Base
    {
        public static Dictionary<object, object> Spells { get; } = new Dictionary<object, object>();

        public static Spell Q { get; set; }
        public static Spell W { get; set; }
        public static Spell E { get; set; }
        public static Spell R { get; set; }

        public readonly Random Rng;

        public static Orbwalking.Orbwalker Orbwalker { get; set; }

        public Base(Spell q, Spell w, Spell e, Spell r, Random rng)
        {
            Rng = rng;
            Q = q;
            W = w;
            E = e;
            R = r;
        }

        public Base()
        {
            Rng = new Random();
        }

        public readonly string BaseName = Names.ProjectName + Static.Objects.Player.ChampionName + ".";
    }
}