using Aimtec;
using Aimtec.SDK.Events;
using AIO.Utilities;

namespace AIO.Champions
{
    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Ashe
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public void Methods()
        {
            Game.OnUpdate += OnUpdate;
            ImplementationClass.IOrbwalker.PostAttack += OnPostAttack;
            Render.OnPresent += OnPresent;
            AttackableUnit.OnLeaveVisible += OnLeaveVisibility;
            Dash.HeroDashed += OnGapcloser;

            //Events.OnInterruptableTarget += Ashe.OnInterruptableTarget;
        }

        #endregion
    }
}