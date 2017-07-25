
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Events;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;
    using Aimtec.SDK.Util;

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

            /// <summary>
            ///     Initializes the spells.
            /// </summary>
            this.Spells();

            /// <summary>
            ///     Reloads the MineField.
            /// </summary>
            this.ReloadMineField();

            /// <summary>
            ///     Reloads the WorkedGrounds.
            /// </summary>
            this.ReloadWorkedGrounds();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Called on spell cast.
        /// </summary>
        /// <param name="sender">The SpellBook.</param>
        /// <param name="args">The <see cref="SpellBookCastSpellEventArgs" /> instance containing the event data.</param>
        public void OnCastSpell(Obj_AI_Base sender, SpellBookCastSpellEventArgs args)
        {
            if (sender.IsMe)
            {
                switch (ImplementationClass.IOrbwalker.Mode)
                {
                    case OrbwalkingMode.Combo:
                        switch (args.Slot)
                        {
                            case SpellSlot.Q:
                                switch (MenuClass.Spells["q"]["combomode"].As<MenuList>().Value)
                                {
                                    case 0:
                                        if (this.IsNearWorkedGround())
                                        {
                                            args.Process = false;
                                        }
                                        break;
                                }
                                break;

                            case SpellSlot.W:
                                var spellBook = UtilityClass.Player.SpellBook;
                                if (UtilityClass.Player.Mana <
                                     spellBook.GetSpell(SpellSlot.W).Cost +
                                     spellBook.GetSpell(SpellSlot.E).Cost &&
                                    MenuClass.Spells["w"]["customization"]["onlyeready"].As<MenuBool>().Enabled)
                                {
                                    args.Process = false;
                                }
                                break;
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
                    case "Taliyah_Base_Q_aoe.troy":
                    case "Taliyah_Base_Q_aoe_river.troy":
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
            if (obj != null && obj.IsValid)
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
            if (gapSender == null ||
                !gapSender.IsEnemy ||
                Invulnerable.Check(gapSender, DamageType.Magical, false))
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                args.EndPos.Distance(UtilityClass.Player.ServerPosition) < SpellClass.E.Range &&
                MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
            {
                if (args.EndPos.Distance(UtilityClass.Player.ServerPosition) >= 200)
                {
                    SpellClass.E.Cast(args.EndPos);
                }
                else
                {
                    SpellClass.E.Cast(gapSender.ServerPosition);
                }
            }

            /// <summary>
            ///     The Anti-Gapcloser W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                args.EndPos.Distance(UtilityClass.Player.ServerPosition) < SpellClass.W.Range &&
                MenuClass.Spells["w"]["gapcloser"].As<MenuBool>().Enabled)
            {
                SpellClass.W.Cast((Vector3)args.EndPos, (Vector3)args.EndPos.Extend(args.StartPos, 200f));
            }
        }

        /*
        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (UtilityClass.Player.IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.W.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.W.SpellData.Range)
                && MenuClass.Spells["w"]["interrupter"].As<MenuBool>().Enabled)
            {
                SpellClass.W.Cast(args.Sender.ServerPosition, UtilityClass.Player.ServerPosition);
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
            if (sender.IsMe)
            {
                switch (args.SpellSlot)
                {
                    /// <summary>
                    ///     The W->E Combo Logic.
                    /// </summary>
                    case SpellSlot.W:
                        if (SpellClass.E.Ready)
                        {
                            switch (ImplementationClass.IOrbwalker.Mode)
                            {
                                case OrbwalkingMode.Combo:
                                    if (MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
                                    {
                                        SpellClass.E.Cast(args.End);
                                    }
                                    break;
                            }
                        }
                        break;

                    /// <summary>
                    ///     Automatically Mount on R Logic.
                    /// </summary>
                    case SpellSlot.R:
                        if (SpellClass.R.Ready &&
                            MenuClass.Spells["r"]["mountr"].As<MenuBool>().Enabled)
                        {
                            DelayAction.Queue(500, () =>
                                {
                                    SpellClass.R.Cast();
                                });
                        }
                        break;
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
                    this.Jungleclear();
                    break;
            }
        }

        #endregion
    }
}