#pragma warning disable 1587
namespace AIO
{
    using Aimtec;

    using AIO.Utilities;

    /// <summary>
    ///     The general class.
    /// </summary>
    internal partial class General
    {
        /// <summary>
        ///     Loads the methods.
        /// </summary>
        public static void Methods()
        {
            SpellBook.OnCastSpell += OnCastSpell;
            UtilityClass.Orbwalker.PreAttack += OnPreAttack;
        }
    }
}
