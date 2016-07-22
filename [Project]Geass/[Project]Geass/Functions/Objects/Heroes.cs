using LeagueSharp;
using LeagueSharp.Common;
using System.Collections.Generic;
using System.Linq;

namespace _Project_Geass.Functions.Objects
{
    public static class Heroes
    {
        public static List<Obj_AI_Hero> GetEnemies() => ObjectManager.Get<Obj_AI_Hero>().Where(enemy => enemy.IsEnemy).ToList();

        public static List<Obj_AI_Hero> GetEnemies(float range) => GetEnemies().Where(enemy => enemy.IsValidTarget(range)).ToList();

        public static List<Obj_AI_Hero> GetAllies() => ObjectManager.Get<Obj_AI_Hero>().Where(ally => !ally.IsEnemy).ToList();

        public static List<Obj_AI_Hero> GetAllies(float range, Obj_AI_Hero player) => GetAllies().Where(ally => ally.Distance(player) < range).ToList();
    }
}