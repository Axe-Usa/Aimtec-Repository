
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

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
        public void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic W Logic. 
            /// </summary>
            if (SpellClass.W.Ready &&
                SpellClass.E.Ready &&
                MenuClass.Spells["w"]["logical"].As<MenuBool>().Enabled)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsImmobile() &&
                        t.IsValidTarget(SpellClass.W.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false)))
                {
                    Vector3 targetPosAfterW;
                    var targetPred = SpellClass.W.GetPrediction(target).CastPosition;
                    if (UtilityClass.Player.Distance(this.GetUnitPositionAfterPull(target)) >= 200f)
                    {
                        targetPosAfterW = this.GetUnitPositionAfterPull(target);
                    }
                    else
                    {
                        targetPosAfterW = this.GetUnitPositionAfterPush(target);
                    }

                    //SpellClass.W.Cast(targetPred, targetPosAfterW);
                    SpellClass.W.Cast(targetPosAfterW, targetPred);
                }
            }
        }

        #endregion
    }
}