namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Evelynn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            Render.OnPresent += this.OnPresent;
            ImplementationClass.IOrbwalker.OnNonKillableMinion += this.OnNonKillableMinion;
        }

        #endregion
    }
}