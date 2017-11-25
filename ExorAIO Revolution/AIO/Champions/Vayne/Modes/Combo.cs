
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
                const int condemnPushDistance = 475;
                const int threshold = 50;

                foreach (var target in
                    GameObjects.EnemyHeroes.Where(t =>
                        t.IsValidTarget(SpellClass.E.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        !t.IsValidTarget(UtilityClass.Player.BoundingRadius) &&
                        MenuClass.Spells["e"]["whitelist"][t.ChampionName.ToLower()].Enabled))
                {
                    var targetPos = target.ServerPosition;
                    for (var i = UtilityClass.Player.BoundingRadius; i < condemnPushDistance-threshold; i += 10)
                    {
                        if (targetPos.Extend(playerPos, -i).IsWall(true) &&
                            targetPos.Extend(playerPos, -i-target.BoundingRadius).IsWall(true))
                        {
                            if (MenuClass.Spells["e"]["emode"].As<MenuList>().Value == 0)
                            {
                                var predPoint = targetPos.Extend(target.Path.FirstOrDefault(), -(target.MoveSpeed * SpellClass.E.Delay));
                                if (target.IsImmobile(SpellClass.E.Delay*2) ||
                                    predPoint.Extend(playerPos, -i).IsWall(true) &&
                                    predPoint.Extend(playerPos, -i-target.BoundingRadius).IsWall(true))
                                {
                                    SpellClass.E.CastOnUnit(target);
                                }
                            }
                            else
                            {
                                SpellClass.E.CastOnUnit(target);
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}