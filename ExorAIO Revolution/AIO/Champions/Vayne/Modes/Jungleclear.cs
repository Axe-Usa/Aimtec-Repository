
#pragma warning disable 1587
namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Vayne
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on post attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public void Jungleclear(object sender, PostAttackEventArgs args)
        {
            var jungleTarget = args.Target as Obj_AI_Minion;
            if (jungleTarget == null ||
                !Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget) ||
                jungleTarget.Health < UtilityClass.Player.GetAutoAttackDamage(jungleTarget) * 2)
            {
                return;
            }

            /// <summary>
            ///     The E Jungleclear Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["jungleclear"]) &&
                MenuClass.Spells["e"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                if (!Extensions.GetLegendaryJungleMinionsTargets().Contains(jungleTarget))
                {
                    for (var i = 1; i < 10; i++)
                    {
                        var playerPos = UtilityClass.Player.Position;
                        var predictedPos1 = SpellClass.E.GetPrediction(jungleTarget).UnitPosition;
                        var predictedPos2 = SpellClass.E2.GetPrediction(jungleTarget).UnitPosition;

                        var targetPosition = jungleTarget.Position.Extend(playerPos, -40 * i);
                        var targetPositionExtended = jungleTarget.Position.Extend(playerPos, -41 * i);

                        var UnitPosition1 = predictedPos1.Extend(playerPos, -40 * i);
                        var UnitPosition1Extended = predictedPos1.Extend(playerPos, -41 * i);

                        var UnitPosition2 = predictedPos2.Extend(playerPos, -40 * i);
                        var UnitPosition2Extended = predictedPos2.Extend(playerPos, -41 * i);

                        if (NavMesh.WorldToCell(targetPosition).Flags.HasFlag(NavCellFlags.Wall) &&
                            NavMesh.WorldToCell(targetPositionExtended).Flags.HasFlag(NavCellFlags.Wall) &&
                            NavMesh.WorldToCell(UnitPosition1).Flags.HasFlag(NavCellFlags.Wall) &&
                            NavMesh.WorldToCell(UnitPosition1Extended).Flags.HasFlag(NavCellFlags.Wall) &&
                            NavMesh.WorldToCell(UnitPosition2).Flags.HasFlag(NavCellFlags.Wall) &&
                            NavMesh.WorldToCell(UnitPosition2Extended).Flags.HasFlag(NavCellFlags.Wall))
                        {
                            SpellClass.E.CastOnUnit(jungleTarget);
                        }
                    }
                }
            }

            /// <summary>
            ///     The Q Jungleclear Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.Cast(Game.CursorPos);
            }
        }

        #endregion
    }
}