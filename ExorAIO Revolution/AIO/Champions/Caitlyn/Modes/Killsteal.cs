
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Damage.JSON;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

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
        public void Killsteal()
        {
            /// <summary>
            ///     The KillSteal Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.Q.GetBestKillableHero(DamageType.Physical);
                if (bestTarget != null &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)))
                {
                    var collisions = SpellClass.Q.GetPrediction(bestTarget).Collisions;
                    if (collisions.Any())
                    {
                        if (bestTarget.HasBuff("caitlynyordletrapsight"))
                        {
                            if (UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q, DamageStage.SecondForm) >= bestTarget.GetRealHealth())
                            {
                                SpellClass.Q.Cast(bestTarget);
                            }
                        }
                        else
                        {
                            if (UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q) >= bestTarget.GetRealHealth())
                            {
                                SpellClass.Q.Cast(bestTarget);
                            }
                        }
                    }
                    else
                    {
                        if (UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q, DamageStage.SecondForm) >= bestTarget.GetRealHealth())
                        {
                            SpellClass.Q.Cast(bestTarget);
                        }
                    }
                }
            }
        }

        #endregion
    }
}