
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
        ///     Called on pre attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public void OnPreAttack(object sender, PreAttackEventArgs args)
        {
            var orbTarget = ImplementationClass.IOrbwalker.GetOrbwalkingTarget() as Obj_AI_Base;
            if (orbTarget != null)
            {
                if (IsAllured(orbTarget) && !IsFullyAllured(orbTarget) &&
                    MenuClass.Spells["miscellaneous"]["dontaasemiallured"].As<MenuBool>().Enabled)
                {
                    args.Cancel = true;
                }
            }
        }

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
            ///     The Anti-Gapcloser R.
            /// </summary>
            if (SpellClass.R.Ready)
            {
                switch (args.Type)
                {
                    case GapSpellType.Targeted:
                        if (args.Target.IsMe)
                        {
                            SpellClass.R.Cast(args.StartPosition);
                        }
                        break;
                    default:
                        if (args.EndPosition.Distance(UtilityClass.Player.ServerPosition) <= UtilityClass.Player.AttackRange)
                        {
                            SpellClass.R.Cast(args.StartPosition);
                        }
                        break;
                }
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

            /// <summary>
            ///     Initializes the Automatic events.
            /// </summary>
            Automatic();

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
                    Combo();
                    break;
                case OrbwalkingMode.Mixed:
                    Harass();
                    break;
                case OrbwalkingMode.Laneclear:
                    Laneclear();
                    Jungleclear();
                    break;
            }
        }

        #endregion
    }
}