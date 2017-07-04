
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Taliyah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Loads Taliyah.
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
        ///     Fired upon GameObject creation.
        /// </summary>
        public static void OnCreate(GameObject obj)
        {
            if (obj.IsValid &&
                GameObjects.AllyMinions.Contains(obj))
            {
                switch (obj.Name)
                {
                    case "Taliyah_Base_Q_aoe_bright.troy":
                        WorkedGrounds.Add(obj.NetworkId, obj.Position);
                        break;

                    case "Taliyah_Base_E_Mines.troy":
                        MineField.Add(obj.NetworkId, obj.Position);
                        break;
                }
            }
        }

        /// <summary>
        ///     Fired upon GameObject creation.
        /// </summary>
        public static void OnDestroy(GameObject obj)
        {
            if (WorkedGrounds.Any(o => o.Key == obj.NetworkId))
            {
                WorkedGrounds.Remove(obj.NetworkId);
            }

            if (MineField.Any(o => o.Key == obj.NetworkId))
            {
                MineField.Remove(obj.NetworkId);
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

            if (SpellClass.E.State == SpellState.Ready && ObjectManager.GetLocalPlayer().Distance(args.End) < SpellClass.E.SpellData.Range - 50f
                && MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Value)
            {
                SpellClass.E.Cast(args.End);
            }

            if (SpellClass.W.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.W.SpellData.Range)
                && MenuClass.Spells["w"]["gapcloser"].As<MenuBool>().Value)
            {
                if (args.Sender.ChampionName.Equals("MasterYi"))
                {
                    DelayAction.Add(250, () => { SpellClass.W.Cast(ObjectManager.GetLocalPlayer().ServerPosition, args.Start); });
                    return;
                }

                SpellClass.W.Cast(
                    args.End,
                    args.Sender.IsMelee
                        ? ObjectManager.GetLocalPlayer().ServerPosition.Extend(args.End, ObjectManager.GetLocalPlayer().Distance(args.End) * 2)
                        : ObjectManager.GetLocalPlayer().ServerPosition);
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public static void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (ObjectManager.GetLocalPlayer().IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.W.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.W.SpellData.Range)
                && MenuClass.Spells["w"]["interrupter"].As<MenuBool>().Value)
            {
                SpellClass.W.Cast(args.Sender.ServerPosition, ObjectManager.GetLocalPlayer().ServerPosition);
            }
        }
        */

        /// <summary>
        ///     Called while processing spellcast operations.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            /// <summary>
            ///     Automatically Mount on R Logic.
            /// </summary>
            if (sender.IsMe)
            {
                if (SpellClass.R.Ready &&
                    args.SpellSlot.Equals(SpellSlot.R) &&
                    MenuClass.Spells["r"]["mountr"].As<MenuBool>().Value)
                {
                    SpellClass.R.Cast();
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
            ///     Initializes the spells.
            /// </summary>
            Spells();

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Killsteal();

            if (UtilityClass.IOrbwalker.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Automatic();

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (UtilityClass.IOrbwalker.Mode)
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