
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using Utilities;

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
            /// <summary>
            ///     The W->Boulders Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Value)
            {
                var bestTargets = UtilityClass.GetBestEnemyHeroesTargetsInRange(SpellClass.W.Range);
                foreach (var target in bestTargets)
                {
                    if (bestTargets.Count() == 1 ||
                        MenuClass.Spells["w"]["selection"][target.ChampionName.ToLower()].As<MenuList>().Value < 3)
                    {
                        continue;
                    }

                    foreach (var boulder in MineField)
                    {
                        var boulderPos = boulder.Value;
                        if (target.Distance(boulderPos) < SpellClass.E.Range &&
                            UtilityClass.Player.Distance(boulderPos) < SpellClass.W.Range)
                        {
                            /*
                            SpellClass.W.Cast(bestTarget, boulderPos);
                            */
                        }
                    }
                }
            }

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
                bestTarget.IsValidTarget(SpellClass.Q.Range - 50f) &&
                UtilityClass.Player.HasItem(ItemId.RylaisCrystalScepter) &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Value)
            {
                switch (MenuClass.Spells["q"]["combomode"][bestTarget.ChampionName.ToLower()].As<MenuList>().Value)
                {
                    case 0:
                        if (!IsNearWorkedGround())
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
                                    MenuClass.Spells["w"]["selection"][t.ChampionName.ToLower()].As<MenuList>().Value < 3))
                                {
                                    SpellClass.W.Cast(target, UtilityClass.Player.Position.Extend(target.Position, UtilityClass.Player.Distance(target) * 2));
                                }
                                break;
                        }
                    }
                    */

                    /// <summary>
                    ///     The W->E Combo Logic.
                    /// </summary>
                    if (SpellClass.E.Ready &&
                        bestTarget.IsValidTarget(SpellClass.W.Range) &&
                        MenuClass.Spells["e"]["combo"].As<MenuBool>().Value)
                    {
                        SpellClass.E.Cast(bestTarget.Position);
                    }
                }
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                bestTarget.IsValidTarget(SpellClass.W.Range) &&
                UtilityClass.Player.SpellBook.GetSpell(SpellSlot.W).CooldownEnd - Game.ClockTime <= 3f &&
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
                switch (MenuClass.Spells["q"]["combomode"][bestTarget.ChampionName.ToLower()].As<MenuList>().Value)
                {
                    case 0:
                        if (!IsNearWorkedGround())
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