
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Lucian
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Lucian.
        /// </summary>
        public Lucian()
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

        /*
        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            if (SpellClass.E.State == SpellState.Ready && args.Sender.IsMelee && args.SkillshotType == GapcloserType.Targeted
                && MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
            {
                if (args.Target.IsMe)
                {
                    SpellClass.E.Cast(UtilityClass.Player.ServerPosition.Extend(args.Sender.ServerPosition, -475f));
                }
            }
        }
        */

        /// <summary>
        ///     Called on animation trigger.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BasePlayAnimationEventArgs" /> instance containing the event data.</param>
        public void OnPlayAnimation(Obj_AI_Base sender, Obj_AI_BasePlayAnimationEventArgs args)
        {
            if (sender != null &&
                sender.IsMe &&
                ImplementationClass.IOrbwalker.Mode != OrbwalkingMode.None)
            {
                if (args.Animation.Equals("Spell1") || args.Animation.Equals("Spell2"))
                {
                    UtilityClass.Player.IssueOrder(OrderType.MoveTo, Game.CursorPos);
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
            if (!UtilityClass.Player.HasBuff("LucianR"))
            {
                /// <summary>
                ///     Initializes the orbwalkingmodes.
                /// </summary>
                switch (ImplementationClass.IOrbwalker.Mode)
                {
                    case OrbwalkingMode.Combo:
                        this.Weaving(sender, args);
                        break;

                    case OrbwalkingMode.Laneclear:
                        this.Laneclear(sender, args);
                        this.Jungleclear(sender, args);
                        this.Buildingclear(sender, args);
                        break;
                }
            }
        }

        /// <summary>
        ///     Called on pre attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public void OnPreAttack(object sender, PreAttackEventArgs args)
        {
            if (UtilityClass.Player.HasBuff("LucianR"))
            {
                args.Cancel = true;
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
            ///     Initializes the Automatic actions.
            /// </summary>
            this.Automatic();

            if (UtilityClass.Player.HasBuff("LucianR"))
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (ImplementationClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    this.Combo();
                    break;

                case OrbwalkingMode.Laneclear:
                    this.Laneclear();
                    break;

                case OrbwalkingMode.Mixed:
                    this.Harass();
                    break;
            }
        }

        #endregion
    }
}