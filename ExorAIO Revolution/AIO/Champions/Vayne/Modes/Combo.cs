
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Events;
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
        public void Combo()
        {
            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["engage"].As<MenuBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget) &&
                    !bestTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(bestTarget)))
                {
                    var posAfterQ = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, 300f);
                    if (posAfterQ.CountEnemyHeroesInRange(1000f) < 3 &&
                        UtilityClass.Player.Distance(Game.CursorPos) > UtilityClass.Player.AttackRange &&
                        bestTarget.Distance(posAfterQ) < UtilityClass.Player.GetFullAttackRange(bestTarget))
                    {
                        SpellClass.Q.Cast(posAfterQ);
                    }
                }
            }

            /// <summary>
            ///     The E Stun Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                !UtilityClass.Player.IsDashing() &&
                MenuClass.Spells["e"]["logical"].As<MenuBool>().Enabled)
            {
                var playerPos = UtilityClass.Player.Position;
                const int CondemnDistancePush = 410;

                foreach (var target in
                    GameObjects.EnemyHeroes.Where(t =>
                        t.IsValidTarget(SpellClass.E.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        !t.IsValidTarget(UtilityClass.Player.BoundingRadius) &&
                        t.GetRealHealth() >
                            UtilityClass.Player.GetAutoAttackDamage(t) *
                            MenuClass.Spells["e"]["customization"]["eaacheck"].As<MenuSlider>().Value &&
                        MenuClass.Spells["e"]["whitelist"][t.ChampionName.ToLower()].Enabled))
                {
                    for (var i = 1; i < 10; i++)
                    {
                        var targetPos = (Vector2)target.Position;

                        var posImpact1 = (Vector2)target.Position.Extend(playerPos, -CondemnDistancePush);
                        var posImpact2 = (Vector2)target.Position.Extend(playerPos, -CondemnDistancePush - 10f);

                        var predImpact1 = (Vector2)SpellClass.E.GetPrediction(target).UnitPosition.Extend(playerPos, -CondemnDistancePush);
                        var predImpact2 = (Vector2)SpellClass.E.GetPrediction(target).UnitPosition.Extend(playerPos, -CondemnDistancePush - 10f);

                        if (Bools.AnyWallInBetween(targetPos, posImpact1) && Bools.AnyWallInBetween(targetPos, posImpact2) &&
                            Bools.AnyWallInBetween(targetPos, predImpact1) && Bools.AnyWallInBetween(targetPos, predImpact2))
                        {
                            SpellClass.E.CastOnUnit(target);
                        }
                    }
                }
            }
        }

        #endregion
    }
}