
using System.Linq;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Util;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The menu class.
    /// </summary>
    internal partial class MissFortune
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
                    MenuClass.Q.Add(new MenuBool("killsteal", "Killsteal"));
                    MenuClass.Q.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                }
                MenuClass.Spells.Add(MenuClass.Q);

                /// <summary>
                ///     Sets the Extended Q menu.
                /// </summary>
                MenuClass.Q3 = new Menu("extendedq", "Use Extended Q in:");
                {
                    MenuClass.Q3.Add(new MenuBool("combo", "Combo"));
                    MenuClass.Q3.Add(new MenuBool("killsteal", "Killsteal"));
                    MenuClass.Q3.Add(new MenuSliderBool("mixed", "Harass / if Mana >= %", true, 50, 0, 99));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the Whitelist menu for the Extended Q.
                        /// </summary>
                        MenuClass.WhiteList = new Menu("whitelist", "Extended Harass: Whitelist");
                        {
                            //MenuClass.WhiteList.Add(new MenuSeperator("extendedsep", "Note: The Whitelist only works for Mixed and Laneclear."));
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                MenuClass.WhiteList.Add(new MenuBool(target.ChampionName.ToLower(), "Harass: " + target.ChampionName));
                            }
                        }
                        MenuClass.Q3.Add(MenuClass.WhiteList);
                    }
                    else
                    {
                        MenuClass.Q3.Add(new MenuSeperator("exseparator", "Whitelist Menu not needed."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.Q3);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                MenuClass.W = new Menu("w", "Use W to:");
                {
                    MenuClass.W.Add(new MenuBool("combo", "Combo"));
                    MenuClass.W.Add(new MenuSliderBool("mixed", "Harass / if Mana >= %", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("buildings", "Demolish buildings / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the W spell.
                    /// </summary>
                    MenuClass.W2 = new Menu("customization", "Customization:");
                    {
                        //MenuClass.W2.Add(new MenuSeperator("separator1", "Laneclear settings:"));
                        MenuClass.W2.Add(new MenuSlider("laneclear", "Only Laneclear if hittable minions >= x%", 3, 1, 10));
                    }
                    MenuClass.W.Add(MenuClass.W2);
                }
                MenuClass.Spells.Add(MenuClass.W);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                MenuClass.E = new Menu("e", "Use E to:");
                {
                    MenuClass.E.Add(new MenuBool("combo", "Combo"));
                    MenuClass.E.Add(new MenuSeperator("separator"));
                    Gapcloser.Attach(MenuClass.E, "Anti-Gapcloser");
                    MenuClass.E.Add(new MenuSeperator("separator2"));
                    MenuClass.E.Add(new MenuSliderBool("harass", "Harass / if Mana >= %", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the E spell.
                    /// </summary>
                    MenuClass.E2 = new Menu("customization", "Customization:");
                    {
                        MenuClass.E2.Add(new MenuBool("noeoutaarange", "Don't E out of AA range from target", false));
                        if (GameObjects.EnemyHeroes.Any())
                        {
                            var count = GameObjects.EnemyHeroes.Count();
                            MenuClass.E2.Add(new MenuSliderBool("erangecheck", "Don't E if pos has >= X enemies in range", false, count >= 3 ? 3 : count, 1, GameObjects.EnemyHeroes.Count()));
                        }
                        else
                        {
                            MenuClass.E2.Add(new MenuSeperator("exseparator", "Don't E if pos has >= / No enemies found, no need for a position range check."));
                        }
                        MenuClass.E2.Add(new MenuBool("noeturret", "Don't use E under Enemy Turret", false));
                    }
                    MenuClass.E.Add(MenuClass.E2);
                }
                MenuClass.Spells.Add(MenuClass.E);

                /// <summary>
                ///     Sets the menu for the R.
                /// </summary>
                MenuClass.R = new Menu("r", "Use R to:");
                {
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
                        MenuClass.R.Add(new MenuSeperator("exseparator", "Whitelist Menu not needed."));
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
                MenuClass.Miscellaneous.Add(new MenuSliderBool("focusp", "Switch target for Passive / unless killable in x AAs", true, 3, 1, 10));
            }
            MenuClass.Root.Add(MenuClass.Miscellaneous);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("q", "Q Range"));
                MenuClass.Drawings.Add(new MenuBool("qcone", "Extended Q Cones", false));
                MenuClass.Drawings.Add(new MenuBool("e", "E Range", false));
                MenuClass.Drawings.Add(new MenuBool("r", "R Range", false));
                MenuClass.Drawings.Add(new MenuBool("passivetarget", "Love Tap Target"));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}