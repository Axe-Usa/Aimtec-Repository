
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
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
            /// <summary>
            ///     The Jungleclear W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.ManaPercent()
                > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["Jungleclear"]) &&
                MenuClass.Spells["w"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                if (Extensions.GetGenericJungleMinionsTargets().Count(m => m.IsValidTarget(SpellClass.W.Width, false, this.BallPosition)) >= MenuClass.Miscellaneous["w"]["Jungleclear"].As<MenuSlider>().Value)
                {
                    SpellClass.W.Cast();
                }
            }

            /// <summary>
            ///     The Jungleclear E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["Jungleclear"]) &&
                MenuClass.Spells["e"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                var polygon = new Geometry.Rectangle(
                    (Vector2)UtilityClass.Player.Position,
                    (Vector2)UtilityClass.Player.Position.Extend(this.BallPosition, UtilityClass.Player.Distance(this.BallPosition)),
                    SpellClass.E.Width);

                if (Extensions.GetGenericJungleMinionsTargets().Count(t => t.IsValidTarget() && !polygon.IsOutside((Vector2)t.Position)) >= MenuClass.Miscellaneous["e"]["Jungleclear"].As<MenuSlider>().Value)
                {
                    SpellClass.E.CastOnUnit(UtilityClass.Player);
                }
            }

            /// <summary>
            ///     The Jungleclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["Jungleclear"]) &&
                MenuClass.Spells["q"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                /*
                var farmLocation = SpellClass.Q.GetLinearFarmLocation(Extensions.GetGenericJungleMinionsTargets(), SpellClass.Q.Width);
                if (farmLocation.MinionsHit >= MenuClass.Miscellaneous["q"]["Jungleclear"].As<MenuSlider>().Value)
                {
                    SpellClass.Q.Cast(farmLocation.Position);
                }
                */
            }
        }

        #endregion
    }
}