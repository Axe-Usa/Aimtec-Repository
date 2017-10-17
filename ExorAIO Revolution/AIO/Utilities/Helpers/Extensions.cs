using System.Collections.Generic;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.TargetSelector;
using Spell = Aimtec.SDK.Spell;

namespace AIO.Utilities
{
    /// <summary>
    ///     The UtilityData class.
    /// </summary>
    internal static class Extensions
    {
        #region Public Methods and Operators

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
            return GameObjects.AllyHeroes.Where(h => h.IsValidTarget(range, true)).ToList();
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
            Obj_AI_Hero target = null;

            var selectedTarget = TargetSelector.GetSelectedTarget();
            if (selectedTarget != null)
            {
                target = selectedTarget;
            }

            var orbTarget = ImplementationClass.IOrbwalker.GetOrbwalkingTarget() as Obj_AI_Hero;
            if (orbTarget != null)
            {
                target = orbTarget;
            }

            var tsTarget = ImplementationClass.ITargetSelector.GetTarget(range);
            if (tsTarget != null)
            {
                target = tsTarget;
            }

            if (!target.IsZombie() &&
                target.IsValidTarget() &&
                !Invulnerable.Check(target))
            {
                return target;
            }

            return null;
        }

        /// <summary>
        ///     Gets the best valid killable enemy hero target in the game inside a determined range.
        /// </summary>
        public static Obj_AI_Hero GetBestKillableHero(this Spell spell, DamageType damageType = DamageType.True, bool ignoreShields = false)
        {
            return ImplementationClass.ITargetSelector.GetOrderedTargets(spell.Range-100f).FirstOrDefault(t => !t.IsZombie() && !Invulnerable.Check(t, damageType, ignoreShields));
        }

        /// <summary>
        ///     Gets the best valid killable enemy heroes targets in the game inside a determined range.
        /// </summary>
        public static IEnumerable<Obj_AI_Hero> GetBestKillableHeroes(this Spell spell, DamageType damageType = DamageType.True, bool ignoreShields = false)
        {
            return ImplementationClass.ITargetSelector.GetOrderedTargets(spell.Range).Where(t => !t.IsZombie() && !Invulnerable.Check(t, damageType, ignoreShields));
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
            return GameObjects.EnemyMinions.Where(m => m.IsValidSpellTarget(range) && m.UnitSkinName.Contains("Minion") && !m.UnitSkinName.Contains("Odin")).ToList();
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
            if (MenuClass.General["junglesmall"].As<MenuBool>().Enabled)
            {
                return GameObjects.Jungle.Where(m => m.IsValidSpellTarget(range)).ToList();
            }

            return GameObjects.Jungle.Where(m => (!GameObjects.JungleSmall.Contains(m) || m.UnitSkinName.Equals("Sru_Crab")) && m.IsValidSpellTarget(range)).ToList();
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
            return GameObjects.JungleLarge.Where(m => m.IsValidSpellTarget(range)).ToList();
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
            return GameObjects.JungleLegendary.Where(m => m.IsValidSpellTarget(range)).ToList();
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
            return GameObjects.JungleSmall.Where(m => m.IsValidSpellTarget(range)).ToList();
        }

        #endregion
    }
}