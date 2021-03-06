﻿using System;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;
using SharpDX.Direct3D9;
using _Project_Geass.Data.Cache;
using Color = System.Drawing.Color;

namespace _Project_Geass.Drawing.Champions
{

    internal class DamageIndicator
    {
        #region Public Constructors

        public DamageIndicator(Utility.HpBarDamageIndicator.DamageToUnitDelegate _delegate, int range, bool debugger=false)
        {
            DxLine=new Line(DxDevice) {Width=9};
            _debugger=debugger;
            Range=range;
            _damageToUnitDelegate=_delegate;

            LeagueSharp.Drawing.OnDraw+=Drawing_OnDraw;

            LeagueSharp.Drawing.OnPreReset+=DrawingOnOnPreReset;
            LeagueSharp.Drawing.OnPostReset+=DrawingOnOnPostReset;
            AppDomain.CurrentDomain.DomainUnload+=CurrentDomainOnDomainUnload;
            AppDomain.CurrentDomain.ProcessExit+=CurrentDomainOnDomainUnload;
        }

        #endregion Public Constructors

        #region Public Properties

        public Vector2 StartPosition => new Vector2(Unit.HPBarPosition.X+Offset.X, Unit.HPBarPosition.Y+Offset.Y);

        #endregion Public Properties

        #region Public Fields

        public Device DxDevice=LeagueSharp.Drawing.Direct3DDevice;
        public Line DxLine;

        #endregion Public Fields

        #region Public Methods

        public void DrawDmg(float dmg, ColorBGRA color)
        {
            var hpPosNow=GetHpPosAfterDmg(0);
            var hpPosAfter=GetHpPosAfterDmg(dmg);

            fillHPBar(hpPosNow, hpPosAfter, color);
            //fillHPBar((int)(hpPosNow.X - startPosition.X), (int)(hpPosAfter.X- startPosition.X), color);
        }

        public void SetFill(Color color) {Fill=color;}
        public void SetFillEnabled(bool enable) {FillEnabled=enable;}
        public void SetKillable(Color color) {Killable=color;}
        public void SetKillableEnabled(bool enable) {KillableEnabled=enable;}

        #endregion Public Methods

        #region Private Fields

        //
        private const int Width=104;
        private readonly Utility.HpBarDamageIndicator.DamageToUnitDelegate _damageToUnitDelegate;
        /*
                private const int Thinkness = 9;
        */

        // ReSharper disable once NotAccessedField.Local
        private readonly bool _debugger;

        #endregion Private Fields

        /*
                private static readonly Vector2 BarOffset = new Vector2(10, 25);
        */

        #region Private Properties

        private Color Fill{get;set;}
        private bool FillEnabled{get;set;}
        private Color Killable{get;set;}
        private bool KillableEnabled{get;set;}

        private Vector2 Offset
        {
            get
            {
                if (Unit!=null)
                    return Unit.IsAlly? new Vector2(34, 9) : new Vector2(10, 20);

                return new Vector2();
            }
        }

        private int Range{get;}
        private Obj_AI_Hero Unit{get;set;}

        #endregion Private Properties

        #region Private Methods

        private void CurrentDomainOnDomainUnload(object sender, EventArgs eventArgs) {DxLine.Dispose();}

        private void Drawing_OnDraw(EventArgs args)
        {
            if (!FillEnabled&&!KillableEnabled)
                return;

            foreach (var enemy in Objects.GetCacheEnemies(Range))
            {
                // Get damage to unit
                var damage=_damageToUnitDelegate(enemy);

                // Continue on 0 damage
                if (damage<=0)
                    continue;

                if ((damage>enemy.Health)&&KillableEnabled)
                    LeagueSharp.Drawing.DrawText(enemy.HPBarPosition.X+10, enemy.HPBarPosition.Y+3, Killable, "Killable");

                if (FillEnabled)
                {
                    Unit=enemy;
                    var hpPosNow=GetHpPosAfterDmg(0);
                    var hpPosAfter=GetHpPosAfterDmg(damage);

                    fillHPBar(hpPosNow, hpPosAfter, new ColorBGRA(Fill.B, Fill.G, Fill.R, 200));

                    //var damagePercentage = ((enemy.Health - damage) > 0 ? (enemy.Health - damage) : 0) / enemy.MaxHealth;
                    //var currentHealthPercentage = enemy.Health / enemy.MaxHealth;

                    //var startPoint = new Vector2(
                    //    (int)(enemy.HPBarPosition.X + BarOffset.X + damagePercentage * Width),
                    //    (int)(enemy.HPBarPosition.Y + BarOffset.Y) - 5);
                    //var endPoint =
                    //    new Vector2(
                    //        (int)(enemy.HPBarPosition.X + BarOffset.X + currentHealthPercentage * Width) + 1,
                    //        (int)(enemy.HPBarPosition.Y + BarOffset.Y) - 5);

                    // Draw the line LeagueSharp.Drawing.DrawLine(startPoint, endPoint, Thinkness, Fill);

                    // if (_debugger) Console.WriteLine($"GeassLib: {enemy.Name} {startPoint} {endPoint}");
                }
            }
        }

        private void DrawingOnOnPostReset(EventArgs args) {DxLine.OnResetDevice();}
        private void DrawingOnOnPreReset(EventArgs args) {DxLine.OnLostDevice();}

        private void fillHPBar(Vector2 from, Vector2 to, ColorBGRA color)
        {
            DxLine.Begin();

            DxLine.Draw(new[] {new Vector2((int)from.X, (int)from.Y+4f), new Vector2((int)to.X, (int)to.Y+4f)}, color);

            DxLine.End();
        }

        private Vector2 GetHpPosAfterDmg(float dmg)
        {
            var w=GetHpProc(dmg)*Width;
            return new Vector2(StartPosition.X+w, StartPosition.Y);
        }

        private float GetHpProc(float dmg=0)
        {
            var health=Unit.Health-dmg>0? Unit.Health-dmg : 0;
            return health/Unit.MaxHealth;
        }

        #endregion Private Methods
    }

}