
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Orbwalking;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Loads Lucian.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus();

            /// <summary>
            ///     Initializes the spells.
            /// </summary>
            Spells();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods();
        }

        /// <summary>
        ///     Fired on present.
        /// </summary>
        public static void OnPresent()
        {
            /// <summary>
            ///     Initializes the drawings.
            /// </summary>
            Drawings();
        }

        /// <summary>
        ///     Called on pre attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPreAttack(object sender, PreAttackEventArgs args)
        {
            if (UtilityClass.Player.HasBuff("LucianR"))
            {
                args.Cancel = true;
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPostAttack(object sender, PostAttackEventArgs args)
        {
            if (!UtilityClass.Player.HasBuff("LucianR"))
            {
                /// <summary>
                ///     Initializes the orbwalkingmodes.
                /// </summary>
                switch (Orbwalker.Implementation.Mode)
                {
                    case OrbwalkingMode.Combo:
                        Weaving(sender, args);
                        break;

                    case OrbwalkingMode.Laneclear:
                        Laneclear(sender, args);
                        Jungleclear(sender, args);
                        Buildingclear(sender, args);
                        break;
                }
            }
        }

        /*
        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            if (SpellClass.E.State == SpellState.Ready && args.Sender.IsMelee && args.SkillType == GapcloserType.Targeted
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
        public static void OnPlayAnimation(Obj_AI_Base sender, Obj_AI_BasePlayAnimationEventArgs args)
        {
            if (sender.IsMe &&
                Orbwalker.Implementation.Mode != OrbwalkingMode.None)
            {
                if (args.Animation.Equals("Spell1") || args.Animation.Equals("Spell2"))
                {
                    UtilityClass.Player.IssueOrder(OrderType.MoveTo, Game.CursorPos);
                }
            }
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void OnUpdate()
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Killsteal();

            if (Orbwalker.Implementation.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Automatic();

            if (UtilityClass.Player.HasBuff("LucianR"))
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Orbwalker.Implementation.Mode)
            {
                case OrbwalkingMode.Combo:
                    Combo();
                    break;

                case OrbwalkingMode.Laneclear:
                    Laneclear();
                    break;

                case OrbwalkingMode.Mixed:
                    Harass();
                    break;
            }
        }

        #endregion
    }
}