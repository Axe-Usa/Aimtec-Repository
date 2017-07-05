
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
        #region Fields

        /// <summary>
        ///     Returns the MineField position.
        /// </summary>
        public Dictionary<int, Vector3> MineField = new Dictionary<int, Vector3>();

        /// <summary>
        ///     Returns the WorkedGrounds position.
        /// </summary>
        public Dictionary<int, Vector3> WorkedGrounds = new Dictionary<int, Vector3>();

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Returns true if there are any worked grounds in a determined range from the player.
        /// </summary>
        public bool AnyTerrainInRange(float range)
        {
            return this.CountTerrainsInRange(range) > 0;
        }

        /// <summary>
        ///     Returns the number of worked grounds in a determined range from the player.
        /// </summary>
        public int CountTerrainsInRange(float range)
        {
            return this.WorkedGrounds.Count(o => o.Value.Distance(UtilityClass.Player.Position) <= range);
        }

        /// <summary>
        ///     Returns true if the player is near a worked ground.
        /// </summary>
        public bool IsNearWorkedGround()
        {
            return this.AnyTerrainInRange(412.5f);
        }

        #endregion
    }
}