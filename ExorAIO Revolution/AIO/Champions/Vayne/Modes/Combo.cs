
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
                !UtilityClass.Player.IsDashing() &&
                MenuClass.Spells["e"]["emode"].As<MenuList>().Value != 2)
            {
                var playerPos = UtilityClass.Player.ServerPosition;

                const int threshold = 55;
                const int condemnPushDistance = 405;

                foreach (var target in
                    GameObjects.EnemyHeroes.Where(t =>
                        t.IsValidTarget(SpellClass.E.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        !t.IsValidTarget(UtilityClass.Player.BoundingRadius) &&
                        MenuClass.Spells["e"]["whitelist"][t.ChampionName.ToLower()].Enabled))
                {
                    var predictedPos = SpellClass.E.GetPrediction(target).CastPosition;
                    if (MenuClass.Spells["e"]["emode"].As<MenuList>().Value == 0)
                    {
                        var predPosition = predictedPos.Extend(playerPos, -condemnPushDistance);
                        var predPositionExtended = predictedPos.Extend(playerPos, -(condemnPushDistance + threshold));
                        if (!Bools.AnyWallInBetween(target.ServerPosition, predPosition) ||
                            !Bools.AnyWallInBetween(target.ServerPosition, predPositionExtended))
                        {
                            return;
                        }
                    }

                    var targetPosition = target.ServerPosition.Extend(playerPos, -condemnPushDistance);
                    var targetPositionExtended = target.ServerPosition.Extend(playerPos, -(condemnPushDistance + threshold));
                    if (Bools.AnyWallInBetween(target.ServerPosition, targetPosition) &&
                        Bools.AnyWallInBetween(target.ServerPosition, targetPositionExtended))
                    {
                        SpellClass.E.CastOnUnit(target);
                    }
                }
            }
        }

        #endregion
    }
}