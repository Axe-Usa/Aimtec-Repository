
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
    internal partial class Xayah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range - 100f);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget) &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)))
                {
                    SpellClass.Q.Cast(bestTarget);
                }
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["combo"].As<MenuSliderBool>().Enabled)
            {
                if (GameObjects.EnemyHeroes.Any(t =>
                        this.IsPerfectFeatherTarget(t) &&
                        this.CountFeathersHitOnUnit(t) >= MenuClass.Spells["e"]["combo"].As<MenuSliderBool>().Value))
                {
                    SpellClass.E.Cast();
                }
            }
        }

        #endregion
    }
}