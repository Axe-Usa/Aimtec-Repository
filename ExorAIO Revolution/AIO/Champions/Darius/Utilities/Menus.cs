
using System.Linq;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal partial class Darius
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the menus.
        /// </summary>
        public void Menus()
        {
            /// <summary>
            ///     Sets the menu for the spells.
            /// </summary>
            MenuClass.Spells = new Menu("spells", "Spells");
            {
                /// <summary>
                ///     Sets the menu for the Q.
                /// </summary>
                MenuClass.Q = new Menu("q", "Use Q to:");
                {
                    MenuClass.Q.Add(new MenuBool("combo", "Combo"));
                    if (GameObjects.EnemyHeroes.Count() >= 2)
                    {
                        MenuClass.Q.Add(new MenuSliderBool("aoe", "AoE / If can hit >= x enemies", true, 2, 2, GameObjects.EnemyHeroes.Count()));
                    }
                    else
                    {
                        MenuClass.Q.Add(new MenuSeperator("separator", "AoE / Not enough enemies found"));
                    }
                    MenuClass.Q.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.Q.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the Q spell.
                    /// </summary>
                    MenuClass.Q2 = new Menu("customization", "Q Customization:");
                    {
                        MenuClass.Q3 = new Menu("qmodes", "Q Modes Menu:");
                        {
                            MenuClass.Q3.Add(new MenuList("combo", "Q Combo Mode:", new [] { "Only Blade", "Always cast Q" }, 0));
                            MenuClass.Q3.Add(new MenuList("harass", "Q Harass Mode:", new[] { "Only Blade",  "Always cast Q" }, 1));
                        }
                        MenuClass.Q2.Add(MenuClass.Q3);

                        MenuClass.Q2.Add(new MenuBool("onlyqafterw", "Only Q after W in Combo"));
                        MenuClass.Q2.Add(new MenuSlider("laneclear", "Only Laneclear if hittable minions >= x%", 3, 1, 10));
                    }
                    MenuClass.Q.Add(MenuClass.Q2);

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the Q Harass Whitelist.
                        /// </summary>
                        MenuClass.WhiteList = new Menu("whitelist", "Harass: Whitelist");
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                MenuClass.WhiteList.Add(new MenuBool(target.ChampionName.ToLower(), "Harass: " + target.ChampionName));
                            }
                        }
                        MenuClass.Q.Add(MenuClass.WhiteList);
                    }
                    else
                    {
                        MenuClass.Q.Add(new MenuSeperator("exseparator", "No enemies found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.Q);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                MenuClass.W = new Menu("w", "Use W to:");
                {
                    MenuClass.W.Add(new MenuBool("combo", "Combo"));
                    MenuClass.W.Add(new MenuSliderBool("buildings", "Demolish buildings / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the W Harass Whitelist.
                        /// </summary>
                        MenuClass.WhiteList2 = new Menu("whitelist", "Harass: Whitelist");
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                MenuClass.WhiteList2.Add(new MenuBool(target.ChampionName.ToLower(), "Harass: " + target.ChampionName));
                            }
                        }
                        MenuClass.W.Add(MenuClass.WhiteList2);
                    }
                    else
                    {
                        MenuClass.W.Add(new MenuSeperator("exseparator", "No enemies found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.W);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                MenuClass.E = new Menu("e", "Use E to:");
                {
                    MenuClass.E.Add(new MenuBool("combo", "Combo"));
                    MenuClass.E.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the E Harass Whitelist.
                        /// </summary>
                        MenuClass.WhiteList3 = new Menu("whitelist", "Harass: Whitelist");
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                MenuClass.WhiteList3.Add(new MenuBool(target.ChampionName.ToLower(), "Harass: " + target.ChampionName));
                            }
                        }
                        MenuClass.E.Add(MenuClass.WhiteList3);
                    }
                    else
                    {
                        MenuClass.E.Add(new MenuSeperator("exseparator", "No enemies found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.E);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                MenuClass.R = new Menu("r", "Use R to:");
                {
                    MenuClass.R.Add(new MenuBool("killsteal", "KillSteal"));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the R Whitelist.
                        /// </summary>
                        MenuClass.WhiteList4 = new Menu("whitelist", "Ultimate: Whitelist Menu");
                        {
                            //MenuClass.WhiteList.Add(new MenuSeperator("separator1", "WhiteList only works for Combo"));
                            //MenuClass.WhiteList.Add(new MenuSeperator("separator2", "not Killsteal (Automatic)"));
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                MenuClass.WhiteList4.Add(new MenuBool(target.ChampionName.ToLower(), "Use against: " + target.ChampionName));
                            }
                        }
                        MenuClass.R.Add(MenuClass.WhiteList4);
                    }
                    else
                    {
                        MenuClass.R.Add(new MenuSeperator("exseparator", "No enemies found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.R);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the menu for the drawings.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("q", "Q Range", false));
                MenuClass.Drawings.Add(new MenuBool("e", "E Range", false));
                MenuClass.Drawings.Add(new MenuBool("r", "R Range", false));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}