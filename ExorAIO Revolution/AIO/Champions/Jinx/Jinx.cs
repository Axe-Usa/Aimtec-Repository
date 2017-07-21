
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

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
        ///     Called on teleport issued.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseTeleportEventArgs" /> instance containing the event data.</param>
        public void OnTeleport(Obj_AI_Base sender, Obj_AI_BaseTeleportEventArgs args)
        {
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["teleport"].As<MenuBool>().Enabled)
            {
                foreach (var target in ObjectManager.Get<Obj_AI_Minion>().Where(
                    m =>
                        m.IsEnemy &&
                        m.Distance(UtilityClass.Player) <= SpellClass.E.Range))
                {
                    if (target.Buffs.Any(b => b.IsValid && b.IsActive && b.Name.Equals("teleport_target")))
                    {
                        SpellClass.E.Cast(target.ServerPosition);
                    }
                }
            }
        }

        /*
        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (UtilityClass.Player.IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.E.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.E.SpellData.Range)
                && MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
            {
                SpellClass.E.Cast(args.IsDirectedToPlayer ? UtilityClass.Player.ServerPosition : args.End);
            }
        }
        */

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void OnUpdate()
        {
            if (UtilityClass.Player.SpellBook.GetSpell(SpellSlot.Q).Level < 5)
            {
                SpellClass.Q2 = new Spell(SpellSlot.Q, SpellClass.Q.Range + 50f + 25f * UtilityClass.Player.SpellBook.GetSpell(SpellSlot.Q).Level);
            }

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