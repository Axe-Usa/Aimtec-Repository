
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

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Tristana
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Loads Tryndamere.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus();

            /// <summary>
            ///     Updates the spells.
            /// </summary>
            Spells();

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
        ///     Gets the total explosion damage on a determined unit.
        /// </summary>
        public static double GetTotalExplosionDamage(Obj_AI_Base unit)
        {
            var player = UtilityClass.Player;
            return player.GetSpellDamage(unit, SpellSlot.E) +
                   player.GetSpellDamage(unit, SpellSlot.E, DamageStage.Buff);
        }

        /// <summary>
        ///     Called on pre attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPreAttack(object sender, PreAttackEventArgs args)
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
                    UtilityClass.Orbwalker.ForceTarget(forceTarget);
                }
            }

            switch (UtilityClass.Orbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    Combo(sender, args);
                    break;
                case OrbwalkingMode.Mixed:
                    Harass(sender, args);
                    break;
                case OrbwalkingMode.Laneclear:
                    Laneclear(sender, args);
                    Jungleclear(sender, args);
                    Buildingclear(sender, args);
                    break;
            }
        }

        /// <summary>
        ///     Fired when a buff is added.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="buff">The buff.</param>
        public static void OnAddBuff(Obj_AI_Base sender, Buff buff)
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

        /*
        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public static void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (ObjectManager.GetLocalPlayer().IsDead)
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser W Logic.
            /// </summary>
            if (SpellClass.W.State == SpellState.Ready && args.Sender.IsMelee && args.IsDirectedToPlayer
                && ObjectManager.GetLocalPlayer().Distance(args.End) < ObjectManager.GetLocalPlayer().FullAttackRange(Targets.Target
                && MenuClass.Spells["w"]["gapcloser"].As<MenuBool>().Value)
            {
                SpellClass.W.Cast(ObjectManager.GetLocalPlayer().ServerPosition.Extend(args.Sender.ServerPosition, -SpellClass.W.SpellData.Range));
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
                ObjectManager.GetLocalPlayer().SpellBook.CastSpell(SpellSlot.R, args.Sender);
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public static void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (ObjectManager.GetLocalPlayer().IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.R.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.R.SpellData.Range)
                && MenuClass.Spells["r"]["interrupter"].As<MenuBool>().Value)
            {
                ObjectManager.GetLocalPlayer().SpellBook.CastSpell(SpellSlot.R, args.Sender);
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
            if (UtilityClass.Orbwalker.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Automatic();
        }

        #endregion
    }
}