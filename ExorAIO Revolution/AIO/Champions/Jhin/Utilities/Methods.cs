namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Events;

    using AIO.Utilities;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Jhin
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
            Obj_AI_Base.OnProcessSpellCast += this.OnProcessSpellCast;
            ImplementationClass.IOrbwalker.PreAttack += this.OnPreAttack;
            ImplementationClass.IOrbwalker.PostAttack += this.OnPostAttack;
            Dash.HeroDashed += this.OnGapcloser;
            //Obj_AI_Base.OnIssueOrder += this.OnIssueOrder;
        }

        #endregion
    }
}