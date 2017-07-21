
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Kalista
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     Orbwalk on minions.
            /// </summary>
            var minion = Extensions.GetAllGenericMinionsTargetsInRange(UtilityClass.Player.AttackRange).OrderBy(o => o.Health).FirstOrDefault();
            if (minion != null &&
                !GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(UtilityClass.Player.GetFullAttackRange(t))) &&
                MenuClass.Miscellaneous["minionsorbwalk"].As<MenuBool>().Enabled)
            {
                UtilityClass.Player.IssueOrder(OrderType.AttackUnit, minion);
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            var playerSpellbook = UtilityClass.Player.SpellBook;
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.Mana >=
                    playerSpellbook.GetSpell(SpellSlot.Q).Cost +
                    playerSpellbook.GetSpell(SpellSlot.E).Cost &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range);
                if (bestTarget.IsValidTarget() &&
                    !Invulnerable.Check(bestTarget))
                {
                    var collisions = (IList<Obj_AI_Base>)SpellClass.Q.GetPrediction(bestTarget).CollisionObjects;
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
        }

        #endregion
    }
}