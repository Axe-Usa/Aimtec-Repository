
#pragma warning disable 1587

namespace AIO.Champions
{
    using System;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class KogMaw
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public static void Combo()
        {
            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                GameObjects.EnemyHeroes.Any(t => t.IsValidTarget(SpellClass.W.Range)) &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Value)
            {
                SpellClass.W.Cast();
            }

            var heroTarget = UtilityClass.GetBestEnemyHeroTarget();
            if (!heroTarget.IsValidTarget() ||
                Invulnerable.Check(heroTarget, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                heroTarget.HealthPercent() < 40 &&
                heroTarget.IsValidTarget(SpellClass.R.Range) &&
                UtilityClass.Player.Mana
                    > UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).Cost + 50 * (UtilityClass.Player.GetBuffCount("kogmawlivingartillerycost") + 1) &&
                MenuClass.Spells["r"]["combo"].As<MenuSliderBool>().Enabled &&
                MenuClass.Spells["r"]["combo"].As<MenuSliderBool>().Value
                    > UtilityClass.Player.GetBuffCount("kogmawlivingartillerycost"))
            {
                SpellClass.R.Cast(heroTarget);
            }
        }

        #endregion
    }
}