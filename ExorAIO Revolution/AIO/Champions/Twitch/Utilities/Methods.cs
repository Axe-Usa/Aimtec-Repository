namespace AIO.Champions
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Twitch
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            UtilityClass.IOrbwalker.PostAttack += this.OnPostAttack;
            SpellBook.OnCastSpell += this.OnCastSpell;
            RenderManager.OnPresent += this.OnPresent;
        }

        #endregion
    }
}