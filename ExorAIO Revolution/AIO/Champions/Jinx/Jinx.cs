
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.CodeDom;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Jinx
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Loads Jinx.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus();

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
        ///     Called on teleport issued.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseTeleportEventArgs" /> instance containing the event data.</param>
        public static void OnTeleport(Obj_AI_Base sender, Obj_AI_BaseTeleportEventArgs args)
        {
            if (sender.IsEnemy)
            {
                if (SpellClass.E.Ready &&
                    MenuClass.Spells["e"]["teleport"].As<MenuBool>().Enabled)
                {
                    foreach (var target in GameObjects.EnemyMinions.Where(t =>
                        t.Buffs.Any(b =>
                            b.Caster.IsEnemy &&
                            b.Name.Equals("teleport_target")) &&
                        t.Distance(UtilityClass.Player) <= SpellClass.E.Range))
                    {
                        SpellClass.E.Cast(target.Position);
                    }
                }
            }
        }

        /// <summary>
        ///     Called on orbwalker action.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPreAttack(object sender, PreAttackEventArgs args)
        {
            switch (Orbwalker.Implementation.Mode)
            {
                case OrbwalkingMode.Laneclear:
                    Laneclear(sender, args);
                    Jungleclear(sender, args);
                    break;
                case OrbwalkingMode.Lasthit:
                    Lasthit(sender, args);
                    break;
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
            if (ObjectManager.GetLocalPlayer().IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.E.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.E.SpellData.Range)
                && MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Value)
            {
                SpellClass.E.Cast(args.IsDirectedToPlayer ? ObjectManager.GetLocalPlayer().ServerPosition : args.End);
            }
        }
        */

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
            ///     Updates the spells.
            /// </summary>
            Spells();

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Killsteal();

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Automatic();

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
            }
        }

        #endregion
    }
}