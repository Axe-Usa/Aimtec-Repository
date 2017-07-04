
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ezreal
    {
        #region Public Methods and Operators


        /// <summary>
        ///     Loads Ezreal.
        /// </summary>
        public static void OnLoad()
        {
            /// <summary>
            ///     Initializes the menus.
            /// </summary>
            Menus();

            /// <summary>
            ///     Initializes the spells.
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
        ///     Fired when a buff is added.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="buff">The buff.</param>
        public static void OnAddBuff(Obj_AI_Base sender, Buff buff)
        {
            if (sender.IsMe &&
                SpellClass.E.Ready &&
                MenuClass.Spells["e"]["antigrab"].As<MenuBool>().Value)
            {
                if (buff.Name.Equals("ThreshQ") ||
                    buff.Name.Equals("rocketgrab2"))
                {
                    SpellClass.E.Cast(UtilityClass.Player.Position.Extend(buff.Caster.Position, -SpellClass.E.Range));
                }
            }
        }

        /// <summary>
        ///     Called on non killable minion.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NonKillableMinionEventArgs" /> instance containing the event data.</param>
        public static void OnNonKillableMinion(object sender, NonKillableMinionEventArgs args)
        {
            var minion = (Obj_AI_Minion)args.Target;

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (UtilityClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Laneclear:
                case OrbwalkingMode.Lasthit:
                case OrbwalkingMode.Mixed:
                    if (SpellClass.Q.Ready &&
                        minion.GetRealHealth() <
                            UtilityClass.Player.GetSpellDamage(minion, SpellSlot.Q) &&
                        UtilityClass.Player.ManaPercent()
                            > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["farmhelper"]) &&
                        MenuClass.Spells["q"]["farmhelper"].As<MenuSliderBool>().Enabled)
                    {
                        SpellClass.Q.Cast(minion);
                    }
                    break;
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPostAttack(object sender, PostAttackEventArgs args)
        {
            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (UtilityClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    Weaving(sender, args);
                    break;
                case OrbwalkingMode.Laneclear:
                    Jungleclear(sender, args);
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
            if (ObjectManager.GetLocalPlayer().IsDead)
            {
                return;
            }

            if (SpellClass.E.State == SpellState.Ready && args.Sender.IsMelee && args.Sender.IsValidTarget(SpellClass.E.SpellData.Range)
                && args.SkillType == GapcloserType.Targeted
                && MenuClass.Spells["e"]["gapcloser"].As<MenuBool>().Value)
            {
                if (args.Target.IsMe)
                {
                    SpellClass.E.Cast(ObjectManager.GetLocalPlayer().ServerPosition.Extend(args.Sender.ServerPosition, SpellClass.E.SpellData.Range));
                }
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

            if (UtilityClass.IOrbwalker.IsWindingUp)
            {
                return;
            }

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Automatic();

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (UtilityClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    Combo();
                    break;
                case OrbwalkingMode.Mixed:
                    Harass();
                    break;
                case OrbwalkingMode.Lasthit:
                    Lasthit();
                    break;
                case OrbwalkingMode.Laneclear:
                    Laneclear();
                    break;
            }
        }

        #endregion
    }
}