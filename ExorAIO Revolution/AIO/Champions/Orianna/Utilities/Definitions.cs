
#pragma warning disable 1587

namespace AIO.Champions
{
    using System;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;

    using AIO.Utilities;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class Orianna
    {
        #region Fields

        /// <summary>
        ///     The default position of the ball.
        /// </summary>
        public Vector3? BallPosition;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Gets the position of the ball.
        /// </summary>
        public Vector3? GetBallPosition()
        {
            var possiblePosition1 = GameObjects.AllyMinions.FirstOrDefault(m =>
                    Math.Abs(m.Health) > 0 &&
                    m.UnitSkinName.Equals("OriannaBall"));
            if (possiblePosition1 != null)
            {
                return possiblePosition1.ServerPosition;
            }

            var possiblePosition2 = GameObjects.AllyHeroes.FirstOrDefault(a =>
                    !a.IsMe &&
                    a.Buffs.Any(b =>
                        b.IsValid && b.IsActive &&
                        b.Caster != null && b.Caster.IsMe &&
                        b.Name.Equals("orianaghost")));
            if (possiblePosition2 != null)
            {
                return possiblePosition2.ServerPosition;
            }

            if (UtilityClass.Player.HasBuff("orianaghostself"))
            {
                return UtilityClass.Player.ServerPosition;
            }

            return null;
        }

        /// <summary>
        ///     Updates the position of the ball.
        /// </summary>
        public void UpdateBallPosition()
        {
            this.BallPosition = this.GetBallPosition() != null
                ? this.GetBallPosition()
                : null;
        }

        #endregion
    }
}