using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace _Project_Geass.Bootloaders.Core.Events.Drawing.Minions
{
    internal class LastHitHelper
    {
        public LastHitHelper()
        {
            Game.OnUpdate += OnMinionDraw;
        }

        public void OnMinionDraw(EventArgs args)
        {
            if (Globals.Static.Objects.Player.IsDead) return;
            if (!Globals.Static.Objects.ProjectMenu.Item(Constants.Names.Menu.DrawingItemBase + ".Minion." + "Boolean.LastHitHelper").GetValue<bool>()) return;

            foreach (var minion in Functions.Objects.Minions.GetEnemyMinions(Globals.Static.Objects.ProjectMenu.Item(Constants.Names.Menu.DrawingItemBase + ".Minion." + "Slider.RenderDistance").GetValue<Slider>().Value))
            {
                if (Globals.Static.Objects.Player.GetAutoAttackDamage(minion) - 5 > minion.Health) // Is killable
                    Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + 100, Globals.Static.Objects.ProjectMenu.Item(Constants.Names.Menu.DrawingItemBase + ".Minion." + "Circle.KillableColor").GetValue<Circle>().Color, 2);
            }
        }
    }
}