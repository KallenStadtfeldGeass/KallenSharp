using GeassBlitz.Misc;
using LeagueSharp;


namespace GeassBlitz.Libaries
{
    internal class Loader : Core
    {
        private static GeassLib.Loader _loader;

        public static void LoadAssembly()
        {

            //Initilize Menus

            //Load Data
            Champion.Player = ObjectManager.Player;

            //Load Menus into SMenu


            _loader = new GeassLib.Loader($"{Champion.Player.ChampionName}", false, true, Data.Level.AbilitySequence, true, true);

            //Add SMenu to main menu
            SMenu.AddToMainMenu();
        }
    }
}
