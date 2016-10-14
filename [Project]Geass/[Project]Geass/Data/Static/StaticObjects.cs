using System;
using System.Drawing;
using LeagueSharp;
using LeagueSharp.Common;
using _Project_Geass.Logging;
using Color = SharpDX.Color;

namespace _Project_Geass.Data.Static
{

    internal class Objects
    {
        #region Public Fields

        public static DateTime AssemblyLoadTime=DateTime.Now;
        public static Obj_AI_Hero Player=ObjectManager.Player;
        public static Logger ProjectLogger=new Logger(Names.ProjectName);
        public static Menu ProjectMenu=new Menu($"{Names.ProjectName}.{Player.ChampionName}", $"{Names.ProjectName}.{Player.ChampionName}", true).SetFontStyle(FontStyle.Bold, Color.LightSkyBlue);
        public static Menu SettingsMenu=new Menu(Names.SettingsName, Names.SettingsName, true).SetFontStyle(FontStyle.Bold, Color.GreenYellow);

        #endregion Public Fields
    }

}