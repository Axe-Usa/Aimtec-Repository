
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
                UtilityClass.IOrbwalker.Attach(MenuClass.Root);

                /// <summary>
                ///     Loads the general menu.
                /// </summary>
                MenuClass.General = new Menu("generalmenu", "General Menu");
                {
                    MenuClass.General.Add(new MenuSeperator("supportseparator1", "If enabled, the character will not autoattack"));
                    MenuClass.General.Add(new MenuSeperator("supportseparator2", "or cast spells on minions if any ally is in range."));
                    MenuClass.General.Add(new MenuBool("supportmode", "Support Mode", false));

                    MenuClass.General.Add(new MenuSeperator("aacomboseparator1", "If enabled, the character will not autoattack while in Combo Mode"));
                    MenuClass.General.Add(new MenuSeperator("aacomboseparator2", "Except if the player has a Sheen-like Autoattack Buff"));
                    MenuClass.General.Add(new MenuSeperator("aacomboseparator3", "E.G. Lichbane or Sona/Diana's AA Passive."));
                    MenuClass.General.Add(new MenuBool("disableaa", "Disable AutoAttacks in Combo", false));

                    MenuClass.General.Add(new MenuSeperator("sheenseparator1", "If enabled, the character will not cast spells on"));
                    MenuClass.General.Add(new MenuSeperator("sheenseparator2", "targets in autoattack range, if the player can"));
                    MenuClass.General.Add(new MenuSeperator("sheenseparator3", "AutoAttack and it has a Sheen-Like item/passive."));
                    MenuClass.General.Add(new MenuBool("usesheen", "Use Sheen Weaving Power"));
                }
                MenuClass.Root.Add(MenuClass.General);
            }
            MenuClass.Root.Attach();
        }

        #endregion
    }
}