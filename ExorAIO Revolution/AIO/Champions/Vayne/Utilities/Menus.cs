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
    internal partial class Vayne
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
                    MenuClass.Q.Add(new MenuBool("engage", "Engager", false));
                    MenuClass.Q.Add(new MenuSeperator("separator"));
                    Gapcloser.Attach(MenuClass.Q, "Anti-Gapcloser");
                    MenuClass.Q.Add(new MenuSeperator("separator2"));
                    MenuClass.Q.Add(new MenuSliderBool("farmhelper", "Farmhelper / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("buildings", "Demolish buildings / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the Q spell.
                    /// </summary>
                    MenuClass.Q2 = new Menu("customization", "Customization:");
                    {
                        MenuClass.Q2.Add(new MenuBool("noqoutaarange", "Don't Q out of AA range from target", false));
                        MenuClass.Q2.Add(new MenuBool("onlyqifmouseoutaarange", "Only Q if mouse out of AA Range", false));
                        if (GameObjects.EnemyHeroes.Any())
                        {
                            var count = GameObjects.EnemyHeroes.Count();
                            MenuClass.Q2.Add(new MenuSliderBool("qrangecheck", "Don't Q if pos has >= X enemies in range", false, count >= 3 ? 3 : count, 1, GameObjects.EnemyHeroes.Count()));
                        }
                        else
                        {
                            MenuClass.Q2.Add(new MenuSeperator("exseparator", "Don't Q if pos has >= / No enemies found, no need for a position range check."));
                        }
                        MenuClass.Q2.Add(new MenuBool("wstacks", "Use Q only to proc 3rd W Ring", false));
                        MenuClass.Q2.Add(new MenuBool("noqturret", "Don't use Q under Enemy Turret", false));
                    }
                    MenuClass.Q.Add(MenuClass.Q2);
                }
                MenuClass.Spells.Add(MenuClass.Q);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                MenuClass.E = new Menu("e", "Use E to:");
                {
                    MenuClass.E.Add(new MenuList("emode", "Condemn Mode", new []{ "Exory", "Perfect BETA", "Don't Condemn to stun"}, 0));
                    MenuClass.E.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.E.Add(new MenuSeperator("separator"));
                    Gapcloser.Attach(MenuClass.E, "Anti-Gapcloser");
                    MenuClass.E.Add(new MenuSeperator("separator2"));
                    MenuClass.E.Add(new MenuBool("interrupter", "Interrupt Enemy Channels"));
                    MenuClass.E.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSeperator("separator3"));
                    MenuClass.E.Add(new MenuBool("bool", "Semi-Automatic E"));
                    MenuClass.E.Add(new MenuKeyBind("key", "Key:", KeyCode.T, KeybindType.Press));

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the E Whitelist.
                        /// </summary>
                        MenuClass.WhiteList = new Menu("whitelist", "Condemn: Whitelist");
                        {
                            //MenuClass.WhiteList.Add(new MenuSeperator("separator1", "WhiteList only works for Combo"));
                            //MenuClass.WhiteList.Add(new MenuSeperator("separator2", "not Killsteal (Automatic)"));
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                MenuClass.WhiteList.Add(new MenuBool(target.ChampionName.ToLower(), "Stun: " + target.ChampionName));
                            }
                        }
                        MenuClass.E.Add(MenuClass.WhiteList);
                    }
                    else
                    {
                        MenuClass.E.Add(new MenuSeperator("exseparator", "Whitelist Menu not needed."));
                    }
                }
                MenuClass.Spells.Add(MenuClass.E);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            MenuClass.Miscellaneous = new Menu("miscellaneous", "Miscellaneous");
            {
                MenuClass.Miscellaneous.Add(new MenuBool("focusw", "Focus enemies with 2 W stacks"));
                MenuClass.Miscellaneous.Add(new MenuSlider("stealthtime", "Stay Invisible: For at least x ms [1000 ms = 1 second]", 0, 0, 1000));

                if (GameObjects.EnemyHeroes.Any())
                {
                    var count = GameObjects.EnemyHeroes.Count();
                    MenuClass.Miscellaneous.Add(new MenuSlider("stealthcheck", "Stay Invisible: if >= x enemies in AA Range", count >= 3 ? 3 : count, 0, count));
                }
                else
                {
                    MenuClass.Miscellaneous.Add(new MenuSeperator("exseparator", "No enemies found, no need for stealth slider check."));
                }
            }
            MenuClass.Root.Add(MenuClass.Miscellaneous);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("q", "Q Range"));
                MenuClass.Drawings.Add(new MenuBool("e", "E Range", false));
                MenuClass.Drawings.Add(new MenuBool("epred", "E Prediction Rectangles", false));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}