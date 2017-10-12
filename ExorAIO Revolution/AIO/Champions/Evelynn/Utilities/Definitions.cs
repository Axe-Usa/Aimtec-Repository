
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Evelynn
    {
        /// <summary>
        ///     Returns true if the target is Allured.
        /// </summary>
        public bool IsAllured(Obj_AI_Base unit)
        {
            return unit.HasBuff("EvelynnW");
        }

        /// <summary>
        ///     Returns true if the target is fully Allured.
        /// </summary>
        public bool IsFullyAllured(Obj_AI_Base unit)
        {
            if (unit.HasBuff("EvelynnW"))
            {
                var normalObjects = ObjectManager.Get<GameObject>().Where(o => o.IsValid && o.Name == "Evelynn_Base_W_Fizz_Mark_Decay");
                return normalObjects.Any(o => ObjectManager.Get<Obj_AI_Base>().Where(t => t.Team != o.Team).MinBy(t => t.Distance(o)) == unit);
            }

            return false;
        }

        /// <summary>
        ///     Returns the real range of the Q spell.
        /// </summary>
        public float GetRealQRange()
        {
            return IsHateSpikeSkillshot() ? SpellClass.Q.Range : SpellClass.Q2.Range;
        }

        /// <summary>
        ///     Returns true if the E is in the Empowered state.
        /// </summary>
        public bool IsWhiplashEmpowered()
        {
            return UtilityClass.Player.GetSpell(SpellSlot.E).ToggleState == 2;
        }

        /// <summary>
        ///     Returns true if the Q is in the Empowered state.
        /// </summary>
        public bool IsHateSpikeSkillshot()
        {
            return UtilityClass.Player.GetSpell(SpellSlot.Q).ToggleState == 1;
        }
    }
}