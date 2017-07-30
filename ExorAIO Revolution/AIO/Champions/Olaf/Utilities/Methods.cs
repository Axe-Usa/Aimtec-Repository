namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Olaf
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            GameObject.OnCreate += this.OnCreate;
            GameObject.OnDestroy += this.OnDestroy;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            Render.OnPresent += this.OnPresent;
        }

        #endregion
    }
}