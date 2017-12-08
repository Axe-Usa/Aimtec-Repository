
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Damage.JSON;
using Aimtec.SDK.Extensions;
using AIO.Utilities;

namespace AIO.Champions
{
    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Vayne
    {
        /// <summary>
        ///     Returns wether or not a position is a perfect wall position
        /// </summary>
        bool IsPerfectWallPosition(Vector3 point, Obj_AI_Hero target, Vector3 startPos, float byHowMuch)
        {
            return point.Extend(startPos, -byHowMuch).IsWall(true) &&
                   point.Extend(startPos, -(byHowMuch + target.BoundingRadius)).IsWall(true);
        }
    }
}