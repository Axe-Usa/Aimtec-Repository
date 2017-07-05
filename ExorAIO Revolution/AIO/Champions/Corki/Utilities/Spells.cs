namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spell class.
    /// </summary>
    internal partial class Corki
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 825f);
            SpellClass.E = new Spell(SpellSlot.E, 600f + UtilityClass.Player.BoundingRadius);
            SpellClass.R = new Spell(SpellSlot.R, 1250f);
            SpellClass.R2 = new Spell(SpellSlot.R, 1250f);

            SpellClass.Q.SetSkillshot(0.55f, 250f, 1000f, false, SkillType.Circle);
            SpellClass.E.SetSkillshot(0.3f, UtilityClass.GetAngleByDegrees(35f), 1500f, false, SkillType.Cone);
            SpellClass.R.SetSkillshot(0.25f, 75f, 2000f, true, SkillType.Line);
            SpellClass.R2.SetSkillshot(0.25f, 150f, 2000f, true, SkillType.Line);
        }

        #endregion
    }
}