
using System;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using AIO.Utilities;

#pragma warning disable 1587
namespace AIO
{
    /// <summary>
    ///     The general class.
    /// </summary>
    internal partial class General
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on postattack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPostAttack(object sender, PostAttackEventArgs args)
        {
            if (args.Target.IsBuilding())
            {
                return;
            }

            var hydraItems = new[] { ItemId.TitanicHydra, ItemId.RavenousHydra, ItemId.Tiamat };
            if (MenuClass.Hydra != null &&
                UtilityClass.Player.Inventory.Slots.Any(t => hydraItems.Contains(t.ItemId)))
            {
                switch (ImplementationClass.IOrbwalker.Mode)
                {
                    case OrbwalkingMode.Combo:
                        if (!MenuClass.Hydra["combo"].As<MenuBool>().Enabled)
                        {
                            return;
                        }
                        break;
                    case OrbwalkingMode.Mixed:
                        if (!MenuClass.Hydra["mixed"].As<MenuBool>().Enabled)
                        {
                            return;
                        }
                        break;
                    case OrbwalkingMode.Laneclear:
                        if (!MenuClass.Hydra["laneclear"].As<MenuBool>().Enabled)
                        {
                            return;
                        }
                        break;
                    case OrbwalkingMode.Lasthit:
                        if (!MenuClass.Hydra["lasthit"].As<MenuBool>().Enabled)
                        {
                            return;
                        }
                        break;
                    default:
                        if (!MenuClass.Hydra["manual"].As<MenuBool>().Enabled)
                        {
                            return;
                        }
                        break;
                }

                var hydraSlot = UtilityClass.Player.Inventory.Slots.FirstOrDefault(s => hydraItems.Contains(s.ItemId));
                if (hydraSlot != null)
                {
                    var hydraSpellSlot = hydraSlot.SpellSlot;
                    if (hydraSpellSlot != SpellSlot.Unknown &&
                        UtilityClass.Player.SpellBook.GetSpell(hydraSpellSlot).State == SpellState.Ready)
                    {
                        UtilityClass.Player.SpellBook.CastSpell(hydraSpellSlot);
                    }
                }
            }
        }

        /// <summary>
        ///     Called on preattack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPreAttack(object sender, PreAttackEventArgs args)
        {
            switch (ImplementationClass.IOrbwalker.Mode)
            {
                /// <summary>
                ///     The 'No AA in Combo' Logic.
                /// </summary>
                case OrbwalkingMode.Combo:
                    if (MenuClass.General["disableaa"].As<MenuBool>().Enabled &&
                        !UtilityClass.Player.HasSheenLikeBuff())
                    {
                        args.Cancel = true;
                    }
                    break;

                /// <summary>
                ///     The 'Support Mode' Logic.
                /// </summary>
                case OrbwalkingMode.Mixed:
                case OrbwalkingMode.Lasthit:
                case OrbwalkingMode.Laneclear:
                    if (Extensions.GetEnemyLaneMinionsTargets().Contains(args.Target) &&
                        MenuClass.General["supportmode"].As<MenuBool>().Enabled)
                    {
                        args.Cancel = GameObjects.AllyHeroes.Any(a => !a.IsMe && a.Distance(UtilityClass.Player) < 2500);
                    }
                    break;
            }
        }

        /// <summary>
        ///     Fired on spell cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SpellBookCastSpellEventArgs" /> instance containing the event data.</param>
        public static void OnCastSpell(Obj_AI_Base sender, SpellBookCastSpellEventArgs args)
        {
            var usedSlot = args.Slot;

            /// <summary>
            ///     The 'Preserve Mana' Logic.
            /// </summary>
            if (sender.IsMe &&
                UtilityClass.SpellSlots.Contains(usedSlot))
            {
                var championSpellManaCosts = UtilityClass.ManaCostArray.FirstOrDefault(v => v.Key == UtilityClass.Player.ChampionName).Value;
                if (championSpellManaCosts == null)
                {
                    return;
                }

                var spellBook = UtilityClass.Player.SpellBook;
                var data = UtilityClass.PreserveManaData;

                foreach (var slot in UtilityClass.SpellSlots)
                {
                    var spell = spellBook.GetSpell(slot);
                    var menuOption = MenuClass.PreserveMana[slot.ToString().ToLower()];
                    if (menuOption != null &&
                        menuOption.As<MenuBool>().Enabled)
                    {
                        if (data.ContainsKey(slot) &&
                            data.FirstOrDefault(d => d.Key == slot).Value != championSpellManaCosts[slot][spell.Level-1])
                        {
                            data.Remove(slot);
                            Console.WriteLine($"Preserve Mana List: Removed {slot} (Updated ManaCost).");
                        }

                        if (!data.ContainsKey(slot) &&
                            !spell.State.HasFlag(SpellState.NotLearned))
                        {
                            data.Add(slot, championSpellManaCosts[slot][spell.Level-1]);
                            Console.WriteLine($"Preserve Mana List: Added {slot}, Cost: {championSpellManaCosts[slot][spell.Level-1]}.");
                        }
                    }
                    else
                    {
                        if (data.ContainsKey(slot))
                        {
                            data.Remove(slot);
                            Console.WriteLine($"Preserve Mana List: Removed {slot} (Disabled).");
                        }
                    }
                }

                var sum = data
                    .Where(d => Bools.CanUseSpell(d.Key))
                    .Sum(s => s.Value);

                if (!data.Keys.Contains(usedSlot) &&
                    UtilityClass.Player.Mana - spellBook.GetSpell(usedSlot).Cost < sum)
                {
                    Console.WriteLine($"Preserve Mana List: Denied Spell {usedSlot} Usage (Mana: {UtilityClass.Player.Mana}, Cost: {spellBook.GetSpell(usedSlot).Cost}), Preserve Mana Quantity: {sum}");
                    args.Process = false;
                }

                /// <summary>
                ///     The 'Preserve Spells' Logic.
                /// </summary>
                switch (ImplementationClass.IOrbwalker.Mode)
                {
                    case OrbwalkingMode.Combo:
                    case OrbwalkingMode.Mixed:
                        var target = Extensions.GetBestEnemyHeroTargetInRange(UtilityClass.Player.GetSpell(args.Slot).SpellData.CastRange);
                        if (target.IsValidTarget(UtilityClass.Player.GetFullAttackRange(target)) &&
                            target.GetRealHealth() <=
                                UtilityClass.Player.GetAutoAttackDamage(target) *
                                MenuClass.PreserveSpells[args.Slot.ToString().ToLower()].As<MenuSlider>().Value)
                        {
                            args.Process = false;
                        }
                        break;
                }
            }
        }

        #endregion
    }
}