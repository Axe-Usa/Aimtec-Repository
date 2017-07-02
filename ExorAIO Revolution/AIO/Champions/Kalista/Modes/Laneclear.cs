
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Kalista
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Laneclear()
        {
            /// <summary>
            ///     The Q Laneclear Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["laneclear"]) &&
                MenuClass.Spells["q"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                /*
                var farmLocation = UtilityClass.GetAllGenericMinionsTargets().Where(m => m.Health < (float)UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q)).ToList();
                if (SpellClass.Q.GetLineFarmLocation(farmLocation, SpellClass.Q.Width).MinionsHit >= 3)
                {
                    SpellClass.Q.Cast(SpellClass.Q.GetLineFarmLocation(farmLocation, SpellClass.Q.Width).Position);
                }
                */
            }

            /// <summary>
            ///     The E Laneclear Logics.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["laneclear"].As<MenuBool>().Enabled)
            {
                if (UtilityClass.GetEnemyLaneMinionsTargets().Any(m => IsPerfectRendTarget(m) && m.GetRealHealth() < GetTotalRendDamage(m)))
                {
                    SpellClass.E.Cast();
                }
            }
        }

        #endregion
    }
}