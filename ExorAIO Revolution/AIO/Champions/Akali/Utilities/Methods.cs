namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Akali
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            ImplementationClass.IOrbwalker.PreAttack += this.OnPreAttack;
            Render.OnPresent += this.OnPresent;
            SpellBook.OnCastSpell += this.OnCastSpell;
        }

        #endregion
    }
}