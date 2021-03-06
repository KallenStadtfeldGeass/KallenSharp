﻿using System.Collections.Generic;
using LeagueSharp.Common;
using _Project_Geass.Data.Static;
using _Project_Geass.Module.Champions.Heroes.Events;

namespace _Project_Geass.Module.Champions.Core
{

    internal class Bootloader
    {

        /// <summary>
        ///     The champion bundled
        /// </summary>
        public static readonly Dictionary<string, bool> ChampionBundled=new Dictionary<string, bool> {{nameof(Ashe), true}, {nameof(Caitlyn), false}, {nameof(Ezreal), true}, {"Graves", false}, {nameof(Kalista), false}, {nameof(Tristana), true}};

        public static void Load(bool manaEnabled, Orbwalking.Orbwalker orbWalker)
#pragma warning restore CC0091 // Use static method
        {
            switch (Objects.Player.ChampionName)
            {
                case nameof(Tristana):
                    // ReSharper disable once UnusedVariable
                    var tristana=new Tristana(manaEnabled, orbWalker);
                    break;

                case nameof(Ezreal):
                    // ReSharper disable once UnusedVariable
                    var ezreal=new Ezreal(manaEnabled, orbWalker);
                    break;

                case nameof(Ashe):
                    // ReSharper disable once UnusedVariable
                    var ashe=new Ashe(manaEnabled, orbWalker);
                    break;
            }
        }

    }

}