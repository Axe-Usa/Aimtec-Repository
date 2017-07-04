namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class KogMaw
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            //Events.OnGapCloser += KogMaw.OnGapCloser;
            UtilityClass.Orbwalker.PostAttack += OnPostAttack;
            RenderManager.OnPresent += OnPresent;
        }

        #endregion
    }
}