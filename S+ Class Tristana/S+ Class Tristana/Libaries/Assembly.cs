﻿using LeagueSharp;
using System;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

namespace S__Class_Tristana.Libaries
{
    class Assembly
    {
        public String GetName()
        {
            return "S+ Class Tristana";
        }

        public void CheckVersion()
        {
            try
            {
                var match =
                                   new Regex(
                                       @"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]")
                                       .Match(DownloadServerVersion());

                if (!match.Success) return;
                var gitVersion =
                    new Version(
                        string.Format(
                            "{0}.{1}.{2}.{3}",
                            match.Groups[1],
                            match.Groups[2],
                            match.Groups[3],
                            match.Groups[4]));

                if (gitVersion <= System.Reflection.Assembly.GetExecutingAssembly().GetName().Version) return;
                Game.PrintChat("<b> <font color=\"#F88017\">S+</font> Class <font color=\"#F88017\">Tristana</font></b> - <font color=\"#008080\">Version:</font>{0} Available!", gitVersion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Game.PrintChat("<b> <font color=\"#008080\">S+ Class Tristana Unable to check for updates</font></b>");
            }
        }

        private string DownloadServerVersion()
        {
            using (var wC = new WebClient()) return wC.DownloadString("https://raw.githubusercontent.com/KallenStadtfeldGeass/KallenSharp/master/S_Plus_Class_Kalista/S_Plus_Class_Kalista/Properties/AssemblyInfo.cs");
        }
    }
}