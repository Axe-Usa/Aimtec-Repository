
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
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
        ///     Called on tick update.
        /// </summary>
        public void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                GameObjects.EnemyHeroes.Count(
                    t =>
                        !Invulnerable.Check(t, DamageType.Magical) &&
                        t.IsValidTarget(SpellClass.R.Width - t.BoundingRadius, false, this.BallPosition)) >=
                MenuClass.Spells["r"]["aoe"].As<MenuSliderBool>().Value &&
                MenuClass.Spells["r"]["aoe"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.R.Cast();
            }
        }

        #endregion
    }
}