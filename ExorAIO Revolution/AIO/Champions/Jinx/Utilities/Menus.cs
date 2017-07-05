
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
    internal partial class Jinx
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public void Menus()
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
                    MenuClass.Q.Add(new MenuBool("combo", "Use Fishbones if target out of PowPow range"));
                    //MenuClass.Q.Add(new MenuSeperator("separator2"));
                    MenuClass.Q.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("lasthit", "Lasthit out of PowPow range / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the menu for the Q customization.
                    /// </summary>
                    MenuClass.Q2 = new Menu("customization", "Fishbones Customization:");
                    {
                        //MenuClass.Q2.Add(new MenuSeperator("separator1", "General settings:"));
                        MenuClass.Q2.Add(new MenuSlider("splashrange", "Splash damage radius", 160, 125, 175));
                        //MenuClass.Q2.Add(new MenuSeperator("separator2"));
                        //MenuClass.Q2.Add(new MenuSeperator("separator3", "Combo settings:"));
                        //MenuClass.Q2.Add(new MenuSeperator("separator4", "This option will also be valid for the PowPow range."));
                        MenuClass.Q2.Add(new MenuSliderBool("minenemies", "Use Fishbones / if hittable enemies >= x", true, 3, 2, GameObjects.EnemyHeroes.Count()));
                        //MenuClass.Q2.Add(new MenuSeperator("separator5"));
                        //MenuClass.Q2.Add(new MenuSeperator("separator6", "Laneclear settings:"));
                        MenuClass.Q2.Add(new MenuSlider("laneclear", "Use Fishbones if Hittable minions >= x", 3, 2, 5));
                        //MenuClass.Q2.Add(new MenuSeperator("separator7"));
                        //MenuClass.Q2.Add(new MenuSeperator("separator8", "Jungleclear settings:"));
                        MenuClass.Q2.Add(new MenuSlider("jungleclear", "Use Fishbones if Hittable minions >= x", 2, 1, 5));
                    }
                    MenuClass.Q.Add(MenuClass.Q2);

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
                        //MenuClass.Q.Add(new MenuSeperator("exseparator", "No enemy champions found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.Q);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                MenuClass.W = new Menu("w", "Use W to:");
                {
                    MenuClass.W.Add(new MenuList("mode", "W Cast Mode", new[] { "out of PowPow range", "out of Fishbones range", "Don't use W in Combo" }, 0));
                    MenuClass.W.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.W.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

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
                        //MenuClass.W.Add(new MenuSeperator("exseparator", "No enemy champions found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.W);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                MenuClass.E = new Menu("e", "Use E to:");
                {
                    MenuClass.E.Add(new MenuSliderBool("aoe", "Try AoE / If can hit >= x enemies", true, 2, 1, GameObjects.EnemyHeroes.Count()));
                    MenuClass.E.Add(new MenuBool("logical", "On Hard-CC'd/Stasis Enemies"));
                    MenuClass.E.Add(new MenuBool("teleport", "On Teleport"));
                    MenuClass.E.Add(new MenuBool("gapcloser", "Anti-Gapcloser"));
                    MenuClass.E.Add(new MenuBool("interrupter", "On Channelling Immobile Targets"));
                }
                MenuClass.Spells.Add(MenuClass.E);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                MenuClass.R = new Menu("r", "Use R to:");
                {
                    //MenuClass.R.Add(new MenuSeperator("separator1", "It will ult the lowest on health,"));
                    //MenuClass.R.Add(new MenuSeperator("separator2", "whitelisted and non-invulnerable enemy in range."));
                    MenuClass.R.Add(new MenuBool("bool", "Semi-Automatic R"));
                    MenuClass.R.Add(new MenuKeyBind("key", "Key:", KeyCode.T, KeybindType.Press));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the R Whitelist.
                        /// </summary>
                        MenuClass.WhiteList3 = new Menu("whitelist", "Ultimate: Whitelist");
                        {
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                MenuClass.WhiteList3.Add(new MenuBool(target.ChampionName.ToLower(), "Use against: " + target.ChampionName));
                            }
                        }
                        MenuClass.R.Add(MenuClass.WhiteList3);
                    }
                    else
                    {
                        //MenuClass.R.Add(new MenuSeperator("exseparator", "No enemy champions found, no need for a Whitelist Menu."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.R);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            MenuClass.Miscellaneous = new Menu("miscellaneous", "Miscellaneous");
            {
                MenuClass.Miscellaneous.Add(new MenuSliderBool("wsafetycheck", "W only if enemies in Fishbones Range <= x", true, 2, 1, GameObjects.EnemyHeroes.Count()));
            }
            MenuClass.Root.Add(MenuClass.Miscellaneous);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("q", "Q Range"));
                MenuClass.Drawings.Add(new MenuBool("w", "W Range"));
                MenuClass.Drawings.Add(new MenuBool("e", "E Range"));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}