
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Damage.JSON;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class MissFortune
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
                if (MenuClass.Spells["extendedq"]["killsteal"].As<MenuBool>().Enabled)
                {
                    var unitsInQRange = Extensions.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range);
                    var unitsToIterateIfPlayerNormallyKillable =
                        unitsInQRange.Any(m => m.Health < UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q))
                            ? unitsInQRange.Where(m => m.Health < UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q)).ToList()
                            : unitsInQRange.ToList();
                    var unitsToIterateIfPlayerEmpoweredKillable =
                            unitsToIterateIfPlayerNormallyKillable.Where(u => u.GetRealHealth() < UtilityClass.Player.GetSpellDamage(u, SpellSlot.Q)).ToList();

                    List<Obj_AI_Base> unitsToIterate = null;
                    var heroMultiplier = this.GetHeroLoveTapDamageMultiplier();
                    foreach (var hero in Extensions.GetBestEnemyHeroesTargetsInRange(SpellClass.Q2.Range))
                    {
                        if (UtilityClass.Player.GetSpellDamage(hero, SpellSlot.Q) + heroMultiplier >= hero.GetRealHealth())
                        {
                            unitsToIterate = unitsToIterateIfPlayerNormallyKillable;
                        }
                        else if (UtilityClass.Player.GetSpellDamage(hero, SpellSlot.Q, DamageStage.Empowered) + heroMultiplier >= hero.GetRealHealth())
                        {
                            unitsToIterate = unitsToIterateIfPlayerEmpoweredKillable;
                        }

                        if (unitsToIterate != null)
                        {
                            foreach (var minion in unitsToIterate)
                            {
                                var polygon = this.QCone(minion);
                                if ((this.LoveTapTargetNetworkId == hero.NetworkId || unitsToIterate.All(m => polygon.IsOutside((Vector2)m.ServerPosition))) &&
                                    polygon.IsInside((Vector2)Prediction.GetPrediction(hero, UtilityClass.Player.Distance(hero) / SpellClass.Q2.Speed + SpellClass.Q2.Delay).UnitPosition))
                                {
                                    if (UtilityClass.Player.GetSpellDamage(hero, SpellSlot.Q, DamageStage.Empowered) >= hero.GetRealHealth())
                                    {
                                        SpellClass.Q.CastOnUnit(minion);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}