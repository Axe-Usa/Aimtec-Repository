namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Jhin
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;
            RenderManager.OnPresent += this.OnPresent;
            ImplementationClass.IOrbwalker.PreAttack += this.OnPreAttack;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            Obj_AI_Base.OnTeleport += this.OnTeleport;
            SpellBook.OnCastSpell += this.OnCastSpell;
            ImplementationClass.IOrbwalker.OnNonKillableMinion += this.OnNonKillableMinion;

            //Events.OnGapCloser += OnGapCloser;
        }

        #endregion
    }
}