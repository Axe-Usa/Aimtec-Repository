namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Twitch
    {
        #region Public Methods and Operators

        /// <summary>
        ///     The methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            Orbwalker.Implementation.PostAttack += OnPostAttack;
            SpellBook.OnCastSpell += OnCastSpell;
            RenderManager.OnPresent += OnPresent;
        }

        #endregion
    }
}