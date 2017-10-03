
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q2"]["combo"].As<MenuBool>().Enabled)
            {
                var target = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q2.Range);
                if (target.IsValidTarget())
                {
                    foreach (var minion in from minion in Extensions.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range)
                        let polygon = QRectangle(minion)
                        where
                            target != minion &&
                            polygon.IsInside((Vector2)target.ServerPosition) &&
                            polygon.IsInside((Vector2)SpellClass.Q2.GetPrediction(target).CastPosition)
                        select minion)
                    {
                        SpellClass.Q.CastOnUnit(minion);
                    }
                }
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["engage"].As<MenuBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.E.Range);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget) &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)))
                {
                    var posAfterE = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, 425f);
                    if (posAfterE.CountEnemyHeroesInRange(1000f) < 3 &&
                        UtilityClass.Player.Distance(Game.CursorPos) > UtilityClass.Player.AttackRange &&
                        bestTarget.Distance(posAfterE) < UtilityClass.Player.GetFullAttackRange(bestTarget))
                    {
                        SpellClass.E.Cast(posAfterE);
                    }
                }
            }
        }

        #endregion
    }
}