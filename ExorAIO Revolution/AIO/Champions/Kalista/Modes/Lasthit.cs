#pragma warning disable 1587
namespace AIO.Champions
{
    using System.Linq;

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
        public void Lasthit()
        {
            /// <summary>
            ///     The E Lasthit Logics.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["laneclear"].As<MenuBool>().Enabled)
            {
                if (Extensions.GetEnemyLaneMinionsTargetsInRange(SpellClass.E.Range).Any(m => this.IsPerfectRendTarget(m) && m.Health <= this.GetTotalRendDamage(m)))
                {
                    SpellClass.E.Cast();
                }
            }
        }

        #endregion
    }
}