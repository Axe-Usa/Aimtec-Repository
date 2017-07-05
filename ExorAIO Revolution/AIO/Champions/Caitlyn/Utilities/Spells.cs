namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The settings class.
    /// </summary>
    internal partial class Caitlyn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the spells.
        /// </summary>
        public void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 1250f);
            SpellClass.Q2 = new Spell(SpellSlot.Q, 1250f);
            SpellClass.W = new Spell(SpellSlot.W, 800f);
            SpellClass.E = new Spell(SpellSlot.E, 950f);
            SpellClass.R = new Spell(SpellSlot.R, 1500f + 500f * UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).Level);

            SpellClass.Q.SetSkillshot(0.65f, 60f, 2200f, false, SkillType.Line);
            SpellClass.Q2.SetSkillshot(0.65f, 120f, 2200f, false, SkillType.Line);
            SpellClass.W.SetSkillshot(1.5f, 67.5f, float.MaxValue, false, SkillType.Circle);
            SpellClass.E.SetSkillshot(0.30f, 70f, 2000f, true, SkillType.Line);
        }

        #endregion
    }
}