
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Caitlyn
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Caitlyn.
        /// </summary>
        public Caitlyn()
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
                        if (GameObjects.EnemyHeroes.Count(t => !t.IsDead && t.Distance(UtilityClass.Player) < UtilityClass.Player.AttackRange)
                                > MenuClass.Spells["q"]["customization"]["safeq"].As<MenuSlider>().Value)
                        {
                            args.Process = false;
                        }
                        break;

                    case SpellSlot.W:
                        if (ObjectManager.Get<Obj_AI_Minion>().Any(m => m.Distance(args.End) < 200 && m.UnitSkinName.Equals("CaitlynTrap")))
                        {
                            args.Process = false;
                        }
                        break;

                    case SpellSlot.E:
                        if (Game.TickCount - UtilityClass.LastTick < 1000)
                        {
                            return;
                        }

                        if (ImplementationClass.IOrbwalker.Mode == OrbwalkingMode.None &&
                            MenuClass.Miscellaneous["reversede"].As<MenuBool>().Enabled)
                        {
                            UtilityClass.LastTick = Game.TickCount;
                            SpellClass.E.Cast(UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, -SpellClass.E.Range));
                        }
                        break;
                }
            }
        }

        /// <summary>
        ///     Called on do-cast.
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
                    this.Weaving(sender, args);
                    break;
                case OrbwalkingMode.Laneclear:
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
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        public void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (sender.IsMe)
            {
                /// <summary>
                ///     Initializes the orbwalkingmodes.
                /// </summary>
                switch (ImplementationClass.IOrbwalker.Mode)
                {
                    case OrbwalkingMode.Combo:
                        switch (args.SpellData.Name)
                        {
                            case "CaitlynEntrapment":
                            case "CaitlynEntrapmentMissile":
                                if (SpellClass.W.Ready &&
                                    MenuClass.Spells["w"]["triplecombo"].As<MenuBool>().Enabled)
                                {
                                    var bestTarget = GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(SpellClass.E.Range)).OrderBy(o => o.Distance(args.End)).First();
                                    if (bestTarget != null)
                                    {
                                        SpellClass.W.Cast(bestTarget.ServerPosition);
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
        }

        /// <summary>
        ///     Called on teleport issued.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseTeleportEventArgs" /> instance containing the event data.</param>
        public void OnTeleport(Obj_AI_Base sender, Obj_AI_BaseTeleportEventArgs args)
        {
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["teleport"].As<MenuBool>().Enabled)
            {
                foreach (var target in ObjectManager.Get<Obj_AI_Minion>().Where(
                    m =>
                        m.IsEnemy &&
                        m.Distance(UtilityClass.Player) <= SpellClass.W.Range))
                {
                    if (target.Buffs.Any(b => b.IsValid && b.IsActive && b.Name.Equals("teleport_target")))
                    {
                        SpellClass.W.Cast(target.ServerPosition);
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

            if (SpellClass.E.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.E.SpellData.Range)
                && MenuClass.Spells["e"]["interrupter"].As<MenuBool>().Enabled)
            {
                if (!SpellClass.E.GetPrediction(args.Sender).CollisionObjects.Any())
                {
                    SpellClass.E.Cast(SpellClass.E.GetPrediction(args.Sender).UnitPosition);
                }
            }

            if (SpellClass.W.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.W.SpellData.Range)
                && MenuClass.Spells["w"]["interrupter"].As<MenuBool>().Enabled)
            {
                SpellClass.W.Cast(SpellClass.W.GetPrediction(args.Sender).CastPosition);
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