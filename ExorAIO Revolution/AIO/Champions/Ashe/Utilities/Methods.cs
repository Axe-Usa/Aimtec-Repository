namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Ashe
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            Render.OnPresent += this.OnPresent;
            AttackableUnit.OnLeaveVisible += this.OnLeaveVisibility;
            Dash.HeroDashed += this.OnGapcloser;

            //Events.OnInterruptableTarget += Ashe.OnInterruptableTarget;
        }

        #endregion
    }
}