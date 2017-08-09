
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Evelynn
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Evelynn.
        /// </summary>
        public Evelynn()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            this.Menus();

            /// <summary>
            ///     Initializes the spells.
            /// </summary>
            this.Spells();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            this.Methods();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Called on non killable minion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NonKillableMinionEventArgs" /> instance containing the event data.</param>
        public void OnNonKillableMinion(object sender, NonKillableMinionEventArgs args)
        {
            var minion = (Obj_AI_Minion)args.Target;
            if (minion == null)
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (ImplementationClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Laneclear:
                case OrbwalkingMode.Lasthit:
                case OrbwalkingMode.Mixed:
                    if (SpellClass.Q.Ready &&
                        minion.IsValidTarget(SpellClass.Q.Range) &&
                        UtilityClass.Player.ManaPercent()
                            > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["lasthitunk"]) &&
                        MenuClass.Spells["q"]["lasthitunk"].As<MenuSliderBool>().Enabled)
                    {
                        if (minion.Health <= UtilityClass.Player.GetSpellDamage(minion, SpellSlot.Q))
                        {
                            SpellClass.Q.CastOnUnit(minion);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        ///     Fired on present.
        /// </summary>
        public void OnPresent()
        {
            /// <summary>
            ///     Initializes the drawings.
            /// </summary>
            this.Drawings();
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void OnUpdate()
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            this.Killsteal();

            if (ImplementationClass.IOrbwalker.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Orbwalker.Implementation.Mode)
            {
                case OrbwalkingMode.Combo:
                    this.Combo();
                    break;
                case OrbwalkingMode.Mixed:
                    this.Harass();
                    break;
                case OrbwalkingMode.Laneclear:
                    this.Laneclear();
                    this.Jungleclear();
                    break;
                case OrbwalkingMode.Lasthit:
                    this.Lasthit();
                    break;
            }
        }

        #endregion
    }
}