
// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Taliyah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Jungleclear()
        {
            var jungleTarget = ObjectManager.Get<Obj_AI_Minion>().FirstOrDefault(m => Extensions.GetGenericJungleMinionsTargets().Contains(m));
            if (jungleTarget == null ||
                jungleTarget.GetRealHealth() < UtilityClass.Player.GetAutoAttackDamage(jungleTarget) * 3)
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear Rylai Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                jungleTarget.IsValidTarget(SpellClass.Q.Range) &&
                UtilityClass.Player.HasItem(ItemId.RylaisCrystalScepter) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                switch (MenuClass.Spells["q"]["customization"]["qmodes"]["jungleclear"].As<MenuList>().Value)
                {
                    case 0:
                        if (!this.IsNearWorkedGround())
                        {
                            SpellClass.Q.Cast(jungleTarget);
                        }
                        break;
                    case 1:
                        SpellClass.Q.Cast(jungleTarget);
                        break;
                }
            }

            var targetPosAfterW = new Vector3();

            /// <summary>
            ///     The Jungleclear W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                jungleTarget.IsValidTarget(SpellClass.W.Range) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["jungleclear"]) &&
                MenuClass.Spells["w"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                var bestBoulderHitPos = this.GetBestBouldersHitPosition(jungleTarget);
                var bestBoulderHitPosHitBoulders = this.GetBestBouldersHitPositionHitBoulders(jungleTarget);
                var jungleTargetPredPos = SpellClass.W.GetPrediction(jungleTarget).CastPosition;

                if (SpellClass.E.Ready)
                {
                    if (UtilityClass.Player.Distance(this.GetUnitPositionAfterPull(jungleTarget)) >= 200f)
                    {
                        targetPosAfterW = this.GetUnitPositionAfterPull(jungleTarget);
                    }
                    else
                    {
                        targetPosAfterW = this.GetUnitPositionAfterPush(jungleTarget);
                    }

                    //SpellClass.W.Cast(jungleTargetPredPos, targetPosAfterW);
                    SpellClass.W.Cast(targetPosAfterW, jungleTargetPredPos);
                }
                else if (bestBoulderHitPos != Vector3.Zero && bestBoulderHitPosHitBoulders > 0)
                {
                    //SpellClass.W.Cast(jungleTargetPredPos, bestBoulderHitPos);
                    SpellClass.W.Cast(bestBoulderHitPos, jungleTargetPredPos);
                }
            }

            /// <summary>
            ///     The Jungleclear E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                jungleTarget.IsValidTarget(SpellClass.E.Range) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["jungleclear"]) &&
                MenuClass.Spells["e"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.E.Cast(SpellClass.W.Ready
                    ? targetPosAfterW
                    : jungleTarget.Position);
            }

            /// <summary>
            ///     The Jungleclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                jungleTarget.IsValidTarget(SpellClass.Q.Range) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                switch (MenuClass.Spells["q"]["customization"]["qmodes"]["jungleclear"].As<MenuList>().Value)
                {
                    case 0:
                        if (!this.IsNearWorkedGround())
                        {
                            SpellClass.Q.Cast(jungleTarget);
                        }
                        break;
                    case 1:
                        SpellClass.Q.Cast(jungleTarget);
                        break;
                }
            }
        }

        #endregion
    }
}