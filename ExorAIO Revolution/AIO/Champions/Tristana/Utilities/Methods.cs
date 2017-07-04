namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Tristana
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            BuffManager.OnAddBuff += OnAddBuff;
            UtilityClass.Orbwalker.PreAttack += OnPreAttack;
            RenderManager.OnPresent += OnPresent;

            /*
            Events.OnGapCloser += OnGapCloser;
            Events.OnInterruptableTarget += OnInterruptableTarget;
            */
        }

        #endregion
    }
}