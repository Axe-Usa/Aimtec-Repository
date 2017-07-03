
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Twitch
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public static void Combo()
        {
            var bestTarget = UtilityClass.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget())
            {
                return;
            }

            /// <summary>
            ///     The W Harass Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                bestTarget.IsValidTarget(SpellClass.W.Range) &&
                MenuClass.Spells["w"]["combo"].As<MenuSliderBool>().Enabled)
            {
                if (bestTarget.CountEnemyHeroesInRange(SpellClass.W.Width) >= MenuClass.Spells["w"]["combo"].As<MenuSliderBool>().Value)
                {
                    SpellClass.W.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}