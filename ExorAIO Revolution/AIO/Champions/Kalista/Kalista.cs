
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Collections.Generic;
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
    internal partial class Kalista
    {
        #region Static Fields

        /// <summary>
        ///     Gets all the important jungle locations.
        /// </summary>
        internal static readonly List<Vector3> Locations = new List<Vector3>
                                                           {
                                                               new Vector3(9827.56f, -71.2406f, 4426.136f),
                                                               new Vector3(4951.126f, -71.2406f, 10394.05f),
                                                               new Vector3(10998.14f, 51.72351f, 6954.169f),
                                                               new Vector3(7082.083f, 56.2041f, 10838.25f),
                                                               new Vector3(3804.958f, 52.11121f, 7875.456f),
                                                               new Vector3(7811.249f, 53.81299f, 4034.486f)
                                                           };

        #endregion

        #region Public Properties

        /// <summary>
        ///     Gets or sets the SoulBound.
        /// </summary>
        public static Obj_AI_Hero SoulBound;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Loads Kalista.
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
        ///     Returns true if the target is a perfectly valid rend target.
        /// </summary>
        public static bool IsPerfectRendTarget(Obj_AI_Base unit)
        {
            switch (unit.Type)
            {
                case GameObjectType.obj_AI_Minion:
                case GameObjectType.AIHeroClient:
                    var orbTarget = UtilityClass.IOrbwalker.GetTarget() as Obj_AI_Hero;
                    if (orbTarget != null && unit.IsValidTarget(orbTarget.NetworkId == unit.NetworkId ? SpellClass.E.Range : SpellClass.Q.Range))
                    {
                        return unit.HasBuff("kalistaexpungemarker") && (!(unit is Obj_AI_Hero) || !Invulnerable.Check((Obj_AI_Hero)unit));
                    }
                    break;
            }

            return false;
        }

        /// <summary>
        ///     Gets the total rend damage on a determined unit.
        /// </summary>
        public static double GetTotalRendDamage(Obj_AI_Base unit)
        {
            var player = UtilityClass.Player;
            return player.GetSpellDamage(unit, SpellSlot.E) +
                   player.GetSpellDamage(unit, SpellSlot.E, DamageStage.Buff);
        }

        /// <summary>
        ///     Called on pre attack.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPreAttack(object sender, PreAttackEventArgs args)
        {
            /// <summary>
            ///     The Target Forcing Logic.
            /// </summary>
            if (MenuClass.Miscellaneous["focusw"].As<MenuBool>().Enabled)
            {
                var orbTarget = args.Target as Obj_AI_Hero;
                var forceTarget = Extensions.GetBestEnemyHeroesTargets().FirstOrDefault(t =>
                        t.HasBuff("kalistacoopstrikemarkally") &&
                        t.IsValidTarget(UtilityClass.Player.GetFullAttackRange(t)));

                if (orbTarget != null &&
                    forceTarget != null &&
                    orbTarget.NetworkId != forceTarget.NetworkId)
                {
                    UtilityClass.IOrbwalker.ForceTarget(forceTarget);
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

        /// <summary>
        ///     Called on process spell cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        public static void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            var target = args.Target;
            if (sender.IsMe &&
                target != null && target is Obj_AI_Hero &&
                args.SpellData.Name.Equals("KalistaPInvocation"))
            {
                SoulBound = (Obj_AI_Hero)target;
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
                    if (SpellClass.E.Ready &&
                        IsPerfectRendTarget(minion) &&
                        minion.GetRealHealth() <= GetTotalRendDamage(minion) &&
                        MenuClass.Spells["e"]["farmhelper"].As<MenuBool>().Enabled)
                    {
                        SpellClass.E.Cast();
                    }
                    break;
            }
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        private static void OnUpdate()
        {
            if (UtilityClass.Player.IsDead)
            {
                return;
            }

            /// <summary>
            ///     Initializes the Automatic actions.
            /// </summary>
            Automatic();

            /// <summary>
            ///     Initializes the Killsteal events.
            /// </summary>
            Killsteal();

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
                case OrbwalkingMode.Laneclear:
                    Laneclear();
                    break;
            }
        }

        #endregion
    }
}