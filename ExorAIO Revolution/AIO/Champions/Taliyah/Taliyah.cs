
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Util;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
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
            Menus();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods();

            /// <summary>
            ///     Initializes the spells.
            /// </summary>
            Spells();

            /// <summary>
            ///     Reloads the MineField.
            /// </summary>
            ReloadMineField();

            /// <summary>
            ///     Reloads the WorkedGrounds.
            /// </summary>
            ReloadWorkedGrounds();
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
                                        if (IsNearWorkedGround())
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
            if (obj.IsValid)
            {
                switch (obj.Name)
                {
                    case "Taliyah_Base_Q_aoe.troy":
                    case "Taliyah_Base_Q_aoe_river.troy":
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
        public void OnDestroy(GameObject obj)
        {
            if (obj.IsValid)
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
            
            if (sender == null || !sender.IsEnemy)
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser E Logic.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                switch (args.Type)
                {
                    case GapSpellType.Targeted:
                        if (sender.IsMelee &&
                            args.Target.IsMe)
                        {
                            SpellClass.E.Cast(args.StartPosition);
                        }
                        break;
                    default:
                        if (args.EndPosition.Distance(UtilityClass.Player.ServerPosition) <= UtilityClass.Player.AttackRange/2)
                        {
                            SpellClass.E.Cast(args.StartPosition);
                        }
                        break;
                }
            }

            /// <summary>
            ///     The Anti-Gapcloser W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                !Invulnerable.Check(sender, DamageType.Magical, false))
            {
                switch (args.Type)
                {
                    case GapSpellType.Targeted:
                        if (sender.IsMelee &&
                            args.Target.IsMe)
                        {
                            SpellClass.W.Cast(args.EndPosition, args.EndPosition.Extend(args.StartPosition, 200f));
                        }
                        break;
                    default:
                        if (args.EndPosition.Distance(UtilityClass.Player.ServerPosition) <= UtilityClass.Player.AttackRange/2)
                        {
                            SpellClass.W.Cast(args.EndPosition, args.EndPosition.Extend(args.StartPosition, sender.IsMelee ? 200f : -200f));
                        }
                        break;
                }
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
        ///     Called while processing spellcast operations.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        public void OnPerformCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
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
            Killsteal();

            if (ImplementationClass.IOrbwalker.IsWindingUp)
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
            switch (ImplementationClass.IOrbwalker.Mode)
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