
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Akali
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called onpostattack.
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
            ///     The Q Jungleclear Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                Extensions.GetGenericJungleMinionsTargetsInRange(SpellClass.Q.Range).Any() &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["Jungleclear"]) &&
                MenuClass.Spells["q"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.CastOnUnit(Extensions.GetGenericJungleMinionsTargetsInRange(SpellClass.Q.Range).First());
            }

            /// <summary>
            ///     The E Jungleclear Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["Jungleclear"]) &&
                MenuClass.Spells["e"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.E.Cast();
            }
        }

        #endregion
    }
}