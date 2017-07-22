namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spell class.
    /// </summary>
    internal partial class Taliyah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the spells.
        /// </summary>
        public void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 1000f);
            SpellClass.W = new Spell(SpellSlot.W, 900f);
            SpellClass.E = new Spell(SpellSlot.E, 800f);
            SpellClass.R = new Spell(SpellSlot.R, 1500 + 1500 * UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).Level);

            SpellClass.Q.SetSkillshot(0.275f, 100f, 3600f, false, SkillshotType.Line, false, HitChance.OutOfRange);
            SpellClass.W.SetSkillshot(1.25f, 200f, float.MaxValue, false, SkillshotType.Circle, true);
            SpellClass.E.SetSkillshot(0.30f, UtilityClass.GetAngleByDegrees(80f), float.MaxValue, false, SkillshotType.Cone, false, HitChance.OutOfRange);
        }

        #endregion
    }
}