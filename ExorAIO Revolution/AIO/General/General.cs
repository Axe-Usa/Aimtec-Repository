
#pragma warning disable 1587
namespace AIO
{
    using System;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The general class.
    /// </summary>
    internal partial class General
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired upon GameObject creation.
        /// </summary>
        public void OnCreate(GameObject obj)
        {
            if (obj != null && obj.IsValid)
            {
                if (UtilityClass.Traps.Keys.Contains(obj.Name))
                {
                    UtilityClass.ActualTraps.Add(obj.NetworkId, new Tuple<Vector3, float>(obj.Position, UtilityClass.Traps.First(t => t.Key == obj.Name).Value));
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
                if (UtilityClass.ActualTraps.Keys.Contains(obj.NetworkId))
                {
                    UtilityClass.ActualTraps.Remove(obj.NetworkId);
                }
            }
        }

        /// <summary>
        ///     Fired on spell cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseNewPathEventArgs" /> instance containing the event data.</param>
        public static void OnNewPath(Obj_AI_Base sender, Obj_AI_BaseNewPathEventArgs args)
        {
            if (args.IsDash)
            {
                if (UtilityClass.ActualTraps.Any(t => args.Path.First().Distance(t.Value.Item1) < t.Value.Item2))
                {
                    //args.
                }
            }
        }

        /// <summary>
        ///     Called on postattack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public static void OnPostAttack(object sender, PostAttackEventArgs args)
        {
            if (MenuClass.General["hydrareset"].Enabled)
            {
                if (UtilityClass.Player.HasItem(ItemId.Tiamat))
                {
                    var tiamat = UtilityClass.Player.Inventory.Slots.First(s => s.ItemId == ItemId.Tiamat);
                    if (tiamat != null)
                    {
                        //UtilityClass.Player.SpellBook.CastSpell(tiamat);
                    }
                }
            }
        }

        /// <summary>
        ///     Called on preattack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
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