
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
                    if (!UtilityClass.Player.HasSheenLikeBuff() &&
                        UtilityClass.Player.Level >=
                                MenuClass.General["disableaa"].As<MenuSliderBool>().Value &&
                        MenuClass.General["disableaa"].As<MenuSliderBool>().Enabled)
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
            if (sender.IsMe &&
                UtilityClass.SpellSlots.Contains(args.Slot))
            {
                /// <summary>
                ///     The 'Preserve Mana' Logic.
                /// </summary>
                var championSpellManaCosts = UtilityClass.ManaCostArray.FirstOrDefault(v => v.Key == UtilityClass.Player.ChampionName).Value;
                if (championSpellManaCosts != null)
                {
                    var spellBook = UtilityClass.Player.SpellBook;
                    var data = UtilityClass.PreserveManaData;

                    foreach (var slot in UtilityClass.SpellSlots)
                    {
                        var spell = spellBook.GetSpell(slot);
                        var menuOption = MenuClass.PreserveMana[slot.ToString().ToLower()];
                        if (menuOption != null &&
                            menuOption.As<MenuBool>().Enabled)
                        {
                            var registeredSpellData = data.FirstOrDefault(d => d.Key == slot).Value;
                            var actualSpellData = championSpellManaCosts[slot][spell.Level - 1];

                            if (data.ContainsKey(slot) &&
                                registeredSpellData != actualSpellData)
                            {
                                data.Remove(slot);
                                Console.WriteLine($"Preserve Mana List: Removed {slot} (Updated ManaCost).");
                            }

                            if (!data.ContainsKey(slot) &&
                                !spell.State.HasFlag(SpellState.NotLearned))
                            {
                                data.Add(slot, actualSpellData);
                                Console.WriteLine($"Preserve Mana List: Added {slot}, Cost: {actualSpellData}.");
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

                        var sum = data
                            .Where(s => MenuClass.PreserveMana[s.Key.ToString().ToLower()].As<MenuBool>().Enabled)
                            .Sum(s => s.Value);
                        if (sum <= 0)
                        {
                            return;
                        }

                        var spellData =
                            championSpellManaCosts[args.Slot][UtilityClass.Player.GetSpell(args.Slot).Level - 1];
                        var mana = UtilityClass.Player.Mana;
                        if (!data.Keys.Contains(args.Slot) && mana - spellData < sum)
                        {
                            Console.WriteLine($"Preserve Mana List: Denied Spell {args.Slot} Usage (Mana: {mana}, Cost: {spellData}), Preserve Mana Quantity: {sum}");
                            args.Process = false;
                        }
                    }
                }

                /// <summary>
                ///     The 'Preserve Spells' Logic.
                /// </summary>
                switch (ImplementationClass.IOrbwalker.Mode)
                {
                    case OrbwalkingMode.Combo:
                    case OrbwalkingMode.Mixed:
                        var target = ImplementationClass.IOrbwalker.GetOrbwalkingTarget() as Obj_AI_Hero;
                        if (target != null)
                        {
                            if (target.GetRealHealth() <=
                                    UtilityClass.Player.GetAutoAttackDamage(target) *
                                    MenuClass.PreserveSpells[args.Slot.ToString().ToLower()].As<MenuSlider>().Value)
                            {
                                args.Process = false;
                            }
                        }
                        break;
                }
            }
        }

        #endregion
    }
}