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
            UtilityClass.IOrbwalker.PreAttack += this.OnPreAttack;
            UtilityClass.IOrbwalker.PostAttack += this.OnPostAttack;
            //Events.OnGapCloser += OnGapCloser;
            Obj_AI_Base.OnPlayAnimation += this.OnPlayAnimation;
        }

        #endregion
    }
}