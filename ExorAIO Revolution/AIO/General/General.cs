
#pragma warning disable 1587
namespace AIO
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

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

        #endregion
    }
}