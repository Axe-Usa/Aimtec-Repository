namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Jinx
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            Orbwalker.Implementation.PreAttack += OnPreAttack;
            RenderManager.OnPresent += OnPresent;
            Obj_AI_Base.OnTeleport += OnTeleport;

            //Events.OnGapCloser += OnGapCloser;
            //Events.OnInterruptableTarget += OnInterruptableTarget;
        }

        #endregion
    }
}