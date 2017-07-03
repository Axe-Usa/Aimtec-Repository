namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spells class.
    /// </summary>
    internal partial class Ezreal
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 1150f);
            SpellClass.W = new Spell(SpellSlot.W, 1000f);
            SpellClass.E = new Spell(SpellSlot.E, 475f);
            SpellClass.R = new Spell(SpellSlot.R, 1500f);

            SpellClass.Q.SetSkillshot(0.25f, 60f, 2000f, true, SkillType.Line);
            SpellClass.W.SetSkillshot(0.25f, 80f, 1600f, false, SkillType.Line);
            SpellClass.E.SetSkillshot(0.75f, 60f, 1000f, false, SkillType.Circle);
            SpellClass.R.SetSkillshot(1.1f, 160f, 2000f, false, SkillType.Line);
        }

        #endregion
    }
}