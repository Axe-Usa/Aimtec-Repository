
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ahri
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Harass()
        {
            var bestTarget = Extensions.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The W Harass Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                bestTarget.IsValidTarget(SpellClass.W.Range) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["harass"]) &&
                MenuClass.Spells["w"]["harass"].As<MenuSliderBool>().Enabled &&
                MenuClass.Spells["w"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
            {
                SpellClass.W.Cast();
            }

            /// <summary>
            ///     The Q Harass Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                bestTarget.IsValidTarget(SpellClass.Q.Range) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["harass"]) &&
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
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["harass"]) &&
                MenuClass.Spells["e"]["harass"].As<MenuSliderBool>().Enabled &&
                MenuClass.Spells["e"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
            {
                SpellClass.E.Cast(bestTarget);
            }
        }

        #endregion
    }
}