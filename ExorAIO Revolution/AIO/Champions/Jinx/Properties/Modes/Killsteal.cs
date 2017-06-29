
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Jinx
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Killsteal()
        {
            /// <summary>
            ///     The KillSteal W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.CountEnemyHeroesInRange(SpellClass.Q.Range) < 3 &&
                MenuClass.Spells["w"]["killsteal"].As<MenuBool>().Value)
            {
                var bestTarget = SpellClass.Q.GetBestKillableHero(DamageType.Physical);
                if (bestTarget != null &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.AttackRange))
                {
                    if (UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.W) >= bestTarget.GetRealHealth())
                    {
                        SpellClass.W.Cast(bestTarget);
                    }
                }
            }
        }

        #endregion
    }
}