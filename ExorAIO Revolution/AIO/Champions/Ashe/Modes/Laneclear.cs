
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ashe
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Laneclear()
        {
            /// <summary>
            ///     The Laneclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.HasBuff("AsheQCastReady") &&
                UtilityClass.Player.HasItem(ItemId.RunaansHurricane)&&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["laneclear"]) &&
                MenuClass.Spells["q"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.Cast();
            }

            /// <summary>
            ///     The Laneclear W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["w"]["laneclear"]) &&
                MenuClass.Spells["w"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                /*
                var minions = UtilityClass.GetEnemyLaneMinionsTargetsInRange(SpellClass.W.Range);
                var farmLocation = SpellClass.W.GetConicalFarmLocation(minions, SpellClass.W.Width);
                if (farmLocation.MinionsHit >= 3)
                {
                    SpellClass.W.Cast(farmLocation.Position);
                }
                */
            }
        }

        #endregion
    }
}