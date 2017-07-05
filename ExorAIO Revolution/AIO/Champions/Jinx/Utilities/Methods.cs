namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Jinx
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            UtilityClass.IOrbwalker.PreAttack += this.OnPreAttack;
            RenderManager.OnPresent += this.OnPresent;
            Obj_AI_Base.OnTeleport += this.OnTeleport;

            //Events.OnGapCloser += OnGapCloser;
            //Events.OnInterruptableTarget += OnInterruptableTarget;
        }

        #endregion
    }
}