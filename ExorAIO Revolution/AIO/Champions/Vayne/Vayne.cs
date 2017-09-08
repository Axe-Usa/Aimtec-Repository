
using System.Linq;
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
    internal partial class Vayne
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Vayne.
        /// </summary>
        public Vayne()
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

        #endregion

        #region Public Methods and Operators

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
                    Lasthit(sender, args);
                    Laneclear(sender, args);
                    Jungleclear(sender, args);
                    Buildingclear(sender, args);
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
            if (!UtilityClass.Player.IsUnderEnemyTurret() &&
                UtilityClass.Player.HasBuff("vaynetumblefade"))
            {
                var invisibilityBuff = UtilityClass.Player.GetBuff("vaynetumblefade");
                if (invisibilityBuff.GetRemainingBuffTime() >
                    invisibilityBuff.EndTime - invisibilityBuff.StartTime - MenuClass.Miscellaneous["stealthtime"].As<MenuSlider>().Value / 1000f)
                {
                    args.Cancel = true;
                }

                else if (UtilityClass.Player.HasBuff("summonerexhaust"))
                {
                    args.Cancel = true;
                }

                else if (GameObjects.EnemyHeroes.Count(t => t.IsValidTarget(UtilityClass.Player.AttackRange)) >=
                    MenuClass.Miscellaneous["stealthcheck"].As<MenuSlider>().Value)
                {
                    args.Cancel = true;
                }
            }

            /// <summary>
            ///     The Target Forcing Logic.
            /// </summary>
            if (MenuClass.Miscellaneous["focusw"].As<MenuBool>().Enabled)
            {
                var forceTarget = Extensions.GetBestEnemyHeroesTargets().FirstOrDefault(t =>
                        t.GetBuffCount("vaynesilvereddebuff") == 2 &&
                        t.IsValidTarget(UtilityClass.Player.GetFullAttackRange(t)));
                if (forceTarget != null)
                {
                    //ImplementationClass.IOrbwalker.ForceTarget(forceTarget);
                    args.Target = forceTarget;
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

            var gapSender = args.Unit;
            if (gapSender == null || !gapSender.IsEnemy || !gapSender.IsMelee)
            {
                return;
            }

            var playerPos = UtilityClass.Player.ServerPosition;

            /// <summary>
            ///     The Anti-Gapcloser Q.
            /// </summary>
            if (SpellClass.Q.Ready &&
                args.EndPosition.Distance(playerPos) <= 300 &&
                MenuClass.Spells["q"]["gapcloser"].As<MenuBool>().Enabled)
            {
                SpellClass.Q.Cast(UtilityClass.Player.ServerPosition.Extend(args.StartPosition, -300f));
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser E.
            /// </summary>
            if (SpellClass.E.Ready &&
                !Invulnerable.Check(gapSender, DamageType.Magical, false) &&
                MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
            {
                if (args.EndPosition.Distance(playerPos) <= 300)
                {
                    SpellClass.E.CastOnUnit(gapSender);
                }
                else
                {
                    const int condemnPushDistance = 410 / 10;
                    for (var i = 1; i < 10; i++)
                    {
                        var endPos = args.EndPosition.Extend(playerPos, -condemnPushDistance * i);
                        if (endPos.IsWall(true))
                        {
                            SpellClass.E.CastOnUnit(gapSender);
                        }
                    }
                }
            }
        }

        /*
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
            }
        }

        #endregion
    }
}