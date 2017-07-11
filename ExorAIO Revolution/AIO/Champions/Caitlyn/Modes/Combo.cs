
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Caitlyn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
            {
                var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.W.Range);
                if (heroTarget.IsValidTarget() &&
                    !Invulnerable.Check(heroTarget, DamageType.Magical, false) &&
                    SpellClass.W.GetPrediction(heroTarget).HitChance >= HitChance.High)
                {
                    SpellClass.W.Cast(heroTarget);
                }
            }
        }

        #endregion
    }
}