namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Kalista
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            UtilityClass.IOrbwalker.PreAttack += OnPreAttack;
            UtilityClass.IOrbwalker.PostAttack += OnPostAttack;
            RenderManager.OnPresent += OnPresent;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            UtilityClass.IOrbwalker.OnNonKillableMinion += OnNonKillableMinion;
        }

        #endregion
    }
}