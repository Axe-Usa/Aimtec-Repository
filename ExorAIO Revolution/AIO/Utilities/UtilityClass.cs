
// ReSharper disable ArrangeMethodOrOperatorBody
#pragma warning disable 1587

namespace AIO.Utilities
{
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.TargetSelector;

    /// <summary>
    ///     The UtilityData class.
    /// </summary>
    internal static class UtilityClass
    {
        #region Static Fields

        /// <summary>
        ///     Gets the Player.
        /// </summary>
        public static Obj_AI_Hero Player = ObjectManager.GetLocalPlayer();

        /// <summary>
        ///     The last tick.
        /// </summary>
        public static int LastTick = 0;

        /// <summary>
        ///     The jungle HP bar offset list.
        /// </summary>
        internal static readonly string[] JungleList =
            {
                "SRU_Dragon_Air", "SRU_Dragon_Fire", "SRU_Dragon_Water",
                "SRU_Dragon_Earth", "SRU_Dragon_Elder", "SRU_Baron",
                "SRU_RiftHerald", "SRU_Red", "SRU_Blue", "SRU_Gromp",
                "Sru_Crab", "SRU_Krug", "SRU_Razorbeak", "SRU_Murkwolf"
            };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Returns true if there is a Wall between X pos and Y pos.
        /// </summary>
        public static bool AnyWallInBetween(Vector3 startPos, Vector3 endPos)
        {
            for (var i = 0; i < startPos.Distance(endPos); i++)
            {
                var point = NavMesh.WorldToCell(startPos.Extend(endPos, i));
                if (point.Flags.HasFlag(NavCellFlags.Wall | NavCellFlags.Building))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Gets the health with Blitzcrank's Shield support.
        /// </summary>
        /// <param name="unit">
        ///     The unit.
        /// </param>
        /// <returns>
        ///     The target Health with Blitzcrank's Shield support.
        /// </returns>
        public static float GetRealHealth(this Obj_AI_Base unit)
        {
            var debuffer = 0f;

            /// <summary>
            ///     Gets the predicted reduction from Blitzcrank Shield.
            /// </summary>
            var hero = unit as Obj_AI_Hero;
            if (hero != null)
            {
                if (hero.ChampionName.Equals("Blitzcrank") && !hero.HasBuff("BlitzcrankManaBarrierCD"))
                {
                    debuffer += hero.Mana / 2;
                }
            }
            return unit.Health + unit.PhysicalShield + unit.HPRegenRate + debuffer;
        }

        /// <summary>
        ///     Gets the valid small jungle minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetSmallJungleMinionsTargetsInRange(float range)
        {
            return GameObjects.JungleSmall.Where(m => m.IsValidTarget(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid small jungle minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetSmallJungleMinionsTargets()
        {
            return GetSmallJungleMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid generic (All but small) jungle minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetGenericJungleMinionsTargetsInRange(float range)
        {
            return GameObjects.Jungle.Where(m => m.IsValidTarget(range) && (!GameObjects.JungleSmall.Contains(m) || m.Name.Equals("Sru_Crab"))).ToList();
        }

        /// <summary>
        ///     Gets the valid generic (All but small) jungle minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetGenericJungleMinionsTargets()
        {
            return GetGenericJungleMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid large jungle minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetLargeJungleMinionsTargetsInRange(float range)
        {
            return GameObjects.JungleLarge.Where(m => m.IsValidTarget(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid large jungle minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetLargeJungleMinionsTargets()
        {
            return GetLargeJungleMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid legendary jungle minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetLegendaryJungleMinionsTargetsInRange(float range)
        {
            return GameObjects.JungleLegendary.Where(m => m.IsValidTarget(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid legendary jungle minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetLegendaryJungleMinionsTargets()
        {
            return GetLegendaryJungleMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid lane minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetEnemyLaneMinionsTargetsInRange(float range)
        {
            return GameObjects.EnemyMinions.Where(m => m.IsValidTarget(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid lane minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetEnemyLaneMinionsTargets()
        {
            return GetEnemyLaneMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid generic unit targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Base> GetAllGenericUnitTargetsInRange(float range)
        {
            return GameObjects.EnemyHeroes.Where(h => h.IsValidTarget(range)).Concat<Obj_AI_Base>(GetAllGenericMinionsTargetsInRange(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid generic unit targets in the game.
        /// </summary>
        public static List<Obj_AI_Base> GetAllGenericUnitTargets()
        {
            return GetAllGenericUnitTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid enemy heroes targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Hero> GetEnemyHeroesTargetsInRange(float range)
        {
            return GameObjects.EnemyHeroes.Where(h => h.IsValidTarget(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid enemy heroes targets in the game.
        /// </summary>
        public static List<Obj_AI_Hero> GetEnemyHeroesTargets()
        {
            return GetEnemyHeroesTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid generic (lane or jungle) minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetAllGenericMinionsTargetsInRange(float range)
        {
            return GetEnemyLaneMinionsTargetsInRange(range).Concat(GetGenericJungleMinionsTargetsInRange(range)).ToList();
        }

        /// <summary>
        ///     Gets the valid generic (lane or jungle) minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetAllGenericMinionsTargets()
        {
            return GetAllGenericMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the valid ally lane minions targets in the game inside a determined range.
        /// </summary>
        public static List<Obj_AI_Minion> GetAllyLaneMinionsTargetsInRange(float range)
        {
            return GameObjects.AllyMinions.Where(m => m.IsValidTarget(range, true)).ToList();
        }

        /// <summary>
        ///     Gets the valid ally lane minions targets in the game.
        /// </summary>
        public static List<Obj_AI_Minion> GetAllyLaneMinionsTargets()
        {
            return GetAllyLaneMinionsTargetsInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the best valid enemy hero target in the game inside a determined range.
        /// </summary>
        public static Obj_AI_Hero GetBestEnemyHeroTargetInRange(float range)
        {
            var ts = TargetSelector.Implementation;
            var target = ts.GetTarget(range);
            if (target != null && !Invulnerable.Check(target))
            {
                return target;
            }

            return ts.GetOrderedTargets(range).FirstOrDefault(t => !Invulnerable.Check(t));
        }

        /// <summary>
        ///     Gets the best valid enemy hero target in the game.
        /// </summary>
        public static Obj_AI_Hero GetBestEnemyHeroTarget()
        {
            return GetBestEnemyHeroTargetInRange(float.MaxValue);
        }

        /// <summary>
        ///     Gets the best valid enemy heroes targets in the game inside a determined range.
        /// </summary>
        public static IOrderedEnumerable<Obj_AI_Hero> GetBestEnemyHeroesTargetsInRange(float range)
        {
            return TargetSelector.Implementation.GetOrderedTargets(range);
        }

        /// <summary>
        ///     Gets the best valid enemy heroes targets in the game inside a determined range.
        /// </summary>
        public static IOrderedEnumerable<Obj_AI_Hero> GetBestEnemyHeroesTargets()
        {
            return GetBestEnemyHeroesTargetsInRange(float.MaxValue);
        }

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

        public static Obj_AI_Hero GetBestKillableHero(this Aimtec.SDK.Spell spell, DamageType damageType = DamageType.True, bool ignoreShields = false)
        {
            return GameObjects.EnemyHeroes
                .Where(t =>
                    t.IsValidTarget(spell.Range) &&
                    !Invulnerable.Check(t, damageType, ignoreShields))
                .OrderBy(o => TargetSelector.Implementation.GetOrderedTargets(spell.Range))
                .FirstOrDefault();
        }

        #endregion
    }
}