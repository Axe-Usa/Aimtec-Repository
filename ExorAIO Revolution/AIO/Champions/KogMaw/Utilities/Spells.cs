namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Prediction.Skillshots;

    using Utilities;

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
        public static void Spells()
        {
            SpellClass.Q = new Spell(SpellSlot.Q, 1175f);

            var target = UtilityClass.Orbwalker.GetTarget();
            SpellClass.W = new Spell(SpellSlot.W, (target != null ? UtilityClass.Player.GetFullAttackRange(target) : UtilityClass.Player.AttackRange) + (60f + 30f * ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.W).Level));
            SpellClass.E = new Spell(SpellSlot.E, 1280f);
            SpellClass.R = new Spell(SpellSlot.R, 900f + 300f * ObjectManager.GetLocalPlayer().SpellBook.GetSpell(SpellSlot.R).Level);

            SpellClass.Q.SetSkillshot(0.25f, 70f, 1650f, true, SkillType.Line);
            SpellClass.E.SetSkillshot(0.25f, 120f, 1350f, false, SkillType.Line);
            SpellClass.R.SetSkillshot(1.2f, 120f, float.MaxValue, false, SkillType.Circle);
        }

        #endregion
    }
}