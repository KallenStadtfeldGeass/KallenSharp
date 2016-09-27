using _Project_Geass.Logging;
using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace _Project_Geass.Functions
{
    internal class StaticObjects
    {
        public static Logger ProjectLogger = new Logger(Names.ProjectName);
        public static Menu SettingsMenu = new Menu(Names.SettingsName, Names.SettingsName, true);

        public static Obj_AI_Hero Player = ObjectManager.Player;
        public static DateTime AssemblyLoadTime = DateTime.Now;

        public static Menu ProjectMenu = new Menu($"{Names.ProjectName}.{Player.ChampionName}",
            $"{Names.ProjectName}.{Player.ChampionName}", true);
    }
}