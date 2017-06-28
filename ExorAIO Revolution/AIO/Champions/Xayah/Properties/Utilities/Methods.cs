namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Orbwalking;

    /// <summary>
    ///     The methods class.
    /// </summary>
    internal partial class Xayah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the methods.
        /// </summary>
        public static void Methods()
        {
            Game.OnUpdate += OnUpdate;
            SpellBook.OnCastSpell += OnCastSpell;
            RenderManager.OnPresent += OnPresent;
            Orbwalker.Implementation.PostAttack += OnPostAttack;
            //Events.OnGapCloser += OnGapCloser;
            GameObject.OnCreate += OnCreate;
            GameObject.OnDestroy += OnDestroy;
        }

        #endregion
    }
}