﻿using _Project_Geass.Data;
using _Project_Geass.Globals;
using LeagueSharp.Common;
using System;

namespace _Project_Geass.Module.Core.Drawing.Events
{
    internal class LastHitHelper
    {
        public LastHitHelper()
        {
            LeagueSharp.Drawing.OnDraw += OnDraw;
        }

        public void OnDraw(EventArgs args)
        {
            if (Static.Objects.Player.IsDead) return;
            if (!Static.Objects.ProjectMenu.Item(Names.Menu.LastHitHelperItemBase + ".Minion." + "Boolean.LastHitHelper").GetValue<bool>()) return;

            foreach (var minion in Functions.Objects.Minions.GetEnemyMinions(Static.Objects.Player.AttackRange + 150))
            {
                if (Static.Objects.Player.GetAutoAttackDamage(minion) - 5 > minion.Health) // Is killable
                {
                    Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius - 10, Static.Objects.ProjectMenu.Item(Names.Menu.LastHitHelperItemBase + ".Minion." + "Circle.KillableColor").GetValue<Circle>().Color, 3);
                }
            }
        }
    }
}