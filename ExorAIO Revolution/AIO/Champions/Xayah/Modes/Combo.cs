
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Xayah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                GameObjects.EnemyHeroes.Any(
                    t =>
                        this.IsPerfectFeatherTarget(t) &&
                        this.CountFeathersHitOnUnit(t) >= MenuClass.Spells["e"]["combo"].As<MenuSliderBool>().Value) &&
                MenuClass.Spells["e"]["combo"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.E.Cast();
            }
        }

        #endregion
    }
}