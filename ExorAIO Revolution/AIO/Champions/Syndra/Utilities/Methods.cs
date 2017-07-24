namespace AIO.Champions
{
    using Aimtec;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Syndra
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            GameObject.OnCreate += this.OnCreate;
            GameObject.OnDestroy += this.OnDestroy;
            Render.OnPresent += this.OnPresent;
            SpellBook.OnCastSpell += this.OnCastSpell;
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;

            //Events.OnGapCloser += this.OnGapCloser;
            //Events.OnInterrupt += this.OnInterrupt;
        }

        #endregion
    }
}