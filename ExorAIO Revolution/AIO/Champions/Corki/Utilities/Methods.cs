namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Corki
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            RenderManager.OnPresent += this.OnPresent;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            ImplementationClass.IOrbwalker.OnNonKillableMinion += this.OnNonKillableMinion;
        }

        #endregion
    }
}