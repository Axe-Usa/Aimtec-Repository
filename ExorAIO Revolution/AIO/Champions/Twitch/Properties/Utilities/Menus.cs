
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

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
                    MenuClass.Q.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
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
                    MenuClass.W.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
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
                }
                MenuClass.Spells.Add(MenuClass.E);

                /// <summary>
                ///     Sets the menu for the E Whitelist.
                /// </summary>
                MenuClass.WhiteList = new Menu("whitelist", "Junglesteal Expunge: Whitelist");
                {
                    foreach (var target in UtilityClass.GetLargeJungleMinionsTargets().Concat(UtilityClass.GetLegendaryJungleMinionsTargets()))
                    {
                        MenuClass.WhiteList.Add(new MenuBool(target.Name, "Expunge: " + target.Name));
                    }
                }
                MenuClass.E.Add(MenuClass.WhiteList);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            MenuClass.Miscellaneous = new Menu("miscellaneous", "Miscellaneous");
            {
                MenuClass.Miscellaneous.Add(new MenuBool("stealthrecall", "Use Stealth Recall"));
                MenuClass.Miscellaneous.Add(new MenuBool("dontwinr", "Don't use W while using R"));

                /// <summary>
                ///     Sets the menu for the E customization.
                /// </summary>
                MenuClass.E2 = new Menu("e2", "Expunge Customization:");
                {
                    MenuClass.E2.Add(new MenuSeperator("separator1", "Laneclear Options:"));
                    MenuClass.E2.Add(new MenuSlider("laneclear", "Laneclear / If can Kill >= x minions", 3, 1, 5));
                }
                MenuClass.Miscellaneous.Add(MenuClass.E2);
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