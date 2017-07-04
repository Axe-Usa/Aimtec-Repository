namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Vayne
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            UtilityClass.IOrbwalker.PreAttack += OnPreAttack;
            UtilityClass.IOrbwalker.PostAttack += OnPostAttack;
            RenderManager.OnPresent += OnPresent;
        }

        #endregion
    }
}