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
    internal partial class Lucian
    {
        #region Fields

        /// <summary>
        ///     The Q Rectangle.
        /// </summary>
        public Geometry.Rectangle QRectangle(Obj_AI_Base unit)
        {
            return new Geometry.Rectangle(
                UtilityClass.Player.ServerPosition,
                UtilityClass.Player.ServerPosition.Extend(unit.ServerPosition, SpellClass.Q2.Range - 100f),
                SpellClass.Q2.Width);
        }

        #endregion
    }
}