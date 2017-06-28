
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
    internal partial class KogMaw
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Laneclear()
        {
            /// <summary>
            ///     The Laneclear E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["laneclear"]) &&
                MenuClass.Spells["e"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                /*
                var minions = UtilityClass.GetEnemyLaneMinionsTargetsInRange(SpellClass.E.Range);
                var farmLocation = SpellClass.E.GetLineFarmLocation(minions, SpellClass.E.Width);
                if (farmLocation.MinionsHit >= 3)
                {
                    SpellClass.E.Cast(farmLocation.Position);
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
                if (UtilityClass.Player.HasItem(ItemId.RunaansHurricane))
                {
                    ObjectManager.GetLocalPlayer().SpellBook.CastSpell(SpellSlot.W);
                }
            }
        }

        #endregion
    }
}