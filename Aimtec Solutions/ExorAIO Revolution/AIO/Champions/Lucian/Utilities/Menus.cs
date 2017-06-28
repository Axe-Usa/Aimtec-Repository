
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Util;

    using AIO.Utilities;

    /// <summary>
    ///     The menu class.
    /// </summary>
    internal partial class Lucian
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
                    MenuClass.Q.Add(new MenuBool("killsteal", "Killsteal"));
                    MenuClass.Q.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    {
                        /// <summary>
                        ///     Sets the Extended Q menu.
                        /// </summary>
                        MenuClass.Q2 = new Menu("extended", "Use Extended Q in:");
                        {
                            MenuClass.Q2.Add(new MenuBool("combo", "Combo"));
                            MenuClass.Q2.Add(new MenuBool("killsteal", "Killsteal"));
                            MenuClass.Q2.Add(new MenuSliderBool("mixed", "Harass / if Mana >= %", true, 50, 0, 99));
                            MenuClass.Q2.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= %", true, 50, 0, 99));
                        }
                        MenuClass.Q.Add(MenuClass.Q2);

                        if (GameObjects.EnemyHeroes.Any())
                        {
                            /// <summary>
                            ///     Sets the Whitelist menu for the Extended Q.
                            /// </summary>
                            MenuClass.WhiteList = new Menu("whitelist", "Extended Harass: Whitelist");
                            {
                                MenuClass.WhiteList.Add(new MenuSeperator("extendedsep", "Note: The Whitelist only works for Mixed and Laneclear."));
                                foreach (var target in GameObjects.EnemyHeroes)
                                {
                                    MenuClass.WhiteList.Add(new MenuBool(target.ChampionName.ToLower(), "Harass: " + target.ChampionName));
                                }
                            }
                            MenuClass.Q.Add(MenuClass.WhiteList);
                        }
                        else
                        {
                            MenuClass.Q.Add(new MenuSeperator("exseparator", "No enemy champions found, no need for a Whitelist Menu."));
                        }
                    }
                }
                MenuClass.Spells.Add(MenuClass.Q);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                MenuClass.W = new Menu("w", "Use W to:");
                {
                    MenuClass.W.Add(new MenuBool("combo", "Combo"));
                    MenuClass.W.Add(new MenuBool("killsteal", "Killsteal"));
                    MenuClass.W.Add(new MenuSliderBool("Laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("buildings", "Demolish buildings / if Mana >= x%", true, 50, 0, 99));
                }
                MenuClass.Spells.Add(MenuClass.W);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                MenuClass.E = new Menu("e", "Use E to:");
                {
                    MenuClass.E.Add(new MenuSeperator("esep1", "[THE DASH WILL ALWAYS BE DIRECTED TOWARDS YOUR MOUSE]"));
                    MenuClass.E.Add(new MenuSeperator("esep2", "Exory: Smart dynamic Short & Long dash."));
                    MenuClass.E.Add( new MenuSeperator("esep3", "Always Long: Always dash at the maximum distance."));
                    MenuClass.E.Add(new MenuSeperator("esep4", "Always Short: This Logic will make you always dash at the minimum distance."));
                    MenuClass.E.Add(new MenuSeperator("esep5", "None: It will not use E in Combo."));

                    MenuClass.E.Add(new MenuList("mode", "E Mode", new[] { "Exory", "Always Long", "Always Short", "Don't use E in Combo" }, 1));
                    MenuClass.E.Add(new MenuBool("engage", "Engage"));
                    MenuClass.E.Add(new MenuBool("gapcloser", "Anti-Gapcloser"));
                    MenuClass.E.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSliderBool("buildings", "Demolish buildings / if Mana >= x%", true, 50, 0, 99));
                }
                MenuClass.Spells.Add(MenuClass.E);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                MenuClass.R = new Menu("r", "Use R to:");
                {
                    MenuClass.R.Add(new MenuSeperator("separator", "How does it work:"));
                    MenuClass.R.Add(new MenuSeperator("separator2", "Keep the button pressed until you want to stop the ultimate."));
                    MenuClass.R.Add(new MenuSeperator("separator3", "You don't have to press both Spacebar and the Semi-Automatic key."));
                    MenuClass.R.Add(new MenuSeperator("separator4", "It automatically orbwalks while using his R, so just press the key."));

                    MenuClass.R.Add(new MenuBool("bool", "Semi-Automatic R"));
                    MenuClass.R.Add(new MenuKeyBind("key", "Key:", KeyCode.T, KeybindType.Press));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the R Whitelist.
                        /// </summary>
                        MenuClass.WhiteList2 = new Menu("whitelist", "Ultimate: Whitelist");
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                MenuClass.WhiteList2.Add(new MenuBool(target.ChampionName.ToLower(), "Use against: " + target.ChampionName));
                            }
                        }

                        MenuClass.R.Add(MenuClass.WhiteList2);
                    }
                    else
                    {
                        MenuClass.R.Add(new MenuSeperator("exseparator", "No enemy champions found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.R);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("q", "Q Range", false));
                MenuClass.Drawings.Add(new MenuBool("qe", "Extended Q Range", false));
                MenuClass.Drawings.Add(new MenuBool("w", "W Range", false));
                MenuClass.Drawings.Add(new MenuBool("e", "E Range", false));
                MenuClass.Drawings.Add(new MenuBool("r", "R Range", false));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}