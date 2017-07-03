namespace AIO.Champions
{
    using Aimtec;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Taliyah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            GameObject.OnCreate += OnCreate;
            GameObject.OnDestroy += OnDestroy;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            RenderManager.OnPresent += OnPresent;

            /*
            Events.OnGapCloser += Taliyah.OnGapCloser;
            Events.OnInterruptableTarget += Taliyah.OnInterruptableTarget;
            */
        }

        #endregion
    }
}