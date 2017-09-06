
using System.Linq;
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
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
        ///     Called on post attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public void Jungleclear(object sender, PostAttackEventArgs args)
        {
            var jungleTarget = args.Target as Obj_AI_Minion;
            if (jungleTarget == null ||
                !Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget) ||
                jungleTarget.GetRealHealth() < UtilityClass.Player.GetAutoAttackDamage(jungleTarget) * 2)
            {
                return;
            }

            /// <summary>
            ///     The E Jungleclear Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["jungleclear"]) &&
                MenuClass.Spells["e"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                if (UtilityClass.JungleList.Contains(jungleTarget.UnitSkinName) &&
                    !Extensions.GetLegendaryJungleMinionsTargets().Contains(jungleTarget))
                {
                    var playerPos = UtilityClass.Player.ServerPosition;
                    const int condemnPushDistance = 410 / 10;
                    for (var i = 1; i < 10; i++)
                    {
                        var predictedPos = SpellClass.E.GetPrediction(jungleTarget).UnitPosition;

                        var targetPosition = jungleTarget.ServerPosition.Extend(playerPos, -condemnPushDistance * i);
                        var targetPositionExtended = jungleTarget.ServerPosition.Extend(playerPos, (-condemnPushDistance + 1) * i);

                        var predPosition = predictedPos.Extend(playerPos, -condemnPushDistance * i);
                        var predPositionExtended = predictedPos.Extend(playerPos, (-condemnPushDistance + 1) * i);

                        if (targetPosition.IsWall(true) && targetPositionExtended.IsWall(true) &&
                            predPosition.IsWall(true) && predPositionExtended.IsWall(true))
                        {
                            SpellClass.E.CastOnUnit(jungleTarget);
                        }
                    }
                }
            }

            /// <summary>
            ///     The Q Jungleclear Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.Cast(Game.CursorPos);
            }
        }

        #endregion
    }
}