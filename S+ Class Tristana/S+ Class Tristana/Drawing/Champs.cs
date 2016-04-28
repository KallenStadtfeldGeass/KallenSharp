using LeagueSharp.Common;
using System;
using System.Linq;
using Color = System.Drawing.Color;

namespace S__Class_Tristana.Drawing
{
    class Champs : Core
    {
        public const string _MenuNameBase = ".Champions Menu";
        public const string _MenuItemBase = ".Champions.";

        private Utility.HpBarDamageIndicator.DamageToUnitDelegate _damageToEnemy;
        public Damage _Damage = new Damage();

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

        public void OnDrawEnemy(EventArgs args)
        {
            if (!SMenu.Item(_MenuItemBase + "Boolean.DrawOnEnemy").GetValue<bool>() || DamageToEnemy == null)
                return;

            foreach (var unit in HeroManager.Enemies.Where(unit => unit.IsValid && unit.IsHPBarRendered && _Champion.Player.Distance(unit) < 1000))
            {
                const int xOffset = 10;
                const int yOffset = 20;
                const int width = 103;
                const int height = 8;

                var barPos = unit.HPBarPosition;
                var damage = _Damage.CalculateDamage(unit);
                var percentHealthAfterDamage = Math.Max(0, unit.Health - damage) / unit.MaxHealth;
                var yPos = barPos.Y + yOffset;
                var xPosDamage = barPos.X + xOffset + width * percentHealthAfterDamage;
                var xPosCurrentHp = barPos.X + xOffset + width * unit.Health / unit.MaxHealth;

                if (SMenu.Item(_MenuItemBase + "Boolean.DrawOnEnemy.KillableColor").GetValue<Circle>().Active &&
                    damage > unit.Health)
                    LeagueSharp.Drawing.DrawText(barPos.X + xOffset, barPos.Y + yOffset - 13,
                        SMenu.Item(_MenuItemBase + "Boolean.DrawOnEnemy.KillableColor").GetValue<Circle>().Color,
                        "Killable");

                LeagueSharp.Drawing.DrawLine(xPosDamage, yPos, xPosDamage, yPos + height, 1, Color.LightGray);

                if (!SMenu.Item(_MenuItemBase + "Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Active) return;

                var differenceInHp = xPosCurrentHp - xPosDamage;
                var pos1 = barPos.X + 9 + (107 * percentHealthAfterDamage);

                for (var i = 0; i < differenceInHp; i++)
                {
                    LeagueSharp.Drawing.DrawLine(pos1 + i, yPos, pos1 + i, yPos + height, 1,
                        SMenu.Item(_MenuItemBase + "Boolean.DrawOnEnemy.FillColor").GetValue<Circle>().Color);
                }
            }
        }

        public void OnDrawSelf(EventArgs args)
        {
            if (!SMenu.Item(_MenuItemBase + "Boolean.DrawOnSelf").GetValue<bool>())
                return;

            if (!_Champion.Player.Position.IsOnScreen())
                return;

            if (SMenu.Item(_MenuItemBase + "Boolean.DrawOnSelf.ComboColor").GetValue<Circle>().Active && _Champion.GetSpellR().Level > 0)
                Render.Circle.DrawCircle(_Champion.Player.Position, _Champion.GetSpellR().Range, SMenu.Item(_MenuItemBase + "Boolean.DrawOnSelf.ComboColor").GetValue<Circle>().Color, 2);

            if (SMenu.Item(_MenuItemBase + "Boolean.DrawOnSelf.WColor").GetValue<Circle>().Active && _Champion.GetSpellW().Level > 0)
                Render.Circle.DrawCircle(_Champion.Player.Position, _Champion.GetSpellW().Range, SMenu.Item(_MenuItemBase + "Boolean.DrawOnSelf.WColor").GetValue<Circle>().Color, 2);



        }

        private Color GetColor(bool b)
        {
            return b ? Color.White : Color.SlateGray;
        }

    }
}
