
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
    internal partial class Orianna
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
                    MenuClass.Q.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.Q.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("jungleclear", "JungleClear / if Mana >= x%", true, 50, 0, 99));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the Q Whitelist.
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
                        MenuClass.Q.Add(new MenuSeperator("exseparator", "No enemy champions found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.Q);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                MenuClass.W = new Menu("w", "Use W to:");
                {
                    MenuClass.W.Add(new MenuBool("combo", "Combo"));
                    MenuClass.W.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.W.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("jungleclear", "JungleClear / if Mana >= x%", true, 50, 0, 99));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the W Whitelist.
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
                        MenuClass.W.Add(new MenuSeperator("exseparator", "No enemy champions found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.W);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                MenuClass.E = new Menu("e", "Use E to:");
                {
                    MenuClass.E.Add(new MenuBool("combo", "Combo"));
                    MenuClass.E.Add(new MenuBool("logical", "Shield Allies"));
                    MenuClass.E.Add(new MenuBool("gapcloser", "Anti-Gapcloser"));
                    MenuClass.E.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSliderBool("jungleclear", "JungleClear / if Mana >= x%", true, 50, 0, 99));
                    {
                        /// <summary>
                        ///     Sets the whitelist menu for the E.
                        /// </summary>
                        MenuClass.WhiteList = new Menu("whitelist", "Shield Allies: Whitelist Menu", true);
                        {
                            foreach (var ally in GameObjects.AllyHeroes)
                            {
                                MenuClass.WhiteList.Add(new MenuBool(ally.ChampionName.ToLower(), "Only use for: " + ally.ChampionName));
                            }
                        }
                        MenuClass.E.Add(MenuClass.WhiteList);
                    }
                }
                MenuClass.Spells.Add(MenuClass.E);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                MenuClass.R = new Menu("r", "Use R to:");
                {
                    MenuClass.R.Add(new MenuBool("interrupter", "Interrupt Enemy Channels"));
                    MenuClass.R.Add(new MenuSliderBool("aoe", "AoE / If can hit >= x enemies", true, 2, 1, GameObjects.EnemyHeroes.Count()));
                }
                MenuClass.Spells.Add(MenuClass.R);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            MenuClass.Miscellaneous = new Menu("miscellaneous", "Miscellaneous");
            {
                MenuClass.Miscellaneous.Add(new MenuBool("speedw", "Use W to speed Orianna up while chasing in combo", false));
                MenuClass.Miscellaneous.Add(new MenuBool("blockr", "Block Manual R if it will not hit any enemy"));

                /// <summary>
                ///     Sets the customization menu for the Q spell.
                /// </summary>
                MenuClass.Q2 = new Menu("q", "Q Customization:");
                {
                    MenuClass.Q2.Add(new MenuSlider("laneclear", "Laneclear / if Minions Hit >= x%", 3, 1, 10));
                    MenuClass.Q2.Add(new MenuSlider("jungleclear", "Jungleclear / if Minions Hit >= x%", 3, 1, 10));
                }
                MenuClass.Miscellaneous.Add(MenuClass.Q2);

                /// <summary>
                ///     Sets the customization menu for the W spell.
                /// </summary>
                MenuClass.W2 = new Menu("w", "W Customization:");
                {
                    MenuClass.W2.Add(new MenuSlider("laneclear", "Laneclear / if Minions Hit >= x%", 4, 1, 10));
                    MenuClass.W2.Add(new MenuSlider("jungleclear", "Jungleclear / if Minions Hit >= x%", 4, 1, 10));
                }
                MenuClass.Miscellaneous.Add(MenuClass.W2);

                /// <summary>
                ///     Sets the customization menu for the E spell.
                /// </summary>
                MenuClass.E2 = new Menu("e", "E Customization:");
                {
                    MenuClass.E2.Add(new MenuSlider("laneclear", "Laneclear / if Minions Hit >= x%", 3, 1, 10));
                    MenuClass.E2.Add(new MenuSlider("jungleclear", "Jungleclear / if Minions Hit >= x%", 3, 1, 10));
                }
                MenuClass.Miscellaneous.Add(MenuClass.E2);
            }
            MenuClass.Root.Add(MenuClass.Miscellaneous);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("q", "Q Range"));
                MenuClass.Drawings.Add(new MenuBool("e", "E Range"));
                MenuClass.Drawings.Add(new MenuBool("ball", "Ball Position"));
                MenuClass.Drawings.Add(new MenuBool("ballw", "Ball W Range"));
                MenuClass.Drawings.Add(new MenuBool("ballr", "Ball R Range"));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}