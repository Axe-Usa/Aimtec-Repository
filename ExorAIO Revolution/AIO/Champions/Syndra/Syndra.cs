
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Syndra
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Syndra.
        /// </summary>
        public Syndra()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            this.Menus();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            this.Methods();

            /// <summary>
            ///     Updates the spells.
            /// </summary>
            this.Spells();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Fired on spell cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SpellBookCastSpellEventArgs" /> instance containing the event data.</param>
        public void OnCastSpell(Obj_AI_Base sender, SpellBookCastSpellEventArgs args)
        {
            if (sender.IsMe &&
                args.Slot == SpellSlot.W)
            {
                if (Game.TickCount - UtilityClass.LastTick >= 500)
                {
                    UtilityClass.LastTick = Game.TickCount;
                }
                else
                {
                    args.Process = false;
                }
            }
        }

        /// <summary>
        ///     Fired upon GameObject creation.
        /// </summary>
        public void OnCreate(GameObject obj)
        {
            if (obj != null)
            {
                switch (obj.Name)
                {
                    case "Syndra_Base_Q_idle.troy":
                    case "Syndra_Base_Q_Lv5_idle.troy":
                        this.DarkSpheres.Add(obj.NetworkId, obj.Position);
                        break;
                }
            }
        }

        /// <summary>
        ///     Fired upon GameObject creation.
        /// </summary>
        public void OnDestroy(GameObject obj)
        {
            if (obj != null && obj.IsValid)
            {
                if (this.DarkSpheres.Any(o => o.Key == obj.NetworkId))
                {
                    this.DarkSpheres.Remove(obj.NetworkId);
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
            if (UtilityClass.Player.IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.Q.State == SpellState.Ready && !Invulnerable.Check(args.Sender) && args.Sender.IsValidTarget(SpellClass.Q.SpellData.Range)
                && MenuClass.Spells["q"]["gapcloser"].As<MenuBool>().Enabled)
            {
                SpellClass.Q.Cast(args.End);
            }
            if (SpellClass.E.State == SpellState.Ready && !Invulnerable.Check(args.Sender) && args.Sender.IsValidTarget(SpellClass.E.SpellData.Range)
                && MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
            {
                SpellClass.E.Cast(args.End);
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
            this.Killsteal();

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            this.Automatic();

            /// <summary>
            ///     Reloads the DarkSpheres.
            /// </summary>
            this.ReloadDarkSpheres();

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