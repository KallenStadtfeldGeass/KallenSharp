﻿using LeagueSharp;
using System;
using System.Net;
using System.Text.RegularExpressions;

namespace Geass_Tristana.Libaries
{
    internal class Assembly
    {
        private string DownloadServerVersion
        {
            get
            {
                using (var wC = new WebClient()) return wC.DownloadString("https://raw.githubusercontent.com/KallenStadtfeldGeass/KallenSharp/master/S_Plus_Class_Kalista/S_Plus_Class_Kalista/Properties/AssemblyInfo.cs");
            }
        }

        public void CheckVersion()
        {
            try
            {
                var match =
                                   new Regex(
                                       @"\[assembly\: AssemblyVersion\(""(\d{1,})\.(\d{1,})\.(\d{1,})\.(\d{1,})""\)\]")
                                       .Match(DownloadServerVersion);

                if (!match.Success) return;
                var gitVersion =
                    new Version(
                        $"{match.Groups[1]}.{match.Groups[2]}.{match.Groups[3]}.{match.Groups[4]}");

                if (gitVersion <= System.Reflection.Assembly.GetExecutingAssembly().GetName().Version) return;
                Game.PrintChat("<b> <font color=\"#F88017\">S+</font> Class <font color=\"#F88017\">Tristana</font></b> - <font color=\"#008080\">Version:</font>{0} Available!", gitVersion);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Game.PrintChat("<b> <font color=\"#008080\">S+ Class Tristana Unable to check for updates</font></b>");
            }
        }
    }
}