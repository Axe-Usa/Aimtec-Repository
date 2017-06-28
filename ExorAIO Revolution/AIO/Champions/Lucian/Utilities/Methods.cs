namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

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
            Orbwalker.Implementation.PreAttack += OnPreAttack;
            Orbwalker.Implementation.PostAttack += OnPostAttack;
            //Events.OnGapCloser += OnGapCloser;
            Obj_AI_Base.OnPlayAnimation += OnPlayAnimation;
        }

        #endregion
    }
}