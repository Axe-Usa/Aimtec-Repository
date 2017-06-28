namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

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
            Orbwalker.Implementation.PreAttack += OnPreAttack;
            Orbwalker.Implementation.PostAttack += OnPostAttack;
            RenderManager.OnPresent += OnPresent;
        }

        #endregion
    }
}