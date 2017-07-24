namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            ImplementationClass.IOrbwalker.PreAttack += this.OnPreAttack;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            Render.OnPresent += this.OnPresent;
            Obj_AI_Base.OnPlayAnimation += this.OnPlayAnimation;

            //Events.OnGapCloser += OnGapCloser;
        }

        #endregion
    }
}