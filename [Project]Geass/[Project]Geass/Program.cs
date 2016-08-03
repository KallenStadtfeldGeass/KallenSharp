using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;
using _Project_Geass.Data;

namespace _Project_Geass
{
    internal class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        private static void Main()
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        /// <summary>
        /// Raises the <see cref="E:Load" /> event.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs"/> instance containing the event data.</param>
        
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