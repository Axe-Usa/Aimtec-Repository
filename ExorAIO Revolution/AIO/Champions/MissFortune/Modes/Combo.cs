
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
        ///     Called on tick update.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The Extended Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q2"]["combo"].As<MenuBool>().Enabled)
            {
                var unitsToIterate = Extensions.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range);
                unitsToIterate = MenuClass.Spells["q2"]["customization"]["combo"].As<MenuBool>().Enabled
                    ? unitsToIterate.Where(m => m.Health <= UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q)).ToList()
                    : unitsToIterate;

                foreach (var enemy in GameObjects.EnemyHeroes.Where(t =>
                    t.IsValidTarget(SpellClass.Q2.Range) &&
                    MenuClass.Spells["q2"]["whitelist"][t.ChampionName.ToLower()].Enabled))
                {
                    foreach (var minion in unitsToIterate
                        .OrderBy(t => t.Health)
                        .Where(m =>
                            QCone(m).IsInside((Vector2)enemy.ServerPosition) &&
                            QCone(m).IsInside((Vector2)SpellClass.Q2.GetPrediction(enemy).CastPosition)))
                    {
                        var polygon = QCone(minion);
                        if (LoveTapTargetNetworkId == enemy.NetworkId ||
                            GameObjects.EnemyMinions.All(m => polygon.IsOutside((Vector2)m.ServerPosition)))
                        {
                            UtilityClass.CastOnUnit(SpellClass.Q, minion);
                        }
                    }
                }
            }

            /// <summary>
            ///     The Harass E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.E.Range);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical, false))
                {
                    SpellClass.E.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}