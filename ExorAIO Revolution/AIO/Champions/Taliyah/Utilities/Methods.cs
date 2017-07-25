namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Taliyah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += this.OnUpdate;
            GameObject.OnCreate += this.OnCreate;
            GameObject.OnDestroy += this.OnDestroy;
            SpellBook.OnCastSpell += this.OnCastSpell;
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;
            Render.OnPresent += this.OnPresent;
            Dash.HeroDashed += this.OnGapcloser;

            //Events.OnInterruptableTarget += Taliyah.OnInterruptableTarget;
        }

        #endregion
    }
}