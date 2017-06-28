
#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Caitlyn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Laneclear()
        {
            /*
            /// <summary>
            ///     The Laneclear Q Logics.
            /// </summary>
            if (SpellClass.Q.Ready)
            {
                /*
                /// <summary>
                ///     The Laneclear Q Logic.
                /// </summary>
                if (SpellClass.Q.Ready &&
                    UtilityClass.Player.ManaPercent() >
                        ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["laneclear"]) &&
                    MenuClass.Spells["q"]["laneclear"].As<MenuSliderBool>().Enabled)
                {
                    var farmLocation = SpellClass.Q.GetLineFarmLocation(UtilityClass.GetEnemyLaneMinionsTargets(), SpellClass.Q.Width);
                    if (farmLocation.MinionsHit >= 3)
                    {
                        SpellClass.Q.Cast(farmLocation.Position);
                    }
                }
            }
            */
        }

        #endregion
    }
}