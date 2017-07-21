namespace NabbTracker
{
    using Aimtec.SDK.Menu;

    /// <summary>
    ///     The Utility class.
    /// </summary>
    internal class MenuClass
    {
        #region Public Properties

        /// <summary>
        ///     The Colorblind Menu.
        /// </summary>
        public static Menu ColorblindMenu { internal get; set; }

        /// <summary>
        ///     The ExpTracker Menu.
        /// </summary>
        public static Menu ExpTracker { internal get; set; }

        /// <summary>
        ///     The Main Menu.
        /// </summary>
        public static Menu Menu { internal get; set; }

        /// <summary>
        ///     The Miscellaneous Menu.
        /// </summary>
        public static Menu Miscellaneous { internal get; set; }

        /// <summary>
        ///     The SpellTracker Menu.
        /// </summary>
        public static Menu SpellTracker { internal get; set; }

        /// <summary>
        ///     The TowerRangeTracker Menu.
        /// </summary>
        public static Menu TowerRangeTracker { internal get; set; }

        #endregion
    }
}