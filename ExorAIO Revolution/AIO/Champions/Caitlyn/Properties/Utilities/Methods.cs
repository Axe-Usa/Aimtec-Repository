namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Caitlyn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            SpellBook.OnCastSpell += OnCastSpell;
            RenderManager.OnPresent += OnPresent;
            Orbwalker.Implementation.PostAttack += OnPostAttack;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            //Events.OnGapCloser += OnGapCloser;
            //Events.OnInterruptableTarget += OnInterruptableTarget;
        }

        #endregion
    }
}