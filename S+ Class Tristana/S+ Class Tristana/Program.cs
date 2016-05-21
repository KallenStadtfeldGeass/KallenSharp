using LeagueSharp;
using LeagueSharp.Common;
using S__Class_Tristana.Other;
using System;

namespace S__Class_Tristana
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

            var loader = new Libaries.Loader();
            loader.LoadAssembly();
        }
    }
}