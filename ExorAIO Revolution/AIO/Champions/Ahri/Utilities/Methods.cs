using Aimtec;
using Aimtec.SDK.Events;

namespace AIO.Champions
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Ahri
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += OnUpdate;
            Render.OnPresent += OnPresent;
            Obj_AI_Base.OnPerformCast += OnPerformCast;
            Dash.HeroDashed += OnGapcloser;
        }

        #endregion
    }
}