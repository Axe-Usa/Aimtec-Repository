
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Laneclear()
        {
            /// <summary>
            ///     Extended.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["extended"]["laneclear"]) &&
                MenuClass.Spells["q"]["extended"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                foreach (var minion in from minion in UtilityClass.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range)
                    let polygon = new Geometry.Rectangle((Vector2)UtilityClass.Player.Position, (Vector2)UtilityClass.Player.Position.Extend(minion.Position, SpellClass.Q2.Range), SpellClass.Q2.Width)
                    where polygon.IsInside((Vector2)SpellClass.Q2.GetPrediction(UtilityClass.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range)).PredictedPosition)
                    select minion)
                {
                    SpellClass.Q.CastOnUnit(minion);
                }
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public static void Laneclear(object sender, PostAttackEventArgs args)
        {
            /// <summary>
            ///     The E Laneclear Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["laneclear"]) &&
                MenuClass.Spells["e"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.E.Cast(UtilityClass.Player.Position.Extend(Game.CursorPos, UtilityClass.Player.BoundingRadius));
            }

            /*
            /// <summary>
            ///     The Laneclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["laneclear"]) &&
                MenuClass.Spells["q"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                var farmLocation = SpellClass.Q2.GetLineFarmLocation(UtilityClass.GetEnemyLaneMinionsTargets(), SpellClass.Q2.Width);
                if (farmLocation.MinionsHit >= 3)
                {
                    SpellClass.Q.CastOnUnit(farmLocation.FirstMinion);
                    return;
                }
            }

            /// <summary>
            ///     The Laneclear W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["laneclear"]) &&
                MenuClass.Spells["w"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                var farmLocation = SpellClass.W.GetCircularFarmLocation(UtilityClass.GetEnemyLaneMinionsTargets(), SpellClass.W.Width);
                if (farmLocation.MinionsHit >= 2)
                {
                    SpellClass.W.Cast(farmLocation.Position);
                }
            }
            */
        }

        #endregion
    }
}