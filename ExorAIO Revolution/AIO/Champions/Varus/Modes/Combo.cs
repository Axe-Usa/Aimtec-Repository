
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Varus
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
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
            {
                var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.E.Range);
                if (heroTarget != null)
                {
                    if (!Invulnerable.Check(heroTarget) &&
                        !heroTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(heroTarget)) &&
                        GetBlightStacks(heroTarget) >= MenuClass.Spells["e"]["customization"]["combostacks"].As<MenuSlider>().Value)
                    {
                        SpellClass.E.Cast(heroTarget);
                    }
                }
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready)
            {
                var heroTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.ChargedMaxRange);
                if (heroTarget != null)
                {
                    if (!Invulnerable.Check(heroTarget) &&
                        MenuClass.Spells["q"]["combolong"].As<MenuBool>().Enabled &&
                        MenuClass.Spells["q"]["whitelist"][heroTarget.ChampionName.ToLower()].As<MenuBool>().Enabled &&
                        (!heroTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(heroTarget)) || IsChargingPiercingArrow()))
                    {
                        PiercingArrowLogicalCast(heroTarget);
                    }

                    if (IsChargingPiercingArrow() &&
                        MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled &&
                        GetBlightStacks(heroTarget) >= MenuClass.Spells["q"]["customization"]["combostacks"].As<MenuSlider>().Value)
                    {
                        SpellClass.Q.Cast(heroTarget);
                    }
                }
            }
        }

        #endregion
    }
}