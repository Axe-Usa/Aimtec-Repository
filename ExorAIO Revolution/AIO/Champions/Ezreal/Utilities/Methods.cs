namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

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
            Obj_AI_Base.OnProcessAutoAttack += this.OnProcessAutoAttack;
            Dash.HeroDashed += this.OnGapcloser;

            //Events.OnGapCloser += OnGapCloser;
        }

        #endregion
    }
}