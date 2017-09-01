// ReSharper disable ArrangeMethodOrOperatorBody


using Aimtec.SDK.Extensions;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
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

        /// <summary>
        ///     Returns Fishbones' SplashRange.
        /// </summary>
        public const float SplashRange = 250f;

        #endregion
    }
}