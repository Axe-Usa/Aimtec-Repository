
using System.Linq;
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ezreal
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Laneclear()
        {
            /// <summary>
            ///     The Laneclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["laneclear"])
                && MenuClass.Spells["q"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                var minionTargets = Extensions.GetEnemyLaneMinionsTargetsInRange(SpellClass.Q.Range).Where(m => m.GetRealHealth() < UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q));
                var objAiMinions = minionTargets as Obj_AI_Minion[] ?? minionTargets.ToArray();
                if (objAiMinions.Any(m => m.GetRealHealth() > UtilityClass.Player.GetAutoAttackDamage(m) || !m.IsValidTarget(UtilityClass.Player.AttackRange)))
                {
                    SpellClass.Q.Cast(objAiMinions.FirstOrDefault());
                }
            }
        }

        #endregion
    }
}