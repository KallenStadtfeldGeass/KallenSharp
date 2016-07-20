using LeagueSharp.Common;
using System;
using GeassBlitz.Misc;
using GeassLib.Drawing.Champions;

namespace GeassBlitz.Drawing
{
    internal class Champs : Core, GeassLib.Interfaces.Drawing.Champion
    {
        public const string MenuItemBase = ".Champions.";
        public const string MenuNameBase = ".Champions Menu";

        private readonly DamageIndicator _damageIndicator = new DamageIndicator(DamageLib.CalcDamage, 1000, true);

        public void OnDrawEnemy(EventArgs args)
        {
            if (!SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy").GetValue<bool>())
            {
                _damageIndicator.SetFillEnabled(false);
                _damageIndicator.SetKillableEnabled(false);
                return;
            }

            _damageIndicator.SetFillEnabled(SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Active);
            _damageIndicator.SetFill(SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Color);


            _damageIndicator.SetKillableEnabled(SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.KillableColor").GetValue<Circle>().Active);
            _damageIndicator.SetKillable(SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.KillableColor").GetValue<Circle>().Color);
        }

        public void OnDrawSelf(EventArgs args)
        {
            if (!SMenu.Item(MenuItemBase + "Boolean.DrawOnSelf").GetValue<bool>())
                return;

            if (!Champion.Player.Position.IsOnScreen())
                return;

            if (SMenu.Item(MenuItemBase + "Boolean.DrawOnSelf.ComboColor").GetValue<Circle>().Active && Champion.GetSpellQ.Level > 0)
                Render.Circle.DrawCircle(Champion.Player.Position, Champion.GetSpellQ.Range, SMenu.Item(MenuItemBase + "Boolean.DrawOnSelf.ComboColor").GetValue<Circle>().Color, 2);
        }

    }
}
