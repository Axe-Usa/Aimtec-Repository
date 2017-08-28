
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Damage.JSON;
using Aimtec.SDK.Extensions;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Tristana
    {
        #region Fields

        /// <summary>
        ///     Returns true if the target is affected by the E charge, else, false.
        /// </summary>
        public bool IsCharged(Obj_AI_Base unit)
        {
            return unit.HasBuff("TristanaECharge");
        }

        /// <summary>
        ///     Gets the total explosion damage on a determined unit.
        /// </summary>
        public double GetTotalExplosionDamage(Obj_AI_Base unit)
        {
            var player = UtilityClass.Player;
            return player.GetSpellDamage(unit, SpellSlot.E) +
                   player.GetSpellDamage(unit, SpellSlot.E, DamageStage.Buff);
        }

        #endregion
    }
}