﻿using LeagueSharp.Common;
using SharpDX;
using _Project_Geass.Data.Static;

namespace _Project_Geass.Module.Core.OnLevel.Menus
{

    internal sealed class Abilities
    {
        #region Public Constructors

        public Abilities(Menu menu, int[] abiSeq, bool enabled)
        {
            if (!enabled)
                return;

            menu.AddSubMenu(Menu());

            // ReSharper disable once UnusedVariable
            var onLevel=new Events.Abilities(abiSeq);

            Objects.ProjectLogger.WriteLog("OnLevel Menu and events loaded.");
        }

        #endregion Public Constructors

        #region Private Methods

        private Menu Menu()
        {
            var menu=new Menu(Names.Menu.LevelNameBase, "levelMenu");
            menu.AddItem(new MenuItem(Names.Menu.LevelItemBase+"Boolean.AutoLevelUp", "Auto Level").SetValue(true)).SetTooltip("Auto Level Up skills", Color.Aqua);
            return menu;
        }

        #endregion Private Methods
    }

}