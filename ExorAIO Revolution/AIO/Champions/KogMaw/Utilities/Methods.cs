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
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            //Events.OnGapCloser += KogMaw.OnGapCloser;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            RenderManager.OnPresent += this.OnPresent;
        }

        #endregion
    }
}