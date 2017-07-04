
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
    internal partial class Twitch
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
                    MenuClass.Q.Add(new MenuSliderBool("Jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("buildings", "Demolish buildings / If Mana >= x%", true, 50, 0, 99));
                }
                MenuClass.Spells.Add(MenuClass.Q);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                MenuClass.W = new Menu("w", "Use W to:");
                {
                    MenuClass.W.Add(new MenuSliderBool("combo", "Combo / If can hit >= x enemies", true, 1, 1, GameObjects.EnemyHeroes.Count()));
                    MenuClass.W.Add(new MenuBool("gapcloser", "Anti-Gapcloser"));
                    MenuClass.W.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("Jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the W spell.
                    /// </summary>
                    MenuClass.W2 = new Menu("customization", "W Customization:");
                    {
                        MenuClass.W2.Add(new MenuSeperator("separator1", "General settings:"));
                        MenuClass.W2.Add(new MenuBool("dontwinr", "Don't use W while using R"));
                        MenuClass.W2.Add(new MenuSeperator("separator2"));
                        MenuClass.W2.Add(new MenuSeperator("separator3", "Laneclear settings:"));
                        MenuClass.W2.Add(new MenuSeperator("separator4", "v (Will only take into account minions) v:"));
                        MenuClass.W2.Add(new MenuSeperator("separator5", "v (with less than or equal 4 venom stacks) v:"));
                        MenuClass.W2.Add(new MenuSlider("laneclear", "Only Laneclear if hittable minions >= x%", 3, 1, 10));
                    }
                    MenuClass.W.Add(MenuClass.W2);

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the W Whitelist.
                        /// </summary>
                        MenuClass.WhiteList = new Menu("whitelist", "Harass: Whitelist");
                        {
                            MenuClass.WhiteList.Add(new MenuSeperator("extendedsep", "Note: The Whitelist only works for Harass."));
                            foreach (var target in GameObjects.EnemyHeroes)
                            {
                                MenuClass.WhiteList.Add(new MenuBool(target.ChampionName.ToLower(), "Harass: " + target.ChampionName));
                            }
                        }
                        MenuClass.W.Add(MenuClass.WhiteList);
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
                    MenuClass.E.Add(new MenuBool("ondeath", "Before death"));
                    MenuClass.E.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.E.Add(new MenuBool("junglesteal", "Junglesteal"));
                    MenuClass.E.Add(new MenuSliderBool("logical", "Automatic / If stacks >= x", true, 6, 1, 6));
                    MenuClass.E.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the E spell.
                    /// </summary>
                    MenuClass.E2 = new Menu("customization", "E Customization:");
                    {
                        MenuClass.E2.Add(new MenuSeperator("separator1", "Laneclear settings:"));
                        MenuClass.E2.Add(new MenuSlider("laneclear", "Only Laneclear if killable minions >= x%", 3, 1, 10));
                    }
                    MenuClass.E.Add(MenuClass.E2);
                }
                MenuClass.Spells.Add(MenuClass.E);

                /// <summary>
                ///     Sets the menu for the E Whitelist.
                /// </summary>
                MenuClass.WhiteList2 = new Menu("whitelist", "Junglesteal Expunge: Whitelist");
                {
                    foreach (var target in Extensions.GetLargeJungleMinionsTargets().Concat(Extensions.GetLegendaryJungleMinionsTargets()))
                    {
                        MenuClass.WhiteList2.Add(new MenuBool(target.Name, "Expunge: " + target.Name));
                    }
                }
                MenuClass.E.Add(MenuClass.WhiteList2);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            MenuClass.Miscellaneous = new Menu("miscellaneous", "Miscellaneous");
            {
                MenuClass.Miscellaneous.Add(new MenuBool("stealthrecall", "Use Stealth Recall"));
            }
            MenuClass.Root.Add(MenuClass.Miscellaneous);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("w", "W Range"));
                MenuClass.Drawings.Add(new MenuBool("e", "E Range"));
                MenuClass.Drawings.Add(new MenuBool("r", "R Range"));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}