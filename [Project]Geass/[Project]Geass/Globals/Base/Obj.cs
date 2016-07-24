using _Project_Geass.Logging;
using LeagueSharp;
using LeagueSharp.Common;
using System;

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
            ProjectLogger = new Logger(Constants.Names.ProjectName);
            SettingsMenu = new Menu(Constants.Names.SettingsName, Constants.Names.SettingsName, true);
            Player = ObjectManager.Player;
            ProjectMenu = new Menu($"{Constants.Names.ProjectName}.{Player.ChampionName}", $"{Constants.Names.ProjectName}.{Player.ChampionName}", true);
            AssemblyLoadTime = DateTime.Now;
        }
    }
}