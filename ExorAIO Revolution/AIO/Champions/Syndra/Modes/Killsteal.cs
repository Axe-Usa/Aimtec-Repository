
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Syndra
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
                var bestTarget = SpellClass.Q.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q) >= bestTarget.GetRealHealth())
                {
                    SpellClass.Q.Cast(bestTarget.ServerPosition);
                }
            }

            /// <summary>
            ///     The W KillSteal Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.W.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.W) >= bestTarget.GetRealHealth())
                {
                    /*
                    var obj = this.ForceOfWillObject();
                    if (obj.IsValid &&
                        !this.IsHoldingForceOfWillObject())
                    {
                        SpellClass.W.CastOnUnit(obj);
                    }*/

                    if (IsHoldingForceOfWillObject())
                    {
                        SpellClass.W.Cast(bestTarget.ServerPosition);
                    }
                }
            }

            /// <summary>
            ///     The R KillSteal Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["killsteal"].As<MenuBool>().Enabled)
            {
                var bestTarget = SpellClass.R.GetBestKillableHero(DamageType.Magical);
                if (bestTarget != null &&
                    GetPerfectUnleashedPowerDamage(bestTarget) >= bestTarget.GetRealHealth())
                {
                    SpellClass.R.CastOnUnit(bestTarget);
                }
            }
        }

        #endregion
    }
}