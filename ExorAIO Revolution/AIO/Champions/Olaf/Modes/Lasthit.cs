#pragma warning disable 1587
namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Olaf
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Lasthit()
        {
            /// <summary>
            ///     The E Big Minions Lasthit Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["lasthit"].As<MenuBool>().Enabled)
            {
                foreach (var minion in Extensions.GetEnemyLaneMinionsTargetsInRange(SpellClass.E.Range).Where(m =>
                    (m.UnitSkinName.Contains("Siege") || m.UnitSkinName.Contains("Super")) &&
                    m.Health < UtilityClass.Player.GetSpellDamage(m, SpellSlot.E)))
                {
                    SpellClass.E.CastOnUnit(minion);
                }
            }
        }

        #endregion
    }
}