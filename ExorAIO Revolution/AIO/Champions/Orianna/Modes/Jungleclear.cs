
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
    internal partial class Orianna
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Jungleclear()
        {
            if (this.BallPosition == null)
            {
                return;
            }

            var jungleTarget = ImplementationClass.IOrbwalker.GetOrbwalkingTarget() as Obj_AI_Minion;
            if (!jungleTarget.IsValidTarget() ||
                !Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget) ||
                jungleTarget?.GetRealHealth() < UtilityClass.Player.GetAutoAttackDamage(jungleTarget) * 4)
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["jungleclear"]) &&
                MenuClass.Spells["w"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                if (Extensions.GetGenericJungleMinionsTargets().Any(m => m.IsValidTarget(SpellClass.W.Width, false, true, (Vector3)this.BallPosition)))
                {
                    SpellClass.W.Cast();
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
                var polygon = new Geometry.Rectangle(
                    (Vector2)UtilityClass.Player.ServerPosition,
                    (Vector2)UtilityClass.Player.ServerPosition.Extend((Vector3)this.BallPosition, UtilityClass.Player.Distance((Vector3)this.BallPosition)),
                    SpellClass.E.Width);

                if (jungleTarget != null &&
                    !polygon.IsOutside((Vector2)jungleTarget.ServerPosition))
                {
                    SpellClass.E.CastOnUnit(UtilityClass.Player);
                }
            }

            /// <summary>
            ///     The Jungleclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.GetPredictionInput(jungleTarget).From = (Vector3)this.BallPosition;
                SpellClass.Q.Cast(SpellClass.Q.GetPrediction(jungleTarget).CastPosition);
            }
        }

        #endregion
    }
}