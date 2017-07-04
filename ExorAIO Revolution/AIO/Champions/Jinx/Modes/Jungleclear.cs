#pragma warning disable 1587
namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using Utilities;

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
        public static void Jungleclear(object sender, PreAttackEventArgs args)
        {
            var minionTarget = args.Target as Obj_AI_Minion;
            if (minionTarget == null)
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > MenuClass.Spells["q"]["Jungleclear"].As<MenuSliderBool>().Value &&
                MenuClass.Spells["q"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                var minionsInRange = GameObjects.EnemyMinions.Count(m =>
                    m.Distance(minionTarget) < MenuClass.Spells["q"]["customization"]["splashrange"].Value);

                if (UtilityClass.Player.HasBuff("JinxQ"))
                {
                    if (minionsInRange < MenuClass.Spells["q"]["customization"]["Jungleclear"].Value)
                    {
                        SpellClass.Q.Cast();
                    }
                }
                else
                {
                    if (minionsInRange >= MenuClass.Spells["q"]["customization"]["Jungleclear"].Value)
                    {
                        SpellClass.Q.Cast();
                    }
                }
            }

            /// <summary>
            ///     The Jungleclear W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                minionTarget.IsValidTarget(SpellClass.W.Range) &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["Jungleclear"]) &&
                MenuClass.Spells["w"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.W.Cast(minionTarget);
            }
        }

        #endregion
    }
}