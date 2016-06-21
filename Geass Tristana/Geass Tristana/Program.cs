using Geass_Tristana.Other;
using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace Geass_Tristana
{
    internal class Program : Core
    {
        private static void Main()
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName != "Tristana")
                return;

            GeassLib.Loader.Load();
            Libaries.Loader.LoadAssembly();
        }
    }
}