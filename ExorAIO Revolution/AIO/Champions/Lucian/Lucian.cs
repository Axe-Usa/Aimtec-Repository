
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
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
        /// <param name="args">The <see cref="Gapcloser.GapcloserArgs" /> instance containing the event data.</param>
        public void OnGapcloser(Obj_AI_Hero sender, Gapcloser.GapcloserArgs args)
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            var enabledOption = MenuClass.Gapcloser["enabled"];
            if (enabledOption == null || !enabledOption.As<MenuBool>().Enabled)
            {
                return;
            }

            if (sender == null || !sender.IsEnemy || !sender.IsMelee)
            {
                return;
            }

            var spellOption = MenuClass.SubGapcloser[$"{sender.ChampionName.ToLower()}.{args.SpellName.ToLower()}"];
            if (spellOption == null || !spellOption.As<MenuBool>().Enabled)
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
                    case Gapcloser.Type.Targeted:
                        if (args.Target.IsMe)
                        {
                            var targetPos = UtilityClass.Player.ServerPosition.Extend(args.StartPosition, -SpellClass.E.Range);
                            if (targetPos.PointUnderEnemyTurret())
                            {
                                return;
                            }

                            SpellClass.E.Cast(targetPos);
                        }
                        break;
                    default:
                        var targetPos2 = UtilityClass.Player.ServerPosition.Extend(args.EndPosition, -SpellClass.E.Range);
                        if (targetPos2.PointUnderEnemyTurret())
                        {
                            return;
                        }

                        if (args.EndPosition.Distance(UtilityClass.Player.ServerPosition) <= UtilityClass.Player.AttackRange)
                        {
                            SpellClass.E.Cast(targetPos2);
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
                switch (args.Animation)
                {
                    case "Spell1":
                    case "Spell2":
                        UtilityClass.Player.IssueOrder(OrderType.MoveTo, Game.CursorPos);
                        break;
                }
            }
        }

        /// <summary>
        ///     Called on animation trigger.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        public void OnPerformCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (sender != null &&
                sender.IsMe &&
                ImplementationClass.IOrbwalker.Mode != OrbwalkingMode.None)
            {
                switch (args.SpellSlot)
                {
                    case SpellSlot.Q:
                    case SpellSlot.W:
                        UtilityClass.Player.IssueOrder(OrderType.MoveTo, Game.CursorPos);
                        break;
                }
            }
        }

        /// <summary>
        ///     Called on animation trigger.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        public void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (sender != null &&
                sender.IsMe &&
                ImplementationClass.IOrbwalker.Mode != OrbwalkingMode.None)
            {
                switch (args.SpellSlot)
                {
                    case SpellSlot.Q:
                    case SpellSlot.W:
                        UtilityClass.Player.IssueOrder(OrderType.MoveTo, Game.CursorPos);
                        break;
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

            if (IsCulling())
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