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
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            SpellBook.OnCastSpell += OnCastSpell;
            RenderManager.OnPresent += OnPresent;
            UtilityClass.Orbwalker.PostAttack += OnPostAttack;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            Obj_AI_Base.OnTeleport += OnTeleport;

            //Events.OnGapCloser += OnGapCloser;
            //Events.OnInterruptableTarget += OnInterruptableTarget;
        }

        #endregion
    }
}