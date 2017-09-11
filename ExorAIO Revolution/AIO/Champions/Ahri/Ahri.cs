
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Util;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
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
            Menus();

            /// <summary>
            ///     Initializes the methods.
            /// </summary>
            Methods();

            /// <summary>
            ///     Updates the spells.
            /// </summary>
            Spells();
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
            Drawings();
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

                                    const float rRadius = 500f;
                                    var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.R.Range + rRadius);
                                    if (heroTarget == null ||
                                         Invulnerable.Check(heroTarget, DamageType.Magical) ||
                                        !MenuClass.Spells["r"]["whitelist"][heroTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                                    {
                                        break;
                                    }

                                    var position = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, SpellClass.R.Range);
                                    if (heroTarget.IsValidTarget(rRadius, false, false, position))
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
                            const float rRadius = 500f;
                            var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.R.Range + rRadius);
                            if (heroTarget == null ||
                                Invulnerable.Check(heroTarget, DamageType.Magical) ||
                                !MenuClass.Spells["r"]["whitelist"][heroTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                            {
                                break;
                            }

                            var position = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, SpellClass.R.Range);
                            if (heroTarget.IsValidTarget(rRadius, false, false, position))
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
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="GapcloserArgs" /> instance containing the event data.</param>
        public void OnGapcloser(Obj_AI_Hero sender, GapcloserArgs args)
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            if (sender == null ||
                !sender.IsEnemy ||
                Invulnerable.Check(sender, DamageType.Magical, false))
            {
                return;
            }

            /// <summary>
            ///     The Anti-Gapcloser E.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                switch (args.Type)
                {
                    case GapSpellType.Targeted:
                        if (sender.IsMelee &&
                            args.Target.IsMe)
                        {
                            SpellClass.E.Cast(args.StartPosition);
                        }
                        break;
                    default:
                        if (args.EndPosition.Distance(UtilityClass.Player.ServerPosition) <= SpellClass.E.Range)
                        {
                            SpellClass.E.Cast(args.EndPosition);
                        }
                        break;
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
                case OrbwalkingMode.Mixed:
                    Harass();
                    break;
                case OrbwalkingMode.Laneclear:
                    Laneclear();
                    Jungleclear();
                    break;
            }
        }

        #endregion
    }
}