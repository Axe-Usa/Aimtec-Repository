
using System.Linq;
using Aimtec;
using Aimtec.SDK.Events;
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
                !UtilityClass.Player.IsDashing())
            {
                var playerPos = UtilityClass.Player.ServerPosition;
                switch (MenuClass.Spells["e"]["emode"].As<MenuList>().Value)
                {
                    case 0:
                        SpellClass.E.Delay = 0.5f;
                        SpellClass.E.Speed = 1200f;

                        break;
                    case 1:
                        SpellClass.E.Delay = 0.4f;
                        SpellClass.E.Speed = 1000f;
                        break;
                    default:
                        return;
                }

                const int threshold = 25 / 10;
                var predictedPos = new Vector3();
                const int condemnPushDistance = 410 / 10;

                foreach (var target in
                    GameObjects.EnemyHeroes.Where(t =>
                        t.IsValidTarget(SpellClass.E.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        !t.IsValidTarget(UtilityClass.Player.BoundingRadius) &&
                        MenuClass.Spells["e"]["whitelist"][t.ChampionName.ToLower()].Enabled))
                {
                    var prediction = SpellClass.E.GetPrediction(target);
                    switch (MenuClass.Spells["e"]["emode"].As<MenuList>().Value)
                    {
                        case 0:
                            predictedPos = prediction.UnitPosition;
                            break;
                        case 1:
                            predictedPos = prediction.CastPosition;
                            break;
                    }

                    for (var i = 1; i < 10; i++)
                    {
                        var targetPosition = target.ServerPosition.Extend(playerPos, -condemnPushDistance * i);
                        var targetPositionExtended = target.ServerPosition.Extend(playerPos, (-condemnPushDistance + threshold) * i);

                        var predPosition = predictedPos.Extend(playerPos, -condemnPushDistance * i);
                        var predPositionExtended = predictedPos.Extend(playerPos, (-condemnPushDistance + threshold) * i);

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