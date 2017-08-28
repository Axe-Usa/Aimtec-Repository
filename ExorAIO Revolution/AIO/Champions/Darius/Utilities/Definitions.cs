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
    internal partial class Darius
    {
        #region Fields

        /// <summary>
        ///     Returns true if the target is a perfectly valid blade target.
        /// </summary>
        public bool IsValidBladeTarget(Obj_AI_Base unit)
        {
            var unitDistanceToPlayer = unit.Distance(UtilityClass.Player);
            return
                unit.IsValidSpellTarget() &&
                unitDistanceToPlayer >= 205 &&
                unitDistanceToPlayer <= SpellClass.Q.Range;
        }

        #endregion
    }
}