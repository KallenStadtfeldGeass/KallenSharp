using LeagueSharp.Common;
using S__Class_Tristana.Other;
using System;
using System.Linq;
using Color = System.Drawing.Color;
using Damage = S__Class_Tristana.Other.Damage;

namespace S__Class_Tristana.Drawing
{
    internal class Champs : Core
    {
        public const string MenuItemBase = ".Champions.";
        public const string MenuNameBase = ".Champions Menu";

        private Utility.HpBarDamageIndicator.DamageToUnitDelegate _damageToEnemy;

        public void OnDrawEnemy(EventArgs args)
        {
            if (!SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy").GetValue<bool>() || DamageToEnemy == null)
                return;

            foreach (var unit in HeroManager.Enemies.Where(unit => unit.IsValid && unit.IsHPBarRendered && Champion.Player.Distance(unit) < 1000))
            {
                const int xOffset = 10;
                const int yOffset = 20;
                const int width = 103;
                const int height = 8;

                var barPos = unit.HPBarPosition;
                var damage = Damage.CalculateDamage(unit);
                var percentHealthAfterDamage = Math.Max(0, unit.Health - damage) / unit.MaxHealth;
                var yPos = barPos.Y + yOffset;
                var xPosDamage = barPos.X + xOffset + width * percentHealthAfterDamage;
                var xPosCurrentHp = barPos.X + xOffset + width * unit.Health / unit.MaxHealth;

                if (SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.KillableColor").GetValue<Circle>().Active &&
                    damage > unit.Health)
                    LeagueSharp.Drawing.DrawText(barPos.X + xOffset, barPos.Y + yOffset - 13,
                        SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.KillableColor").GetValue<Circle>().Color,
                        "Killable");

                LeagueSharp.Drawing.DrawLine(xPosDamage, yPos, xPosDamage, yPos + height, 1, Color.LightGray);

                if (!SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Active) return;

                var differenceInHp = xPosCurrentHp - xPosDamage;
                var pos1 = barPos.X + 9 + (107 * percentHealthAfterDamage);

                for (var i = 0; i < differenceInHp; i++)
                {
                    LeagueSharp.Drawing.DrawLine(pos1 + i, yPos, pos1 + i, yPos + height, 1,
                        SMenu.Item(MenuItemBase + "Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Color);
                }
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