
using System.Drawing;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class Vayne
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the menus.
        /// </summary>
        public void Drawings()
        {
            /// <summary>
            ///     Loads the Q drawing.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Drawings["q"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.Q.Range, 30, Color.LightGreen);
            }

            /// <summary>
            ///     Loads the E drawings.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                /// <summary>
                ///     Loads the E range drawing.
                /// </summary>
                if (MenuClass.Drawings["e"].As<MenuBool>().Enabled)
                {
                    Render.Circle(UtilityClass.Player.Position, SpellClass.E.Range, 30, Color.Cyan);
                }

                /// <summary>
                ///     Loads the E rectangles drawing.
                /// </summary>
                if (MenuClass.Drawings["epred"].As<MenuBool>().Enabled)
                {
                    var playerPos = UtilityClass.Player.Position;
                    const int condemnPushDistance = 410;
                    const int threshold = 60;

                    foreach (var target in GameObjects.EnemyHeroes.Where(t => t.IsValidTarget(SpellClass.E.Range)))
                    {
                        var targetPos = target.Position;
                        var targetRadius = target.BoundingRadius;
                        const int checkedPoints = 10;

                        var posImpact = target.Position.Extend(playerPos, -(condemnPushDistance + threshold));
                        var posRectangle = new Vector3Geometry.Rectangle(targetPos, posImpact, targetRadius);
                        posRectangle.Draw(Bools.AnyWallInBetween(targetPos, posImpact)
                            ? Color.Blue
                            : Color.OrangeRed);

                        if (Bools.AnyWallInBetween(targetPos, posImpact))
                        {
                            for (var i = 1; i < checkedPoints; i++)
                            {
                                var posImpactIter = target.Position.Extend(playerPos, -condemnPushDistance / checkedPoints * i);
                                var wallPosRectangle = new Vector3Geometry.Rectangle(targetPos, posImpactIter, targetRadius);
                                wallPosRectangle.Draw(posImpactIter.IsWall(true)
                                    ? Color.Blue
                                    : Color.OrangeRed);

                                var posImpactIterTh = target.Position.Extend(playerPos, -(condemnPushDistance + threshold) / checkedPoints * i);
                                var wallPosRectangleTh = new Vector3Geometry.Rectangle(targetPos, posImpactIterTh, targetRadius);
                                wallPosRectangleTh.Draw(posImpactIterTh.IsWall(true)
                                    ? Color.Blue
                                    : Color.OrangeRed);
                            }
                        }

                        if (MenuClass.Spells["e"]["emode"].As<MenuList>().Value == 0)
                        {
                            var predImpact = SpellClass.E.GetPrediction(target).CastPosition.Extend(playerPos, -(condemnPushDistance + threshold));
                            var predRectangle = new Vector3Geometry.Rectangle(targetPos, predImpact, targetRadius);
                            predRectangle.Draw(Bools.AnyWallInBetween(targetPos, predImpact)
                                ? Color.Blue
                                : Color.OrangeRed);

                            if (Bools.AnyWallInBetween(targetPos, predImpact))
                            {
                                for (var i = 1; i < checkedPoints; i++)
                                {
                                    var predImpactIter = SpellClass.E.GetPrediction(target).CastPosition.Extend(playerPos, -condemnPushDistance / checkedPoints * i);
                                    var wallPredRectangle = new Vector3Geometry.Rectangle(targetPos, predImpactIter, targetRadius);
                                    wallPredRectangle.Draw(predImpactIter.IsWall(true)
                                        ? Color.Blue
                                        : Color.OrangeRed);

                                    var predImpactIterTh = SpellClass.E.GetPrediction(target).CastPosition.Extend(playerPos, -(condemnPushDistance + threshold) / checkedPoints * i);
                                    var wallPredRectangleTh = new Vector3Geometry.Rectangle(targetPos, predImpactIterTh, targetRadius);
                                    wallPredRectangleTh.Draw(predImpactIterTh.IsWall(true)
                                        ? Color.Blue
                                        : Color.OrangeRed);
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}