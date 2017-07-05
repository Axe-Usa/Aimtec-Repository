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
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            GameObject.OnCreate += this.OnCreate;
            GameObject.OnDestroy += this.OnDestroy;
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;
            RenderManager.OnPresent += this.OnPresent;

            /*
            Events.OnGapCloser += Taliyah.OnGapCloser;
            Events.OnInterruptableTarget += Taliyah.OnInterruptableTarget;
            */
        }

        #endregion
    }
}