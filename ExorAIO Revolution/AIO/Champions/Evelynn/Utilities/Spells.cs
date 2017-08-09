namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spell class.
    /// </summary>
    internal partial class Evelynn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public void Spells()
        {
            SpellClass.R = new Spell(SpellSlot.R, 650f);

            SpellClass.R.SetSkillshot(0.25f, 350f, 1000f, true, SkillshotType.Circle);
        }

        #endregion
    }
}