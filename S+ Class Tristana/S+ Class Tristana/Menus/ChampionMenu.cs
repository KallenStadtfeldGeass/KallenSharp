﻿using LeagueSharp.Common;
using Color = System.Drawing.Color;

namespace S__Class_Tristana.Menus
{
    internal class ChampionMenu : Drawing.Champs
    {
        private Menu _Menu
        {
            get
            {
                var menu = new Menu(MenuNameBase, "enemyMenu");

                var enemyMenu = new Menu(".Enemys", "enemyMenu");
                enemyMenu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnEnemy", "Draw On Enemys").SetValue(true));
                enemyMenu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnEnemy.FillColor", "Combo Damage Fill").SetValue(new Circle(true, Color.DarkGray)));
                enemyMenu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnEnemy.KillableColor", "Killable Text").SetValue(new Circle(true, Color.DarkGray)));

                var selfMenu = new Menu(".Self", "selfMenu");
                selfMenu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnSelf", "Draw On Self").SetValue(true));
                selfMenu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnSelf.ComboColor", "Combo Range (R)").SetValue(new Circle(true, Color.OrangeRed)));
                selfMenu.AddItem(new MenuItem(MenuItemBase + "Boolean.DrawOnSelf.WColor", "W Range").SetValue(new Circle(true, Color.Green)));

                menu.AddSubMenu(enemyMenu);
                menu.AddSubMenu(selfMenu);

                return menu;
            }
        }

        public void Load()
        {
            SMenu.AddSubMenu(_Menu);
            var champs = new Drawing.Champs();
            champs.DamageToEnemy = champs.Damage.CalculateDamage;

            LeagueSharp.Drawing.OnDraw += champs.OnDrawEnemy;
            LeagueSharp.Drawing.OnDraw += champs.OnDrawSelf;
        }
    }
}