namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spell class.
    /// </summary>
    internal partial class Vayne
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, UtilityClass.Player.AttackRange + 300f);
            SpellClass.W = new Spell(SpellSlot.W);
            SpellClass.E = new Spell(SpellSlot.E, 550f + UtilityClass.Player.BoundingRadius);
            SpellClass.E2 = new Spell(SpellSlot.E, 550f + UtilityClass.Player.BoundingRadius);
            SpellClass.R = new Spell(SpellSlot.R);

            SpellClass.E.SetSkillshot(0.45f, 50f, 1000f, false, SkillType.Line);
            SpellClass.E2.SetSkillshot(0.65f, 50f, 1000f, false, SkillType.Line);
        }

        #endregion
    }
}