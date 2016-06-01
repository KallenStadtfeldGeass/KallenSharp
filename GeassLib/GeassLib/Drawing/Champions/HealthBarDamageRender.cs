using System;
using LeagueSharp;
using SharpDX;
using SharpDX.Direct3D9;


namespace GeassLib.Drawing.Champions
{
    public class HealthBarDamageRender
    {
        private const ushort Height = 8;
        private const ushort Width = 103;
        private readonly Line _renderLine;
        public HealthBarDamageRender()
        {
            _renderLine = new Line(LeagueSharp.Drawing.Direct3DDevice) {Width = 9};
            LeagueSharp.Drawing.OnPreReset += DrawingOnOnPreReset;
            LeagueSharp.Drawing.OnPostReset += DrawingOnOnPostReset;
            AppDomain.CurrentDomain.DomainUnload += CurrentDomainOnDomainUnload;
            AppDomain.CurrentDomain.ProcessExit += CurrentDomainOnDomainUnload;
        }
        private Obj_AI_Hero Target { get; set; }

        private Vector2 Offset => Target.IsAlly ? new Vector2(34, 9) : new Vector2(10, 20);

        private Vector2 StartPosition => new Vector2(Target.HPBarPosition.X + Offset.X, Target.HPBarPosition.Y + Offset.Y);


        private void CurrentDomainOnDomainUnload(object sender, EventArgs eventArgs) => _renderLine.Dispose();

        private void DrawingOnOnPostReset(EventArgs args) => _renderLine.OnResetDevice();

        private void DrawingOnOnPreReset(EventArgs args) => _renderLine.OnLostDevice();


        private float GetRemainingHealthPercent(float dmg = 0) => (Target.Health - dmg / Target.MaxHealth);

        private Vector2 GetHealthPosition(float dmg = 0) => new Vector2(StartPosition.X + GetRemainingHealthPercent(dmg) * Width, StartPosition.Y);

        private void FillHealthBar(Vector2 from, Vector2 to, ColorBGRA color)
        {
            _renderLine.Begin();

            _renderLine.Draw(new[] {
                new Vector2((int) from.X, (int) from.Y + 4f),
                new Vector2((int) to.X, (int) to.Y + 4f) }, color);

            _renderLine.End();
        }
    

    public void DrawDamageOnEnemy(Obj_AI_Hero target, float damage, ColorBGRA c)
        {
            if (damage <= 0) return;
            if (target == null) return;
            Target = target;
            var hpPosNow = GetHealthPosition();
            var hpPosAfter = GetHealthPosition(damage);
            FillHealthBar(hpPosNow, hpPosAfter, c);
            
        }
    }
}

