
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;

    using Utilities;

    /// <summary>
    ///     The menu class.
    /// </summary>
    internal partial class Tristana
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public static void Menus()
        {
            /// <summary>
            ///     Sets the spells menu.
            /// </summary>
            MenuClass.Spells = new Menu("spells", "Spells");
            {
                /// <summary>
                ///     Sets the menu for the Q.
                /// </summary>
                MenuClass.Q = new Menu("q", "Use Q to:");
                {
                    MenuClass.Q.Add(new MenuBool("combo", "Combo"));
                    MenuClass.Q.Add(new MenuBool("harass", "Harass"));
                    MenuClass.Q.Add(new MenuBool("buildings", "Demolish buildings"));
                    MenuClass.Q.Add(new MenuBool("laneclear", "Laneclear"));
                    MenuClass.Q.Add(new MenuBool("jungleclear", "Jungleclear"));
                }
                MenuClass.Spells.Add(MenuClass.Q);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                MenuClass.W = new Menu("w", "Use W to:");
                {
                    MenuClass.W.Add(new MenuBool("antigrab", "Anti-Grab"));
                    MenuClass.W.Add(new MenuBool("gapcloser", "Anti-Gapcloser"));
                }
                MenuClass.Spells.Add(MenuClass.W);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                MenuClass.E = new Menu("e", "Use E to:");
                {
                    MenuClass.E.Add(new MenuBool("combo", "Combo"));
                    MenuClass.E.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSliderBool("buildings", "Demolish buildings / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the Q Whitelist.
                        /// </summary>
                        MenuClass.WhiteList = new Menu("whitelist", "E Harass: Whitelist");
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                MenuClass.WhiteList.Add(new MenuBool(target.ChampionName.ToLower(), "Harass: " + target.ChampionName));
                            }
                        }
                        MenuClass.E.Add(MenuClass.WhiteList);
                    }
                    else
                    {
                        MenuClass.E.Add(new MenuSeperator("exseparator", "No enemy champions found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.E);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                MenuClass.R = new Menu("r", "Use R to:");
                {
                    MenuClass.R.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.R.Add(new MenuBool("gapcloser", "Anti-Gapcloser"));
                    MenuClass.R.Add(new MenuBool("interrupter", "Interrupt Enemy Channels"));
                }
                MenuClass.Spells.Add(MenuClass.R);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            MenuClass.Miscellaneous = new Menu("miscellaneous", "Miscellaneous");
            {
                MenuClass.Miscellaneous.Add(new MenuBool("focuse", "Focus E-charged enemies"));
            }
            MenuClass.Root.Add(MenuClass.Miscellaneous);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("edmg", "E Damage"));
                MenuClass.Drawings.Add(new MenuBool("w", "W Range", false));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}