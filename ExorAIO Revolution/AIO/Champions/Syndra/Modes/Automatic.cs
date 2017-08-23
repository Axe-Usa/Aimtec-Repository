
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Syndra
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Automatic()
        {
            SpellClass.E.Width = UtilityClass.GetAngleByDegrees(UtilityClass.Player.SpellBook.GetSpell(SpellSlot.E).Level < 5 ? 40 : 60);
            SpellClass.R.Range = UtilityClass.Player.BoundingRadius + UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).Level < 3 ? 675f : 750f;
        }

        #endregion
    }
}