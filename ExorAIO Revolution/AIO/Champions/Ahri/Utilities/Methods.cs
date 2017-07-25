namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

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
            Render.OnPresent += this.OnPresent;
            Obj_AI_Base.OnPerformCast += this.OnPerformCast;
            Dash.HeroDashed += this.OnGapcloser;
        }

        #endregion
    }
}