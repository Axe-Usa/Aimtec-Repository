namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spells class.
    /// </summary>
    internal partial class Jinx
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 525f + UtilityClass.Player.BoundingRadius);
            SpellClass.Q2 = new Spell(SpellSlot.Q, SpellClass.Q.Range + 50f + 25f * UtilityClass.Player.SpellBook.GetSpell(SpellSlot.Q).Level);
            SpellClass.W = new Spell(SpellSlot.W, 1450f);
            SpellClass.E = new Spell(SpellSlot.E, 900f);
            SpellClass.R = new Spell(SpellSlot.R, 1500f);

            SpellClass.W.SetSkillshot(0.6f, 85f, 3200f, true, SkillshotType.Line);
            SpellClass.E.SetSkillshot(1f, 100f, 1000f, false, SkillshotType.Circle, false, HitChance.Low);
            SpellClass.R.SetSkillshot(0.6f, 140f, 1700f, false, SkillshotType.Line, false, HitChance.Low);
        }

        #endregion
    }
}