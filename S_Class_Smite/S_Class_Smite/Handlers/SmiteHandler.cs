using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using S_Class_Smite.Libary;

namespace S_Class_Smite.Handlers
{
    class SmiteHandler : Core
    {
        private const string _MenuNameBase = ".Smite";

        private const string _MenuMonsterNameBase = ".Monsters";
        private const string _MenuMonsterItemBase = _MenuNameBase + ".Monster.";

        private const string _MenuEnemyNameBase = ".Enemies";
        private const string _MenuEnemyItemBase = _MenuNameBase + ".Enemy.";

        public static void Load()
        {
            SMenu.AddSubMenu(Menu());
           // Game.OnUpdate += OnUpdate;
        }

        private static Menu Menu()
        {
            var menu = new Menu(_MenuNameBase, "smiteOptionMenu");

            var monsterMenu = new Menu(_MenuMonsterNameBase, "monsterMenu");
            foreach (var monster in MonsterStructures.MonsterBarDictionary.Keys)
            {
                monsterMenu.AddItem(new MenuItem(_MenuMonsterItemBase + "Boolean.Smite." + monster, ".Smite " + monster.Replace("SRU_","")).SetValue(true));
            }

            var enemyMenu = new Menu(_MenuEnemyNameBase, "enemyMenu");
            enemyMenu.AddItem(new MenuItem(_MenuEnemyItemBase + "Boolean.Smite.Enemy", ".Smite KS Enemies").SetValue(true));

            menu.AddSubMenu(monsterMenu);
            menu.AddSubMenu(enemyMenu);

            return menu;
        }


    }
}
