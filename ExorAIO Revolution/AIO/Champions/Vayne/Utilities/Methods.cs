namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Vayne
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            ImplementationClass.IOrbwalker.PreAttack += this.OnPreAttack;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            Obj_AI_Base.OnPerformCast += this.OnPerformCast;
            Render.OnPresent += this.OnPresent;
        }

        #endregion
    }
}