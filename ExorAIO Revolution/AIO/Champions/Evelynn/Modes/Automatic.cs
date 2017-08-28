
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Evelynn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.HasBuffOfType(BuffType.Slow) &&
                MenuClass.Spells["w"]["logical"].As<MenuBool>().Value)
            {
                SpellClass.W.Cast();
            }
        }

        #endregion
    }
}