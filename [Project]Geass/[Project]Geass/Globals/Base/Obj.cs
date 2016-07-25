using _Project_Geass.Logging;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using _Project_Geass.Data;

namespace _Project_Geass.Globals.Base
{
    internal class Obj
    {
        public Logger ProjectLogger { get; set; }
        public Menu ProjectMenu { get; set; }
        public Menu SettingsMenu { get; set; }

        public Obj_AI_Hero Player { get; set; }
        public DateTime AssemblyLoadTime;

        public Obj()
        {
            ProjectLogger = new Logger(Names.ProjectName);
            SettingsMenu = new Menu(Names.SettingsName, Names.SettingsName, true);
            Player = ObjectManager.Player;
            ProjectMenu = new Menu($"{Names.ProjectName}.{Player.ChampionName}", $"{Names.ProjectName}.{Player.ChampionName}", true);
            AssemblyLoadTime = DateTime.Now;
        }
    }
}