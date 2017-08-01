
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
    internal partial class Sivir
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
                    MenuClass.Q.Add(new MenuBool("killsteal", "KillSteal"));
                    MenuClass.Q.Add(new MenuSliderBool("harass", "Harass / If Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.Q.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the Q spell.
                    /// </summary>
                    MenuClass.Q2 = new Menu("customization", "Q Customization:");
                    {
                        //MenuClass.Q2.Add(new MenuSeperator("separator3", "Laneclear settings:"));
                        MenuClass.Q2.Add(new MenuSlider("laneclear", "Only Laneclear if hittable minions >= x%", 4, 1, 10));
                    }
                    MenuClass.Q.Add(MenuClass.Q2);

                    if (GameObjects.EnemyHeroes.Any())
                    {
                        /// <summary>
                        ///     Sets the menu for the W Harass Whitelist.
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
                    MenuClass.W.Add(new MenuSliderBool("harass", "Harass / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("laneclear", "Laneclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("jungleclear", "Jungleclear / if Mana >= x%", true, 50, 0, 99));
                    MenuClass.W.Add(new MenuSliderBool("buildings", "Demolish buildings / if Mana >= x%", true, 50, 0, 99));

                    /// <summary>
                    ///     Sets the customization menu for the W spell.
                    /// </summary>
                    MenuClass.W2 = new Menu("customization", "W Customization:");
                    {
                        //MenuClass.W2.Add(new MenuSeperator("separator3", "Laneclear settings:"));
                        MenuClass.W2.Add(new MenuSlider("laneclear", "Only Laneclear if hittable minions >= x%", 4, 1, 10));
                    }
                    MenuClass.W.Add(MenuClass.W2);

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
                    MenuClass.E.Add(new MenuSliderBool("logical", "Use Shield / with X ms Delay", true, 0, 0, 250));
                    {
                        /// <summary>
                        ///     Sets the menu for the E Whitelist.
                        /// </summary>
                        MenuClass.WhiteList = new Menu("whitelist", "Shield: Whitelist Menu");
                        {
                            MenuClass.WhiteList.Add(new MenuBool("minions", "Shield: Dragon's Attacks"));
                            foreach (var enemy in GameObjects.EnemyHeroes)
                            {
                                if (enemy.ChampionName.Equals("Alistar"))
                                {
                                    MenuClass.WhiteList.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.pulverize",
                                            $"Shield: {enemy.ChampionName}'s Pulverize (Q)"));
                                }
                                if (enemy.ChampionName.Equals("Braum"))
                                {
                                    MenuClass.WhiteList.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.passive",
                                            $"Shield: {enemy.ChampionName}'s Passive"));
                                }
                                if (enemy.ChampionName.Equals("Jax"))
                                {
                                    MenuClass.WhiteList.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.jaxcounterstrike",
                                            $"Shield: {enemy.ChampionName}'s JaxCounterStrike (E)"));
                                }
                                if (enemy.ChampionName.Equals("KogMaw"))
                                {
                                    MenuClass.WhiteList.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.kogmawicathiansurprise",
                                            $"Shield: {enemy.ChampionName}'s KogMawIcathianSurprise (Passive)"));
                                }
                                if (enemy.ChampionName.Equals("Nautilus"))
                                {
                                    MenuClass.WhiteList.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.nautilusravagestrikeattack",
                                            $"Shield: {enemy.ChampionName}'s NautilusRavageStrikeAttack (Passive)"));
                                }
                                if (enemy.ChampionName.Equals("Udyr"))
                                {
                                    MenuClass.WhiteList.Add(
                                        new MenuBool(
                                            $"{enemy.ChampionName.ToLower()}.udyrbearattack",
                                            $"Shield: {enemy.ChampionName}'s UdyrBearAttack (E)"));
                                }

                                string[] excludedSpellsList = { "KatarinaE", "nautiluspiercinggaze" };
                                string[] assassinList = { "Akali", "Leblanc", "Talon" };

                                foreach (var spell in SpellDatabase.Get().Where(s =>
                                    !excludedSpellsList.Contains(s.SpellName) &&
                                    s.ChampionName.Equals(enemy.ChampionName)))
                                {
                                    if (spell.CastType != null)
                                    {
                                        if (enemy.IsMelee &&
                                            spell.CastType.Contains(CastType.Activate) &&
                                            spell.SpellType.HasFlag(SpellType.Activated) &&
                                            ImplementationClass.IOrbwalker.IsReset(spell.SpellName) || spell.CastType.Contains(CastType.EnemyChampions) &&
                                            (spell.SpellType.HasFlag(SpellType.Targeted) || spell.SpellType.HasFlag(SpellType.TargetedMissile)))
                                        {
                                            MenuClass.WhiteList.Add(
                                                new MenuBool(
                                                    $"{enemy.ChampionName.ToLower()}.{spell.SpellName.ToLower()}",
                                                    $"Shield: {enemy.ChampionName}'s {spell.SpellName} ({spell.Slot})"
                                                    + (assassinList.Contains(enemy.ChampionName) ? "[May not work]" : "")));
                                        }
                                    }
                                }
                            }
                        }

                        MenuClass.E.Add(MenuClass.WhiteList);
                    }
                }
                MenuClass.Spells.Add(MenuClass.E);
            }
            MenuClass.Root.Add(MenuClass.Spells);

            /// <summary>
            ///     Sets the menu for the drawings.
            /// </summary>
            MenuClass.Drawings = new Menu("drawings", "Drawings");
            {
                MenuClass.Drawings.Add(new MenuBool("q", "Q Range", false));
            }
            MenuClass.Root.Add(MenuClass.Drawings);
        }

        #endregion
    }
}