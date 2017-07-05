namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Ahri
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            RenderManager.OnPresent += this.OnPresent;

            //Events.OnGapCloser += OnGapCloser;
        }

        #endregion
    }
}