namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spells class.
    /// </summary>
    internal partial class KogMaw
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 1175f);
            SpellClass.W = new Spell(SpellSlot.W, UtilityClass.Player.AttackRange + (60f + 30f * UtilityClass.Player.SpellBook.GetSpell(SpellSlot.W).Level));
            SpellClass.E = new Spell(SpellSlot.E, 1280f);
            SpellClass.R = new Spell(SpellSlot.R, 900f + 300f * UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).Level);

            SpellClass.Q.SetSkillshot(0.25f, 70f, 1650f, true, SkillshotType.Line, false, HitChance.Medium);
            SpellClass.E.SetSkillshot(0.25f, 120f, 1350f, false, SkillshotType.Line, false, HitChance.Low);
            SpellClass.R.SetSkillshot(1.5f, 225f, float.MaxValue, false, SkillshotType.Circle, false, HitChance.Medium);
        }

        #endregion
    }
}