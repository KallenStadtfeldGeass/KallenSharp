using System;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using SPrediction;
using _Project_Geass.Functions.Objects;
using Collision = SPrediction.Collision;

namespace _Project_Geass.Functions
{

    internal class Prediction
    {
        #region Public Fields

        public static readonly int PredictionMethod=StaticObjects.SettingsMenu.Item($"{Names.Menu.BaseItem}.PredictionMethod").GetValue<StringList>().SelectedIndex;

        #endregion Public Fields

        #region Private Methods

        private static bool ValidChampion(Obj_AI_Hero target) {return !target.HasBuffOfType(BuffType.Invulnerability)&&!target.HasBuffOfType(BuffType.SpellImmunity)&&!target.HasBuffOfType(BuffType.SpellShield);}

        #endregion Private Methods

        #region Public Methods

        public static bool CheckColision(PredictionOutput prediction) //Returns if a colision is meet
        {
            foreach (var obj in prediction.CollisionObjects) StaticObjects.ProjectLogger.WriteLog($"Name:{obj.Name}:IsDead:{obj.IsDead}:IsEnemy{obj.IsEnemy}:IsChampion:{obj.IsChampion()}");
            return prediction.CollisionObjects.Any(obj => !obj.IsDead||!obj.IsChampion()||!obj.IsEnemy);
        }

        public static bool CheckColision(SebbyLib.Prediction.PredictionOutput prediction) //Returns if a colision is meet
        {
            foreach (var obj in prediction.CollisionObjects) StaticObjects.ProjectLogger.WriteLog($"Name:{obj.Name}:IsDead:{obj.IsDead}:IsEnemy{obj.IsEnemy}:IsChampion:{obj.IsChampion()}");
            return prediction.CollisionObjects.Any(obj => !obj.IsDead||!obj.IsChampion()||!obj.IsEnemy);
        }

        public static bool CheckColision(SPrediction.Prediction.Result prediction) //Returns if a colision is meet
        {
            foreach (var obj in prediction.CollisionResult.Units) StaticObjects.ProjectLogger.WriteLog($"Name:{obj.Name}:IsDead:{obj.IsDead}:IsEnemy{obj.IsEnemy}:IsChampion:{obj.IsChampion()}");
            return prediction.CollisionResult.Objects.HasFlag(Collision.Flags.Minions)||prediction.CollisionResult.Objects.HasFlag(Collision.Flags.YasuoWall);
        }

        public static bool DoCast(Spell spell, Obj_AI_Hero target, HitChance minHitChance, bool colisionCheck=false)
        {
            StaticObjects.ProjectLogger.WriteLog("DoCast Call");
            if (PredictionMethod==0)
            {
                var output=spell.GetPrediction(target);

                if (colisionCheck) if (CheckColision(output)) return false;

                if (minHitChance>output.Hitchance) return false;
                spell.Cast(output.CastPosition);
                return true;
            }
            if (PredictionMethod==1)
            {
                var output=SebbyLib.Prediction.Prediction.GetPrediction(target, spell.Delay);
                if (colisionCheck) if (CheckColision(output)) return false;

                if (minHitChance>(HitChance)output.Hitchance) return false;
                spell.Cast(output.CastPosition);
                return true;
            }
            if (PredictionMethod==2)
            {
                var output=spell.GetSPrediction(target);

                if (colisionCheck) if (CheckColision(output)) return false;

                if (minHitChance>output.HitChance) return false;
                spell.Cast(output.CastPosition);
                return true;
            }
            return false;
        }

        public static HitChance GetHitChance(string value) => (HitChance)Enum.Parse(typeof(HitChance), value);

        public static string[] GetHitChanceNames()
        {
            var names=Enum.GetNames(typeof(HitChance));
            return new[] {names[(int)HitChance.High], names[(int)HitChance.VeryHigh], names[(int)HitChance.Medium], names[(int)HitChance.Low], names[(int)HitChance.Immobile], names[(int)HitChance.Dashing]};
        }

        public static IOrderedEnumerable<Obj_AI_Hero> OrderTargets(Spell spell)
        {
            var damageType=spell.DamageType==TargetSelector.DamageType.Physical;
            return damageType? Heroes.GetEnemies(spell.Range).Where(ValidChampion).OrderBy(hp => hp.Health/hp.PercentArmorMod) : Heroes.GetEnemies(spell.Range).Where(ValidChampion).OrderBy(hp => hp.Health/hp.PercentMagicReduction);
        }

        #endregion Public Methods
    }

}