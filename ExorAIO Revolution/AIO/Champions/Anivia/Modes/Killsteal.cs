
using System.Linq;
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
    internal partial class Anivia
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
                if (bestTarget != null)
                {
                    switch (UtilityClass.Player.SpellBook.GetSpell(SpellSlot.Q).ToggleState)
                    {
                        case 1:
                            if (UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q) >= bestTarget.GetRealHealth())
                            {
                                SpellClass.Q.Cast(bestTarget);
                            }
                            break;
                        case 2:
                            if (FlashFrost != null &&
                                GameObjects.EnemyHeroes.Any(t =>
                                    t.IsValidTarget(SpellClass.Q.Width, false, true, FlashFrost.Position) &&
                                    UtilityClass.Player.GetSpellDamage(bestTarget, SpellSlot.Q) >= bestTarget.GetRealHealth()))
                            {
                                SpellClass.Q.Cast();
                            }
                            break;
                    }
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
                    GetFrostBiteDamage(bestTarget) >= bestTarget.GetRealHealth())
                {
                    UtilityClass.CastOnUnit(SpellClass.E, bestTarget);
                }
            }
        }

        #endregion
    }
}