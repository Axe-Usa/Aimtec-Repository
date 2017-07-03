
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;

    using AIO.Utilities;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class Taliyah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Returns the WorkedGrounds position.
        /// </summary>
        public static Dictionary<int, Vector3> WorkedGrounds = new Dictionary<int, Vector3>();

        /// <summary>
        ///     Returns the MineField position.
        /// </summary>
        public static Dictionary<int, Vector3> MineField = new Dictionary<int, Vector3>();

        /// <summary>
        ///     Returns the number of worked grounds in a determined range from the player.
        /// </summary>
        public static int CountTerrainsInRange(float range)
        {
            return WorkedGrounds.Count(o => o.Value.Distance(UtilityClass.Player.Position) <= range);
        }

        /// <summary>
        ///     Returns true if there are any worked grounds in a determined range from the player.
        /// </summary>
        public static bool AnyTerrainInRange(float range)
        {
            return CountTerrainsInRange(range) > 0;
        }

        /// <summary>
        ///     Returns true if the player is near a worked ground.
        /// </summary>
        public static bool IsNearWorkedGround()
        {
            return AnyTerrainInRange(412.5f);
        }

        #endregion
    }
}