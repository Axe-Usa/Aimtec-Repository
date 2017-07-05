namespace AIO.Utilities
{
    using Aimtec;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;

    /// <summary>
    ///     The Mana manager class.
    /// </summary>
    internal class ManaManager
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The minimum mana needed to cast the Spell from the 'slot' SpellSlot.
        /// </summary>
        public static int GetNeededMana(SpellSlot slot, MenuComponent value)
        {
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