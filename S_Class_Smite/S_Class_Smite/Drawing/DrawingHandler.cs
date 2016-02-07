using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp.Common;
using S_Class_Smite.Libary;

namespace S_Class_Smite.Drawing
{
    class DrawingHandler : Core
    {
        private const string _MenuNameBase = ".Drawing Menu";
        private const string _MenuItemBase = ".Drawing.";
        public static void Load()
        {
            var _Menu = new Menu(_MenuNameBase, "drawingMenu");
            _Menu.AddSubMenu(DrawingOnMonsters.DrawingMonsterMenu());
            Core.SMenu.AddSubMenu(_Menu);

           // DrawingOnMonsters.DamageToMonster = Smite.SmiteDamage;
            LeagueSharp.Drawing.OnDraw += DrawingOnMonsters.OnDrawMonster;

        }
    }
}
