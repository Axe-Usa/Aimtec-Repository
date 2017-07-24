// ReSharper disable ArrangeMethodOrOperatorBody


#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec.SDK.Extensions;

    using AIO.Utilities;

    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Jinx
    {
        #region Fields

        /// <summary>
        ///     Returns true if the player is using Fishbones, else, false;
        /// </summary>
        public bool IsUsingFishBones()
        {
            return UtilityClass.Player.HasBuff("jinxq");
        }

        #endregion
    }
}