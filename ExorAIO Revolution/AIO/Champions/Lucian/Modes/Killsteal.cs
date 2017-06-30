
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
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
        ///     Fired when the game is updated.
        /// </summary>
        public static void Killsteal()
        {
            /// <summary>
            ///     The Q Killsteal Logic.
            /// </summary>
            if (SpellClass.Q.Ready)
            {
                /// <summary>
                ///     Normal.
                /// </summary>
                if (MenuClass.Spells["q"]["killsteal"].As<MenuBool>().Enabled)
                {
                    var bestTarget = SpellClass.Q.GetBestKillableHero(DamageType.Physical);
                    if (bestTarget != null &&
                        !bestTarget.IsValidTarget(UtilityClass.Player.AttackRange) &&
                        UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q) >= bestTarget.GetRealHealth())
                    {
                        SpellClass.Q.CastOnUnit(bestTarget);
                    }
                }

                /// <summary>
                ///     Extended.
                /// </summary>
                if (MenuClass.Spells["q"]["extended"]["killsteal"].As<MenuBool>().Enabled)
                {
                    foreach (var minion in from minion in UtilityClass.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range)
                        let polygon = new Geometry.Rectangle((Vector2)UtilityClass.Player.Position, (Vector2)UtilityClass.Player.Position.Extend(minion.Position, SpellClass.Q2.Range), SpellClass.Q2.Width)
                        where polygon.IsInside((Vector2)SpellClass.Q2.GetPrediction(SpellClass.Q.GetBestKillableHero(DamageType.Physical)).PredictedPosition)
                        select minion)
                    {
                        SpellClass.Q.CastOnUnit(minion);
                    }
                }
            }

            /// <summary>
            ///     The KillSteal W Logic.
            /// </summary>
            if (MenuClass.Spells["w"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.W.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.AttackRange) &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.W) >= bestTarget.GetRealHealth())
                {
                    SpellClass.W.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}