namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

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
            UtilityClass.IOrbwalker.PostAttack += OnPostAttack;
            BuffManager.OnAddBuff += OnAddBuff;
            UtilityClass.IOrbwalker.OnNonKillableMinion += OnNonKillableMinion;
            RenderManager.OnPresent += OnPresent;

            //Events.OnGapCloser += OnGapCloser;
        }

        #endregion
    }
}