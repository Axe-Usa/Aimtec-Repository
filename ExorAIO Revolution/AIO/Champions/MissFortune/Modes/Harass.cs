
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class MissFortune
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Harass()
        {
            /// <summary>
            ///     The Extended Q Mixed Harass Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["extendedq"]["mixed"]) &&
                MenuClass.Spells["extendedq"]["mixed"].As<MenuSliderBool>().Enabled)
            {
                var unitsInQRange = Extensions.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range);
                var unitsToIterate =
                    unitsInQRange.Any(m => m.Health < UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q))
                        ? unitsInQRange.Where(m => m.Health < UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q))
                        : unitsInQRange;

                var objAiBases = unitsToIterate as IList<Obj_AI_Base> ?? unitsToIterate.ToList();
                foreach (var hero in Extensions.GetBestEnemyHeroesTargetsInRange(SpellClass.Q2.Range))
                {
                    foreach (var minion in objAiBases)
                    {
                        var polygon = this.QCone(minion);
                        if (MenuClass.Spells["extendedq"]["whitelist"][hero.ChampionName.ToLower()].Enabled &&
                            (this.LoveTapTargetNetworkId == hero.NetworkId || objAiBases.All(m => polygon.IsOutside((Vector2)m.ServerPosition))) &&
                            polygon.IsInside((Vector2)Prediction.GetPrediction(hero, UtilityClass.Player.Distance(hero) / SpellClass.Q2.Speed + SpellClass.Q2.Delay).UnitPosition))
                        {
                            SpellClass.Q.CastOnUnit(minion);
                        }
                    }
                }
            }

            /// <summary>
            ///     The Harass E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["harass"]) &&
                MenuClass.Spells["e"]["harass"].As<MenuSliderBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.E.Range);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical, false) &&
                    MenuClass.Spells["e"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                {
                    SpellClass.E.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}