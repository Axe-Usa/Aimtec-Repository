namespace AIO.Champions
{
    using System;

    using Aimtec;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The spell class.
    /// </summary>
    internal partial class Ashe
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the spells.
        /// </summary>
        public static void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q);
            SpellClass.W = new Spell(SpellSlot.W, UtilityClass.Player.BoundingRadius + 1200f);
            SpellClass.E = new Spell(SpellSlot.E, 2000f);
            SpellClass.R = new Spell(SpellSlot.R, 2000f);

            SpellClass.W.SetSkillshot(0.25f, (float)(67.5f * Math.PI / 180), 1500f, true, SkillType.Cone);
            SpellClass.E.SetSkillshot(0.25f, 130f, 1600f, false, SkillType.Line);
            SpellClass.R.SetSkillshot(0.25f, 130f, 1600f, false, SkillType.Line);
        }

        #endregion
    }
}