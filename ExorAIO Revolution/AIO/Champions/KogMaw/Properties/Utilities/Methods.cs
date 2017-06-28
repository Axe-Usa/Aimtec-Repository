namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

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
            Orbwalker.Implementation.PostAttack += OnPostAttack;
            RenderManager.OnPresent += OnPresent;
        }

        #endregion
    }
}