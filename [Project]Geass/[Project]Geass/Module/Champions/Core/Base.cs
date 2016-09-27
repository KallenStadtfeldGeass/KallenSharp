using _Project_Geass.Functions;
using LeagueSharp.Common;
using System;
using System.Collections.Generic;

namespace _Project_Geass.Module.Champions.Core
{
    internal class Base
    {
        public readonly string BaseName = Names.ProjectName + StaticObjects.Player.ChampionName + ".";

        /// <summary>
        ///     The RNG :D
        /// </summary>
        public readonly Random Rng;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Base" /> class.
        /// </summary>
        /// <param name="q">The q.</param>
        /// <param name="w">The w.</param>
        /// <param name="e">The e.</param>
        /// <param name="r">The r.</param>
        /// <param name="rng">The RNG.</param>
        public Base(Spell q, Spell w, Spell e, Spell r, Random rng)
        {
            Rng = rng;
            Q = q;
            W = w;
            E = e;
            R = r;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Base" /> class.
        /// </summary>
        public Base()
        {
            Rng = new Random();
        }

        /// <summary>
        ///     Champion Spells
        /// </summary>
        /// <value>
        ///     Champion spells.
        /// </value>
        public Dictionary<object, object> Spells { get; } = new Dictionary<object, object>();

        public Spell Q { get; set; }
        public Spell W { get; set; }
        public Spell E { get; set; }
        public Spell R { get; set; }

        /// <summary>
        ///     Orbwalker
        /// </summary>
        /// <value>
        ///     The orbwalker.
        /// </value>
        public Orbwalking.Orbwalker Orbwalker { get; set; }
    }
}