
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Jhin
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on post attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public void Jungleclear(object sender, PostAttackEventArgs args)
        {
            var jungleTarget = args.Target as Obj_AI_Minion;
            if (jungleTarget == null ||
                !Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget))
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear Q Logics.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.CastOnUnit(jungleTarget);
            }

            if (jungleTarget.Health < UtilityClass.Player.GetAutoAttackDamage(jungleTarget) * 2)
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear E Logics.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["jungleclear"]) &&
                MenuClass.Spells["e"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.E.Cast(jungleTarget);
            }
        }

        /// <summary>
        ///     Called on post attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public void Jungleclear(object sender, PreAttackEventArgs args)
        {
            var jungleTarget = args.Target as Obj_AI_Minion;
            if (!this.HasFourthShot() ||
                jungleTarget == null ||
                !Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget))
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear Q Logics.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.CastOnUnit(jungleTarget);
            }

            if (jungleTarget.Health < UtilityClass.Player.GetAutoAttackDamage(jungleTarget) * 2)
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear E Logics.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["jungleclear"]) &&
                MenuClass.Spells["e"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.E.Cast(jungleTarget);
            }
        }

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Jungleclear()
        {
            var jungleTarget = (Obj_AI_Minion)ImplementationClass.IOrbwalker.GetTarget();
            if (!this.IsReloading() ||
                !jungleTarget.IsValidTarget() ||
                !Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget))
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear Q Logics.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.CastOnUnit(jungleTarget);
            }

            if (jungleTarget.Health < UtilityClass.Player.GetAutoAttackDamage(jungleTarget) * 2)
            {
                return;
            }

            /// <summary>
            ///     The Jungleclear E Logics.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["jungleclear"]) &&
                MenuClass.Spells["e"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.E.Cast(jungleTarget);
            }
        }

        #endregion
    }
}