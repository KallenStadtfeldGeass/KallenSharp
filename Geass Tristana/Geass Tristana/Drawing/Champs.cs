using Geass_Tristana.Other;
using LeagueSharp.Common;
using System;
using System.Linq;
using GeassLib.Drawing.Champions;
using SharpDX;
using Color = System.Drawing.Color;
using Damage = Geass_Tristana.Other.Damage;

namespace Geass_Tristana.Drawing
{
    internal class Champs : Core, GeassLib.Interfaces.Drawing.Champion
    {
        public const string MenuItemBase = ".Champions.";
        public const string MenuNameBase = ".Champions Menu";
        private readonly GeassLib.Drawing.Champions.HealthBarDamageRender HPBarRender = new HealthBarDamageRender();

        private Utility.HpBarDamageIndicator.DamageToUnitDelegate _damageToEnemy;

        public void OnDrawEnemy(EventArgs args)
        {
            if (!SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy").GetValue<bool>() || DamageToEnemy == null)
                return;

            foreach (var unit in HeroManager.Enemies.Where(unit => unit.IsValid && unit.IsHPBarRendered && Champion.Player.Distance(unit) < 1000))
            {
                var damage = Damage.CalculateDamage(unit);
               

                if (SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.KillableColor").GetValue<Circle>().Active && damage > unit.Health)
                    LeagueSharp.Drawing.DrawText(unit.HPBarPosition.X + 10, unit.HPBarPosition.Y + 3,
                        SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.KillableColor").GetValue<Circle>().Color,
                        "Killable");
;

                if (!SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Active) return;
                var c = SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Color;
                HPBarRender.DrawDamageOnEnemy(unit, damage, new ColorBGRA(c.R,c.G,c.B,100));
            }
        }

        public void OnDrawSelf(EventArgs args)
        {
            if (!SMenu.Item(MenuItemBase + "Boolean.DrawOnSelf").GetValue<bool>())
                return;

            if (!Champion.Player.Position.IsOnScreen())
                return;

            if (SMenu.Item(MenuItemBase + "Boolean.DrawOnSelf.ComboColor").GetValue<Circle>().Active && Champion.GetSpellR.Level > 0)
                Render.Circle.DrawCircle(Champion.Player.Position, Champion.GetSpellR.Range, SMenu.Item(MenuItemBase + "Boolean.DrawOnSelf.ComboColor").GetValue<Circle>().Color, 2);

            if (SMenu.Item(MenuItemBase + "Boolean.DrawOnSelf.WColor").GetValue<Circle>().Active && Champion.GetSpellW.Level > 0)
                Render.Circle.DrawCircle(Champion.Player.Position, Champion.GetSpellW.Range, SMenu.Item(MenuItemBase + "Boolean.DrawOnSelf.WColor").GetValue<Circle>().Color, 2);
        }

        public Utility.HpBarDamageIndicator.DamageToUnitDelegate DamageToEnemy
        {
            get { return _damageToEnemy; }

            set
            {
                if (_damageToEnemy == null)
                {
                    LeagueSharp.Drawing.OnDraw += OnDrawEnemy;
                }
                _damageToEnemy = value;
            }
        }

        public Damage Damage = new Damage();

        /*
                private Color GetColor(bool b)
                {
                    return b ? Color.White : Color.SlateGray;
                }
        */
    }
}