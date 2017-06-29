
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Akali
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Harass()
        {
            var heroTarget = UtilityClass.GetBestEnemyHeroTarget();
            if (!heroTarget.IsValidTarget() ||
                Invulnerable.Check(heroTarget, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The Q Harass Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                heroTarget.IsValidTarget(SpellClass.Q.Range) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["harass"]) &&
                MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.CastOnUnit(heroTarget);
            }
        }

        #endregion
    }
}