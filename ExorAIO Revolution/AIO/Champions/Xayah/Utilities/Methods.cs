using Aimtec;
using Aimtec.SDK.Events;
using AIO.Utilities;

namespace AIO.Champions
{
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
            Game.OnUpdate += OnUpdate;
            SpellBook.OnCastSpell += OnCastSpell;
            Render.OnPresent += OnPresent;
            ImplementationClass.IOrbwalker.PreAttack += OnPreAttack;
            GameObject.OnCreate += OnCreate;
            GameObject.OnDestroy += OnDestroy;
            Dash.HeroDashed += OnGapcloser;
        }

        #endregion
    }
}