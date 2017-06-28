#pragma warning disable 1587
namespace ExorAIO
{
    using System;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Events;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    using AIO.Core;

    using GameObjects = Aimtec.SDK.Util.Cache.GameObjects;

    internal class Program
    {
        #region Methods

        /// <summary>
        ///     The entry point of the application.
        /// </summary>
        private static void Main()
        {
            GameEvents.GameStart += OnStart;
        }

        /// <summary>
        ///     Event which triggers on game start.
        /// </summary>
        private static void OnStart()
        {
            Bootstrap.LoadMenu();
            Bootstrap.LoadChampion();
            Console.WriteLine("ExorAIO: Revolution - " + UtilityClass.Player.ChampionName + (Bools.IsChampionSupported ? " Loaded." : " Not supported."));

            SpellBook.OnCastSpell += OnCastSpell;
            Orbwalker.Implementation.PreAttack += OnPreAttack;
        }

        /// <summary>
        ///     Called on spell cast.
        /// </summary>
        private static void OnCastSpell(Obj_AI_Base sender, SpellBookCastSpellEventArgs args)
        {
            if (sender.IsMe)
            {
                /// <summary>
                ///     The 'Sheen' Logic.
                /// </summary>
                if (UtilityClass.Player.HasSheenLikeBuff() &&
                    MenuClass.General["usesheen"].Enabled)
                {
                    // Basically means it wont cast anything if the player has a sheen buff and the target is inside the aa range and the player can attack.
                    if (!UtilityClass.Player.HasTearLikeItem() &&
                        !UtilityClass.GetBestEnemyHeroesTargetsInRange(2000f).Any() &&
                        UtilityClass.Player.ShouldPreserveSheen() &&
                        UtilityClass.Player.Distance(args.End) <= UtilityClass.Player.AttackRange)
                    {
                        args.Process = false;
                    }
                }

                /// <summary>
                ///     The 'Support Mode' Logic.
                /// </summary>
                if (UtilityClass.GetEnemyLaneMinionsTargets().Contains(args.Target) &&
                    MenuClass.General["supportmode"].Enabled)
                {
                    args.Process = GameObjects.AllyHeroes.Any(a => a.Distance(UtilityClass.Player) >= 2500);
                }
            }
        }

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        private static void OnPreAttack(object sender, PreAttackEventArgs args)
        {
            switch (Orbwalker.Implementation.Mode)
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
                    if (UtilityClass.GetEnemyLaneMinionsTargets().Contains(args.Target) &&
                        MenuClass.General["supportmode"].Enabled)
                    {
                        args.Cancel = GameObjects.AllyHeroes.Any(a => a.Distance(UtilityClass.Player) < 2500);
                    }
                    break;
            }
        }

        #endregion
    }
}