
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Vayne
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
        ///     Called on pre attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPreAttack(object sender,  PreAttackEventArgs args)
        {
            if (!UtilityClass.Player.IsUnderEnemyTurret() &&
                UtilityClass.Player.HasBuff("vaynetumblefade"))
            {
                var invisibilityBuff = UtilityClass.Player.GetBuff("vaynetumblefade");
                if (invisibilityBuff.EndTime - Game.ClockTime >
                    invisibilityBuff.EndTime - invisibilityBuff.StartTime - MenuClass.Miscellaneous["stealthtime"].As<MenuSlider>().Value / 1000f)
                {
                    args.Cancel = true;
                }
                else if (UtilityClass.Player.HasBuff("summonerexhaust"))
                {
                    args.Cancel = true;
                }
            }

            /// <summary>
            ///     The Target Forcing Logic.
            /// </summary>
            if (MenuClass.Miscellaneous["focusw"].As<MenuBool>().Enabled)
            {
                var orbTarget = args.Target as Obj_AI_Hero;
                var forceTarget = Extensions.GetBestEnemyHeroesTargetsInRange(UtilityClass.Player.AttackRange).FirstOrDefault(t => t.GetBuffCount("vaynesilvereddebuff") == 2);

                if (orbTarget != null &&
                    forceTarget != null &&
                    orbTarget.NetworkId != forceTarget.NetworkId)
                {
                    Orbwalker.Implementation.ForceTarget(forceTarget);
                }
            }
        }

        /// <summary>
        ///     Called on post attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPostAttack(object sender, PostAttackEventArgs args)
        {
            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Orbwalker.Implementation.Mode)
            {
                case OrbwalkingMode.Combo:
                    Weaving(sender, args);
                    break;
                case OrbwalkingMode.Laneclear:
                    Laneclear(sender, args);
                    Jungleclear(sender, args);
                    Buildingclear(sender, args);
                    break;
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
            if (UtilityClass.Player.IsDead ||
                Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.E.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.E.SpellData.Range))
            {
                /// <summary>
                ///     The Anti-GapCloser E Logic.
                /// </summary>
                if (args.Sender.IsMelee && args.IsDirectedToPlayer
                    && MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
                {
                    UtilityClass.Player.SpellBook.CastSpell(SpellSlot.E, args.Sender);
                }

                /// <summary>
                ///     The Dash-Condemn Prediction Logic.
                /// </summary>
                if (!UtilityClass.Player.IsDashing()
                    && UtilityClass.Player.Distance(args.End) > UtilityClass.Player.BoundingRadius
                    && MenuClass.Spells["e"]["dashpred"].As<MenuBool>().Enabled
                    && MenuClass.Spells["e"]["whitelist"][args.Sender.ChampionName.ToLower()].As<MenuBool>()
                           .Value)
                {
                    for (var i = 1; i < 10; i++)
                    {
                        var vector = Vector3.Normalize(args.End - UtilityClass.Player.ServerPosition);
                        if ((args.End + vector * (float)(i * 42.5)).IsWall()
                            && (args.End + vector * (float)(i * 44.5)).IsWall())
                        {
                            UtilityClass.Player.SpellBook.CastSpell(SpellSlot.E, args.Sender);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public static void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (UtilityClass.Player.IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.E.State == SpellState.Ready && args.Sender.IsValidTarget(SpellClass.E.SpellData.Range)
                && MenuClass.Spells["e"]["interrupter"].As<MenuBool>().Enabled)
            {
                UtilityClass.Player.SpellBook.CastSpell(SpellSlot.E, args.Sender);
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
            ///     Initializes the Killsteal events.
            /// </summary>
            Killsteal();

            if (Orbwalker.Implementation.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Orbwalker.Implementation.Mode)
            {
                case OrbwalkingMode.Combo:
                    Combo();
                    break;
            }
        }

        #endregion
    }
}