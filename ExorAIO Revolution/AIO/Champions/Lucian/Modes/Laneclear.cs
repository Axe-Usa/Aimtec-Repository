
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Laneclear()
        {
            /// <summary>
            ///     Extended.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["extendedq"]["laneclear"]) &&
                MenuClass.Spells["extendedq"]["laneclear"].As<MenuSliderBool>().Enabled &&
                Extensions.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range).Any(m => m.IsValidTarget(SpellClass.Q.Range)))
            {
                var target = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range);
                foreach (var minion in from minion in Extensions.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range).Where(m => m.IsValidTarget(SpellClass.Q.Range))
                                       let polygon = new Geometry.Rectangle(
                                                            (Vector2)UtilityClass.Player.ServerPosition,
                                                            (Vector2)UtilityClass.Player.ServerPosition.Extend(minion.ServerPosition, SpellClass.Q2.Range-150f),
                                                            SpellClass.Q2.Width)
                                       where
                                            target != null &&
                                            target != minion &&
                                            polygon.IsInside((Vector2)SpellClass.Q2.GetPrediction(target).CastPosition)
                                       select minion)
                {
                    if (MenuClass.Spells["extendedq"]["whitelist"][target.ChampionName.ToLower()].Enabled)
                    {
                        SpellClass.Q.CastOnUnit(minion);
                    }
                }
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public void Laneclear(object sender, PostAttackEventArgs args)
        {
            /// <summary>
            ///     The E Laneclear Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["laneclear"]) &&
                MenuClass.Spells["e"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.E.Cast(UtilityClass.Player.Position.Extend(Game.CursorPos, UtilityClass.Player.BoundingRadius));
            }

            /// <summary>
            ///     The Laneclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["laneclear"]) &&
                MenuClass.Spells["q"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                /*
                var farmLocation = SpellClass.Q2.GetLineFarmLocation(Extensions.GetEnemyLaneMinionsTargets(), SpellClass.Q2.Width);
                if (farmLocation.MinionsHit >= MenuClass.Spells["q"]["customization"]["laneclear"].As<MenuSlider>().Value)
                {
                    SpellClass.Q.CastOnUnit(farmLocation.FirstMinion);
                    return;
                }
                */
            }

            /// <summary>
            ///     The Laneclear W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["laneclear"]) &&
                MenuClass.Spells["w"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                /*
                var farmLocation = SpellClass.W.GetCircularFarmLocation(Extensions.GetEnemyLaneMinionsTargets(), SpellClass.W.Width);
                if (farmLocation.MinionsHit >= MenuClass.Spells["w"]["customization"]["laneclear"].As<MenuSlider>().Value)
                {
                    SpellClass.W.Cast(farmLocation.Position);
                }
                */
            }
        }

        #endregion
    }
}