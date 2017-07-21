
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Vayne
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Killsteal()
        {
            /// <summary>
            ///     The Q KillSteal Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.Q.GetBestKillableHero(DamageType.Physical);
                if (bestTarget != null)
                {
                    var shouldIncludeWDamage = bestTarget.GetBuffCount("vaynesilvereddebuff") == 2;
                    if (!bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)) &&
                        UtilityClass.Player.GetAutoAttackDamage(bestTarget) +
                        UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q) +
                        (shouldIncludeWDamage ? UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.W) : 0) >= bestTarget.GetRealHealth())
                    {
                        SpellClass.Q.Cast(bestTarget.Position);
                    }
                }
            }

            /// <summary>
            ///     The E KillSteal Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.E.GetBestKillableHero(DamageType.Physical);
                if (bestTarget != null)
                {
                    var shouldIncludeWDamage = bestTarget.GetBuffCount("vaynesilvereddebuff") == 2;
                    if (!bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)) &&
                        UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.E) +
                        (shouldIncludeWDamage ? UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.W) : 0) >= bestTarget.GetRealHealth())
                    {
                        SpellClass.E.CastOnUnit(bestTarget);
                    }
                }
            }
        }

        #endregion
    }
}