
using System.Linq;
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Damage.JSON;
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
        public void Killsteal()
        {
            /// <summary>
            ///     The Q Killsteal Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q2"]["killsteal"].As<MenuBool>().Enabled)
            {
                var unitsToIterate = Extensions.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range);
                foreach (var enemy in GameObjects.EnemyHeroes.Where(t =>
                    t.IsValidTarget(SpellClass.Q2.Range) &&
                    t.GetRealHealth() <= UtilityClass.Player.GetSpellDamage(t, SpellSlot.Q, DamageStage.Empowered)))
                {
                    var damageStage = enemy.GetRealHealth() <= UtilityClass.Player.GetSpellDamage(enemy, SpellSlot.Q)
                        ? DamageStage.Default
                        : DamageStage.Empowered;

                    unitsToIterate = damageStage == DamageStage.Empowered
                        ? unitsToIterate.Where(m => m.Health <= UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q)).ToList()
                        : unitsToIterate;

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
                            SpellClass.Q.CastOnUnit(minion);
                        }
                    }
                }
            }
        }

        #endregion
    }
}