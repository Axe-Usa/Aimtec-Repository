namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Ashe
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            UtilityClass.Orbwalker.PostAttack += OnPostAttack;
            RenderManager.OnPresent += OnPresent;
            AttackableUnit.OnLeaveVisible += OnLeaveVisibility;

            //Events.OnGapCloser += Ashe.OnGapCloser;
            //Events.OnInterruptableTarget += Ashe.OnInterruptableTarget;
        }

        #endregion
    }
}