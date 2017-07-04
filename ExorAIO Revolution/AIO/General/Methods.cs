#pragma warning disable 1587
namespace AIO
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

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
            Orbwalker.Implementation.PreAttack += OnPreAttack;
        }
    }
}
