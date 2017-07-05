
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
            if (sender.IsMe)
            {
                if (!GameObjects.EnemyHeroes.Any(
                        t =>
                            !Invulnerable.Check(t, DamageType.Magical, false) &&
                            t.IsValidTarget(SpellClass.R.Width, false, this.BallPosition)))
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

        /*
        /// <summary>
        ///     Fired on an incoming gapcloser.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="Events.GapCloserEventArgs" /> instance containing the event data.</param>
        public void OnGapCloser(object sender, Events.GapCloserEventArgs args)
        {
            if (ObjectManager.GetLocalPlayer().IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.E.State == SpellState.Ready && args.Sender.IsMelee && args.IsDirectedToPlayer
                && MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Value)
            {
                ObjectManager.GetLocalPlayer().SpellBook.CastSpell(SpellSlot.E, ObjectManager.GetLocalPlayer());
            }
        }

        /// <summary>
        ///     Called on interruptable spell.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Events.InterruptableTargetEventArgs" /> instance containing the event data.</param>
        public void OnInterruptableTarget(object sender, Events.InterruptableTargetEventArgs args)
        {
            if (ObjectManager.GetLocalPlayer().IsDead || Invulnerable.Check(args.Sender, DamageType.Magical, false))
            {
                return;
            }

            if (SpellClass.R.State == SpellState.Ready && ((Vector2)GetBallPosition).Distance(args.Sender.ServerPosition) < SpellClass.R.SpellData.Range
                && MenuClass.Spells["r"]["interrupter"].As<MenuBool>().Value)
            {
                ObjectManager.GetLocalPlayer().SpellBook.CastSpell(SpellSlot.R);
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
                MenuClass.Spells["e"]["logical"].As<MenuBool>().Value &&
                MenuClass.Spells["e"]["whitelist"][target.ChampionName.ToLower()].As<MenuBool>().Value)
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
            switch (UtilityClass.IOrbwalker.Mode)
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