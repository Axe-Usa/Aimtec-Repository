
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
    internal partial class Jhin
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Jhin.
        /// </summary>
        public Jhin()
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
        ///     Fired on spell cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SpellBookCastSpellEventArgs" /> instance containing the event data.</param>
        public void OnCastSpell(Obj_AI_Base sender, SpellBookCastSpellEventArgs args)
        {
            if (sender.IsMe)
            {
                if (IsUltimateShooting() &&
                    args.Slot != SpellSlot.R)
                {
                    args.Process = false;
                }

                switch (args.Slot)
                {
                    case SpellSlot.E:
                        var target = ImplementationClass.IOrbwalker.GetOrbwalkingTarget() as Obj_AI_Hero;
                        if (target != null &&
                            target.HasBuff("jhinetrapslow"))
                        {
                            args.Process = false;
                        }
                        break;
                }
            }
        }

        /// <summary>
        ///     Called on post attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public void OnPostAttack(object sender, PostAttackEventArgs args)
        {
            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (ImplementationClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    Weaving(sender, args);
                    break;
                case OrbwalkingMode.Laneclear:
                    Jungleclear(sender, args);
                    break;
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
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (ImplementationClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    Weaving(sender, args);
                    break;
                case OrbwalkingMode.Laneclear:
                    Jungleclear(sender, args);
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

            var gapSender = args.Unit;
            if (gapSender == null ||
                !gapSender.IsEnemy ||
                !gapSender.IsMelee ||
                Invulnerable.Check(gapSender, DamageType.Magical, false))
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser E.
            /// </summary>
            if (SpellClass.E.Ready &&
                args.EndPosition.Distance(UtilityClass.Player.ServerPosition) < SpellClass.E.Range &&
                MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
            {
                SpellClass.E.Cast(args.EndPosition);
            }
        }

        /// <summary>
        ///     Handles the <see cref="E:ProcessSpell" /> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        public void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (sender.IsMe)
            {
                var spellName = args.SpellData.Name;
                if (spellName.Equals("JhinR"))
                {
                    UltimateShotsCount = 0;
                    End = args.End;
                }
                else if (spellName.Equals("JhinRShot"))
                {
                    UltimateShotsCount++;
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

            if (SpellClass.E.State == SpellState.Ready && args.IsDirectedToPlayer && args.Sender.IsValidTarget(SpellClass.E.SpellData.Range)
                && MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
            {
                if (!SpellClass.E.GetPrediction(args.Sender).CollisionObjects.Any())
                {
                    SpellClass.E.Cast(args.Sender.ServerPosition);
                }
            }

            if (SpellClass.W.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.W.SpellData.Range)
                && MenuClass.Spells["w"]["gapcloser"].As<MenuBool>().Enabled)
            {
                SpellClass.W.Cast(args.End);
            }
        }
        */

        /// <summary>
        ///     Fired on issuing an order.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseIssueOrderEventArgs" /> instance containing the event data.</param>
        public void OnIssueOrder(Obj_AI_Base sender, Obj_AI_BaseIssueOrderEventArgs args)
        {
            if (sender.IsMe &&
                IsUltimateShooting() &&
                args.OrderType == OrderType.MoveTo)
            {
                args.ProcessEvent = false;
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

                case OrbwalkingMode.Lasthit:
                    Lasthit();
                    break;
            }
        }

        #endregion
    }
}