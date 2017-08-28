using System.Linq;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587
namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Kalista
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Lasthit()
        {
            /// <summary>
            ///     The E Lasthit Logics.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                if (Extensions.GetEnemyLaneMinionsTargetsInRange(SpellClass.E.Range)
                        .Count(m => IsPerfectRendTarget(m) && m.GetRealHealth() <= GetTotalRendDamage(m)) >= MenuClass.Spells["e"]["laneclear"].As<MenuSliderBool>().Value)
                {
                    SpellClass.E.Cast();
                }
            }
        }

        #endregion
    }
}