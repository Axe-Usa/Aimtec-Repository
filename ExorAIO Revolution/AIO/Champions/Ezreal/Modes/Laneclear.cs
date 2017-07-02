
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ezreal
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Laneclear()
        {
            /// <summary>
            ///     The Laneclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["laneclear"])
                && MenuClass.Spells["q"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                var minionTargets = UtilityClass.GetEnemyLaneMinionsTargetsInRange(SpellClass.Q.Range).Where(m => m.GetRealHealth() < ObjectManager.GetLocalPlayer().GetSpellDamage(m, SpellSlot.Q));
                var objAiMinions = minionTargets as Obj_AI_Minion[] ?? minionTargets.ToArray();
                if (objAiMinions.Any())
                {
                    SpellClass.Q.Cast(objAiMinions.FirstOrDefault());
                }
            }
        }

        #endregion
    }
}