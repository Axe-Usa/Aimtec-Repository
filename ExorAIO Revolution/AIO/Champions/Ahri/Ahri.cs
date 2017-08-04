
#pragma warning disable 1587

namespace AIO.Champions
{
    using System;

    using Aimtec;
    using Aimtec.SDK.Events;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;
    using Aimtec.SDK.Util;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ahri
    {
        #region Constructors and Destructors

        /// <summary>
        ///     Loads Ahri.
        /// </summary>
        public Ahri()
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
            ///     Updates the spells.
            /// </summary>
            this.Spells();
        }

        #endregion

        #region Public Methods and Operators

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
        ///     Called while processing spellcast operations.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        public void OnPerformCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (sender.IsMe)
            {
                switch (args.SpellSlot)
                {
                    case SpellSlot.R:
                        if (SpellClass.W.Ready &&
                            MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
                        {
                            SpellClass.W.Cast();
                        }
                        break;
                }

                switch (MenuClass.Spells["pattern"].As<MenuList>().Value)
                {
                    case 0:
                        switch (args.SpellSlot)
                        {
                            case SpellSlot.E:
                                if (SpellClass.R.Ready &&
                                    MenuClass.Spells["r"]["combo"].As<MenuBool>().Enabled)
                                {
                                    if (!UtilityClass.Player.HasBuff("AhriTumble") &&
                                        MenuClass.Spells["r"]["customization"]["onlyrstarted"].As<MenuBool>().Enabled)
                                    {
                                        break;
                                    }

                                    const float RRadius = 500f;
                                    var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.R.Range + RRadius);
                                    if (heroTarget == null ||
                                         Invulnerable.Check(heroTarget, DamageType.Magical) ||
                                        !MenuClass.Spells["r"]["whitelist"][heroTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                                    {
                                        break;
                                    }

                                    var position = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, SpellClass.R.Range);
                                    if (heroTarget.IsValidTarget(RRadius, false, false, position))
                                    {
                                        DelayAction.Queue(200+Game.Ping, ()=> SpellClass.R.Cast(position));
                                    }
                                }
                                break;
                        }
                        break;

                    case 1:
                        switch (args.SpellSlot)
                        {
                            case SpellSlot.R:
                                if (SpellClass.E.Ready &&
                                    MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
                                {
                                    var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.E.Range-150f);
                                    if (heroTarget == null ||
                                        Invulnerable.Check(heroTarget, DamageType.Magical, false))
                                    {
                                        break;
                                    }

                                    SpellClass.E.Cast(heroTarget);
                                }
                                break;
                        }
                        break;
                }

                switch (args.SpellSlot)
                {
                    case SpellSlot.Q:
                    case SpellSlot.W:
                        if (SpellClass.R.Ready &&
                            UtilityClass.Player.HasBuff("AhriTumble") &&
                            MenuClass.Spells["r"]["combo"].As<MenuBool>().Enabled)
                        {
                            const float RRadius = 500f;
                            var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.R.Range + RRadius);
                            if (heroTarget == null ||
                                Invulnerable.Check(heroTarget, DamageType.Magical) ||
                                !MenuClass.Spells["r"]["whitelist"][heroTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                            {
                                break;
                            }

                            var position = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, SpellClass.R.Range);
                            if (heroTarget.IsValidTarget(RRadius, false, false, position))
                            {
                                SpellClass.R.Cast(position);
                            }
                        }
                        break;
                }
            }
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
            if (gapSender == null ||
                !gapSender.IsEnemy ||
                Invulnerable.Check(gapSender, DamageType.Magical, false))
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser E.
            /// </summary>
            if (SpellClass.E.Ready &&
                args.EndPos.Distance(UtilityClass.Player.ServerPosition) < SpellClass.E.Range &&
                MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Enabled)
            {
                if (args.EndPos.Distance(UtilityClass.Player.ServerPosition) >= 200)
                {
                    SpellClass.E.Cast(args.EndPos);
                }
                else
                {
                    SpellClass.E.Cast(gapSender.ServerPosition);
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
            }
        }

        #endregion
    }
}