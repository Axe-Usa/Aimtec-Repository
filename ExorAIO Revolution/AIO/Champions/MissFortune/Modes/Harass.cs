
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
    internal partial class MissFortune
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Harass()
        {
            /// <summary>
            ///     The Q Mixed Harass Logics.
            /// </summary>
            if (SpellClass.Q.Ready)
            {
                /// <summary>
                ///     The Normal Q Harass Logic.
                /// </summary>
                var orbTarget = ImplementationClass.IOrbwalker.GetOrbwalkingTarget();
                if (orbTarget != null)
                {
                    var heroTarget = orbTarget as Obj_AI_Hero;
                    if (heroTarget != null)
                    {
                        if (UtilityClass.Player.ManaPercent()
                                > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["harass"]) &&
                            MenuClass.Spells["q"]["harass"].As<MenuSliderBool>().Enabled)
                        {
                            if (!Invulnerable.Check(heroTarget) &&
                                MenuClass.Spells["q"]["whitelist"][heroTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                            {
                                SpellClass.E.Cast(heroTarget);
                            }
                        }
                    }
                }
                else
                {
                    /// <summary>
                    ///     The Extended Q Harass Logic.
                    /// </summary>
                    if (UtilityClass.Player.ManaPercent()
                            > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q2"]["harass"]) &&
                        MenuClass.Spells["q2"]["harass"].As<MenuSliderBool>().Enabled)
                    {
                        var unitsToIterate = Extensions.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range);
                        unitsToIterate = MenuClass.Spells["q2"]["customization"]["harass"].As<MenuBool>().Enabled
                            ? unitsToIterate.Where(m => m.Health <= UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q)).ToList()
                            : unitsToIterate;

                        foreach (var enemy in GameObjects.EnemyHeroes.Where(t =>
                            t.IsValidTarget(SpellClass.Q2.Range) &&
                            MenuClass.Spells["q2"]["whitelist"][t.ChampionName.ToLower()].Enabled))
                        {
                            foreach (var minion in unitsToIterate.OrderBy(t => t.Health).Where(m => QCone(m).IsInside((Vector2)enemy.ServerPosition)))
                            {
                                var polygon = QCone(minion);
                                if (LoveTapTargetNetworkId == enemy.NetworkId ||
                                    GameObjects.EnemyMinions.All(m => polygon.IsOutside((Vector2)m.ServerPosition)))
                                {
                                    SpellClass.Q.CastOnUnit(minion);
                                }
                            }
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