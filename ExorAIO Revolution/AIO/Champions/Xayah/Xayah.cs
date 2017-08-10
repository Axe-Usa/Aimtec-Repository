
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Events;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Xayah
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Xayah.
        /// </summary>
        public Xayah()
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

            /// <summary>
            ///     Reloads the WorkedGrounds.
            /// </summary>
            this.ReloadFeathers();
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
            if (sender.IsMe)
            {
                switch (args.Slot)
                {
                    case SpellSlot.Q:
                        if (!this.Interrupt &&
                            UtilityClass.Player.GetBuffCount("XayahPassiveActive") >= 3 &&
                            MenuClass.Miscellaneous["feathersweaving"].As<MenuBool>().Enabled)
                        {
                            args.Process = false;
                        }
                        break;

                    case SpellSlot.W:
                        if (UtilityClass.Player.GetBuffCount("XayahPassiveActive") >= 3 &&
                            MenuClass.Miscellaneous["feathersweaving"].As<MenuBool>().Enabled)
                        {
                            args.Process = false;
                        }
                        break;
                }
            }
        }

        /// <summary>
        ///     Fired upon GameObject creation.
        /// </summary>
        public void OnCreate(GameObject obj)
        {
            if (obj != null && obj.IsValid)
            {
                switch (obj.Name)
                {
                    case "Xayah_Base_Passive_Dagger_Mark8s.troy":
                        this.Feathers.Add(obj.NetworkId, obj.Position);
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
                if (this.Feathers.Any(o => o.Key == obj.NetworkId))
                {
                    this.Feathers.Remove(obj.NetworkId);
                }
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public void OnPreAttack(object sender, PreAttackEventArgs args)
        {
            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (ImplementationClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    this.Weaving(sender, args);
                    break;
                case OrbwalkingMode.Laneclear:
                    this.Buildingclear(sender, args);
                    this.Jungleclear(sender, args);
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
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Dash.DashArgs" /> instance containing the event data.</param>
        public void OnGapcloser(object sender, Dash.DashArgs args)
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            var gapSender = (Obj_AI_Hero)args.Unit;
            if (gapSender == null || !gapSender.IsEnemy || !gapSender.IsMelee)
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser R.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["gapcloser"].As<MenuBool>().Enabled)
            {
                var playerPos = UtilityClass.Player.ServerPosition;
                if (args.EndPos.Distance(playerPos) <= 200)
                {
                    if (SpellClass.Q.Ready)
                    {
                        SpellClass.Q.Cast(args.StartPos);
                    }

                    SpellClass.R.Cast(args.StartPos);
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
                    break;
            }
        }

        #endregion
    }
}