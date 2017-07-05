
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Jinx
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on orbwalker action.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public void Harass(object sender, PreAttackEventArgs args)
        {
            /// <summary>
            ///     The Harass Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                > MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Value &&
                MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Enabled)
            {
                if (!UtilityClass.Player.HasBuff("JinxQ"))
                {
                    if (!Extensions.GetEnemyHeroesTargetsInRange(SpellClass.Q.Range).Any() &&
                        Extensions.GetEnemyHeroesTargetsInRange(SpellClass.Q2.Range).Any())
                    {
                        SpellClass.Q.Cast();
                    }
                }
            }
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Harass()
        {
            var bestTarget = Extensions.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Physical))
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
                SpellClass.W.Cast(bestTarget);
            }
        }

        #endregion
    }
}