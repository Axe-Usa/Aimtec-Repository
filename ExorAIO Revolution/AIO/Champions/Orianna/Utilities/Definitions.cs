
#pragma warning disable 1587

namespace AIO.Champions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;

    using Utilities;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class Orianna
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The default position of the ball.
        /// </summary>
        public static Vector3 BallPosition = UtilityClass.Player.Position;

        /// <summary>
        ///     Gets the position of the ball.
        /// </summary>
        public static Vector3 GetBallPosition()
        {
            var possiblePosition1 = GameObjects.AllyMinions.FirstOrDefault(m => Math.Abs(m.Health) > 0 && m.UnitSkinName.Equals("oriannaball"));
            var possiblePosition2 = GameObjects.AllyHeroes.FirstOrDefault(a => a.Buffs.Any(b => b.Caster.IsMe && b.Name.Equals("orianaghost")));

            if (possiblePosition1 != null)
            {
                return possiblePosition1.Position;
            }

            if (possiblePosition2 != null)
            {
                return possiblePosition2.Position;
            }

            return UtilityClass.Player.Position;
        }

        /// <summary>
        ///     Updates the position of the ball.
        /// </summary>
        public static void UpdateBallPosition()
        {
            BallPosition = GetBallPosition();
        }

        #endregion
    }
}
