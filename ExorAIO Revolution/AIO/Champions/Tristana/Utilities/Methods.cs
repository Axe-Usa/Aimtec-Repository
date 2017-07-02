namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

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
            Orbwalker.Implementation.PreAttack += OnPreAttack;
            RenderManager.OnPresent += OnPresent;

            /*
            Events.OnGapCloser += OnGapCloser;
            Events.OnInterruptableTarget += OnInterruptableTarget;
            */
        }

        #endregion
    }
}