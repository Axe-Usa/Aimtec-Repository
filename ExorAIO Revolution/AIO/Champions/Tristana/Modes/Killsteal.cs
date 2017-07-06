
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
    internal partial class Tristana
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Killsteal()
        {
            /// <summary>
            ///     The R KillSteal Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.R.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null)
                {
                    var shouldIncludeEDamage = bestTarget.HasBuff("TristanaECharge");
                    if (UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.R) +
                        (shouldIncludeEDamage ? this.GetTotalExplosionDamage(bestTarget) : 0) >= bestTarget.GetRealHealth())
                    {
                        SpellClass.R.CastOnUnit(bestTarget);
                    }
                }
            }
        }

        #endregion
    }
}