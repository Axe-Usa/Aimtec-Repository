namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The settings class.
    /// </summary>
    internal partial class Jhin
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the spells.
        /// </summary>
        public void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 600f);
            SpellClass.W = new Spell(SpellSlot.W, 2500f);
            SpellClass.E = new Spell(SpellSlot.E, 750f);
            SpellClass.R = new Spell(SpellSlot.R, 3500f);

            SpellClass.W.SetSkillshot(0.75f, 40f, 5000f, false, SkillshotType.Line);
            SpellClass.E.SetSkillshot(1f, 260f, 1000f, false, SkillshotType.Circle);
            SpellClass.R.SetSkillshot(0.25f, 80f, 5000f, false, SkillshotType.Line);
        }

        #endregion
    }
}