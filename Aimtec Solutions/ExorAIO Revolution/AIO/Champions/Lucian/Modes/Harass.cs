
#pragma warning disable 1587

namespace AIO.Champions
{
    using System;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Util;

    using AIO.Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public static void Harass()
        {
            /// <summary>
            ///     The Extended Q Mixed Harass Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["extended"]["mixed"]) &&
                MenuClass.Spells["q"]["extended"]["mixed"].As<MenuSliderBool>().Enabled)
            {
                /*
                foreach (var minion in from minion in UtilityClass.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range)
                    let polygon = new Geometry.Rectangle(UtilityClass.Player.Position, UtilityClass.Player.Position.Extend(minion.Position, SpellClass.Q2.Range), SpellClass.Q2.Width)
                    where polygon.IsInside(SpellClass.Q2.GetPrediction(UtilityClass.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range)))
                    select minion)
                {
                    SpellClass.Q.CastOnUnit(minion);
                }
                */
            }
        }

        #endregion
    }
}