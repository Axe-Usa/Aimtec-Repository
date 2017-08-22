
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
    internal partial class Orianna
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Orianna.
        /// </summary>
        public Orianna()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            this.Menus();

            /// <summary>
            ///     Initializes the spells.
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
        ///     Fired on spell cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="SpellBookCastSpellEventArgs" /> instance containing the event data.</param>
        public void OnCastSpell(Obj_AI_Base sender, SpellBookCastSpellEventArgs args)
        {
            if (sender.IsMe &&
                this.BallPosition != null &&
                args.Slot == SpellSlot.R)
            {
                var validTargets = GameObjects.EnemyHeroes.Where(t =>
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        t.IsValidTarget(SpellClass.R.Width - t.BoundingRadius - SpellClass.R.Delay * t.BoundingRadius, false, false, (Vector3)this.BallPosition));
                if (!validTargets.Any())
                {
                    args.Process = false;
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
            if (gapSender == null)
            {
                return;
            }

            /// <summary>
            ///     The On-Dash E Logics.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                /// <summary>
                ///     The Anti-Gapcloser E.
                /// </summary>
                if (gapSender.IsEnemy &&
                    MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
                {
                    var playerPos = UtilityClass.Player.ServerPosition;
                    if (args.EndPos.Distance(playerPos) <= 200)
                    {
                        SpellClass.E.CastOnUnit(UtilityClass.Player);
                    }
                    else
                    {
                        var bestAlly = GameObjects.AllyHeroes
                            .Where(a =>
                                !a.IsMe &&
                                a.IsValidTarget(SpellClass.E.Range, true) &&
                                args.EndPos.Distance(a.ServerPosition) <= 200)
                            .MinBy(o => o.MaxHealth);

                        if (bestAlly != null)
                        {
                            SpellClass.E.CastOnUnit(bestAlly);
                        }
                    }
                }

                if (MenuClass.Spells["r"]["aoe"] != null &&
                    MenuClass.Spells["r"]["aoe"].As<MenuSliderBool>().Enabled)
                {
                    return;
                }

                /// <summary>
                ///     The E Engager Logic.
                /// </summary>
                if (gapSender.IsAlly &&
                    gapSender.IsValidTarget(SpellClass.E.Range, true) &&
                    MenuClass.Spells["e"]["engager"].As<MenuBool>().Enabled)
                {
                    if (GameObjects.EnemyHeroes.Count(t =>
                            !Invulnerable.Check(t, DamageType.Magical, false) &&
                            t.IsValidTarget(SpellClass.R.Width - SpellClass.R.Delay * t.BoundingRadius, false, false, (Vector3)args.EndPos)) >= MenuClass.Spells["r"]["aoe"]?.As<MenuSliderBool>().Value &&
                        MenuClass.Spells["e"]["engagerswhitelist"][gapSender.ChampionName.ToLower()].As<MenuBool>().Enabled)
                    {
                        SpellClass.E.CastOnUnit(gapSender);
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

            if (SpellClass.R.State == SpellState.Ready && ((Vector2)GetBallPosition).Distance(args.Sender.ServerPosition) < SpellClass.R.SpellData.Range
                && MenuClass.Spells["r"]["interrupter"].As<MenuBool>().Enabled)
            {
                UtilityClass.Player.SpellBook.CastSpell(SpellSlot.R);
            }
        }
        */

        /// <summary>
        ///     Called on process spell cast;
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        public void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            var target = args.Target as Obj_AI_Hero;
            if (target == null ||
                !Extensions.GetAllyHeroesTargetsInRange(SpellClass.E.Range).Contains(target))
            {
                return;
            }

            if (SpellClass.E.Ready &&
                Bools.ShouldShieldAgainstSender(sender) &&
                MenuClass.Spells["e"]["protect"].As<MenuBool>().Enabled &&
                MenuClass.Spells["e"]["protectwhitelist"][target.ChampionName.ToLower()].As<MenuSliderBool>().Enabled &&
                target.HealthPercent() <= MenuClass.Spells["e"]["protectwhitelist"][target.ChampionName.ToLower()].As<MenuSliderBool>().Value)
            {
                SpellClass.E.CastOnUnit(target);
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
            ///     Updates the position of the ball.
            /// </summary>
            this.UpdateBallPosition();

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            this.Automatic();

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            this.Killsteal();

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
                case OrbwalkingMode.Lasthit:
                    this.Lasthit();
                    break;
            }
        }

        #endregion
    }
}