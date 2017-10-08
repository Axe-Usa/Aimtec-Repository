
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
    internal partial class Orianna
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
                    MenuClass.Q.Add(new MenuBool("combo", "Combo"));
                    MenuClass.Q.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.Q.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("farmhelper", "Farmhelper / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the Q spell.
                    /// </summary>
                    MenuClass.Q2 = new Menu("customization", "Customization:");
                    {
                        //MenuClass.Q2.Add(new MenuSeperator("separator1", "Laneclear settings:"));
                        MenuClass.Q2.Add(new MenuSlider("laneclear", "Only Laneclear if hittable minions >= x%", 3, 1, 10));
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
                        MenuClass.Q.Add(new MenuSeperator("exseparator", "Whitelist not needed."));
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
                    MenuClass.W.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the W spell.
                    /// </summary>
                    MenuClass.W2 = new Menu("customization", "Customization:");
                    {
                        //MenuClass.W2.Add(new MenuSeperator("separator1", "General settings:"));
                        MenuClass.W2.Add(new MenuBool("speedw", "Use W to speed Orianna up while chasing in combo", false));
                        //MenuClass.W2.Add(new MenuSeperator("separator2"));
                        //MenuClass.W2.Add(new MenuSeperator("separator3", "Laneclear Options:"));
                        MenuClass.W2.Add(new MenuSlider("laneclear", "Only Laneclear if hittable minions >= x%", 4, 1, 10));
                    }
                    MenuClass.W.Add(MenuClass.W2);

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
                        MenuClass.W.Add(new MenuSeperator("exseparator", "Whitelist not needed."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.W);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                MenuClass.E = new Menu("e", "Use E to:");
                {
                    MenuClass.E.Add(new MenuBool("combo", "Combo"));
                    MenuClass.E.Add(new MenuBool("protect", "Shield (Protect Allies)"));
                    MenuClass.E.Add(new MenuBool("engager", "Shield (Engagers)"));
                    MenuClass.E.Add(new MenuSeperator("separator"));
                    Gapcloser.Attach(MenuClass.E, "Anti-Gapcloser");
                    MenuClass.E.Add(new MenuSeperator("separator2"));
                    MenuClass.E.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

                    if (GameObjects.AllyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the whitelist menu for the Combo E.
                        /// </summary>
                        MenuClass.WhiteList3 = new Menu("combowhitelist", "Combo: Whitelist");
                        {
                            foreach (var ally in GameObjects.AllyHeroes)
                            {
                                MenuClass.WhiteList3.Add(new MenuBool(ally.ChampionName.ToLower(), "Use for: " + ally.ChampionName, ally.IsMe));
                            }
                        }
                        MenuClass.E.Add(MenuClass.WhiteList3);
                    }
                    else
                    {
                        MenuClass.E.Add(new MenuSeperator("exseparator", "No ally champions found, no need for a Whitelist Menu."));
                    }

                    if (GameObjects.AllyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the whitelist menu for the Protect E.
                        /// </summary>
                        MenuClass.WhiteList4 = new Menu("protectwhitelist", "Shield (Protect Allies): Whitelist");
                        {
                            foreach (var ally in GameObjects.AllyHeroes)
                            {
                                MenuClass.WhiteList4.Add(new MenuSliderBool(ally.ChampionName.ToLower(), "Use for: " + ally.ChampionName + " / if Health < x%", true, 25, 10, 99));
                            }
                        }
                        MenuClass.E.Add(MenuClass.WhiteList4);
                    }
                    else
                    {
                        MenuClass.E.Add(new MenuSeperator("exseparator", "No ally champions found, no need for a Whitelist Menu."));
                    }

                    if (GameObjects.AllyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the whitelist menu for the Engagers E.
                        /// </summary>
                        MenuClass.WhiteList5 = new Menu("engagerswhitelist", "Shield (Engagers): Whitelist");
                        {
                            foreach (var ally in GameObjects.AllyHeroes.Where(a => !a.IsMe))
                            {
                                MenuClass.WhiteList5.Add(new MenuBool(ally.ChampionName.ToLower(), "Use for: " + ally.ChampionName));
                            }
                        }
                        MenuClass.E.Add(MenuClass.WhiteList5);
                    }
                    else
                    {
                        MenuClass.E.Add(new MenuSeperator("exseparator", "No ally champions found, no need for a Whitelist Menu."));
                    }

                    /// <summary>
                    ///     Sets the customization menu for the E spell.
                    /// </summary>
                    MenuClass.E2 = new Menu("customization", "Customization:");
                    {
                        MenuClass.E2.Add(new MenuBool("gaine", "Use E to regain Ball if too far away from target."));
                        //MenuClass.E2.Add(new MenuSeperator("separator1", "Laneclear settings:"));
                        MenuClass.E2.Add(new MenuSlider("laneclear", "Only Laneclear if hittable minions >= x%", 3, 1, 10));
                    }
                    MenuClass.E.Add(MenuClass.E2);
                }
                MenuClass.Spells.Add(MenuClass.E);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                MenuClass.R = new Menu("r", "Use R to:");
                {
                    MenuClass.R.Add(new MenuBool("killsteal", "Killsteal"));
                    MenuClass.R.Add(new MenuBool("interrupter", "Interrupt Enemy Channels"));
                    if (GameObjects.EnemyHeroes.Count() >= 2)
                    {
                        MenuClass.R.Add(new MenuSliderBool("aoe", "AoE / If can hit >= x enemies", true, 2, 2, GameObjects.EnemyHeroes.Count()));
                    }
                    else
                    {
                        MenuClass.R.Add(new MenuSeperator("separator", "AoE / Not enough enemies found"));
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
                MenuClass.Miscellaneous.Add(new MenuBool("blockr", "Block Manual R if it will not hit any enemy"));
            }
            MenuClass.Root.Add(MenuClass.Miscellaneous);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("q", "Q Range", false));
                MenuClass.Drawings.Add(new MenuBool("e", "E Range", false));
                MenuClass.Drawings.Add(new MenuSliderBool("ball", "Ball Position / Draw X circles", true, 2, 1, 5));
                MenuClass.Drawings.Add(new MenuBool("ballw", "Ball W Range", false));
                MenuClass.Drawings.Add(new MenuBool("ballr", "Ball R Range", false));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}