
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
    internal partial class KogMaw
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Harass()
        {
            var bestTarget = UtilityClass.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The Q Harass Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                bestTarget.IsValidTarget(SpellClass.Q.Range) &&
                UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["harass"]) &&
                MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Enabled &&
                MenuClass.Spells["q"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
            {
                SpellClass.Q.Cast(bestTarget);
            }

            /// <summary>
            ///     The E Harass Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                bestTarget.IsValidTarget(SpellClass.E.Range) &&
                UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["harass"]) &&
                MenuClass.Spells["e"]["harass"].As<MenuSliderBool>().Enabled &&
                MenuClass.Spells["e"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
            {
                SpellClass.E.Cast(bestTarget);
            }
        }

        #endregion
    }
}