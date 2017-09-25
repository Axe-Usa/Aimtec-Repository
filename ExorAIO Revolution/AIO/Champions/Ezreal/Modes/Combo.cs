
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;

    using Aimtec.SDK.Extensions;
    using Utilities;

    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Prediction.Skillshots;

    using Prediction = Utilities.Prediction;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ezreal
    {
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
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.W.Range - 150f);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical) &&
                    UtilityClass.Player.TotalAbilityDamage >= GetMinimumApForApMode())
                {
                    var output = Prediction.GetPrediction(SpellClass.W, bestTarget);
                    if (output?.HitChance >= SpellClass.W.HitChance)
                    {
                        SpellClass.W.Cast(output.CastPosition);
                    }
                }
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range - 100f);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget) &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)))
                {
                    var output = Prediction.GetPrediction(SpellClass.Q, bestTarget);
                    if (output?.HitChance >= HitChance.Low)
                    {
                        SpellClass.Q.Cast(output.CastPosition);
                    }
                }
            }
        }
    }
}