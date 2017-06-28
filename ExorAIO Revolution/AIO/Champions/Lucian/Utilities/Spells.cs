namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spells class.
    /// </summary>
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, UtilityClass.Player.BoundingRadius * 4 + 500f);
            SpellClass.Q2 = new Spell(SpellSlot.Q, SpellClass.Q.Range + 400f);
            SpellClass.W = new Spell(SpellSlot.W, 900f);
            SpellClass.E = new Spell(SpellSlot.E, UtilityClass.Player.AttackRange + 425f);
            SpellClass.R = new Spell(SpellSlot.R, 1150f);

            SpellClass.Q2.SetSkillshot(0.25f, 65f, float.MaxValue, false, SkillType.Line);
            SpellClass.W.SetSkillshot(0.30f, 80f, 1600f, true, SkillType.Line);
            SpellClass.R.SetSkillshot(0.25f, 110f, 2500f, true, SkillType.Line);
        }

        #endregion
    }
}