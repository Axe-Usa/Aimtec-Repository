namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class MissFortune
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            ImplementationClass.IOrbwalker.PreAttack += this.OnPreAttack;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            SpellBook.OnCastSpell += this.OnCastSpell;
            Render.OnPresent += this.OnPresent;
            Dash.HeroDashed += this.OnGapcloser;
        }

        #endregion
    }
}