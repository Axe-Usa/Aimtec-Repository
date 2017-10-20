
using Aimtec;
using Aimtec.SDK.Extensions;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Vayne
    {
        #region Fields

        /// <summary>
        ///     Returns true if the wall width is good (>= threshold).
        /// </summary>
        public static bool IsGoodWallWallWidth(Vector3 start, Vector3 direction, int minWallWidth = 60)
        {
            var thickness = 0;

            for (var i = 0; i < minWallWidth; i+=5)
            {
                if (!start.Extend(direction, i).IsWall(true))
                {
                    break;
                }

                if (thickness < minWallWidth)
                {
                    thickness = i;
                }
            }

            return thickness >= minWallWidth;
        }

        #endregion
    }
}