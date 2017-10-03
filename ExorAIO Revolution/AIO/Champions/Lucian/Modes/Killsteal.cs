
using System.Linq;
using Aimtec;
using Aimtec.SDK.Damage;
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
        ///     Fired when the game is updated.
        /// </summary>
        public void Killsteal()
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
                        !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)) &&
                        UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q) >= bestTarget.GetRealHealth())
                    {
                        SpellClass.Q.CastOnUnit(bestTarget);
                    }
                }

                /// <summary>
                ///     Extended.
                /// </summary>
                if (MenuClass.Spells["q2"]["killsteal"].As<MenuBool>().Enabled)
                {
                    var target = Extensions.GetBestEnemyHeroesTargetsInRange(SpellClass.Q2.Range)
                        .MinBy(t => UtilityClass.Player.GetSpellDamage(t, SpellSlot.Q) >= t.GetRealHealth());
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
            }

            /// <summary>
            ///     The KillSteal W Logic.
            /// </summary>
            if (MenuClass.Spells["w"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.W.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)) &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.W) >= bestTarget.GetRealHealth())
                {
                    SpellClass.W.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}