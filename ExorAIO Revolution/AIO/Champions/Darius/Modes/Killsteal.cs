
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Damage.JSON;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Darius
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Killsteal()
        {
            /// <summary>
            ///     The KillSteal R Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.R.GetBestKillableHero(DamageType.Physical);
                if (bestTarget != null &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.R) >= bestTarget.GetRealHealth())
                {
                    SpellClass.R.CastOnUnit(bestTarget);
                }
            }

            /// <summary>
            ///     The KillSteal Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.Q.GetBestKillableHero(DamageType.Physical);
                if (bestTarget != null &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q, IsValidBladeTarget(bestTarget)
                        ? DamageStage.Empowered
                        : DamageStage.Default) >= bestTarget.GetRealHealth())
                {
                    SpellClass.Q.Cast();
                }
            }
        }

        #endregion
    }
}