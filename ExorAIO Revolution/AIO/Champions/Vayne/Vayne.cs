
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

                if (UtilityClass.Player.HasBuff("summonerexhaust"))
                {
                    args.Cancel = true;
                }

                if (GameObjects.EnemyHeroes.Count(t =>
                        t.IsValidTarget(UtilityClass.Player.GetFullAttackRange(t))) >=
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
        /// <param name="args">The <see cref="Gapcloser.GapcloserArgs" /> instance containing the event data.</param>
        public void OnGapcloser(Obj_AI_Hero sender, Gapcloser.GapcloserArgs args)
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
            ///     The Anti-Gapcloser E.
            /// </summary>
            if (SpellClass.E.Ready &&
                !Invulnerable.Check(sender, DamageType.Magical, false))
            {
                if (sender.IsMelee)
                {
                    switch (args.Type)
                    {
                        case Gapcloser.Type.Targeted:
                            if (args.Target.IsMe)
                            {
                                SpellClass.E.CastOnUnit(sender);
                            }
                            break;
                        default:
                            if (args.EndPosition.Distance(UtilityClass.Player.ServerPosition) <= UtilityClass.Player.AttackRange)
                            {
                                SpellClass.E.CastOnUnit(sender);
                            }
                            break;
                    }
                }

                const int condemnPushDistance = 410 / 10;
                for (var i = 1; i < 10; i++)
                {
                    var endPos = args.EndPosition.Extend(UtilityClass.Player.ServerPosition, -condemnPushDistance * i);
                    if (endPos.IsWall(true))
                    {
                        SpellClass.E.CastOnUnit(sender);
                    }
                }
            }

            /// <summary>
            ///     The Anti-Gapcloser Q.
            /// </summary>
            if (SpellClass.Q.Ready)
            {
                if (sender.IsMelee)
                {
                    switch (args.Type)
                    {
                        case Gapcloser.Type.Targeted:
                            if (args.Target.IsMe)
                            {
                                SpellClass.Q.Cast(UtilityClass.Player.ServerPosition.Extend(args.StartPosition, -(SpellClass.Q.Range - UtilityClass.Player.AttackRange)));
                            }
                            break;
                        default:
                            if (args.EndPosition.Distance(UtilityClass.Player.ServerPosition) <= UtilityClass.Player.AttackRange)
                            {
                                SpellClass.Q.Cast(UtilityClass.Player.ServerPosition.Extend(args.StartPosition, -(SpellClass.Q.Range - UtilityClass.Player.AttackRange)));
                            }
                            break;
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