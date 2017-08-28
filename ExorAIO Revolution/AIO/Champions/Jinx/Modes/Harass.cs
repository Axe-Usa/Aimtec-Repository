
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
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
            var heroTarget = args.Target as Obj_AI_Hero;
            if (heroTarget == null)
            {
                return;
            }

            /// <summary>
            ///     The Fishbones to PowPow Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                IsUsingFishBones())
            {
                if (UtilityClass.Player.ManaPercent() <= MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Value)
                {
                    SpellClass.Q.Cast();
                    return;
                }

                var minEnemies = MenuClass.Spells["q"]["customization"]["minenemies"];
                if (heroTarget.IsValidTarget(SpellClass.Q.Range) &&
                    minEnemies == null || minEnemies?.As<MenuSliderBool>().Value >
                        heroTarget.CountEnemyHeroesInRange(MenuClass.Spells["q"]["customization"]["splashrange"].As<MenuSlider>().Value))
                {
                    SpellClass.Q.Cast();
                }
            }
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Harass()
        {
            /// <summary>
            ///     The PowPow to Fishbones Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                !IsUsingFishBones() &&
                MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Enabled)
            {
                if (UtilityClass.Player.ManaPercent() <= MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Value)
                {
                    return;
                }

                var target = ImplementationClass.IOrbwalker.GetOrbwalkingTarget();
                var minEnemies = MenuClass.Spells["q"]["customization"]["minenemies"];
                if (!Extensions.GetEnemyHeroesTargetsInRange(SpellClass.Q.Range).Any() &&
                    Extensions.GetEnemyHeroesTargetsInRange(SpellClass.Q2.Range + 200f).Any() ||
                    minEnemies != null && minEnemies.As<MenuSliderBool>().Value <=
                        target?.CountEnemyHeroesInRange(MenuClass.Spells["q"]["customization"]["splashrange"].As<MenuSlider>().Value))
                {
                    SpellClass.Q.Cast();
                }
            }

            /// <summary>
            ///     The W Harass Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["harass"]) &&
                MenuClass.Spells["w"]["harass"].As<MenuSliderBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.W.Range);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget) &&
                    MenuClass.Spells["w"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                {
                    SpellClass.W.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}