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
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            UtilityClass.IOrbwalker.PreAttack += OnPreAttack;
            UtilityClass.IOrbwalker.PostAttack += OnPostAttack;
            //Events.OnGapCloser += OnGapCloser;
            Obj_AI_Base.OnPlayAnimation += OnPlayAnimation;
        }

        #endregion
    }
}