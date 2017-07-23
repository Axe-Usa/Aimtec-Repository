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
    internal partial class Orianna
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Lasthit()
        {
            /// <summary>
            ///     The Q Farmhelper Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellSlot.Q, MenuClass.Spells["q"]["farmhelper"]) &&
                MenuClass.Spells["q"]["farmhelper"].As<MenuSliderBool>().Enabled)
            {
                foreach (var minion in Extensions.GetEnemyLaneMinionsTargetsInRange(SpellClass.Q.Range).Where(m => !m.IsValidTarget(UtilityClass.Player.GetFullAttackRange(m))))
                {
                    if (minion.Health < UtilityClass.Player.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        SpellClass.Q.Cast(minion);
                    }
                }
            }
        }

        #endregion
    }
}