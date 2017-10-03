
using System.Drawing;
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
                    const int condemnDistancePush = 410;
                    foreach (var target in Extensions.GetBestEnemyHeroesTargetsInRange(SpellClass.E.Range))
                    {
                        var targetPos = target.Position;
                        var targetRadius = target.BoundingRadius;

                        var posImpact = target.Position.Extend(playerPos, -condemnDistancePush);
                        var posRectangle = new Geometry.Rectangle(targetPos, posImpact, targetRadius);

                        var predImpact = SpellClass.E.GetPrediction(target).CastPosition.Extend(playerPos, -condemnDistancePush);
                        var predRectangle = new Geometry.Rectangle(targetPos, predImpact, targetRadius);

                        posRectangle.Draw(Bools.AnyWallInBetween(targetPos, posImpact) ? Color.Blue : Color.OrangeRed);
                        predRectangle.Draw(Bools.AnyWallInBetween(targetPos, predImpact) ? Color.Blue : Color.OrangeRed);
                    }
                }
            }
        }

        #endregion
    }
}