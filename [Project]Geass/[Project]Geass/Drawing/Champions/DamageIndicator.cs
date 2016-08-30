using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using SharpDX.Direct3D9;
using System;
using Color = System.Drawing.Color;

namespace _Project_Geass.Drawing.Champions
{
    internal class DamageIndicator
    {
        private const int Width = 104;

        private readonly Utility.HpBarDamageIndicator.DamageToUnitDelegate _damageToUnitDelegate;

        public static Device DxDevice = LeagueSharp.Drawing.Direct3DDevice;
        public static Line DxLine;

        private Obj_AI_Hero Unit { get; set; }

        private Vector2 Offset
        {
            get
            {
                if (Unit != null)
                {
                    return Unit.IsAlly ? new Vector2(34, 9) : new Vector2(10, 20);
                }

                return new Vector2();
            }
        }

        public DamageIndicator(Utility.HpBarDamageIndicator.DamageToUnitDelegate _delegate, int range)
        {
            DxLine = new Line(DxDevice) { Width = 9 };
            Range = range;
            _damageToUnitDelegate = _delegate;

            LeagueSharp.Drawing.OnDraw += Drawing_OnDraw;

            LeagueSharp.Drawing.OnPreReset += DrawingOnOnPreReset;
            LeagueSharp.Drawing.OnPostReset += DrawingOnOnPostReset;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainOnDomainUnload;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnDomainUnload;
        }

        private float GetHpProc(float dmg = 0)
        {
            return (((Unit.Health - dmg) > 0) ? (Unit.Health - dmg) : 0 / Unit.MaxHealth);
        }

        private Vector2 GetHpPosAfterDmg(float dmg)
        {
            return new Vector2(StartPosition.X + GetHpProc(dmg) * Width, StartPosition.Y);
        }

        public Vector2 StartPosition => new Vector2(Unit.HPBarPosition.X + Offset.X, Unit.HPBarPosition.Y + Offset.Y);

        public void DrawDmg(float dmg, ColorBGRA color)
        {
            var hpPosNow = GetHpPosAfterDmg(0);
            var hpPosAfter = GetHpPosAfterDmg(dmg);

            FillHpBar(hpPosNow, hpPosAfter, color);
        }

        private static void CurrentDomainOnDomainUnload(object sender, EventArgs eventArgs)
        {
            DxLine.Dispose();
        }

        private static void DrawingOnOnPostReset(EventArgs args)
        {
            DxLine.OnResetDevice();
        }

        private static void DrawingOnOnPreReset(EventArgs args)
        {
            DxLine.OnLostDevice();
        }

        public void SetFillEnabled(bool enable)
        {
            FillEnabled = enable;
        }

        public void SetKillableEnabled(bool enable)
        {
            KillableEnabled = enable;
        }

        public void SetFill(Color color)
        {
            Fill = color;
        }

        public void SetKillable(Color color)
        {
            Killable = color;
        }

        private bool FillEnabled { get; set; }
        private bool KillableEnabled { get; set; }
        private int Range { get; set; }
        private Color Fill { get; set; }
        private Color Killable { get; set; }

        void FillHpBar(Vector2 from, Vector2 to, ColorBGRA color)
        {
            DxLine.Begin();

            DxLine.Draw(new[] {
                new Vector2((int) from.X, (int) from.Y + 4f),
                new Vector2((int) to.X, (int) to.Y + 4f) }, color);

            DxLine.End();
        }

        private void Drawing_OnDraw(EventArgs args)
        {
            if (!FillEnabled && !KillableEnabled) return;

            foreach (var enemy in Functions.Objects.Heroes.GetEnemies(Range))
            {
                var damage = _damageToUnitDelegate(enemy);

                if (damage <= 0) continue;

                if (damage > enemy.Health && KillableEnabled)
                    LeagueSharp.Drawing.DrawText(enemy.HPBarPosition.X + 10, enemy.HPBarPosition.Y + 3, Killable, nameof(Killable));

                if (!FillEnabled) continue;
                Unit = enemy;
                var hpPosNow = GetHpPosAfterDmg(0);
                var hpPosAfter = GetHpPosAfterDmg(damage);

                FillHpBar(hpPosNow, hpPosAfter, new ColorBGRA(Fill.B, Fill.G, Fill.R, 200));
            }
        }
    }
}