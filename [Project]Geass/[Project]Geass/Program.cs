using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;

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
            if (Constants.Names.ChampionBundled.Any(champ => ObjectManager.Player.ChampionName == champ))
            {
                // ReSharper disable once UnusedVariable
                var init = new Bootloaders.Initializer();
            }
        }
    }
}