
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Kalista
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Combo()
        {
            /// <summary>
            ///     Orbwalk on minions.
            /// </summary>
            var minionsInAaRange = Extensions.GetAllGenericMinionsTargets().Where(m => m.IsValidTarget(UtilityClass.Player.GetFullAttackRange(m))).ToArray();
            if (minionsInAaRange.Any() &&
                UtilityClass.Player.HasItem(ItemId.RunaansHurricane) &&
                !GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(UtilityClass.Player.GetFullAttackRange(t))) &&
                MenuClass.Miscellaneous["minionsorbwalk"].As<MenuBool>().Enabled)
            {
                UtilityClass.Player.IssueOrder(OrderType.AttackUnit, minionsInAaRange.FirstOrDefault());
            }

            var bestTarget = Extensions.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Physical))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            var playerSpellbook = UtilityClass.Player.SpellBook;
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.Mana <
                    playerSpellbook.GetSpell(SpellSlot.Q).Cost +
                    playerSpellbook.GetSpell(SpellSlot.E).Cost &&
                bestTarget.IsValidTarget(SpellClass.Q.Range) &&
                !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)) &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                var collisions = (IList<Obj_AI_Base>)SpellClass.Q.GetPrediction(bestTarget).Collisions;
                if (collisions.Any())
                {
                    if (collisions.All(c => Extensions.GetAllGenericUnitTargets().Contains(c) && c.GetRealHealth() < UtilityClass.Player.GetSpellDamage(c, SpellSlot.Q)))
                    {
                        SpellClass.Q.Cast(bestTarget);
                    }
                }
                else
                {
                    SpellClass.Q.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}