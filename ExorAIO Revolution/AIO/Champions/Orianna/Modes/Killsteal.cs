
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
    ///     The logics class.
    /// </summary>
    internal partial class Orianna
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Killsteal()
        {
            if (BallPosition == null)
            {
                return;
            }

            /// <summary>
            ///     The KillSteal Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.Q.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q) >= bestTarget.GetRealHealth())
                {
                    SpellClass.Q.GetPredictionInput(bestTarget).From = (Vector3)BallPosition;
                    SpellClass.Q.Cast(SpellClass.Q.GetPrediction(bestTarget).CastPosition);
                }
            }

            /// <summary>
            ///     The KillSteal W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["killsteal"].As<MenuBool>().Enabled)
            {
                if (GameObjects.EnemyHeroes.Any(t =>
                        t.IsValidTarget(SpellClass.W.Width, false, false, (Vector3)BallPosition) &&
                        UtilityClass.Player.GetSpellDamage(t, SpellSlot.W) >= t.GetRealHealth()))
                {
                    SpellClass.W.Cast();
                    return;
                }
            }

            /// <summary>
            ///     The KillSteal R Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["killsteal"].As<MenuBool>().Enabled)
            {
                foreach (var enemy in GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(SpellClass.R.Width - t.BoundingRadius - SpellClass.R.Delay * t.BoundingRadius, false, false, (Vector3)BallPosition)))
                {
                    var dmg = UtilityClass.Player.GetSpellDamage(enemy, SpellSlot.R);
                    if (SpellClass.Q.Ready &&
                        enemy.IsValidTarget(SpellClass.Q.Range))
                    {
                        dmg += UtilityClass.Player.GetSpellDamage(enemy, SpellSlot.Q);
                    }
                    if (SpellClass.W.Ready &&
                       enemy.IsValidTarget(SpellClass.W.Width - SpellClass.W.Delay * enemy.BoundingRadius, false, false, (Vector3)BallPosition))
                    {
                        dmg += UtilityClass.Player.GetSpellDamage(enemy, SpellSlot.W);
                    }
                    if (UtilityClass.Player.ServerPosition.Distance((Vector3)BallPosition) < UtilityClass.Player.AttackRange)
                    {
                        dmg += UtilityClass.Player.GetAutoAttackDamage(enemy);
                    }

                    if (dmg >= enemy.GetRealHealth())
                    {
                        SpellClass.R.Cast();
                    }
                }
            }
        }

        #endregion
    }
}