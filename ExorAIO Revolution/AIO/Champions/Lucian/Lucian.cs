
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Orbwalking;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
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

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="GapcloserArgs" /> instance containing the event data.</param>
        public void OnGapcloser(Obj_AI_Hero sender, GapcloserArgs args)
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }
            
            if (sender == null || !sender.IsEnemy || !sender.IsMelee)
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser E.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                switch (args.Type)
                {
                    case GapSpellType.Targeted:
                        if (args.Target.IsMe)
                        {
                            SpellClass.E.Cast(UtilityClass.Player.ServerPosition.Extend(args.StartPosition, -(SpellClass.E.Range - UtilityClass.Player.AttackRange)));
                        }
                        break;
                    default:
                        if (args.EndPosition.Distance(UtilityClass.Player.ServerPosition) <= UtilityClass.Player.AttackRange)
                        {
                            SpellClass.E.Cast(UtilityClass.Player.ServerPosition.Extend(args.StartPosition, -(SpellClass.E.Range - UtilityClass.Player.AttackRange)));
                        }
                        break;
                }
            }
        }

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
            Drawings();
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
            Killsteal();

            if (ImplementationClass.IOrbwalker.IsWindingUp)
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
            switch (ImplementationClass.IOrbwalker.Mode)
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