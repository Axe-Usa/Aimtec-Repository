
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
    ///     The logics class.
    /// </summary>
    internal partial class Evelynn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Lasthit()
        {
            /// <summary>
            ///     The LastHit Q Out of AA Range Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["lasthitaa"]) &&
                MenuClass.Spells["q"]["lasthitaa"].As<MenuSliderBool>().Enabled)
            {
                var bestMinionTarget = Extensions.GetEnemyLaneMinionsTargets().FirstOrDefault(m => m.GetRealHealth() <= UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q));
                if (bestMinionTarget != null &&
                    bestMinionTarget.IsValidTarget(SpellClass.Q.Range) &&
                    !bestMinionTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestMinionTarget)))
                {
                    SpellClass.Q.CastOnUnit(bestMinionTarget);
                }
            }
        }

        #endregion
    }
}