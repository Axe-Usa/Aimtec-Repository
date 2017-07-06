
#pragma warning disable 1587
namespace AIO
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    using GameObjects = Aimtec.SDK.Util.Cache.GameObjects;

    /// <summary>
    ///     The general class.
    /// </summary>
    internal partial class General
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on spell cast.
        /// </summary>
        public static void OnCastSpell(Obj_AI_Base sender, SpellBookCastSpellEventArgs args)
        {
            if (sender.IsMe &&
                (args.Slot == SpellSlot.Q || args.Slot == SpellSlot.W || args.Slot == SpellSlot.E || args.Slot == SpellSlot.R))
            {
                /// <summary>
                ///     The 'Sheen' Logic.
                /// </summary>
                if (UtilityClass.Player.HasSheenLikeBuff() &&
                    MenuClass.General["usesheen"].Enabled)
                {
                    // Basically means it wont cast anything if the player has a sheen buff and is in combo/laneclear mode.
                    if (!UtilityClass.Player.HasTearLikeItem() &&
                        UtilityClass.Player.ShouldPreserveSheen())
                    {
                        switch (ImplementationClass.IOrbwalker.Mode)
                        {
                            case OrbwalkingMode.Combo:
                            case OrbwalkingMode.Laneclear:
                                args.Process = false;
                                break;
                        }
                    }
                }

                /// <summary>
                ///     The 'Support Mode' Logic.
                /// </summary>
                if (Extensions.GetEnemyLaneMinionsTargets().Contains(args.Target) &&
                    MenuClass.General["supportmode"].Enabled)
                {
                    args.Process = GameObjects.AllyHeroes.Any(a => !a.IsMe && a.Distance(UtilityClass.Player) >= 2500);
                }
            }
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void OnPreAttack(object sender, PreAttackEventArgs args)
        {
            switch (ImplementationClass.IOrbwalker.Mode)
            {
                /// <summary>
                ///     The 'No AA in Combo' Logic.
                /// </summary>
                case OrbwalkingMode.Combo:
                    if (MenuClass.General["disableaa"].Enabled &&
                        !UtilityClass.Player.HasSheenLikeBuff())
                    {
                        args.Cancel = true;
                    }
                    break;

                /// <summary>
                ///     The 'Support Mode' Logic.
                /// </summary>
                case OrbwalkingMode.Mixed:
                case OrbwalkingMode.Lasthit:
                case OrbwalkingMode.Laneclear:
                    if (Extensions.GetEnemyLaneMinionsTargets().Contains(args.Target) &&
                        MenuClass.General["supportmode"].Enabled)
                    {
                        args.Cancel = GameObjects.AllyHeroes.Any(a => !a.IsMe && a.Distance(UtilityClass.Player) < 2500);
                    }
                    break;
            }
        }

        #endregion
    }
}