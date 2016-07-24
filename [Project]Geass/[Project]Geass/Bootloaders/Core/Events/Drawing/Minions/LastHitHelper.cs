using _Project_Geass.Globals;
using LeagueSharp.Common;
using System;

namespace _Project_Geass.Bootloaders.Core.Events.Drawing.Minions
{
    internal class LastHitHelper
    {
        public LastHitHelper()
        {
            LeagueSharp.Drawing.OnDraw += OnMinionDraw;
        }

        public void OnMinionDraw(EventArgs args)
        {
            if (Static.Objects.Player.IsDead) return;
            if (!Static.Objects.ProjectMenu.Item(Constants.Names.Menu.LastHitHelperItemBase + ".Minion." + "Boolean.LastHitHelper").GetValue<bool>()) return;

            foreach (var minion in Functions.Objects.Minions.GetEnemyMinions(Static.Objects.Player.AttackRange + 150))
            {
                if (Static.Objects.Player.GetAutoAttackDamage(minion) - 5 > minion.Health) // Is killable
                {
                    Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius - 10, Static.Objects.ProjectMenu.Item(Constants.Names.Menu.LastHitHelperItemBase + ".Minion." + "Circle.KillableColor").GetValue<Circle>().Color, 3);
                }
            }
        }
    }
}