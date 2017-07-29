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

            SpellClass.W.SetSkillshot(0.75f, 60f, 1200f, true, SkillshotType.Line, false, HitChance.Medium);
            SpellClass.E.SetSkillshot(0.95f, 325f, 1750f, false, SkillshotType.Circle);
            SpellClass.R.SetSkillshot(1.1f, 140f, 2500f, false, SkillshotType.Line, false, HitChance.None);
        }

        #endregion
    }
}