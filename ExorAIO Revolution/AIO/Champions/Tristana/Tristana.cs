
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Damage.JSON;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Tristana
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Tristana.
        /// </summary>
        public Tristana()
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
        ///     Gets the total explosion damage on a determined unit.
        /// </summary>
        public double GetTotalExplosionDamage(Obj_AI_Base unit)
        {
            var player = UtilityClass.Player;
            return player.GetSpellDamage(unit, SpellSlot.E) +
                   player.GetSpellDamage(unit, SpellSlot.E, DamageStage.Buff);
        }

        /// <summary>
        ///     Fired when a buff is added.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="buff">The buff.</param>
        public void OnAddBuff(Obj_AI_Base sender, Buff buff)
        {
            if (sender.IsMe &&
                SpellClass.W.Ready &&
                MenuClass.Spells["w"]["antigrab"].As<MenuBool>().Value)
            {
                if (buff.Name.Equals("ThreshQ") ||
                    buff.Name.Equals("rocketgrab2"))
                {
                    SpellClass.W.Cast(UtilityClass.Player.Position.Extend(buff.Caster.Position, -SpellClass.W.Range));
                }
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
            ///     The Target Forcing Logic.
            /// </summary>
            if (MenuClass.Miscellaneous["focuse"].As<MenuBool>().Enabled)
            {
                var orbTarget = args.Target as Obj_AI_Hero;
                var forceTarget = Extensions.GetBestEnemyHeroesTargetsInRange(UtilityClass.Player.AttackRange).FirstOrDefault(t => t.HasBuff("TristanaECharge"));

                if (orbTarget != null &&
                    forceTarget != null &&
                    orbTarget.NetworkId != forceTarget.NetworkId)
                {
                    ImplementationClass.IOrbwalker.ForceTarget(forceTarget);
                }
            }

            switch (ImplementationClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    this.Combo(sender, args);
                    break;
                case OrbwalkingMode.Mixed:
                    this.Harass(sender, args);
                    break;
                case OrbwalkingMode.Laneclear:
                    this.Laneclear(sender, args);
                    this.Jungleclear(sender, args);
                    this.Buildingclear(sender, args);
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

        /*
        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser W Logic.
            /// </summary>
            if (SpellClass.W.State == SpellState.Ready && args.Sender.IsMelee && args.IsDirectedToPlayer
                && UtilityClass.Player.Distance(args.End) < UtilityClass.Player.FullAttackRange(Targets.Target
                && MenuClass.Spells["w"]["gapcloser"].As<MenuBool>().Value)
            {
                SpellClass.W.Cast(UtilityClass.Player.ServerPosition.Extend(args.Sender.ServerPosition, -SpellClass.W.SpellData.Range));
            }

            if (Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser R Logic.
            /// </summary>
            if (SpellClass.R.State == SpellState.Ready && args.Sender.IsMelee && args.IsDirectedToPlayer
                && args.Sender.IsValidTarget(SpellClass.R.SpellData.Range)
                && MenuClass.Spells["r"]["gapcloser"].As<MenuBool>().Value)
            {
                UtilityClass.Player.SpellBook.CastSpell(SpellSlot.R, args.Sender);
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (UtilityClass.Player.IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.R.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.R.SpellData.Range)
                && MenuClass.Spells["r"]["interrupter"].As<MenuBool>().Value)
            {
                UtilityClass.Player.SpellBook.CastSpell(SpellSlot.R, args.Sender);
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
            ///     Updates the spells.
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
        }

        #endregion
    }
}