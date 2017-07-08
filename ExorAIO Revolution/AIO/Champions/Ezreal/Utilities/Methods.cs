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
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            BuffManager.OnAddBuff += this.OnAddBuff;
            ImplementationClass.IOrbwalker.OnNonKillableMinion += this.OnNonKillableMinion;
            Render.OnPresent += this.OnPresent;

            //Events.OnGapCloser += OnGapCloser;
        }

        #endregion
    }
}