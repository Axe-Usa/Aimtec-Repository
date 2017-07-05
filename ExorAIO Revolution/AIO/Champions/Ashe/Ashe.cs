
#pragma warning disable 1587

namespace AIO.Champions
{
    using System;

    using Aimtec;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ashe
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Ashe.
        /// </summary>
        public Ashe()
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
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        public void OnLeaveVisibility(AttackableUnit sender, EventArgs args)
        {
            if (sender.IsEnemy)
            {
                if (SpellClass.E.Ready &&
                    UtilityClass.Player.SpellBook.GetSpell(SpellSlot.E).Ammo
                    >= (MenuClass.Spells["e"]["logical"].As<MenuBool>().Value ? 2 : 1) &&
                    MenuClass.Spells["e"]["vision"].As<MenuBool>().Value)
                {
                    SpellClass.E.Cast(sender.Position);
                }
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public void OnPostAttack(object sender, PostAttackEventArgs args)
        {
            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (UtilityClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    this.Weaving(sender, args);
                    break;
                case OrbwalkingMode.Laneclear:
                    this.Jungleclear(sender, args);
                    this.Buildingclear(sender, args);
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

        /*
        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (ObjectManager.GetLocalPlayer().IsDead)
            {
                return;
            }

            if (SpellClass.R.State == SpellState.Ready && args.Sender.IsMelee && args.Sender.IsValidTarget(SpellClass.R.SpellData.Range)
                && args.SkillType == GapcloserType.Targeted
                && MenuClass.Spells["r"]["gapcloser"].As<MenuBool>().Value)
            {
                if (args.Target.IsMe)
                {
                    SpellClass.R.Cast(args.Sender.ServerPosition);
                }
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (ObjectManager.GetLocalPlayer().IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.R.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.R.SpellData.Range)
                && MenuClass.Spells["r"]["interrupter"].As<MenuBool>().Value)
            {
                SpellClass.R.Cast(args.Sender.ServerPosition);
            }
        }
        */

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

            if (UtilityClass.IOrbwalker.IsWindingUp)
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
            switch (UtilityClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    this.Combo();
                    break;
                case OrbwalkingMode.Mixed:
                    this.Harass();
                    break;
                case OrbwalkingMode.Laneclear:
                    this.Laneclear();
                    break;
            }
        }

        #endregion
    }
}