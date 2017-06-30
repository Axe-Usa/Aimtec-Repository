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
    internal partial class Jinx
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on orbwalker action.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public static void Laneclear(object sender, PreAttackEventArgs args)
        {
            var minionTarget = args.Target as Obj_AI_Minion;
            if (minionTarget == null)
            {
                return;
            }

            /// <summary>
            ///     The Laneclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent() >
                    MenuClass.Spells["q"]["laneclear"].As<MenuSliderBool>().Value &&
                MenuClass.Spells["q"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                var minionsInRange = GameObjects.EnemyMinions.Count(m => m.Distance(minionTarget) < MenuClass.Miscellaneous["qoptions"]["splashrange"].Value);
                if (UtilityClass.Player.HasBuff("JinxQ"))
                {
                    if (minionsInRange < MenuClass.Miscellaneous["qoptions"]["laneclear"].Value)
                    {
                        SpellClass.Q.Cast();
                    }
                }
                else
                {
                    if (minionsInRange >= MenuClass.Miscellaneous["qoptions"]["laneclear"].Value)
                    {
                        SpellClass.Q.Cast();
                    }
                }
            }
        }

        #endregion
    }
}