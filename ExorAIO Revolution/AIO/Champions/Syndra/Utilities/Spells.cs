using Aimtec;
using Aimtec.SDK.Prediction.Skillshots;
using AIO.Utilities;
using Spell = Aimtec.SDK.Spell;

namespace AIO.Champions
{
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
            SpellClass.E = new Spell(SpellSlot.E, 950f);
            SpellClass.R = new Spell(SpellSlot.R, 675f + UtilityClass.Player.BoundingRadius);

            SpellClass.Q.SetSkillshot(0.6f, 150f, 1000f, false, SkillshotType.Circle, false, HitChance.None);
            SpellClass.W.SetSkillshot(0.25f, 210f, 1450f, false, SkillshotType.Circle, false, HitChance.None);
            SpellClass.E.SetSkillshot(0f, UtilityClass.GetAngleByDegrees(40), 2000f, false, SkillshotType.Cone, false, HitChance.None);
        }

        #endregion
    }
}