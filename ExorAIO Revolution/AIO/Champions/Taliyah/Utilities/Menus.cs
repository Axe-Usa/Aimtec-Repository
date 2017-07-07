
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
    internal partial class Taliyah
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
                    MenuClass.Q.Add(new MenuList("combomode", "Cast Mode", new[] { "Full Q only", "Full + Partial Q", "Don't use Q in Combo" }, 0));
                    MenuClass.Q.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.Q.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 75, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the Q spell.
                    /// </summary>
                    MenuClass.Q2 = new Menu("customization", "Q Customization:");
                    {
                        ////MenuClass.Q2.Add(new MenuSeperator("separator1", "General settings:"));
                        //MenuClass.Q2.Add(new MenuBool("qlock", "Automatically Lock Q on Enemy"));
                        ////MenuClass.Q2.Add(new MenuSeperator("separator2"));
                        //MenuClass.Q2.Add(new MenuSeperator("separator3", "Harass Settings:"));
                        MenuClass.Q2.Add(new MenuBool("harassfull", "Harass: Only with full Q.", false));
                        //MenuClass.Q2.Add(new MenuSeperator("separator4"));
                        //MenuClass.Q2.Add(new MenuSeperator("separator5", "Laneclear settings:"));
                        MenuClass.Q2.Add(new MenuBool("laneclearfull", "Laneclear: Only with full Q."));
                        MenuClass.Q2.Add(new MenuSlider("laneclear", "Only Laneclear if hittable minions >= x%", 3, 1, 10));
                        //MenuClass.Q2.Add(new MenuSeperator("separator6"));
                        //MenuClass.Q2.Add(new MenuSeperator("separator7", "Jungleclear settings:"));
                        MenuClass.Q2.Add(new MenuBool("jungleclearfull", "Jungleclear: Only with full Q."));
                        MenuClass.Q2.Add(new MenuSlider("jungleclear", "Only Jungleclear if hittable minions >= x%", 1, 1, 10));
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
                    MenuClass.W.Add(new MenuBool("logical", "On Hard-CC'd/Stasis Enemies"));
                    MenuClass.W.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 75, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuBool("gapcloser", "Anti-Gapcloser"));
                    MenuClass.W.Add(new MenuBool("interrupter", "Interrupt Enemy Channels"));

                    /// <summary>
                    ///     Sets the customization menu for the W spell.
                    /// </summary>
                    MenuClass.W2 = new Menu("customization", "W Customization:");
                    {
                        //MenuClass.W2.Add(new MenuSeperator("separator1", "General settings:"));
                        MenuClass.W2.Add(new MenuBool("onlyeready", "Combo: Don't Cast W if E on cooldown"));
                        //MenuClass.W2.Add(new MenuSeperator("separator2"));
                        //MenuClass.W2.Add(new MenuSeperator("separator3", "Laneclear settings:"));
                        MenuClass.W2.Add(new MenuSlider("laneclear", "Only Laneclear if hittable minions >= x%", 3, 1, 10));
                    }
                    MenuClass.W.Add(MenuClass.W2);

                    /// <summary>
                    ///     Sets the menu for the selection.
                    /// </summary>
                    MenuClass.WhiteList = new Menu("selection", "Combo: Pull / Push Selection");
                    {
                        foreach (var enemy in GameObjects.EnemyHeroes)
                        {
                            MenuClass.WhiteList.Add(
                                new MenuList(
                                    enemy.ChampionName.ToLower(),
                                    enemy.ChampionName,
                                    new[] { "Always Pull", "Always Push", "Pull if Killable else Push", "Ignore if possible" }, 0));
                        }
                    }
                    MenuClass.W.Add(MenuClass.WhiteList);
                }
                MenuClass.Spells.Add(MenuClass.W);

                /// <summary>
                ///     Sets the menu for the E.
                /// </summary>
                MenuClass.E = new Menu("e", "Use E to:");
                {
                    MenuClass.E.Add(new MenuBool("combo", "Combo"));
                    MenuClass.E.Add(new MenuBool("gapcloser", "Anti-Gapcloser"));
                    MenuClass.E.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.E.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the E spell.
                    /// </summary>
                    MenuClass.E2 = new Menu("customization", "E Customization:");
                    {
                        //MenuClass.E2.Add(new MenuSeperator("separator3", "Laneclear settings:"));
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
                    MenuClass.R.Add(new MenuBool("mountr", "Automatically Mount on R"));
                }
                MenuClass.Spells.Add(MenuClass.R);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the menu for the drawings.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("q", "Q Range"));
                MenuClass.Drawings.Add(new MenuBool("w", "W Range", false));
                MenuClass.Drawings.Add(new MenuBool("e", "E Range", false));
                MenuClass.Drawings.Add(new MenuBool("r", "R Range", false));
                MenuClass.Drawings.Add(new MenuBool("rmm", "R Minimap Range"));
                MenuClass.Drawings.Add(new MenuBool("boulders", "Draw Boulders", false));
                MenuClass.Drawings.Add(new MenuBool("grounds", "Draw Worked Grounds", false));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}