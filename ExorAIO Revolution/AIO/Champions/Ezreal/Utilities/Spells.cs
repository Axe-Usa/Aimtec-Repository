namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

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
        public void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 1150f);
            SpellClass.W = new Spell(SpellSlot.W, 1000f);
            SpellClass.E = new Spell(SpellSlot.E, 475f);
            SpellClass.R = new Spell(SpellSlot.R, 1500f);

            SpellClass.Q.SetSkillshot(0.5f, 80f, 1200f, true, SkillshotType.Line, false, HitChance.None);
            SpellClass.W.SetSkillshot(0.5f, 80f, 1200f, false, SkillshotType.Line, false, HitChance.None);
            SpellClass.R.SetSkillshot(1f, 160f, 2000f, false, SkillshotType.Line, false, HitChance.None);
        }

        #endregion
    }
}