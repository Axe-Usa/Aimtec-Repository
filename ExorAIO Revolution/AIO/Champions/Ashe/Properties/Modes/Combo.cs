
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Ashe
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Combo()
        {
            var heroTarget = UtilityClass.GetBestEnemyHeroTarget();
            if (!heroTarget.IsValidTarget() ||
                Invulnerable.Check(heroTarget, DamageType.Physical))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                !heroTarget.IsValidTarget(UtilityClass.Player.AttackRange) &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
            {
                SpellClass.W.Cast(heroTarget);
            }
        }

        #endregion
    }
}