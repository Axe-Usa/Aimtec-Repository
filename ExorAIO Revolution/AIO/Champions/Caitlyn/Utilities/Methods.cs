namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Caitlyn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            SpellBook.OnCastSpell += this.OnCastSpell;
            RenderManager.OnPresent += this.OnPresent;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;
            Obj_AI_Base.OnTeleport += this.OnTeleport;

            //Events.OnGapCloser += OnGapCloser;
            //Events.OnInterruptableTarget += OnInterruptableTarget;
        }

        #endregion
    }
}