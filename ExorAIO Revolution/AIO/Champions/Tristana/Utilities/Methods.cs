namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Tristana
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            BuffManager.OnAddBuff += this.OnAddBuff;
            ImplementationClass.IOrbwalker.PreAttack += this.OnPreAttack;
            Render.OnPresent += this.OnPresent;
            Dash.HeroDashed += this.OnGapcloser;

            //Events.OnInterruptableTarget += OnInterruptableTarget;
        }

        #endregion
    }
}