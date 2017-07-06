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
            SpellBook.OnCastSpell += this.OnCastSpell;
            Obj_AI_Base.OnTeleport += this.OnTeleport;
            RenderManager.OnPresent += this.OnPresent;
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;
            ImplementationClass.IOrbwalker.PreMove += this.OnPreMove;
            ImplementationClass.IOrbwalker.PreAttack += this.OnPreAttack;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            ImplementationClass.IOrbwalker.OnNonKillableMinion += this.OnNonKillableMinion;

            //Events.OnGapCloser += OnGapCloser;
        }

        #endregion
    }
}