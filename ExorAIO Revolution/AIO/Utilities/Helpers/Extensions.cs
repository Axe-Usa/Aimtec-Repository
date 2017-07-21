namespace AIO.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The UtilityData class.
    /// </summary>
    internal static class Extensions
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Counts the ally heroes inside a determined range of a determined unit.
        /// </summary>
        public static int CountAllyHeroesInRange(float range, GameObject unit)
        {
            return GameObjects.AllyHeroes.Count(h => h.Distance(unit) < range);
        }

        /// <summary>
        ///     Counts the enemy heroes inside a determined range of a determined unit.
        /// </summary>
        public static int CountEnemyHeroesInRange(float range, GameObject unit)
        {
            return GameObjects.EnemyHeroes.Count(h => h.Distance(unit) < range);
        }

        /// <summary>
        ///     Gets the valid generic (lane or jungle) minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetAllGenericMinionsTargets()
        {
            return GetAllGenericMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid generic (lane or jungle) minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetAllGenericMinionsTargetsInRange(float range)
        {
            return GetEnemyLaneMinionsTargetsInRange(range).Concat(GetGenericJungleMinionsTargetsInRange(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid generic unit targets in the game.
        /// </summary>
        public static List<Obj_AI_Base> GetAllGenericUnitTargets()
        {
            return GetAllGenericUnitTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid generic unit targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Base> GetAllGenericUnitTargetsInRange(float range)
        {
            return GameObjects.EnemyHeroes.Where(h => h.IsValidTarget(range)).Concat<Obj_AI_Base>(GetAllGenericMinionsTargetsInRange(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid ally heroes targets in the game.
        /// </summary>
        public static List<Obj_AI_Hero> GetAllyHeroesTargets()
        {
            return GetAllyHeroesTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid ally heroes targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Hero> GetAllyHeroesTargetsInRange(float range)
        {
            return GameObjects.AllyHeroes.Where(h => h.IsValidTarget(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid ally lane minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetAllyLaneMinionsTargets()
        {
            return GetAllyLaneMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid ally lane minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetAllyLaneMinionsTargetsInRange(float range)
        {
            return GameObjects.AllyMinions.Where(m => m.IsValidTarget(range, true)).ToList();
        }

        /// <summary>
        ///     Gets the best valid enemy heroes targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Hero> GetBestEnemyHeroesTargets()
        {
            return GetBestEnemyHeroesTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the best valid enemy heroes targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Hero> GetBestEnemyHeroesTargetsInRange(float range)
        {
            return ImplementationClass.ITargetSelector.GetOrderedTargets(range);
        }

        /// <summary>
        ///     Gets the best valid enemy hero target in the game.
        /// </summary>
        public static Obj_AI_Hero GetBestEnemyHeroTarget()
        {
            return GetBestEnemyHeroTargetInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the best valid enemy hero target in the game inside a determined range.
        /// </summary>
        public static Obj_AI_Hero GetBestEnemyHeroTargetInRange(float range)
        {
            var ts = ImplementationClass.ITargetSelector;
            var target = ts.GetTarget(range);
            if (target != null && target.IsValidTarget() && !Invulnerable.Check(target))
            {
                return target;
            }

            var firstTarget = ts.GetOrderedTargets(range).FirstOrDefault(t => t.IsValidTarget() && !Invulnerable.Check(t));
            if (firstTarget != null)
            {
                return firstTarget;
            }

            return null;
        }

        public static Obj_AI_Hero GetBestKillableHero(this Spell spell, DamageType damageType = DamageType.True, bool ignoreShields = false)
        {
            return ImplementationClass.ITargetSelector.GetOrderedTargets(spell.Range-100f).FirstOrDefault(t => !Invulnerable.Check(t, damageType, ignoreShields));
        }

        /// <summary>
        ///     Gets the valid enemy heroes targets in the game.
        /// </summary>
        public static List<Obj_AI_Hero> GetEnemyHeroesTargets()
        {
            return GetEnemyHeroesTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid enemy heroes targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Hero> GetEnemyHeroesTargetsInRange(float range)
        {
            return GameObjects.EnemyHeroes.Where(h => h.IsValidTarget(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid lane minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetEnemyLaneMinionsTargets()
        {
            return GetEnemyLaneMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid lane minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetEnemyLaneMinionsTargetsInRange(float range)
        {
            var blackList = new[] { "SRU_Plant", "Beacon", "ward", "trinket" };
            return GameObjects.EnemyMinions.Where(m => m.IsValidTarget(range) && !blackList.Any(b => m.UnitSkinName.Contains(b))).ToList();
        }

        /// <summary>
        ///     Gets the valid generic (All but small) jungle minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetGenericJungleMinionsTargets()
        {
            return GetGenericJungleMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid generic (All but small) jungle minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetGenericJungleMinionsTargetsInRange(float range)
        {
            var blackList = new[] { "SRU_Plant", "Beacon", "ward", "trinket" };
            return GameObjects.Jungle.Concat(GameObjects.JungleSmall).Where(m => m.IsValidTarget(range) && !blackList.Any(b => m.UnitSkinName.Contains(b))).ToList();
        }

        /// <summary>
        ///     Gets the valid large jungle minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetLargeJungleMinionsTargets()
        {
            return GetLargeJungleMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid large jungle minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetLargeJungleMinionsTargetsInRange(float range)
        {
            var blackList = new[] { "SRU_Plant", "Beacon", "ward", "trinket" };
            return GameObjects.JungleLarge.Where(m => m.IsValidTarget(range) && !blackList.Any(b => m.UnitSkinName.Contains(b))).ToList();
        }

        /// <summary>
        ///     Gets the valid legendary jungle minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetLegendaryJungleMinionsTargets()
        {
            return GetLegendaryJungleMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid legendary jungle minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetLegendaryJungleMinionsTargetsInRange(float range)
        {
            var blackList = new[] { "SRU_Plant", "Beacon", "ward", "trinket" };
            return GameObjects.JungleLegendary.Where(m => m.IsValidTarget(range) && !blackList.Any(b => m.UnitSkinName.Contains(b))).ToList();
        }

        /// <summary>
        ///     Gets the valid small jungle minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetSmallJungleMinionsTargets()
        {
            return GetSmallJungleMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid small jungle minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetSmallJungleMinionsTargetsInRange(float range)
        {
            var blackList = new[] { "SRU_Plant", "Beacon", "ward", "trinket" };
            return GameObjects.JungleSmall.Where(m => m.IsValidTarget(range) && !blackList.Any(b => m.UnitSkinName.Contains(b))).ToList();
        }

        #endregion
    }
}