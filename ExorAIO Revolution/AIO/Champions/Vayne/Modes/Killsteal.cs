
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
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
            ///     The E KillSteal Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.E.GetBestKillableHero(DamageType.Physical, includeBoundingRadius: true);
                if (bestTarget != null)
                {
                    var shouldIncludeWDamage = bestTarget.GetBuffCount("vaynesilvereddebuff") == 2;
                    if (UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.E) +
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