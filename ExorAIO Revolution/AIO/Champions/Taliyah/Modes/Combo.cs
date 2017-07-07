// ReSharper disable ConvertIfStatementToConditionalTernaryExpression


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
    ///     The logics class.
    /// </summary>
    internal partial class Taliyah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The Rylai Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.HasItem(ItemId.RylaisCrystalScepter))
            {
                var bestRylaiTarget = Extensions.GetBestEnemyHeroTarget();
                if (!bestRylaiTarget.IsValidTarget(SpellClass.Q.Range - 50f) ||
                    Invulnerable.Check(bestRylaiTarget, DamageType.Magical))
                {
                    return;
                }

                switch (MenuClass.Spells["q"]["combomode"].As<MenuList>().Value)
                {
                    case 0:
                        if (!this.IsNearWorkedGround())
                        {
                            SpellClass.Q.Cast(bestRylaiTarget);
                        }
                        break;
                    case 1:
                        SpellClass.Q.Cast(bestRylaiTarget);
                        break;
                }
            }

            /// <summary>
            ///     The W->Boulders Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["boulders"].As<MenuBool>().Enabled)
            {
                var bestTargets = ImplementationClass.ITargetSelector.GetOrderedTargets(SpellClass.W.Range - 100f)
                    .Where(t => MenuClass.Spells["w"]["selection"][t.ChampionName.ToLower()].As<MenuList>().Value < 3);

                var objAiHeroes = bestTargets as Obj_AI_Hero[] ?? bestTargets.ToArray();
                foreach (var target in objAiHeroes)
                {
                    var bestBoulderHitPos = this.GetBestBouldersHitPosition(target);
                    var bestBoulderHitPosHitBoulders = this.GetBestBouldersHitPositionHitBoulders(target);
                    if (bestBoulderHitPos != Vector3.Zero && bestBoulderHitPosHitBoulders > 0)
                    {
                        SpellClass.W.Cast(SpellClass.W.GetPrediction(target).CastPosition, bestBoulderHitPos);
                    }
                }
            }

            foreach (var target in GameObjects.EnemyHeroes)
            {
                /// <summary>
                ///     The W Combo Logic.
                /// </summary>
                if (SpellClass.W.Ready &&
                    (SpellClass.E.Ready ||
                     !MenuClass.Spells["w"]["customization"]["onlyeready"].As<MenuBool>().Enabled) &&
                    target.IsValidTarget(SpellClass.W.Range - 100f) &&
                    MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
                {
                    var targetPosAfterW = new Vector3();
                    switch (MenuClass.Spells["w"]["selection"][target.ChampionName.ToLower()].As<MenuList>().Value)
                    {
                        case 0:
                            targetPosAfterW = this.GetUnitPositionAfterPull(target);
                            break;
                        case 1:
                            targetPosAfterW = this.GetUnitPositionAfterPush(target);
                            break;

                        /// <summary>
                        ///     Pull if killable else Push.
                        /// </summary>
                        case 2:
                            var isKillable = target.GetRealHealth() < UtilityClass.Player.GetSpellDamage(target, SpellSlot.Q) * (this.IsNearWorkedGround() ? 1 : 3) +
                                             UtilityClass.Player.GetSpellDamage(target, SpellSlot.W) +
                                             UtilityClass.Player.GetSpellDamage(target, SpellSlot.E);
                            if (isKillable)
                            {
                                targetPosAfterW = this.GetUnitPositionAfterPull(target);
                            }
                            else
                            {
                                targetPosAfterW = this.GetUnitPositionAfterPush(target);
                            }
                            break;

                        /// <summary>
                        ///     Pull if not near else Push.
                        /// </summary>
                        case 3:
                            if (UtilityClass.Player.Distance(this.GetUnitPositionAfterPull(target)) >= 250f)
                            {
                                targetPosAfterW = this.GetUnitPositionAfterPull(target);
                            }
                            else
                            {
                                targetPosAfterW = this.GetUnitPositionAfterPush(target);
                            }
                            break;

                        /// <summary>
                        ///     Ignore Target If Possible.
                        /// </summary>
                        case 4:
                            if (!GameObjects.EnemyHeroes.Any(
                                    t =>
                                        t.IsValidTarget(SpellClass.W.Range) &&
                                        MenuClass.Spells["w"]["selection"][t.ChampionName.ToLower()].As<MenuList>().Value < 3))
                            {
                                if (UtilityClass.Player.Distance(this.GetUnitPositionAfterPull(target)) >= 250f)
                                {
                                    targetPosAfterW = this.GetUnitPositionAfterPull(target);
                                }
                                else
                                {
                                    targetPosAfterW = this.GetUnitPositionAfterPush(target);
                                }
                            }
                            break;
                    }

                    var targetPred = SpellClass.W.GetPrediction(target).CastPosition;
                    SpellClass.W.Cast(targetPred, targetPosAfterW);
                    SpellClass.E.Cast(targetPred, targetPosAfterW);
                }
            }

            var bestTarget = Extensions.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                bestTarget.IsValidTarget(SpellClass.Q.Range - 50f))
            {
                switch (MenuClass.Spells["q"]["combomode"].As<MenuList>().Value)
                {
                    case 0:
                        if (!this.IsNearWorkedGround())
                        {
                            SpellClass.Q.Cast(bestTarget);
                        }
                        break;
                    case 1:
                        SpellClass.Q.Cast(bestTarget);
                        break;
                }
            }
        }

        #endregion
    }
}