
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
    internal partial class Akali
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public static void Combo()
        {
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
                heroTarget.IsValidTarget(SpellClass.R.Range) &&
                MenuClass.Spells["r"]["combo"].As<MenuBool>().Enabled)
            {
                if ((!heroTarget.IsUnderEnemyTurret() ||
                    !MenuClass.Miscellaneous["safe"].As<MenuBool>().Enabled) &&
                    (UtilityClass.Player.GetBuffCount("AkaliShadowDance") >
                        MenuClass.Miscellaneous["keepstacks"].As<MenuSliderBool>().Value ||
                    !MenuClass.Miscellaneous["keepstacks"].As<MenuSliderBool>().Enabled) &&
                    MenuClass.WhiteList[heroTarget.ChampionName.ToLower()].Enabled)
                {
                    SpellClass.R.CastOnUnit(heroTarget);
                }
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                !heroTarget.HasBuff("AkaliMota") &&
                heroTarget.IsValidTarget(SpellClass.Q.Range) &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                SpellClass.Q.CastOnUnit(heroTarget);
            }

            /// <summary>
            ///     The R Gapclose Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                !heroTarget.IsValidTarget(SpellClass.R.Range) &&
                heroTarget.IsValidTarget(SpellClass.R.Range * 2) &&
                UtilityClass.Player.GetBuffCount("AkaliShadowDance")
                    >= MenuClass.Miscellaneous["gapclose"].As<MenuSliderBool>().Value &&
                MenuClass.Miscellaneous["gapclose"].As<MenuSliderBool>().Enabled)
            {
                var bestMinion = UtilityClass.GetAllGenericMinionsTargetsInRange(SpellClass.R.Range)
                    .Where(m => m.Distance(heroTarget) < SpellClass.Q.Range)
                    .OrderBy(m => m.Distance(heroTarget))
                    .FirstOrDefault();

                if (bestMinion != null)
                {
                    SpellClass.R.CastOnUnit(bestMinion);
                }
            }
        }

        #endregion
    }
}