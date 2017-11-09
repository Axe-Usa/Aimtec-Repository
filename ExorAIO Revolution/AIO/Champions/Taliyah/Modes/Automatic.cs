
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression

using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
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
            SpellClass.R.Range = 1500 + 1500 * UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).Level;

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
                foreach (var target in GameObjects.EnemyHeroes.Where(t =>
                    t.IsImmobile(SpellClass.W.Delay) &&
                    t.IsValidTarget(SpellClass.W.Range) &&
                    !Invulnerable.Check(t, DamageType.Magical, false)))
                {
                    Vector3 targetPosAfterW;
                    var targetPred = SpellClass.W.GetPrediction(target).CastPosition;
                    if (UtilityClass.Player.Distance(GetUnitPositionAfterPull(target)) >= 200f)
                    {
                        targetPosAfterW = GetUnitPositionAfterPull(target);
                    }
                    else
                    {
                        targetPosAfterW = GetUnitPositionAfterPush(target);
                    }

                    //SpellClass.W.Cast(targetPred, targetPosAfterW);
                    SpellClass.W.Cast(targetPosAfterW, targetPred);
                }
            }
        }

        #endregion
    }
}