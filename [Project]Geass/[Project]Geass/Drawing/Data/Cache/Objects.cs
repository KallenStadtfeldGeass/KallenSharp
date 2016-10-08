using System;
using System.Collections.Generic;
using System.Linq;
using LeagueSharp;
using LeagueSharp.Common;
using _Project_Geass.Functions.Objects;
using _Project_Geass.Tick;

namespace _Project_Geass.Drawing.Data.Cache
{

    internal class Objects
    {

        private static List<Obj_AI_Hero> _cacheEnemies;

        private static List<Obj_AI_Base> _cacheMinions;

        /// <summary>
        ///     Gets the cache enemies.
        /// </summary>
        /// <returns></returns>
        public static List<Obj_AI_Hero> GetCacheEnemies() => _cacheEnemies.Where(x => !x.IsDead).ToList();

        /// <summary>
        ///     Gets the cache enemies.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public static List<Obj_AI_Hero> GetCacheEnemies(float range) => _cacheEnemies.Where(x => x.Distance(_Project_Geass.Data.Static.Objects.Player)<range).ToList();

        /// <summary>
        ///     Gets the cache minions.
        /// </summary>
        /// <returns></returns>
        public static List<Obj_AI_Base> GetCacheMinions() => _cacheMinions.Where(x => !x.IsDead).ToList();

        /// <summary>
        ///     Gets the cache minions.
        /// </summary>
        /// <param name="range">The range.</param>
        /// <returns></returns>
        public static List<Obj_AI_Base> GetCacheMinions(float range) => _cacheMinions.Where(x => x.Distance(_Project_Geass.Data.Static.Objects.Player)<range).ToList();

        public static void Load()
        {
            UpdateCache();
            Game.OnUpdate+=OnUpdate;
        }

        private static void UpdateCache()
        {
            _cacheMinions=Minions.GetEnemyMinions2(1500);
            _cacheEnemies=Heroes.GetEnemies();
        }

        private static void OnUpdate(EventArgs args)
        {
            if (!Handler.CheckCacheSync())
                return;

            UpdateCache();
            Handler.UseCacheSync();
        }

    }

}