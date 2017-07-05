namespace AIO.Champions
{
    using Aimtec;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Orianna
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            SpellBook.OnCastSpell += this.OnCastSpell;
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;
            RenderManager.OnPresent += this.OnPresent;

            /*
            Events.OnGapCloser += OnGapCloser;
            Events.OnInterruptableTarget += OnInterruptableTarget;
            */
        }

        #endregion
    }
}