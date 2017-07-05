
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
        #region Public Methods and Operators

        /// <summary>
        ///     Loads the methods.
        /// </summary>
        public static void Methods()
        {
            SpellBook.OnCastSpell += OnCastSpell;
            UtilityClass.IOrbwalker.PreAttack += OnPreAttack;
        }

        #endregion
    }
}