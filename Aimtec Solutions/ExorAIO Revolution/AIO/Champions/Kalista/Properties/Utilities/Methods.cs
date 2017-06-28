namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

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
            Orbwalker.Implementation.PreAttack += OnPreAttack;
            Orbwalker.Implementation.PostAttack += OnPostAttack;
            RenderManager.OnPresent += OnPresent;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            Orbwalker.Implementation.OnNonKillableMinion += OnNonKillableMinion;
        }

        #endregion
    }
}