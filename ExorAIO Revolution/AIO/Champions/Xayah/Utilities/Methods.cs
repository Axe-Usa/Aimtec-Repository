namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Xayah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            SpellBook.OnCastSpell += this.OnCastSpell;
            Render.OnPresent += this.OnPresent;
            ImplementationClass.IOrbwalker.PreAttack += this.OnPreAttack;
            GameObject.OnCreate += this.OnCreate;
            GameObject.OnDestroy += this.OnDestroy;
            Dash.HeroDashed += this.OnGapcloser;
        }

        #endregion
    }
}