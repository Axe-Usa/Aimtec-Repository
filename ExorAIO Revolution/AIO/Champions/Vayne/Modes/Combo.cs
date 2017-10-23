
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

                const int threshold = 60;
                const int condemnPushDistance = 410;

                foreach (var target in
                    GameObjects.EnemyHeroes.Where(t =>
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        t.IsValidTarget(SpellClass.E.Range+t.BoundingRadius) &&
                        !t.IsValidTarget(UtilityClass.Player.BoundingRadius) &&
                        MenuClass.Spells["e"]["whitelist"][t.ChampionName.ToLower()].Enabled))
                {
                    var targetPos = target.ServerPosition;
                    var predPosition = SpellClass.E.GetPrediction(target).CastPosition;
                    for (var i = threshold; i < condemnPushDistance; i += 10)
                    {
                        if (!targetPos.Extend(playerPos, -i).IsWall(true) ||
                            !targetPos.Extend(playerPos, -i-threshold).IsWall(true))
                        {
                            continue;
                        }

                        if (MenuClass.Spells["e"]["emode"].As<MenuList>().Value == 0)
                        {
                            if (!predPosition.Extend(playerPos, -i).IsWall(true) ||
                                !predPosition.Extend(playerPos, -i-threshold).IsWall(true))
                            {
                                continue;
                            }
                        }

                        SpellClass.E.CastOnUnit(target);
                    }
                }
            }
        }

        #endregion
    }
}