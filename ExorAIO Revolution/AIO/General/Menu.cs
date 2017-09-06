
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587
namespace AIO
{
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
                    MenuClass.General.Add(new MenuBool("junglesmall", "Cast to small ones too in Jungleclear", false));

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

                    MenuClass.PreserveMana = new Menu("preservemanamenu", "Preserve Mana Menu");
                    {
                        MenuClass.PreserveMana.Add(new MenuSeperator("separator", "Preserve Mana for:"));
                        foreach (var slot in UtilityClass.SpellSlots)
                        {
                            MenuClass.PreserveMana.Add(new MenuBool(slot.ToString().ToLower(), slot.ToString(), false));
                        }
                    }
                    MenuClass.General.Add(MenuClass.PreserveMana);

                    MenuClass.PreserveSpells = new Menu("preservespellsmenu", "Preserve Spells Menu");
                    {
                        MenuClass.PreserveSpells.Add(new MenuSeperator("separator", "Only works for inside-AA-range targets"));
                        MenuClass.PreserveSpells.Add(new MenuSeperator("separator2", "0 = Don't limit."));
                        foreach (var slot in UtilityClass.SpellSlots)
                        {
                            MenuClass.PreserveSpells.Add(new MenuSlider(slot.ToString().ToLower(), $"Don't cast {slot} if target killable in X AAs", 0, 0, 10));
                        }
                    }
                    MenuClass.General.Add(MenuClass.PreserveSpells);
                }
                MenuClass.Root.Add(MenuClass.General);
            }
            MenuClass.Root.Attach();
        }

        #endregion
    }
}