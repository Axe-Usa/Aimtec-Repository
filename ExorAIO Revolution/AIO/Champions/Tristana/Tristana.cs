
using System.Linq;
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
    internal partial class Tristana
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Tristana.
        /// </summary>
        public Tristana()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods();

            /// <summary>
            ///     Updates the spells.
            /// </summary>
            Spells();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Fired when a buff is added.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="buff">The buff.</param>
        public void OnAddBuff(Obj_AI_Base sender, Buff buff)
        {
            if (sender.IsMe &&
                SpellClass.W.Ready &&
                MenuClass.Spells["w"]["antigrab"].As<MenuBool>().Enabled)
            {
                if (buff.Name.Equals("ThreshQ") ||
                    buff.Name.Equals("rocketgrab2"))
                {
                    SpellClass.W.Cast(UtilityClass.Player.ServerPosition.Extend(buff.Caster.ServerPosition, -SpellClass.W.Range));
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
            /// <summary>
            ///     The Target Forcing Logic.
            /// </summary>
            if (MenuClass.Miscellaneous["focuse"].As<MenuBool>().Enabled)
            {
                var forceTarget = Extensions.GetAllGenericUnitTargets().FirstOrDefault(t =>
                        IsCharged(t) &&
                        t.IsValidTarget(UtilityClass.Player.GetFullAttackRange(t)));

                if (forceTarget is Obj_AI_Minion &&
                    ImplementationClass.IOrbwalker.Mode == OrbwalkingMode.Combo)
                {
                    return;
                }

                if (forceTarget != null)
                {
                    ImplementationClass.IOrbwalker.ForceTarget(forceTarget);
                }
            }

            switch (ImplementationClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    Combo(sender, args);
                    break;
                case OrbwalkingMode.Mixed:
                    Harass(sender, args);
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
            ///     The Anti-Gapcloser W.
            /// </summary>
            if (SpellClass.W.Ready)
            {
                if (sender.IsMelee)
                {
                    switch (args.Type)
                    {
                        case GapSpellType.Targeted:
                            if (args.Target.IsMe)
                            {
                                SpellClass.W.Cast(UtilityClass.Player.ServerPosition.Extend(args.StartPosition, -SpellClass.W.Range));
                                return;
                            }
                            break;
                        default:
                            if (args.EndPosition.Distance(UtilityClass.Player.ServerPosition) <= UtilityClass.Player.AttackRange/2)
                            {
                                SpellClass.W.Cast(UtilityClass.Player.ServerPosition.Extend(args.StartPosition, -SpellClass.W.Range));
                                return;
                            }
                            break;
                    }
                }
            }

            /// <summary>
            ///     The Anti-Gapcloser R.
            /// </summary>
            if (SpellClass.R.Ready &&
                !Invulnerable.Check(sender, DamageType.Magical, false))
            {
                if (sender.IsMelee)
                {
                    switch (args.Type)
                    {
                        case GapSpellType.Targeted:
                            if (args.Target.IsMe)
                            {
                                SpellClass.R.CastOnUnit(sender);
                            }
                            break;
                        default:
                            if (args.EndPosition.Distance(UtilityClass.Player.ServerPosition) <= UtilityClass.Player.AttackRange/2)
                            {
                                SpellClass.R.CastOnUnit(sender);
                            }
                            break;
                    }
                }
            }
        }

        /*
        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (UtilityClass.Player.IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.R.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.R.SpellData.Range)
                && MenuClass.Spells["r"]["interrupter"].As<MenuBool>().Enabled)
            {
                UtilityClass.Player.SpellBook.CastSpell(SpellSlot.R, args.Sender);
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
            ///     Initializes the Automatic actions.
            /// </summary>
            Automatic();
        }

        #endregion
    }
}