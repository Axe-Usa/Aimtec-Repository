namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spell class.
    /// </summary>
    internal partial class Anivia
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 1100f);
            SpellClass.W = new Spell(SpellSlot.W, 1000f);
            SpellClass.E = new Spell(SpellSlot.E, 650f);
            SpellClass.R = new Spell(SpellSlot.R, 625f);

            SpellClass.Q.SetSkillshot(0.25f, 110f, 850f, false, SkillshotType.Line);
            SpellClass.W.SetSkillshot(0.25f, 100f, 1600f, false, SkillshotType.Circle);
        }

        #endregion
    }
}