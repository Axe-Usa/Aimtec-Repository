
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Evelynn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Killsteal()
        {
            /// <summary>
            ///     The E KillSteal Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.E.GetBestKillableHero(DamageType.Physical);
                if (bestTarget != null &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)) &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.E) >= bestTarget.GetRealHealth())
                {
                    SpellClass.E.CastOnUnit(bestTarget);
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
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)) &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.R) >= bestTarget.GetRealHealth())
                {
                    SpellClass.R.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}