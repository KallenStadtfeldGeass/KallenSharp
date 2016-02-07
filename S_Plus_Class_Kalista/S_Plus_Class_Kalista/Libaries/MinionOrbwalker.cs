using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace S_Plus_Class_Kalista.Libaries
{
    class MinionOrbwalker : Core
    {

        private static void OrbWalkMinions()
        {
            /*
            var target = TargetSelector.GetTarget(Champion.E.Range * 1.2f, TargetSelector.DamageType.Physical);

            if (target != null)
            {
                // ReSharper disable once InconsistentNaming
                var Minions = MinionManager.GetMinions(Player.Position, Orbwalking.GetRealAutoAttackRange(Player), MinionTypes.All, MinionTeam.NotAlly);

                if (Minions == null) return;

                var target2 = TargetSelector.GetTarget(700, TargetSelector.DamageType.Physical);

                foreach (var minion in Minions)
                {
                    if (target2 != null) continue;
                    if (Vector3.Distance(ObjectManager.Player.ServerPosition, minion.Position) > Orbwalking.GetRealAutoAttackRange(Player) + 50) continue;
                    if (minion.CharData.BaseSkinName == "gangplankbarrel") continue;
                    Player.IssueOrder(GameObjectOrder.AttackUnit, minion);
                    break;
                }

                if (target.GetBuffCount("kalistaexpungemarker") <= 0) return;

                if (!(Player.Distance(target, true) > Math.Pow(Orbwalking.GetRealAutoAttackRange(target), 2))) return;

                var minions = ObjectManager.Get<Obj_AI_Minion>().Where(m => m.IsValidTarget(Orbwalking.GetRealAutoAttackRange(m)));

                var objAiMinions = minions as Obj_AI_Minion[] ?? minions.ToArray();
                if (
                    objAiMinions.Any(
                        m => Champion.E.CanCast(m) && m.Health <= Damage.DamageCalc.CalculateRendDamage(m)))
                   Champion.UseRend();
                else
                {
                    // ReSharper disable once PossibleMultipleEnumeration
                    var minion =
                        VectorHelper.GetDashObjects(objAiMinions)
                            .Find(
                                m =>
                                    m.Health > Properties.PlayerHero.GetAutoAttackDamage(m) &&
                                    m.Health <
                                    Properties.PlayerHero.GetAutoAttackDamage(m) + Damage.DamageCalc.CalculateRendDamage(m));
                    if (minion != null && minion.CharData.BaseSkinName != "gangplankbarrel")
                    {
                        if (Properties.LandWalker != null) Properties.LandWalker.ForceTarget(minion);
                        else if (Properties.CommonWalker != null) Properties.CommonWalker.ForceTarget(minion);
                    }
                }
            }

            else
            {
                var minions = MinionManager.GetMinions(Properties.PlayerHero.Position, Orbwalking.GetRealAutoAttackRange(Properties.PlayerHero), MinionTypes.All, MinionTeam.NotAlly);
                foreach (var minion in minions)
                {
                    if (Vector3.Distance(ObjectManager.Player.ServerPosition, minion.Position) > Orbwalking.GetRealAutoAttackRange(Properties.PlayerHero) + 50) continue;
                    if (minion.CharData.BaseSkinName == "gangplankbarrel") continue;
                    Player.IssueOrder(GameObjectOrder.AttackUnit, minion);
                    break;
                }
            }
              */
        }

    }
  
}
