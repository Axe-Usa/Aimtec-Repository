
#pragma warning disable 1587

namespace AIO.Champions
{
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
            var jungleTarget = (Obj_AI_Minion)ImplementationClass.IOrbwalker.GetTarget();
            if (!jungleTarget.IsValidTarget() ||
                !Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget) ||
                jungleTarget.Health < UtilityClass.Player.GetAutoAttackDamage(jungleTarget) * 4)
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear Rylai Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.HasItem(ItemId.RylaisCrystalScepter) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                if (this.IsNearWorkedGround() &&
                    MenuClass.Spells["q"]["customization"]["jungleclearfull"].As<MenuBool>().Enabled)
                {
                    return;
                }

                SpellClass.Q.Cast(jungleTarget);
            }

            /// <summary>
            ///     The Jungleclear W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["jungleclear"]) &&
                MenuClass.Spells["w"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                var bestBoulderHitPos = this.GetBestBouldersHitPosition(jungleTarget);
                var bestBoulderHitPosHitBoulders = this.GetBestBouldersHitPositionHitBoulders(jungleTarget);
                if (SpellClass.E.Ready)
                {
                    if (jungleTarget.Distance(UtilityClass.Player) < 300f)
                    {
                        SpellClass.W.Cast((Vector2)jungleTarget.Position.Extend((Vector2)UtilityClass.Player.Position, 75f), -(Vector2)UtilityClass.Player.Position);
                    }
                    else
                    {
                        SpellClass.W.Cast((Vector2)jungleTarget.Position.Extend((Vector2)UtilityClass.Player.Position, 75f), (Vector2)UtilityClass.Player.Position);
                    }
                }
                else if (bestBoulderHitPos != Vector3.Zero && bestBoulderHitPosHitBoulders > 0)
                {
                    SpellClass.W.Cast((Vector2)jungleTarget.Position, (Vector2)bestBoulderHitPos);
                }
            }

            /// <summary>
            ///     The Jungleclear E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["jungleclear"]) &&
                MenuClass.Spells["e"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.E.Cast(jungleTarget.Position);
            }

            /// <summary>
            ///     The Jungleclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                if (this.IsNearWorkedGround() &&
                    MenuClass.Spells["q"]["customization"]["jungleclearfull"].As<MenuBool>().Enabled)
                {
                    return;
                }

                SpellClass.Q.Cast(jungleTarget);
            }
        }

        #endregion
    }
}