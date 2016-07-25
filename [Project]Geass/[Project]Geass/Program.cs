using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;
using _Project_Geass.Data;

namespace _Project_Geass
{
    internal class Program
    {
        private static void Main()
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
            if (Names.ChampionBundled.Any(champ => ObjectManager.Player.ChampionName == champ))
            {
                // ReSharper disable once UnusedVariable
                var init = new Bootloaders.Initializer();
            }
        }
    }
}