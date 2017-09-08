using Aimtec;
using AIO.Utilities;

namespace AIO.Champions
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += OnUpdate;
            ImplementationClass.IOrbwalker.PreAttack += OnPreAttack;
            ImplementationClass.IOrbwalker.PostAttack += OnPostAttack;
            Render.OnPresent += OnPresent;
            Obj_AI_Base.OnPlayAnimation += OnPlayAnimation;
            Gapcloser.OnGapcloser += OnGapcloser;
        }

        #endregion
    }
}