// ReSharper disable ConvertIfStatementToConditionalTernaryExpression


#pragma warning disable 1587

namespace AIO.Champions
{
    using System;
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
            ///     The W->Boulders Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
            {
                var bestTargets = ImplementationClass.ITargetSelector.GetOrderedTargets(SpellClass.W.Range)
                    .Where(t => MenuClass.Spells["w"]["selection"][t.ChampionName.ToLower()].As<MenuList>().Value < 3);

                var objAiHeroes = bestTargets as Obj_AI_Hero[] ?? bestTargets.ToArray();
                foreach (var target in objAiHeroes)
                {
                    var bestBoulderHitPos = this.GetBestBouldersHitPosition(target);
                    var bestBoulderHitPosHitBoulders = this.GetBestBouldersHitPositionHitBoulders(target);
                    if (bestBoulderHitPos != Vector3.Zero && bestBoulderHitPosHitBoulders > 0)
                    {
                        SpellClass.W.Cast(target, bestBoulderHitPos);
                    }
                }
            }

            var bestTarget = Extensions.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The Rylai Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                bestTarget.IsValidTarget(SpellClass.Q.Range - 50f) &&
                UtilityClass.Player.HasItem(ItemId.RylaisCrystalScepter))
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

            foreach (var target in GameObjects.EnemyHeroes)
            {
                /// <summary>
                ///     The W->E Combo Logic.
                /// </summary>
                if (SpellClass.W.Ready &&
                    (SpellClass.E.Ready ||
                     !MenuClass.Spells["w"]["customization"]["onlyeready"].As<MenuBool>().Enabled) &&
                    target.IsValidTarget(SpellClass.W.Range) &&
                    MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
                {
                    switch (MenuClass.Spells["w"]["selection"][target.ChampionName.ToLower()].As<MenuList>().Value)
                    {
                        case 0:
                            SpellClass.W.Cast(target, UtilityClass.Player.Position);
                            break;
                        case 1:
                            SpellClass.W.Cast(target, UtilityClass.Player.Position.Extend(target.Position, UtilityClass.Player.Distance(target) * 2));
                            break;
                        case 2:
                            var isKillable = target.GetRealHealth() < UtilityClass.Player.GetSpellDamage(target, SpellSlot.Q) * (this.IsNearWorkedGround() ? 1 : 3) +
                                             UtilityClass.Player.GetSpellDamage(target, SpellSlot.W) +
                                             UtilityClass.Player.GetSpellDamage(target, SpellSlot.E);
                            if (isKillable)
                            {
                                SpellClass.W.Cast(target, UtilityClass.Player.Position);
                            }
                            else
                            {
                                SpellClass.W.Cast(target, UtilityClass.Player.Position.Extend(target.Position, UtilityClass.Player.Distance(target) * 2));
                            }
                            break;
                        case 3:
                            if (!GameObjects.EnemyHeroes.Any(
                                    t =>
                                        t.IsValidTarget(SpellClass.W.Range) &&
                                        MenuClass.Spells["w"]["selection"][t.ChampionName.ToLower()].As<MenuList>().Value < 3))
                            {
                                SpellClass.W.Cast(target, UtilityClass.Player.Position.Extend(target.Position, UtilityClass.Player.Distance(target) * 2));
                            }
                            break;
                    }
                }
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                bestTarget.IsValidTarget(SpellClass.W.Range) &&
                UtilityClass.Player.SpellBook.GetSpell(SpellSlot.W).CooldownEnd - Game.ClockTime <= 3f &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
            {
                SpellClass.E.Cast(bestTarget.Position);
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                bestTarget.IsValidTarget(SpellClass.Q.Range - 50f))
            {
                Console.WriteLine("The Q Combo Logic. 1111");
                switch (MenuClass.Spells["q"]["combomode"].As<MenuList>().Value)
                {
                    case 0:
                        if (!this.IsNearWorkedGround())
                        {
                            Console.WriteLine("The Q Combo Logic. 2222");
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