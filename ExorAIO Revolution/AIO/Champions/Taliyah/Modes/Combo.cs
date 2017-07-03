
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
        public static void Combo()
        {
            var bestTarget = UtilityClass.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The Rylai Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                bestTarget.IsValidTarget(SpellClass.Q.Range) &&
                UtilityClass.Player.HasItem(ItemId.RylaisCrystalScepter) &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Value)
            {
                if (IsNearWorkedGround() &&
                    MenuClass.Spells["q"]["combofull"].As<MenuBool>().Value)
                {
                    return;
                }

                SpellClass.Q.Cast(bestTarget);
            }

            foreach (var target in GameObjects.EnemyHeroes)
            {
                /// <summary>
                ///     The W->E Combo Logic.
                /// </summary>
                if (SpellClass.W.Ready &&
                    target.IsValidTarget(SpellClass.W.Range) &&
                    MenuClass.Spells["w"]["combo"].As<MenuBool>().Value)
                {
                    /*
                    if (MineField.Any())
                    {
                        var farmLocation = SpellClass.W.GetLineFarmLocation(MineField, target.BoundingRadius);
                        if (farmLocation.MinionsHit >= 4)
                        {
                            SpellClass.W.Cast(target, farmLocation.Position);
                            return;
                        }
                    }
                    else
                    {
                        if (!SpellClass.E.Ready &&
                            MenuClass.Miscellaneous["onlyeready"].As<MenuBool>().Value)
                        {
                            return;
                        }

                        switch (MenuClass.Spells["w"]["selection"][target.ChampionName.ToLower()].As<MenuList>().Value)
                        {
                            case 0:
                                SpellClass.W.Cast(target, UtilityClass.Player.Position);
                                break;

                            case 1:
                                SpellClass.W.Cast(target, UtilityClass.Player.Position.Extend(target.Position, UtilityClass.Player.Distance(target) * 2));
                                break;

                            case 2:
                                var isKillable = target.GetRealHealth() < UtilityClass.Player.GetSpellDamage(target, SpellSlot.Q) * (IsNearWorkedGround() ? 1 : 3) +
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
                                if (!GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(SpellClass.W.Range) &&
                                    MenuClass.Spells["w"]["selection"][target.ChampionName.ToLower()].As<MenuList>().Value < 3))
                                {
                                    SpellClass.W.Cast(target, UtilityClass.Player.Position.Extend(target.Position, UtilityClass.Player.Distance(target) * 2));
                                }
                                break;
                        }
                    }
                    */
                }
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                bestTarget.IsValidTarget(SpellClass.W.Range) &&
                UtilityClass.Player.SpellBook.GetSpell(SpellSlot.W).CooldownEnd - Game.ClockTime <= 3.5f &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Value)
            {
                SpellClass.E.Cast(bestTarget.Position);
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                bestTarget.IsValidTarget(SpellClass.Q.Range - 50f) &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Value)
            {
                if (IsNearWorkedGround() &&
                    MenuClass.Spells["q"]["combofull"].As<MenuBool>().Value)
                {
                    return;
                }

                SpellClass.Q.Cast(bestTarget);
            }
        }

        #endregion
    }
}