
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Jinx
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Jinx.
        /// </summary>
        public Jinx()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            this.Menus();

            /// <summary>
            ///     Updates the spells.
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
        ///     Called on orbwalker action.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public void OnPreAttack(object sender, PreAttackEventArgs args)
        {
            switch (ImplementationClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    this.Combo(sender, args);
                    break;
                case OrbwalkingMode.Laneclear:
                    this.Laneclear(sender, args);
                    this.Jungleclear(sender, args);
                    break;
                case OrbwalkingMode.Lasthit:
                    this.Lasthit(sender, args);
                    break;
                case OrbwalkingMode.Mixed:
                    this.Harass(sender, args);
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
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Dash.DashArgs" /> instance containing the event data.</param>
        public void OnGapcloser(object sender, Dash.DashArgs args)
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            var gapSender = (Obj_AI_Hero)args.Unit;
            if (gapSender == null ||
                !gapSender.IsEnemy ||
                Invulnerable.Check(gapSender, DamageType.Magical, false))
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser E.
            /// </summary>
            if (SpellClass.E.Ready &&
                args.EndPos.Distance(UtilityClass.Player.ServerPosition) < SpellClass.E.Range &&
                MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
            {
                SpellClass.E.Cast(args.EndPos);
            }
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
            ///     Initializes the Automatic actions.
            /// </summary>
            this.Automatic();

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (ImplementationClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    this.Combo();
                    break;
                case OrbwalkingMode.Mixed:
                    this.Harass();
                    break;
            }
        }

        #endregion
    }
}