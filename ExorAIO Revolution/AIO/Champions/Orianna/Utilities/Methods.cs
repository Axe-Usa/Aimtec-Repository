namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Orianna
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            SpellBook.OnCastSpell += this.OnCastSpell;
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;
            Render.OnPresent += this.OnPresent;
            Dash.HeroDashed += this.OnGapcloser;

            //Events.OnInterruptableTarget += OnInterruptableTarget;
        }

        #endregion
    }
}