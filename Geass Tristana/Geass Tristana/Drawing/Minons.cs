using Geass_Tristana.Other;
using LeagueSharp;
using LeagueSharp.Common;
using System;

namespace Geass_Tristana.Drawing
{
    internal class Minons : Core, Interface.IMinons
    {
        public const string MenuItemBase = ".Minions.";
        public const string MenuNameBase = ".Minions Menu";

        public void OnMinionDraw(EventArgs args)
        {
            if (Champion.Player.IsDead) return;
            if (!SMenu.Item(MenuItemBase + "Boolean.DrawOnMinions").GetValue<bool>()) return;

            foreach (var minion in ObjectManager.Get<Obj_AI_Minion>())
            {
                if (minion.Distance(Champion.Player) > SMenu.Item(MenuItemBase + "Boolean.DrawOnMinions.Distance").GetValue<Slider>().Value) continue; // Out of render range
                if (minion.IsAlly) continue; //This is not Dota2
                if (minion.IsDead) continue;//Dont poke the dead
                if (!minion.IsMinion) continue; //Differect Function

                if (Champion.Player.GetAutoAttackDamage(minion) > minion.Health) // Is killable
                {
                    Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + 50, SMenu.Item(MenuItemBase + "Boolean.DrawOnMinions.MarkerKillableColor").GetValue<Circle>().Color, 2);
                }

                //else // Not killable
                //{
                //    Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + 50, SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions.MarkerInnerColor").GetValue<Circle>().Color, 2);

                //    var remainingHp = (int)100 * (minion.Health - _Champion.Player.GetAutoAttackDamage(minion)) / minion.Health;

                //    Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + (float)remainingHp + 50, SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions.MakerOuterColor").GetValue<Circle>().Color, 2);
                //}
            }
        }
    }
}