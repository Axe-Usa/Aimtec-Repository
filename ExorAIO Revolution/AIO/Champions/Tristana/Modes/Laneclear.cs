
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
        public static void Laneclear(object sender, PreAttackEventArgs args)
        {
            /// <summary>
            ///     The Laneclear E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["laneclear"]) &&
                MenuClass.Spells["e"]["laneclear"].As<MenuSliderBool>().Enabled)
            {
                var idealMinion = UtilityClass.GetEnemyLaneMinionsTargets().FirstOrDefault(m => m.IsValidTarget(SpellClass.E.Range) && UtilityClass.GetEnemyLaneMinionsTargets().Count(m2 => m2.Distance(m) < 200f) >= 3);
                if (idealMinion != null)
                {
                    SpellClass.E.Cast(idealMinion);
                }
            }

            var minionTarget = (Obj_AI_Minion)Orbwalker.Implementation.GetTarget();
            if (!UtilityClass.GetEnemyLaneMinionsTargets().Contains(minionTarget))
            {
                return;
            }

            /// <summary>
            ///     The Laneclear Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["laneclear"].As<MenuBool>().Value)
            {
                SpellClass.Q.Cast();
            }
        }

        #endregion
    }
}