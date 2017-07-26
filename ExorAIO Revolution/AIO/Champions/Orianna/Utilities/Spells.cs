namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spell class.
    /// </summary>
    internal partial class Orianna
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 825f);
            SpellClass.W = new Spell(SpellSlot.W, float.MaxValue);
            SpellClass.E = new Spell(SpellSlot.E, 1100f);
            SpellClass.R = new Spell(SpellSlot.R, float.MaxValue);

            SpellClass.Q.SetSkillshot(0.80f, 175f, 1150f, false, SkillshotType.Line, false, HitChance.None);
            SpellClass.W.SetSkillshot(0f, 250f, float.MaxValue, false, SkillshotType.Circle, false, HitChance.None);
            SpellClass.E.SetSkillshot(0.25f, 175f, 2300f, false, SkillshotType.Line, false, HitChance.None);
            SpellClass.R.SetSkillshot(0.75f, 325f, float.MaxValue, false, SkillshotType.Circle, false, HitChance.None);
        }

        #endregion
    }
}