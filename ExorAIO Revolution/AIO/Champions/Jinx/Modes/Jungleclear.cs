
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using AIO.Utilities;

#pragma warning disable 1587
namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Jinx
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on orbwalker action.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public void Jungleclear(object sender, PreAttackEventArgs args)
        {
            var jungleTarget = args.Target as Obj_AI_Minion;
            if (!jungleTarget.IsValidTarget() ||
                !Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget))
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                var minionsInRange = GameObjects.EnemyMinions.Count(
                    m =>
                        m.Distance(jungleTarget) < MenuClass.Spells["q"]["customization"]["splashrange"].Value);

                if (UtilityClass.Player.HasBuff("JinxQ"))
                {
                    if (UtilityClass.Player.ManaPercent()
                            < MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Value ||
                        minionsInRange < MenuClass.Spells["q"]["customization"]["jungleclear"].Value)
                    {
                        SpellClass.Q.Cast();
                    }
                }
                else
                {
                    if (minionsInRange >= MenuClass.Spells["q"]["customization"]["jungleclear"].Value)
                    {
                        SpellClass.Q.Cast();
                    }
                }
            }

            /// <summary>
            ///     The Jungleclear W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                jungleTarget.IsValidTarget(SpellClass.W.Range) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["jungleclear"]) &&
                MenuClass.Spells["w"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.W.Cast(jungleTarget);
            }
        }

        #endregion
    }
}