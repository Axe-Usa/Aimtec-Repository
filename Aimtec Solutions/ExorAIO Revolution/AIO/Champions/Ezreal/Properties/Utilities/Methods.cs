namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Ezreal
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            Orbwalker.Implementation.PostAttack += OnPostAttack;
            //Events.OnGapCloser += OnGapCloser;
            BuffManager.OnAddBuff += OnAddBuff;
            Orbwalker.Implementation.OnNonKillableMinion += OnNonKillableMinion;
            RenderManager.OnPresent += OnPresent;
        }

        #endregion
    }
}