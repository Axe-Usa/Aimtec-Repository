
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Vayne
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The E Stun Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                //!ObjectManager.Player.IsDashing() &&
                MenuClass.Spells["e"]["logical"].As<MenuBool>().Enabled)
            {
                foreach (var target in
                    GameObjects.EnemyHeroes.Where(
                        t =>
                            //!t.IsDashing() &&
                            !Invulnerable.Check(t, DamageType.Magical, false) &&
                            t.IsValidTarget(SpellClass.E.Range + t.BoundingRadius) &&
                            !t.IsValidTarget(UtilityClass.Player.BoundingRadius * 2) &&
                            MenuClass.Spells["e"]["whitelist"][t.ChampionName.ToLower()].Enabled))
                {
                    for (var i = 1; i < 10; i++)
                    {
                        var playerPos = UtilityClass.Player.Position;
                        var predictedPos1 = SpellClass.E.GetPrediction(target).UnitPosition;
                        var predictedPos2 = SpellClass.E2.GetPrediction(target).UnitPosition;

                        var targetPosition = target.Position.Extend(playerPos, -40 * i);
                        var targetPositionExtended = target.Position.Extend(playerPos, -41 * i);

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
                            SpellClass.E.CastOnUnit(target);
                        }
                    }
                }
            }
        }

        #endregion
    }
}