using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Common;
using S_Class_Smite.Drawing;
using S_Class_Smite.Handlers;
using S_Class_Smite.Libary;

namespace S_Class_Smite
{
    class Program
    {
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += OnLoad;
        }

        static void OnLoad(EventArgs args)
        {
            if (!Smite.Load()) // player does not have smite
                return;

            DrawingHandler.Load();

            SmiteHandler.Load();
            Core.SMenu.AddToMainMenu();

        }
    }
}
