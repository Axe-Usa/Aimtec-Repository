
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public static void Combo()
        {
            var bestTarget = UtilityClass.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                bestTarget.IsValidTarget(SpellClass.E.Range) &&
                !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)) &&
                MenuClass.Spells["e"]["engage"].As<MenuBool>().Enabled)
            {
                var posAfterE = UtilityClass.Player.Position.Extend(Game.CursorPos, 425f);
                if (posAfterE.CountEnemyHeroesInRange(1000f) < 3 &&
                    UtilityClass.Player.Distance(Game.CursorPos) > UtilityClass.Player.AttackRange &&
                    bestTarget.Distance(posAfterE) < UtilityClass.Player.AttackRange)
                {
                    SpellClass.E.Cast(posAfterE);
                }
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["extended"]["combo"].As<MenuBool>().Enabled)
            {
                foreach (var minion in from minion in UtilityClass.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range)
                    let polygon = new Geometry.Rectangle((Vector2)UtilityClass.Player.Position, (Vector2)UtilityClass.Player.Position.Extend(minion.Position, SpellClass.Q2.Range), SpellClass.Q2.Width)
                    where polygon.IsInside((Vector2)SpellClass.Q2.GetPrediction(UtilityClass.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range)).PredictedPosition)
                    select minion)
                {
                    SpellClass.Q.CastOnUnit(minion);
                }
            }
        }

        #endregion
    }
}