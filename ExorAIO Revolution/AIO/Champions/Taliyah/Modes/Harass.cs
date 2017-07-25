
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
            /// <summary>
            ///     The Q Harass Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["harass"]) &&
                MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range - 100f);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical) &&
                    SpellClass.Q.GetPrediction(bestTarget).CollisionObjects.Count == 0 &&
                    MenuClass.Spells["q"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                {
                    if (this.IsNearWorkedGround() &&
                        MenuClass.Spells["q"]["customization"]["harassfull"].As<MenuBool>().Enabled)
                    {
                        return;
                    }

                    SpellClass.Q.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}