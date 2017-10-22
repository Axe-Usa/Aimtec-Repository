
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Varus
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Killsteal()
        {
            /// <summary>
            ///     The Q KillSteal Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.Q.GetBestKillableHero(DamageType.Physical);
                if (bestTarget != null &&
                    IsChargingPiercingArrow() &&
                    GetRealPiercingArrowDamage(bestTarget) >= bestTarget.GetRealHealth() &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)))
                {
                    SpellClass.Q.Cast(bestTarget);
                }
            }

            /// <summary>
            ///     The E KillSteal Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.E.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    GetRealHailOfArrowsDamage(bestTarget) >= bestTarget.GetRealHealth() &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)))
                {
                    SpellClass.E.Cast(bestTarget);
                }
            }

            /// <summary>
            ///     The R KillSteal Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.R.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    GetRealChainOfCorruptionDamage(bestTarget) >= bestTarget.GetRealHealth() &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)))
                {
                    SpellClass.R.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}