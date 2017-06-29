
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Vayne
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Combo()
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
                            t.IsValidTarget(SpellClass.E.Range + t.BoundingRadius) &&
                            !Invulnerable.Check(t, DamageType.Magical, false) &&
                            !t.IsValidTarget(UtilityClass.Player.BoundingRadius) &&
                            MenuClass.WhiteList[t.ChampionName.ToLower()].Enabled))
                {
                    for (var i = 1; i < 10; i++)
                    {
                        var playerPos = UtilityClass.Player.Position;
                        var predictedPos1 = SpellClass.E.GetPrediction(target).PredictedPosition;
                        var predictedPos2 = SpellClass.E2.GetPrediction(target).PredictedPosition;

                        var targetPosition = target.Position.Extend(playerPos, -41 * i);
                        var targetPositionExtended = target.Position.Extend(playerPos, -42 * i);

                        var predictedPosition1 = predictedPos1.Extend(playerPos, -41 * i);
                        var predictedPosition1Extended = predictedPos1.Extend(playerPos, -42 * i);

                        var predictedPosition2 = predictedPos2.Extend(playerPos, -41 * i);
                        var predictedPosition2Extended = predictedPos2.Extend(playerPos, -42 * i);

                        if (NavMesh.WorldToCell(targetPosition).Flags.HasFlag(NavCellFlags.Wall) &&
                            NavMesh.WorldToCell(targetPositionExtended).Flags.HasFlag(NavCellFlags.Wall) &&

                            NavMesh.WorldToCell(predictedPosition1).Flags.HasFlag(NavCellFlags.Wall) &&
                            NavMesh.WorldToCell(predictedPosition1Extended).Flags.HasFlag(NavCellFlags.Wall) &&

                            NavMesh.WorldToCell(predictedPosition2).Flags.HasFlag(NavCellFlags.Wall) &&
                            NavMesh.WorldToCell(predictedPosition2Extended).Flags.HasFlag(NavCellFlags.Wall))
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