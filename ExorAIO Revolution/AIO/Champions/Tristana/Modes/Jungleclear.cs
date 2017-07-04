
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Tristana
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public static void Jungleclear(object sender, PreAttackEventArgs args)
        {
            var jungleTarget = (Obj_AI_Minion)UtilityClass.IOrbwalker.GetTarget();
            if (!Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget))
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["Jungleclear"].As<MenuBool>().Value)
            {
                SpellClass.Q.Cast();
            }

            /// <summary>
            ///     The Jungleclear E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["Jungleclear"]) &&
                MenuClass.Spells["e"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.E.Cast(jungleTarget);
            }
        }

        #endregion
    }
}