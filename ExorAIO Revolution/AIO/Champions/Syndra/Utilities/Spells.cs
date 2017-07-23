namespace AIO.Champions
{
    using Aimtec;

    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spells class.
    /// </summary>
    internal partial class Syndra
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 800f);
            SpellClass.W = new Spell(SpellSlot.W, 950f);
            SpellClass.E = new Spell(SpellSlot.E, 700f);
            SpellClass.R = new Spell(SpellSlot.R, UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).Level < 5 ? 675f : 750f + UtilityClass.Player.BoundingRadius);

            SpellClass.Q.SetSkillshot(1f, 200f, float.MaxValue, false, SkillshotType.Circle, false, HitChance.OutOfRange);
            SpellClass.W.SetSkillshot(0.5f, 225f, 1600f, false, SkillshotType.Circle, false, HitChance.OutOfRange);
            SpellClass.E.SetSkillshot(0.25f, UtilityClass.GetAngleByDegrees(UtilityClass.Player.SpellBook.GetSpell(SpellSlot.E).Level < 5 ? 40 : 60), 2500f, false, SkillshotType.Cone, false, HitChance.OutOfRange);
        }

        #endregion
    }
}