using LeagueSharp.Common;
using Color = System.Drawing.Color;


namespace S__Class_Tristana.Menus
{
    class ChampionMenu : Drawing.Champs
    {

        public void Load()
        {
            SMenu.AddSubMenu(_Menu());
            Drawing.Champs _Champs = new Drawing.Champs();
            _Champs.DamageToEnemy = _Champs._Damage.CalculateDamage;
      
            LeagueSharp.Drawing.OnDraw += _Champs.OnDrawEnemy;
            LeagueSharp.Drawing.OnDraw += _Champs.OnDrawSelf;      
        }
        private Menu _Menu()
        {
            var menu = new Menu(_MenuNameBase, "enemyMenu");

            var enemyMenu = new Menu(".Enemys", "enemyMenu");
            enemyMenu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnEnemy", "Draw On Enemys").SetValue(true));
            enemyMenu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnEnemy.FillColor", "Combo Damage Fill").SetValue(new Circle(true, Color.DarkGray)));
            enemyMenu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnEnemy.KillableColor", "Killable Text").SetValue(new Circle(true, Color.DarkGray)));


            var selfMenu = new Menu(".Self", "selfMenu");
            selfMenu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnSelf", "Draw On Self").SetValue(true));
            selfMenu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnSelf.ComboColor", "Combo Range (R)").SetValue(new Circle(true,Color.OrangeRed)));
            selfMenu.AddItem(new MenuItem(_MenuItemBase + "Boolean.DrawOnSelf.WColor", "W Range").SetValue(new Circle(true, Color.Green)));

            menu.AddSubMenu(enemyMenu);
            menu.AddSubMenu(selfMenu);

            return menu;
        }
    }
}
