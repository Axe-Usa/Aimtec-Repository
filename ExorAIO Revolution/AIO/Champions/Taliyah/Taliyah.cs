
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Taliyah
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Taliyah.
        /// </summary>
        public Taliyah()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            this.Menus();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            this.Methods();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Fired upon GameObject creation.
        /// </summary>
        public void OnCreate(GameObject obj)
        {
            if (obj.IsValid &&
                GameObjects.AllyMinions.Contains(obj))
            {
                switch (obj.Name)
                {
                    case "Taliyah_Base_Q_aoe_bright.troy":
                        this.WorkedGrounds.Add(obj.NetworkId, obj.Position);
                        break;

                    case "Taliyah_Base_E_Mines.troy":
                        this.MineField.Add(obj.NetworkId, obj.Position);
                        break;
                }
            }
        }

        /// <summary>
        ///     Fired upon GameObject creation.
        /// </summary>
        public void OnDestroy(GameObject obj)
        {
            if (this.WorkedGrounds.Any(o => o.Key == obj.NetworkId))
            {
                this.WorkedGrounds.Remove(obj.NetworkId);
            }

            if (this.MineField.Any(o => o.Key == obj.NetworkId))
            {
                this.MineField.Remove(obj.NetworkId);
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
        public void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
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
        public void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
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
        public void OnUpdate()
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            /// <summary>
            ///     Initializes the spells.
            /// </summary>
            this.Spells();

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
                case OrbwalkingMode.Laneclear:
                    this.Laneclear();
                    this.Jungleclear();
                    break;
            }
        }

        #endregion
    }
}