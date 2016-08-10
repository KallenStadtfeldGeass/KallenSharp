using _Project_Geass.Globals;
using LeagueSharp;
using LeagueSharp.Common;
using System;
using System.Linq;

namespace _Project_Geass.Extensions
{
    public class OrbwalkerExtension : Orbwalking.Orbwalker
    {
        private readonly Menu _config;

        public static event Orbwalking.OnNonKillableMinionH OnNonKillableMinion;

#pragma warning disable 67

        public static event Orbwalking.OnTargetChangeH OnTargetChange;

#pragma warning restore 67

        private const float LaneClearWaitTimeMod = 2f;

        public static AttackableUnit LastTarget;
        public Obj_AI_Base ForcedTarget;
        public Obj_AI_Minion PrevMinon;

        public OrbwalkerExtension(Menu attachToMenu) : base(attachToMenu)
        {
            _config = attachToMenu;
        }

        private int FarmDelay => _config.Item("FarmDelay").GetValue<Slider>().Value;

        private bool ShouldAttackMinion(Obj_AI_Minion minion)
        {
            if (minion.Name == "WardCorpse" || minion.CharData.BaseSkinName == "jarvanivstandard")
            {
                return false;
            }

            if (MinionManager.IsWard(minion))
            {
                return _config.Item("AttackWards").IsActive();
            }

            return (_config.Item("AttackPetsnTraps").GetValue<bool>() || MinionManager.IsMinion(minion))
                   && minion.CharData.BaseSkinName != "gangplankbarrel";
        }

        /// <summary>
        ///     Fires the on non killable minion event.
        /// </summary>
        /// <param name="minion">The minion.</param>
        private static void FireOnNonKillableMinion(AttackableUnit minion)
        {
            OnNonKillableMinion?.Invoke(minion);
        }

        private bool ShouldWaitUnderTurret(Obj_AI_Minion noneKillableMinion)
        {
            return
                ObjectManager.Get<Obj_AI_Minion>()
                    .Any(
                        minion =>
                        (noneKillableMinion == null || noneKillableMinion.NetworkId != minion.NetworkId)
                        && minion.IsValidTarget() && minion.Team != GameObjectTeam.Neutral
                        && InAutoAttackRange(minion) && MinionManager.IsMinion(minion)
                        && HealthPrediction.LaneClearHealthPrediction(
                            minion,
                            (int)
                            (Static.Objects.Player.AttackDelay * 1000
                             + (Static.Objects.Player.IsMelee
                                    ? Static.Objects.Player.AttackCastDelay * 1000
                                    : Static.Objects.Player.AttackCastDelay * 1000
                                      + 1000 * (Static.Objects.Player.AttackRange + 2 * Static.Objects.Player.BoundingRadius)
                                      / Static.Objects.Player.BasicAttack.MissileSpeed)),
                            FarmDelay) <= Static.Objects.Player.GetAutoAttackDamage(minion));
        }

