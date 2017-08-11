
#pragma warning disable 1587
namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
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
        public void Lasthit(object sender, PreAttackEventArgs args)
        {
            /// <summary>
            ///     The Lasthit Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                > MenuClass.Spells["q"]["lasthit"].As<MenuSliderBool>().Value &&
                MenuClass.Spells["q"]["lasthit"].As<MenuSliderBool>().Enabled)
            {
                if (UtilityClass.Player.HasBuff("JinxQ"))
                {
                    if (!GameObjects.EnemyMinions.Any(
                            m =>
                                m.IsValidTarget(SpellClass.Q2.Range) &&
                                !m.IsValidTarget(UtilityClass.Player.GetFullAttackRange(m)) &&
                                m.GetRealHealth() < UtilityClass.Player.GetAutoAttackDamage(m) + UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q)) &&
                        GameObjects.EnemyMinions.Any(
                            m =>
                                m.IsValidTarget(UtilityClass.Player.GetFullAttackRange(m)) &&
                                m.GetRealHealth() < UtilityClass.Player.GetAutoAttackDamage(m)))
                    {
                        SpellClass.Q.Cast();
                    }
                }
                else
                {
                    if (GameObjects.EnemyMinions.Any(
                            m =>
                                m.IsValidTarget(SpellClass.Q2.Range) &&
                                !m.IsValidTarget(UtilityClass.Player.GetFullAttackRange(m)) &&
                                m.GetRealHealth() < UtilityClass.Player.GetAutoAttackDamage(m) + UtilityClass.Player.GetSpellDamage(m, SpellSlot.Q)) &&
                        !GameObjects.EnemyMinions.Any(
                            m =>
                                m.IsValidTarget(UtilityClass.Player.GetFullAttackRange(m)) &&
                                m.GetRealHealth() < UtilityClass.Player.GetAutoAttackDamage(m)))
                    {
                        SpellClass.Q.Cast();
                    }
                }
            }
        }

        #endregion
    }
}