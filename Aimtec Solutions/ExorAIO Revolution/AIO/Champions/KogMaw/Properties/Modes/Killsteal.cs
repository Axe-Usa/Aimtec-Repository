
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Damage.JSON;
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
        public static void Killsteal()
        {
            /// <summary>
            ///     The Q KillSteal Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.Q.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.AttackRange) &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q) > bestTarget.GetRealHealth())
                {
                    SpellClass.Q.Cast(bestTarget);
                }
            }

            /// <summary>
            ///     The E KillSteal Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.E.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.AttackRange) &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.E) >= bestTarget.GetRealHealth())
                {
                    SpellClass.E.Cast(bestTarget);
                }
            }

            /// <summary>
            ///     The R KillSteal Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["combo"].As<MenuSliderBool>().Value
                    > UtilityClass.Player.GetBuffCount("kogmawlivingartillerycost") &&
                MenuClass.Spells["r"]["killsteal"].As<MenuSliderBool>().Enabled)
            {
                var bestTarget = SpellClass.R.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.AttackRange) &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.R, bestTarget.HealthPercent() < 40 ? DamageStage.Empowered : DamageStage.Default) >= bestTarget.GetRealHealth())
                {
                    SpellClass.R.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}