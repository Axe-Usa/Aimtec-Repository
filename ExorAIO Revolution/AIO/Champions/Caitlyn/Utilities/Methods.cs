namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Caitlyn
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
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;
            Dash.HeroDashed += this.OnGapcloser;

            //Events.OnInterruptableTarget += OnInterruptableTarget;
        }

        #endregion
    }
}