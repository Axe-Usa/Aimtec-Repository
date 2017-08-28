using Aimtec;
using Aimtec.SDK.Events;

namespace AIO.Champions
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Syndra
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += OnUpdate;
            GameObject.OnCreate += OnCreate;
            GameObject.OnDestroy += OnDestroy;
            Render.OnPresent += OnPresent;
            SpellBook.OnCastSpell += OnCastSpell;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            Dash.HeroDashed += OnGapcloser;

            //Events.OnInterrupt += this.OnInterrupt;
        }

        #endregion
    }
}