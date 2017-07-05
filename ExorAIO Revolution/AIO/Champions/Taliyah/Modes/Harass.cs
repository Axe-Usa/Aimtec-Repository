
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
    internal partial class Taliyah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
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
            ///     The Q Harass Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                bestTarget.IsValidTarget(SpellClass.Q.Range) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["harass"]) &&
                MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Enabled &&
                MenuClass.Spells["q"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
            {
                if (this.IsNearWorkedGround() &&
                    MenuClass.Spells["q"]["harassfull"].As<MenuBool>().Value)
                {
                    return;
                }

                SpellClass.Q.Cast(bestTarget);
            }
        }

        #endregion
    }
}