using LeagueSharp.Common;
using System;

namespace _Project_Geass
{
    internal class Program
    {
        #region Private Methods

        /// <summary>
        /// Defines the entry point of the application. 
        /// </summary>
        private static void Main()
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        private static void OnLoad(EventArgs args)
        {
            // ReSharper disable once UnusedVariable
            var init = new Initializer();
        }

        #endregion Private Methods
    }
}