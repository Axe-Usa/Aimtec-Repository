// ReSharper disable ArrangeMethodOrOperatorBody


using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Damage.JSON;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.TargetSelector;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Varus
    {
        #region Fields

        /// <summary>
        ///     Returns Q Charging Logic;
        /// </summary>
        public static void PiercingArrowLogicalCast(Obj_AI_Base target)
        {
            if (!SpellClass.Q.IsCharging &&
                target.IsValidTarget(SpellClass.Q.ChargedMaxRange))
            {
                SpellClass.Q.StartCharging(SpellClass.Q.GetPrediction(target).CastPosition);
            }
            else if (target.IsValidTarget(SpellClass.Q.Range))
            {
                SpellClass.Q.Cast(target);
            }
        }

        /// <summary>
        ///     Returns the real Q Damage;
        /// </summary>
        public static double GetRealPiercingArrowDamage(Obj_AI_Base target)
        {
            return
                UtilityClass.Player.GetSpellDamage(target, SpellSlot.Q) +
                UtilityClass.Player.GetSpellDamage(target, SpellSlot.Q, DamageStage.Buff);
        }

        /// <summary>
        ///     Returns the real E Damage;
        /// </summary>
        public static double GetRealHailOfArrowsDamage(Obj_AI_Base target)
        {
            return
                UtilityClass.Player.GetSpellDamage(target, SpellSlot.E) +
                UtilityClass.Player.GetSpellDamage(target, SpellSlot.E, DamageStage.Buff);
        }

        /// <summary>
        ///     Returns the real R Damage;
        /// </summary>
        public static double GetRealChainOfCorruptionDamage(Obj_AI_Base target)
        {
            return
                UtilityClass.Player.GetSpellDamage(target, SpellSlot.R) +
                UtilityClass.Player.GetSpellDamage(target, SpellSlot.R, DamageStage.Buff);
        }

        /// <summary>
        ///     Returns the number of Blight Stacks a determined target has;
        /// </summary>
        public int GetBlightStacks(Obj_AI_Base target)
        {
            return target.GetRealBuffCount("VarusWDebuff");
        }

        #endregion
    }
}