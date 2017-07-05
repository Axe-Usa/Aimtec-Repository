
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
    internal partial class Kalista
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Sets the menu.
        /// </summary>
        public void Menus()
        {
            /// <summary>
            /// Sets the spells menu.
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
                    MenuClass.Q.Add(new MenuSliderBool("Jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                }
                MenuClass.Spells.Add(MenuClass.Q);

                /// <summary>
                ///     Sets the menu for the W.
                /// </summary>
                MenuClass.W = new Menu("w", "Use W to:");
                {
                    MenuClass.W.Add(new MenuSliderBool("logical", "Check important Jungle spots / if Mana >= x%", true, 50, 0, 99));
                }
                MenuClass.Spells.Add(MenuClass.W);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                MenuClass.E = new Menu("e", "Use E to:");
                {
                    MenuClass.E.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.E.Add(new MenuBool("ondeath", "Before death"));
                    MenuClass.E.Add(new MenuBool("harass", "Harass with minions"));
                    MenuClass.E.Add(new MenuBool("laneclear", "Laneclear"));
                    MenuClass.E.Add(new MenuBool("junglesteal", "Junglesteal"));
                    MenuClass.E.Add(new MenuSeperator("separator"));
                    MenuClass.E.Add(new MenuSeperator("separator1", "It will cast E if there are any minions with"));
                    MenuClass.E.Add(new MenuSeperator("separator2", "stacks the orbwalker cannot reach in time to kill them."));
                    MenuClass.E.Add(new MenuBool("farmhelper", "FarmHelper"));

                    /// <summary>
                    ///     Sets the menu for the E Whitelist.
                    /// </summary>
                    MenuClass.WhiteList = new Menu("whitelist", "Junglesteal Rend: Whitelist");
                    {
                        foreach (var target in Extensions.GetLargeJungleMinionsTargets().Concat(Extensions.GetLegendaryJungleMinionsTargets()))
                        {
                            MenuClass.WhiteList.Add(new MenuBool(target.Name, "Rend: " + target.Name));
                        }
                    }
                    MenuClass.E.Add(MenuClass.WhiteList);
                }
                MenuClass.Spells.Add(MenuClass.E);

                /// <summary>
                ///     Sets the menu for the R Whitelist.
                /// </summary>
                MenuClass.R = new Menu("r", "Use R to:");
                {
                    MenuClass.R.Add(new MenuBool("lifesaver", "Soulbound Lifesaver"));
                }
                MenuClass.Spells.Add(MenuClass.R);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the miscellaneous menu.
            /// </summary>
            MenuClass.Miscellaneous = new Menu("miscellaneous", "Miscellaneous");
            {
                MenuClass.Miscellaneous.Add(new MenuBool("focusw", "Focus enemies to proc W Passive mark"));
                MenuClass.Miscellaneous.Add(new MenuBool("minionsorbwalk", "Orbwalk on Minions in Combo (Only works if you have Runaan's Hurricane)"));
            }
            MenuClass.Root.Add(MenuClass.Miscellaneous);

            /// <summary>
            ///     Sets the drawings menu.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("q", "Q Range", false));
                MenuClass.Drawings.Add(new MenuBool("w", "W Range", false));
                MenuClass.Drawings.Add(new MenuBool("e", "E Range", false));
                MenuClass.Drawings.Add(new MenuBool("edmg", "E Damage"));
                MenuClass.Drawings.Add(new MenuBool("r", "R Range", false));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}