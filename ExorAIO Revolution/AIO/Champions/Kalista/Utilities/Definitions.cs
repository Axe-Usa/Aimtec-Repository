// ReSharper disable ArrangeMethodOrOperatorBody


#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Collections.Generic;

    using Aimtec;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class Kalista
    {
        #region Fields

        /// <summary>
        ///     Gets or sets the SoulBound.
        /// </summary>
        public Obj_AI_Hero SoulBound;

        /// <summary>
        ///     Gets all the important jungle locations.
        /// </summary>
        internal readonly List<Vector3> Locations = new List<Vector3>
                                                        {
                                                            new Vector3(9827.56f, -71.2406f, 4426.136f),
                                                            new Vector3(4951.126f, -71.2406f, 10394.05f),
                                                            new Vector3(10998.14f, 51.72351f, 6954.169f),
                                                            new Vector3(7082.083f, 56.2041f, 10838.25f),
                                                            new Vector3(3804.958f, 52.11121f, 7875.456f),
                                                            new Vector3(7811.249f, 53.81299f, 4034.486f)
                                                        };

        #endregion
    }
}