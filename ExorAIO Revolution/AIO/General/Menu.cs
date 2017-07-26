
#pragma warning disable 1587
namespace AIO
{
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The general class.
    /// </summary>
    internal partial class General
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Loads the menu.
        /// </summary>
        public static void Menu()
        {
            /// <summary>
            ///     Loads the root Menu.
            /// </summary>
            MenuClass.Root = new Menu(UtilityClass.Player.ChampionName.ToLower(), "ExorAIO Revolution: " + UtilityClass.Player.ChampionName, true);
            {
                /// <summary>
                ///     Loads the orbwalker menu.
                /// </summary>
                ImplementationClass.IOrbwalker.Attach(MenuClass.Root);

                /// <summary>
                ///     Loads the general menu.
                /// </summary>
                MenuClass.General = new Menu("generalmenu", "General Menu");
                {
                    MenuClass.General.Add(new MenuBool("supportmode", "Support Mode", false));
                    MenuClass.General.Add(new MenuBool("disableaa", "Disable AutoAttacks in Combo", false));
                    if (UtilityClass.Player.MaxMana >= 200)
                    {
                        MenuClass.General.Add(new MenuBool("nomanagerifblue", "Ignore ManaManagers if has Blue Buff", false));
                    }
                    if (UtilityClass.Player.IsMelee)
                    {
                        MenuClass.Hydra = new Menu("hydramenu", "Hydras Menu");
                        {
                            MenuClass.Hydra.Add(new MenuBool("combo", "Use Hydras in Combo"));
                            MenuClass.Hydra.Add(new MenuBool("laneclear", "Use Hydras in Laneclear"));
                            MenuClass.Hydra.Add(new MenuBool("mixed", "Use Hydras in Harass"));
                            MenuClass.Hydra.Add(new MenuBool("lasthit", "Use Hydras in Lasthit"));
                        }
                        MenuClass.General.Add(MenuClass.Hydra);
                    }
                }
                MenuClass.Root.Add(MenuClass.General);
            }
            MenuClass.Root.Attach();
        }

        #endregion
    }
}