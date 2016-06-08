using Geass_Tristana.Other;
using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace Geass_Tristana.Drawing
{
    internal class Minons : Core, GeassLib.Interfaces.Drawing.Minion
    {
        public const string MenuItemBase = ".Minions.";
        public const string MenuNameBase = ".Minions Menu";

        public void OnMinionDraw(EventArgs args)
        {
            if (Champion.Player.IsDead) return;
            if (!SMenu.Item(MenuItemBase + "Boolean.DrawOnMinions").GetValue<bool>()) return;

            foreach (var minion in GeassLib.Functions.Objects.Minions.GetEnemyMinions(SMenu.Item(MenuItemBase + "Boolean.DrawOnMinions.Distance").GetValue<Slider>().Value))
            {
                if (Champion.Player.GetAutoAttackDamage(minion) > minion.Health) // Is killable
                {
                    //Libaries.Logger.Write($"Draw Minon Killabel Circle");
                    Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + 100, SMenu.Item(MenuItemBase + "Boolean.DrawOnMinions.MarkerKillableColor").GetValue<Circle>().Color, 2);
                }
            }
        }
    }
}