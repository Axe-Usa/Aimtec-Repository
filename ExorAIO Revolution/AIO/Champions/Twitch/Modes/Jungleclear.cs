
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Twitch
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public void Jungleclear(object sender, PostAttackEventArgs args)
        {
            var jungleTarget = (Obj_AI_Minion)UtilityClass.IOrbwalker.GetTarget();
            if (!Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget))
            {
                return;
            }

            /// <summary>
            ///     The W Jungleclear Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.ManaPercent()
                > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["Jungleclear"]) &&
                MenuClass.Spells["w"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.W.Cast(jungleTarget.Position);
                return;
            }

            /// <summary>
            ///     The Q Jungleclear Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["Jungleclear"]) &&
                MenuClass.Spells["q"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.Cast();
            }
        }

        #endregion
    }
}