        public override AttackableUnit GetTarget()
        {
            AttackableUnit result = null;
            var mode = ActiveMode;

            if ((mode == Orbwalking.OrbwalkingMode.Mixed || mode == Orbwalking.OrbwalkingMode.LaneClear)
                && !_config.Item("PriorizeFarm").GetValue<bool>())
            {
                var target = TargetSelector.GetTarget(-1, TargetSelector.DamageType.Physical);
                if (target != null && InAutoAttackRange(target))
                {
                    return target;
                }
            }

            //GankPlank barrels
            var attackGankPlankBarrels = _config.Item("AttackGPBarrel").GetValue<StringList>().SelectedIndex;
            if (attackGankPlankBarrels != 2
                && (attackGankPlankBarrels == 0
                    || (mode == Orbwalking.OrbwalkingMode.LaneClear || mode == Orbwalking.OrbwalkingMode.Mixed
                        || mode == Orbwalking.OrbwalkingMode.LastHit || mode == Orbwalking.OrbwalkingMode.Freeze)))
            {
                var enemyGangPlank =
                    HeroManager.Enemies.FirstOrDefault(
                        e => e.ChampionName.Equals("gangplank", StringComparison.InvariantCultureIgnoreCase));

                if (enemyGangPlank != null)
                {
                    var barrels =
                        ObjectManager.Get<Obj_AI_Minion>()
                            .Where(
                                minion =>
                                minion.Team == GameObjectTeam.Neutral
                                && minion.CharData.BaseSkinName == "gangplankbarrel" && minion.IsHPBarRendered
                                && minion.IsValidTarget() && InAutoAttackRange(minion));

                    var objAiMinions = barrels as Obj_AI_Minion[] ?? barrels.ToArray();
                    foreach (var barrel in objAiMinions)
                    {
                        if (barrel.Health <= 1f)
                        {
                            return barrel;
                        }

                        var t = (int)(Static.Objects.Player.AttackCastDelay * 1000) + Game.Ping / 2
                                    + 1000 * (int)Math.Max(0, Static.Objects.Player.Distance(barrel) - Static.Objects.Player.BoundingRadius)
                                    / (int)Orbwalking.GetMyProjectileSpeed();

                        var barrelBuff =
                            barrel.Buffs.FirstOrDefault(
                                b =>
                                b.Name.Equals("gangplankebarrelactive", StringComparison.InvariantCultureIgnoreCase));

                        if (barrelBuff != null && barrel.Health <= 2f)
                        {
                            var healthDecayRate = enemyGangPlank.Level >= 13
                                                      ? 0.5f
                                                      : (enemyGangPlank.Level >= 7 ? 1f : 2f);
                            var nextHealthDecayTime = Game.Time < barrelBuff.StartTime + healthDecayRate
                                                          ? barrelBuff.StartTime + healthDecayRate
                                                          : barrelBuff.StartTime + healthDecayRate * 2;

                            if (nextHealthDecayTime <= Game.Time + t / 1000f)
                            {
                                return barrel;
                            }
                        }
                    }

                    if (objAiMinions.Any())
                    {
                        return null;
                    }
                }
            }

            /*Killable Minion*/
            if (mode == Orbwalking.OrbwalkingMode.LaneClear || mode == Orbwalking.OrbwalkingMode.Mixed || mode == Orbwalking.OrbwalkingMode.LastHit
                || mode == Orbwalking.OrbwalkingMode.Freeze)
            {
                var minionList =
                    ObjectManager.Get<Obj_AI_Minion>()
                        .Where(minion => minion.IsValidTarget() && InAutoAttackRange(minion))
                        .OrderByDescending(minion => minion.CharData.BaseSkinName.Contains("Siege"))
                        .ThenBy(minion => minion.CharData.BaseSkinName.Contains("Super"))
                        .ThenBy(minion => minion.Health)
                        .ThenByDescending(minion => minion.MaxHealth);

                foreach (var minion in minionList)
                {
                    var t = (int)(Static.Objects.Player.AttackCastDelay * 1000) - 100 + Game.Ping / 2
                            + 1000 * (int)Math.Max(0, Static.Objects.Player.Distance(minion) - Static.Objects.Player.BoundingRadius)
                            / (int)Orbwalking.GetMyProjectileSpeed();

                    if (mode == Orbwalking.OrbwalkingMode.Freeze)
                    {
                        t += 200 + Game.Ping / 2;
                    }

                    var predHealth = HealthPrediction.GetHealthPrediction(minion, t, FarmDelay);

                    if (minion.Team != GameObjectTeam.Neutral && ShouldAttackMinion(minion))
                    {
                        var damage = Static.Objects.Player.GetAutoAttackDamage(minion, true);
                        var killable = predHealth <= damage;

                        if (mode == Orbwalking.OrbwalkingMode.Freeze)
                        {
                            if (minion.Health < 50 || predHealth <= 50)
                            {
                                return minion;
                            }
                        }
                        else
                        {
                            if (predHealth <= 0)
                            {
                                FireOnNonKillableMinion(minion);
                            }

                            if (killable)
                            {
                                return minion;
                            }
                        }
                    }
                }
            }

            //Forced target
            if (ForcedTarget.IsValidTarget() && InAutoAttackRange(ForcedTarget))
            {
                return ForcedTarget;
            }

            /* turrets / inhibitors / nexus */
            if (mode == Orbwalking.OrbwalkingMode.LaneClear
                && (!_config.Item("FocusMinionsOverTurrets").GetValue<KeyBind>().Active
                    || !MinionManager.GetMinions(
                        ObjectManager.Player.Position,
                        Orbwalking.GetRealAutoAttackRange(ObjectManager.Player)).Any()))
            {
                /* turrets */
                foreach (var turret in
                    ObjectManager.Get<Obj_AI_Turret>().Where(t => t.IsValidTarget() && InAutoAttackRange(t)))
                {
                    return turret;
                }

                /* inhibitor */
                foreach (var turret in
                    ObjectManager.Get<Obj_BarracksDampener>()
                        .Where(t => t.IsValidTarget() && InAutoAttackRange(t)))
                {
                    return turret;
                }

                /* nexus */
                foreach (var nexus in
                    ObjectManager.Get<Obj_HQ>().Where(t => t.IsValidTarget() && InAutoAttackRange(t)))
                {
                    return nexus;
                }
            }

            /*Champions*/
            if (mode != Orbwalking.OrbwalkingMode.LastHit)
            {
                if (mode != Orbwalking.OrbwalkingMode.LaneClear || !ShouldWait())
                {
                    var target = TargetSelector.GetTarget(-1, TargetSelector.DamageType.Physical);
                    if (target.IsValidTarget() && InAutoAttackRange(target))
                    {
                        return target;
                    }
                }
            }

            /*Jungle minions*/
            if (mode == Orbwalking.OrbwalkingMode.LaneClear || mode == Orbwalking.OrbwalkingMode.Mixed)
            {
                var jminions =
                    ObjectManager.Get<Obj_AI_Minion>()
                        .Where(
                            mob =>
                            mob.IsValidTarget() && mob.Team == GameObjectTeam.Neutral && InAutoAttackRange(mob)
                            && mob.CharData.BaseSkinName != "gangplankbarrel" && mob.Name != "WardCorpse");

                result = _config.Item("Smallminionsprio").GetValue<bool>()
                             ? jminions.MinOrDefault(mob => mob.MaxHealth)
                             : jminions.MaxOrDefault(mob => mob.MaxHealth);

                if (result != null)
                {
                    return result;
                }
            }

            /* UnderTurret Farming */
            if (mode == Orbwalking.OrbwalkingMode.LaneClear || mode == Orbwalking.OrbwalkingMode.Mixed || mode == Orbwalking.OrbwalkingMode.LastHit
                || mode == Orbwalking.OrbwalkingMode.Freeze)
            {
                var closestTower =
                    ObjectManager.Get<Obj_AI_Turret>()
                        .MinOrDefault(t => t.IsAlly && !t.IsDead ? Static.Objects.Player.Distance(t, true) : float.MaxValue);

                if (closestTower != null && Static.Objects.Player.Distance(closestTower, true) < 1500 * 1500)
                {
                    Obj_AI_Minion farmUnderTurretMinion = null;
                    Obj_AI_Minion noneKillableMinion = null;
                    // return all the minions underturret in auto attack range
                    var minions =
                        MinionManager.GetMinions(Static.Objects.Player.Position, Static.Objects.Player.AttackRange + 200)
                            .Where(
                                minion =>
                                InAutoAttackRange(minion) && closestTower.Distance(minion, true) < 900 * 900)
                            .OrderByDescending(minion => minion.CharData.BaseSkinName.Contains("Siege"))
                            .ThenBy(minion => minion.CharData.BaseSkinName.Contains("Super"))
                            .ThenByDescending(minion => minion.MaxHealth)
                            .ThenByDescending(minion => minion.Health);
                    if (minions.Any())
                    {
                        // get the turret aggro minion
                        var turretMinion =
                            minions.FirstOrDefault(
                                minion =>
                                minion is Obj_AI_Minion && HealthPrediction.HasTurretAggro((Obj_AI_Minion)minion));

                        if (turretMinion != null)
                        {
                            var hpLeftBeforeDie = 0;
                            var hpLeft = 0;
                            var turretAttackCount = 0;
                            var turretStarTick = HealthPrediction.TurretAggroStartTick(
                                turretMinion as Obj_AI_Minion);
                            // from healthprediction (don't blame me :S)
                            var turretLandTick = turretStarTick + (int)(closestTower.AttackCastDelay * 1000)
                                                 + 1000
                                                 * Math.Max(
                                                     0,
                                                     (int)
                                                     (turretMinion.Distance(closestTower)
                                                      - closestTower.BoundingRadius))
                                                 / (int)(closestTower.BasicAttack.MissileSpeed + 70);
                            // calculate the HP before try to balance it
                            for (float i = turretLandTick + 50;
                                 i < turretLandTick + 10 * closestTower.AttackDelay * 1000 + 50;
                                 i = i + closestTower.AttackDelay * 1000)
                            {
                                var time = (int)i - Utils.GameTimeTickCount + Game.Ping / 2;
                                var predHp =
                                    (int)
                                    HealthPrediction.LaneClearHealthPrediction(turretMinion, time > 0 ? time : 0);
                                if (predHp > 0)
                                {
                                    hpLeft = predHp;
                                    turretAttackCount += 1;
                                    continue;
                                }
                                hpLeftBeforeDie = hpLeft;
                                hpLeft = 0;
                                break;
                            }
                            // calculate the hits is needed and possibilty to balance
                            if (hpLeft == 0 && turretAttackCount != 0 && hpLeftBeforeDie != 0)
                            {
                                var damage = (int)Static.Objects.Player.GetAutoAttackDamage(turretMinion, true);
                                var hits = hpLeftBeforeDie / damage;
                                var timeBeforeDie = turretLandTick
                                                    + (turretAttackCount + 1)
                                                    * (int)(closestTower.AttackDelay * 1000)
                                                    - Utils.GameTimeTickCount;
                                var timeUntilAttackReady = Orbwalking.LastAATick + (int)(Static.Objects.Player.AttackDelay * 1000)
                                                           > Utils.GameTimeTickCount + Game.Ping / 2 + 25
                                                               ? Orbwalking.LastAATick + (int)(Static.Objects.Player.AttackDelay * 1000)
                                                                 - (Utils.GameTimeTickCount + Game.Ping / 2 + 25)
                                                               : 0;
                                var timeToLandAttack = Static.Objects.Player.IsMelee
                                                           ? Static.Objects.Player.AttackCastDelay * 1000
                                                           : Static.Objects.Player.AttackCastDelay * 1000
                                                             + 1000
                                                             * Math.Max(
                                                                 0,
                                                                 turretMinion.Distance(Static.Objects.Player)
                                                                 - Static.Objects.Player.BoundingRadius)
                                                             / Static.Objects.Player.BasicAttack.MissileSpeed;
                                if (hits >= 1
                                    && hits * Static.Objects.Player.AttackDelay * 1000 + timeUntilAttackReady
                                    + timeToLandAttack < timeBeforeDie)
                                {
                                    farmUnderTurretMinion = turretMinion as Obj_AI_Minion;
                                }
                                else if (hits >= 1
                                         && hits * Static.Objects.Player.AttackDelay * 1000 + timeUntilAttackReady
                                         + timeToLandAttack > timeBeforeDie)
                                {
                                    noneKillableMinion = turretMinion as Obj_AI_Minion;
                                }
                            }
                            else if (hpLeft == 0 && turretAttackCount == 0 && hpLeftBeforeDie == 0)
                            {
                                noneKillableMinion = turretMinion as Obj_AI_Minion;
                            }
                            // should wait before attacking a minion.
                            if (ShouldWaitUnderTurret(noneKillableMinion))
                            {
                                return null;
                            }
                            if (farmUnderTurretMinion != null)
                            {
                                return farmUnderTurretMinion;
                            }
                            // balance other minions
                            foreach (var minion in
                                minions.Where(
                                    x =>
                                    x.NetworkId != turretMinion.NetworkId && x is Obj_AI_Minion
                                    && !HealthPrediction.HasMinionAggro((Obj_AI_Minion)x)))
                            {
                                var playerDamage = (int)Static.Objects.Player.GetAutoAttackDamage(minion);
                                var turretDamage = (int)closestTower.GetAutoAttackDamage(minion, true);
                                var leftHp = (int)minion.Health % turretDamage;
                                if (leftHp > playerDamage)
                                {
                                    return minion;
                                }
                            }
                            // late game
                            var lastminion =
                                minions.LastOrDefault(
                                    x =>
                                    x.NetworkId != turretMinion.NetworkId && x is Obj_AI_Minion
                                    && !HealthPrediction.HasMinionAggro((Obj_AI_Minion)x));
                            if (lastminion != null && minions.Count() >= 2)
                            {
                                if (1f / Static.Objects.Player.AttackDelay >= 1f
                                    && (int)(turretAttackCount * closestTower.AttackDelay / Static.Objects.Player.AttackDelay)
                                    * Static.Objects.Player.GetAutoAttackDamage(lastminion) > lastminion.Health)
                                {
                                    return lastminion;
                                }
                                if (minions.Count() >= 5 && 1f / Static.Objects.Player.AttackDelay >= 1.2)
                                {
                                    return lastminion;
                                }
                            }
                        }
                        else
                        {
                            // ReSharper disable once ExpressionIsAlwaysNull
                            if (ShouldWaitUnderTurret(noneKillableMinion))
                            {
                                return null;
                            }
                            // balance other minions
                            foreach (var minion in
                                minions.Where(
                                    x => x is Obj_AI_Minion && !HealthPrediction.HasMinionAggro((Obj_AI_Minion)x))
                                )
                            {
                                {
                                    var playerDamage = (int)Static.Objects.Player.GetAutoAttackDamage(minion);
                                    var turretDamage = (int)closestTower.GetAutoAttackDamage(minion, true);
                                    var leftHp = (int)minion.Health % turretDamage;
                                    if (leftHp > playerDamage)
                                    {
                                        return minion;
                                    }
                                }
                            }
                            //late game
                            var lastminion =
                                minions.LastOrDefault(
                                    x => x is Obj_AI_Minion && !HealthPrediction.HasMinionAggro((Obj_AI_Minion)x));
                            if (lastminion == null || minions.Count() < 2) return null;
                            if (minions.Count() >= 5 && 1f / Static.Objects.Player.AttackDelay >= 1.2)
                            {
                                return lastminion;
                            }
                        }
                        return null;
                    }
                }
            }

            /*Lane Clear minions*/
            if (mode == Orbwalking.OrbwalkingMode.LaneClear)
            {
                if (!ShouldWait())
                {
                    if (PrevMinon.IsValidTarget() && InAutoAttackRange(PrevMinon))
                    {
                        var predHealth = HealthPrediction.LaneClearHealthPrediction(
                            PrevMinon,
                            (int)(Static.Objects.Player.AttackDelay * 1000 * LaneClearWaitTimeMod),
                            FarmDelay);
                        if (predHealth >= 2 * Static.Objects.Player.GetAutoAttackDamage(PrevMinon)
                            || Math.Abs(predHealth - PrevMinon.Health) < float.Epsilon)
                        {
                            return PrevMinon;
                        }
                    }

                    result = (from minion in
                                  ObjectManager.Get<Obj_AI_Minion>()
                                  .Where(
                                      minion =>
                                      minion.IsValidTarget() && InAutoAttackRange(minion)
                                      && ShouldAttackMinion(minion))
                              let predHealth =
                                  HealthPrediction.LaneClearHealthPrediction(
                                      minion,
                                      (int)(Static.Objects.Player.AttackDelay * 1000 * LaneClearWaitTimeMod),
                                      FarmDelay)
                              where
                                  predHealth >= 2 * Static.Objects.Player.GetAutoAttackDamage(minion)
                                  || Math.Abs(predHealth - minion.Health) < float.Epsilon
                              select minion).MaxOrDefault(
                                  m => !MinionManager.IsMinion(m, true) ? float.MaxValue : m.Health);

                    if (result != null)
                    {
                        PrevMinon = (Obj_AI_Minion)result;
                    }
                }
            }

            return result;
        }
    }
}