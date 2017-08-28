
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

            if (MenuClass.Hydra != null)
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
                }

                var hydraItems = new[]{ ItemId.TitanicHydra, ItemId.RavenousHydra, ItemId.Tiamat };
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
                    if (MenuClass.General["disableaa"].Enabled &&
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
                        MenuClass.General["supportmode"].Enabled)
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
            /// <summary>
            ///     The 'Preserve Mana' Logic.
            /// </summary>
            if (sender.IsMe &&
                UtilityClass.SpellSlots.Contains(args.Slot))
            {
                float manaToPreserve = 0;
                var spellBook = UtilityClass.Player.SpellBook;

                // ReSharper disable once LoopCanBeConvertedToQuery
                foreach (var slot in UtilityClass.SpellSlots)
                {
                    var spellSlot = spellBook.GetSpell(slot);
                    if ((spellSlot.State == SpellState.Ready ||
                         spellSlot.State == SpellState.Cooldown && spellSlot.GetRemainingCooldownTime() <= 5) &&
                        MenuClass.PreserveMana[slot.ToString().ToLower()].As<MenuBool>().Enabled)
                    {
                        manaToPreserve += spellSlot.Cost;
                    }
                }

                if (UtilityClass.Player.Mana - spellBook.GetSpell(args.Slot).Cost < manaToPreserve)
                {
                    args.Process = false;
                }

                /// <summary>
                ///     The 'Preserve Spells' Logic.
                /// </summary>
                if (ImplementationClass.IOrbwalker.Mode == OrbwalkingMode.Combo)
                {
                    var target = Extensions.GetBestEnemyHeroTargetInRange(UtilityClass.Player.GetSpell(args.Slot).SpellData.CastRange);
                    if (target.IsValidTarget(UtilityClass.Player.GetFullAttackRange(target)) &&
                        target.GetRealHealth() <=
                            UtilityClass.Player.GetAutoAttackDamage(target) *
                            MenuClass.PreserveSpells[args.Slot.ToString().ToLower()].As<MenuSlider>().Value)
                    {
                        args.Process = false;
                    }
                }
            }
        }

        #endregion
    }
}