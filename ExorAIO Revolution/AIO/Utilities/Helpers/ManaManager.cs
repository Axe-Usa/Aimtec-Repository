using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;

namespace AIO.Utilities
{
    /// <summary>
    ///     The Mana manager class.
    /// </summary>
    internal class ManaManager
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The minimum mana needed to cast the Spell from the 'slot' SpellSlot.
        /// </summary>
        public static int GetNeededMana(SpellSlot slot, IMenuComponent value)
        {
            var ignoreManaManagerMenu = MenuClass.General["nomanagerifblue"];
            if (ignoreManaManagerMenu != null &&
                UtilityClass.Player.HasBuff("crestoftheancientgolem") &&
                ignoreManaManagerMenu.As<MenuBool>().Enabled)
            {
                return 0;
            }

            var cost = UtilityClass.Player.SpellBook.GetSpell(slot).Cost;
            return
                value.As<MenuSliderBool>().Value +
                (int)(cost / UtilityClass.Player.MaxMana * 100);
        }

        /// <summary>
        ///     The minimum mana needed to cast the Spell from the 'slot' SpellSlot.
        /// </summary>
        public int GetNeededHealth(SpellSlot slot, MenuComponent value)
        {
            var cost = UtilityClass.Player.SpellBook.GetSpell(slot).Cost;
            return
                value.As<MenuSliderBool>().Value +
                (int)(cost / UtilityClass.Player.MaxHealth * 100);
        }

        #endregion
    }
}