
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Anivia
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Harass()
        {
            /// <summary>
            ///     The R Harass Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["harass"].As<MenuSliderBool>().Enabled)
            {
                var manaCheck =
                    UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.R.Slot, MenuClass.Spells["r"]["harass"]);
                switch (UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).ToggleState)
                {
                    case 1:
                        var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.R.Range);
                        if (bestTarget != null)
                        {
                            if (manaCheck &&
                                !Invulnerable.Check(bestTarget, DamageType.Magical) &&
                                MenuClass.Spells["r"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                            {
                                SpellClass.R.Cast(bestTarget);
                            }
                        }
                        break;
                    case 2:
                        if (!manaCheck ||
                            this.GlacialStorm != null &&
                            !GameObjects.EnemyHeroes.Any(t =>
                                !Invulnerable.Check(t, DamageType.Magical) &&
                                t.IsValidTarget(SpellClass.R.Width, false, true, this.GlacialStorm.Position) &&
                                MenuClass.Spells["r"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled))
                        {
                            SpellClass.R.Cast();
                        }
                        break;
                }
            }

            /// <summary>
            ///     The Q Harass Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Enabled)
            {
                var manaCheck =
                    UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["harass"]);
                switch (UtilityClass.Player.SpellBook.GetSpell(SpellSlot.Q).ToggleState)
                {
                    case 1:
                        var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range);
                        if (bestTarget != null)
                        {
                            if (manaCheck &&
                                !Invulnerable.Check(bestTarget, DamageType.Magical) &&
                                MenuClass.Spells["q"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                            {
                                SpellClass.Q.Cast(bestTarget);
                            }
                        }
                        break;
                    case 2:
                        if (!manaCheck ||
                            this.FlashFrost != null &&
                            !GameObjects.EnemyHeroes.Any(t =>
                                !Invulnerable.Check(t, DamageType.Magical) &&
                                t.IsValidTarget(SpellClass.Q.Width, false, true, this.FlashFrost.Position) &&
                                MenuClass.Spells["q"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled))
                        {
                            SpellClass.Q.Cast();
                        }
                        break;
                }
            }

            /// <summary>
            ///     The E Harass Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["harass"]) &&
                MenuClass.Spells["e"]["harass"].As<MenuSliderBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.E.Range);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical) &&
                    MenuClass.Spells["e"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                {
                    switch (MenuClass.Spells["e"]["customization"]["emodes"]["harass"].As<MenuList>().Value)
                    {
                        case 0:
                            if (this.IsChilled(bestTarget))
                            {
                                SpellClass.E.CastOnUnit(bestTarget);
                            }
                            break;
                        case 1:
                            SpellClass.E.CastOnUnit(bestTarget);
                            break;
                    }
                }
            }
        }

        #endregion
    }
}