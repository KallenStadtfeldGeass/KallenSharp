using System;
using LeagueSharp;
using LeagueSharp.Common;

namespace S__Class_Tristana.Drawing
{
    class Minons : Core
    {
        public const string _MenuNameBase = ".Minions Menu";
        public const string _MenuItemBase = ".Minions.";

        public void OnMinionDraw(EventArgs args)
        {
            if (_Champion.Player.IsDead) return;
            if (!SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions").GetValue<bool>()) return;

            foreach (var minion in ObjectManager.Get<Obj_AI_Minion>())
            {

                if (minion.Distance(_Champion.Player) > SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions.Distance").GetValue<Slider>().Value) continue; // Out of render range
                if (minion.IsAlly) continue; //This is not Dota2
                if (minion.IsDead) continue;//Dont poke the dead
                if (!minion.IsMinion) continue; //Differect Function

                if (_Champion.Player.GetAutoAttackDamage(minion) > minion.Health) // Is killable
                {
                    Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + 50, SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions.MarkerKillableColor").GetValue<Circle>().Color, 2);
                }


                else // Not killable
                {
                    Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + 50, SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions.MarkerInnerColor").GetValue<Circle>().Color, 2);

                    var remainingHp = (int)100 * (minion.Health - _Champion.Player.GetAutoAttackDamage(minion)) / minion.Health;

                    Render.Circle.DrawCircle(minion.Position, minion.BoundingRadius + (float)remainingHp + 50, SMenu.Item(_MenuItemBase + "Boolean.DrawOnMinions.MakerOuterColor").GetValue<Circle>().Color, 2);
                }
            }

        }
    }
}
