namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Orbwalking;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    using Spell = Aimtec.SDK.Spell;

    /// <summary>
    ///     The settings class.
    /// </summary>
    internal partial class Twitch
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the spells.
        /// </summary>
        public static void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q);
            SpellClass.W = new Spell(SpellSlot.W, 950f);
            SpellClass.E = new Spell(SpellSlot.E, 1200f);

            var target = Orbwalker.Implementation.GetTarget();
            SpellClass.R = new Spell(SpellSlot.R, UtilityClass.Player.GetFullAttackRange(target != null ? target : null) + 300f);

            SpellClass.W.SetSkillshot(0.25f, 120f, 1400f, false, SkillType.Circle);
        }

        #endregion
    }
}