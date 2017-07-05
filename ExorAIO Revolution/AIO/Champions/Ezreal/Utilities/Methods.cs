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
            UtilityClass.IOrbwalker.PostAttack += this.OnPostAttack;
            BuffManager.OnAddBuff += this.OnAddBuff;
            UtilityClass.IOrbwalker.OnNonKillableMinion += this.OnNonKillableMinion;
            RenderManager.OnPresent += this.OnPresent;

            //Events.OnGapCloser += OnGapCloser;
        }

        #endregion
    }
}