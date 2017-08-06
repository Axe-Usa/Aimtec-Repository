
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
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
                var playerPos = UtilityClass.Player.ServerPosition;
                const int CondemnPushDistance = 410 / 10;

                foreach (var target in
                    GameObjects.EnemyHeroes.Where(t =>
                        t.IsValidTarget(SpellClass.E.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        !t.IsValidTarget(UtilityClass.Player.BoundingRadius) &&
                        MenuClass.Spells["e"]["whitelist"][t.ChampionName.ToLower()].Enabled))
                {
                    for (var i = 1; i < 10; i++)
                    {
                        var predictedPos = SpellClass.E.GetPrediction(target).UnitPosition;

                        var targetPosition = target.ServerPosition.Extend(playerPos, -CondemnPushDistance * i);
                        var targetPositionExtended = target.ServerPosition.Extend(playerPos, (-CondemnPushDistance + 1) * i);

                        var predPosition = predictedPos.Extend(playerPos, -CondemnPushDistance * i);
                        var predPositionExtended = predictedPos.Extend(playerPos, (-CondemnPushDistance + 1) * i);

                        if (targetPosition.IsWall(true) && targetPositionExtended.IsWall(true) &&
                            predPosition.IsWall(true) && predPositionExtended.IsWall(true))
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