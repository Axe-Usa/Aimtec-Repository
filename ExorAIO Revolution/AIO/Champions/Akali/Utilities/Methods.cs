namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Akali
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
        }

        #endregion
    }
}