
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Anivia
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Anivia.
        /// </summary>
        public Anivia()
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
        ///     Called on non killable minion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NonKillableMinionEventArgs" /> instance containing the event data.</param>
        public void OnNonKillableMinion(object sender, NonKillableMinionEventArgs args)
        {
            var minion = (Obj_AI_Minion)args.Target;
            if (minion == null)
            {
                return;
            }

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (ImplementationClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Laneclear:
                case OrbwalkingMode.Lasthit:
                case OrbwalkingMode.Mixed:
                    if (SpellClass.E.Ready &&
                        minion.IsValidTarget(SpellClass.E.Range) &&
                        UtilityClass.Player.ManaPercent()
                            > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["lasthitunk"]) &&
                        MenuClass.Spells["e"]["lasthitunk"].As<MenuSliderBool>().Enabled)
                    {
                        if (minion.GetRealHealth() <= this.GetFrostBiteDamage(minion))
                        {
                            SpellClass.E.CastOnUnit(minion);
                        }
                    }
                    break;
            }
        }

        /// <summary>
        ///     Fired upon GameObject creation.
        /// </summary>
        public void OnCreate(GameObject obj)
        {
            if (obj != null && obj.IsValid)
            {
                switch (obj.Name)
                {
                    case "Anivia_Base_Q_AOE_Mis.troy":
                        this.FlashFrost = obj;
                        break;
                    case "Anivia_Base_R_indicator_ring.troy":
                        this.GlacialStorm = obj;
                        break;
                }
            }
        }

        /// <summary>
        ///     Fired upon GameObject creation.
        /// </summary>
        public void OnDestroy(GameObject obj)
        {
            if (obj != null && obj.IsValid)
            {
                switch (obj.Name)
                {
                    case "Anivia_Base_Q_AOE_Mis.troy":
                        this.FlashFrost = null;
                        break;
                    case "Anivia_Base_R_indicator_ring.troy":
                        this.GlacialStorm = null;
                        break;
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
            if (gapSender == null || !gapSender.IsEnemy || !gapSender.IsMelee)
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser W.
            /// </summary>
            if (SpellClass.W.Ready &&
                args.EndPos.Distance(UtilityClass.Player.ServerPosition) < SpellClass.W.Range &&
                MenuClass.Spells["w"]["gapcloser"].As<MenuBool>().Enabled)
            {
                var playerPos = UtilityClass.Player.ServerPosition;
                // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                if (args.EndPos.Distance(playerPos) <= 200)
                {
                    SpellClass.W.Cast(playerPos.Extend(args.StartPos, UtilityClass.Player.BoundingRadius));
                }
                else
                {
                    SpellClass.W.Cast(gapSender.ServerPosition.Extend(args.EndPos, UtilityClass.Player.BoundingRadius));
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
            this.Killsteal();

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            this.Automatic();

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (Orbwalker.Implementation.Mode)
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
                case OrbwalkingMode.Lasthit:
                    this.Lasthit();
                    break;
            }
        }

        #endregion
    }
}