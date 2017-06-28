#pragma warning disable 1587
namespace AIO.Core
{
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Champions;
    using AIO.Utilities;

    /// <summary>
    ///     The bootstrap class.
    /// </summary>
    internal class Bootstrap
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Tries to load the champion which is being currently played.
        /// </summary>
        public static void LoadChampion()
        {
            switch (UtilityClass.Player.ChampionName)
            {
                case "Akali":
                    Akali.OnLoad();
                    return;
                /*
                case "Amumu":
                    Amumu.OnLoad();
                    break;
                case "Anivia":
                    new Anivia().OnLoad();
                    break;
                */
                case "Ashe":
                    Ashe.OnLoad();
                    break;

                case "Caitlyn":
                    Caitlyn.OnLoad();
                    break;
                /*
                case "Cassiopeia":
                    new Cassiopeia().OnLoad();
                    break;
                case "Corki":
                    new Corki().OnLoad();
                    break;
                case "Darius":
                    new Darius().OnLoad();
                    break;
                case "Diana":
                    new Diana().OnLoad();
                    break;
                case "Draven":
                    new Draven().OnLoad();
                    break;
                case "DrMundo":
                    new DrMundo().OnLoad();
                    break;
                case "Evelynn":
                    new Evelynn().OnLoad();
                    break;
                */
                case "Ezreal":
                    Ezreal.OnLoad();
                    break;
                /*
                case "Graves":
                    new Graves().OnLoad();
                    break;
                case "Jax":
                    new Jax().OnLoad();
                    break;
                case "Jhin":
                    new Jhin().OnLoad();
                    break;
                case "Jinx":
                    new Jinx().OnLoad();
                    break;
                */
                case "Kalista":
                    Kalista.OnLoad();
                    break;
                /*
                case "Karma":
                    new Karma().OnLoad();
                    break;
                case "Karthus":
                    new Karthus().OnLoad();
                    break;
                */
                case "KogMaw":
                    KogMaw.OnLoad();
                    break;

                case "Lucian":
                    Lucian.OnLoad();
                    break;
                /*
                case "Lux":
                    new Lux().OnLoad();
                    break;
                case "MissFortune":
                    new MissFortune().OnLoad();
                    break;
                case "Nautilus":
                    new Nautilus().OnLoad();
                    break;
                case "Nocturne":
                    new Nocturne().OnLoad();
                    break;
                case "Nunu":
                    new Nunu().OnLoad();
                    break;
                case "Olaf":
                    new Olaf().OnLoad();
                    break;
                case "Orianna":
                    new Orianna().OnLoad();
                    break;
                case "Pantheon":
                    new Pantheon().OnLoad();
                    break;
                case "Quinn":
                    new Quinn().OnLoad();
                    break;
                case "Renekton":
                    new Renekton().OnLoad();
                    break;
                case "Ryze":
                    new Ryze().OnLoad();
                    break;
                case "Sivir":
                    new Sivir().OnLoad();
                    break;
                case "Sona":
                    new Sona().OnLoad();
                    break;
                case "Taliyah":
                    new Taliyah().OnLoad();
                    break;
                case "Tristana":
                    new Tristana().OnLoad();
                    break;
                case "Tryndamere":
                    new Tryndamere().OnLoad();
                    break;
                case "Twitch":
                    new Twitch().OnLoad();
                    break;
                case "Udyr":
                    new Udyr().OnLoad();
                    break;
                */
                case "Vayne":
                    Vayne.OnLoad();
                    return;
                /*
                case "Veigar":
                    new Veigar().OnLoad();
                    break;
                */
                case "Xayah":
                    Xayah.OnLoad();
                    return;
                /*
                case "Warwick":
                    new Warwick().OnLoad();
                    break;
                */
                default:
                    Bools.IsChampionSupported = false;
                    return;
            }
        }

        /// <summary>
        ///     Loads the menus.
        /// </summary>
        public static void LoadMenu()
        {
            /// <summary>
            ///     Loads the root Menu.
            /// </summary>
            MenuClass.Root = new Menu(UtilityClass.Player.ChampionName.ToLower(), "ExorAIO Revolution: " + UtilityClass.Player.ChampionName, true);
            {
                /// <summary>
                ///     Loads the orbwalker menu.
                /// </summary>
                Orbwalker.Implementation.Attach(MenuClass.Root);

                /// <summary>
                ///     Loads the general menu.
                /// </summary>
                MenuClass.General = new Menu("generalmenu", "General Menu");
                {
                    MenuClass.General.Add(new MenuSeperator("supportseparator1", "If enabled, the character will not autoattack"));
                    MenuClass.General.Add(new MenuSeperator("supportseparator2", "or cast spells on minions if any ally is in range."));
                    MenuClass.General.Add(new MenuBool("supportmode", "Support Mode", false));

                    MenuClass.General.Add(new MenuSeperator("aacomboseparator1", "If enabled, the character will not autoattack while in Combo Mode"));
                    MenuClass.General.Add(new MenuSeperator("aacomboseparator2", "Except if the player has a Sheen-like Autoattack Buff"));
                    MenuClass.General.Add(new MenuSeperator("aacomboseparator3", "E.G. Lichbane or Sona/Diana's AA Passive."));
                    MenuClass.General.Add(new MenuBool("disableaa", "Disable AutoAttacks in Combo", false));

                    MenuClass.General.Add(new MenuSeperator("sheenseparator1", "If enabled, the character will not cast spells on"));
                    MenuClass.General.Add(new MenuSeperator("sheenseparator2", "targets in autoattack range, if the player can"));
                    MenuClass.General.Add(new MenuSeperator("sheenseparator3", "AutoAttack and it has a Sheen-Like item/passive."));
                    MenuClass.General.Add(new MenuBool("usesheen", "Use Sheen Weaving Power"));
                }
                MenuClass.Root.Add(MenuClass.General);
            }
            MenuClass.Root.Attach();
        }

        #endregion
    }
}