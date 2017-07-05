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
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            UtilityClass.IOrbwalker.PreAttack += this.OnPreAttack;
            UtilityClass.IOrbwalker.PostAttack += this.OnPostAttack;
            RenderManager.OnPresent += this.OnPresent;
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;
            UtilityClass.IOrbwalker.OnNonKillableMinion += this.OnNonKillableMinion;
        }

        #endregion
    }
}