// ReSharper disable ArrangeMethodOrOperatorBody


using Aimtec;
using Aimtec.SDK.Extensions;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class KogMaw
    {
        #region Fields

        /// <summary>
        ///     Returns true if the player is using BioArcaneBarrage, else, false;
        /// </summary>
        public bool IsUsingBioArcaneBarrage()
        {
            return UtilityClass.Player.HasBuff("KogMawBioArcaneBarrage");
        }

        #endregion
    }
}