// ReSharper disable ArrangeMethodOrOperatorBody


#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Damage.JSON;
    using Aimtec.SDK.Extensions;

    using AIO.Utilities;

    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Corki
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Gets the total missile damage on a determined unit.
        /// </summary>
        public double GetMissileDamage(Obj_AI_Base unit)
        {
            var player = UtilityClass.Player;
            return this.HasBigOne()
                       ? player.GetSpellDamage(unit, SpellSlot.R, DamageStage.SecondForm)
                       : player.GetSpellDamage(unit, SpellSlot.R);
        }

        /// <summary>
        ///     returns true if the player has the BigOne, else false.
        /// </summary>
        public bool HasBigOne()
        {
            return UtilityClass.Player.HasBuff("corkimissilebarragecounterbig");
        }

        #endregion
    }
}