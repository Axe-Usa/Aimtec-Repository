
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ahri
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Combo()
        {
            var heroTarget = Extensions.GetBestEnemyHeroTarget();
            if (!heroTarget.IsValidTarget() ||
                Invulnerable.Check(heroTarget, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
            {
                if (SpellClass.R.Ready &&
                    heroTarget.IsValidTarget(SpellClass.W.Range + SpellClass.R.Range) &&
                    UtilityClass.Player.HasBuff("AhriTumble") || !MenuClass.Spells["r"]["customization"]["onlyrstarted"].As<MenuBool>().Enabled)
                {
                    SpellClass.W.Cast();
                }
                else if (heroTarget.IsValidTarget(SpellClass.W.Range))
                {
                    SpellClass.W.Cast();
                }
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                heroTarget.IsValidTarget(SpellClass.E.Range - 100f) &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
            {
                SpellClass.E.Cast(heroTarget);
                return;
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["combo"].As<MenuBool>().Enabled &&
                MenuClass.Spells["r"]["whitelist"][heroTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
            {
                var position = UtilityClass.Player.Position.Extend(Game.CursorPos, SpellClass.R.Range);
                if (heroTarget.IsValidTarget(600f + heroTarget.BoundingRadius * 2, false, position) &&
                    (UtilityClass.Player.HasBuff("AhriTumble") || !MenuClass.Spells["r"]["customization"]["onlyrstarted"].As<MenuBool>().Enabled))
                {
                    SpellClass.R.Cast(position);
                }
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                heroTarget.IsValidTarget(SpellClass.Q.Range - 100f) &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                SpellClass.Q.Cast(heroTarget);
            }
        }

        #endregion
    }
}