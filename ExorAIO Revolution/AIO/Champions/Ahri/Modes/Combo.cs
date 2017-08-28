
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
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
            switch (MenuClass.Spells["pattern"].As<MenuList>().Value)
            {
                case 0:
                    /// <summary>
                    ///     The E Combo Logic.
                    /// </summary>
                    if (SpellClass.E.Ready &&
                        MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
                    {
                        var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.E.Range-150f);
                        if (heroTarget == null ||
                            Invulnerable.Check(heroTarget, DamageType.Magical, false))
                        {
                            break;
                        }

                        SpellClass.E.Cast(heroTarget);
                    }
                    break;

                case 1:
                    /// <summary>
                    ///     The R Combo Logic.
                    /// </summary>
                    if (SpellClass.R.Ready &&
                        MenuClass.Spells["r"]["combo"].As<MenuBool>().Enabled)
                    {
                        if (!UtilityClass.Player.HasBuff("AhriTumble") &&
                            MenuClass.Spells["r"]["customization"]["onlyrstarted"].As<MenuBool>().Enabled)
                        {
                            break;
                        }

                        const float RRadius = 500f;
                        var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.R.Range + RRadius);
                        if (heroTarget == null ||
                            Invulnerable.Check(heroTarget, DamageType.Magical) ||
                            !MenuClass.Spells["r"]["whitelist"][heroTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                        {
                            break;
                        }

                        var position = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, SpellClass.R.Range);
                        if (heroTarget.IsValidTarget(RRadius, false, false, position))
                        {
                            SpellClass.R.Cast(position);
                        }
                    }
                    break;
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
            {
                var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.E.Range-150f);
                if (heroTarget != null &&
                    !Invulnerable.Check(heroTarget, DamageType.Magical, false))
                {
                    SpellClass.E.Cast(heroTarget);
                }
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
            {
                var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.W.Range-100f);
                if (heroTarget != null &&
                    !Invulnerable.Check(heroTarget, DamageType.Magical))
                {
                    SpellClass.W.Cast();
                }
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range-150f);
                if (heroTarget != null &&
                    !Invulnerable.Check(heroTarget, DamageType.Magical))
                {
                    SpellClass.Q.Cast(heroTarget);
                }
            }
        }

        #endregion
    }
}
