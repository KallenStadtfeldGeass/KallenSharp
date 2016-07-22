using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace _Project_Geass
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
            foreach (var champ in Constants.Names.ChampionBundled)
            {
                if (ObjectManager.Player.ChampionName != champ) continue;
                var init = new Bootloaders.Initializer();
                return;
            }
        }
    }
}