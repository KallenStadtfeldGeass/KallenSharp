using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;

namespace GeassLib.Functions.Objects
{
    public static class Heroes
    {
        public static List<Obj_AI_Hero> GetEnemies()
        {
            return ObjectManager.Get<LeagueSharp.Obj_AI_Hero>().Where(enemy => enemy.IsEnemy).ToList();
        }

        public static List<Obj_AI_Hero> GetAllies()
        {
            return ObjectManager.Get<LeagueSharp.Obj_AI_Hero>().Where(enemy => !enemy.IsEnemy).ToList();
        }
    }
}
