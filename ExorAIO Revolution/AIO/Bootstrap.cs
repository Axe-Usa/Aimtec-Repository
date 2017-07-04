#pragma warning disable 1587
namespace AIO
{
    using Champions;
    using Utilities;

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
                case "Ahri":
                    Ahri.OnLoad();
                    return;

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
                */
                case "Jinx":
                    Jinx.OnLoad();
                    break;

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
                */
                case "Orianna":
                    Orianna.OnLoad();
                    break;
                /*
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
                */
                case "Taliyah":
                    Taliyah.OnLoad();
                    break;
                case "Tristana":
                    Tristana.OnLoad();
                    break;
                /*
                case "Tryndamere":
                    new Tryndamere().OnLoad();
                    break;
                */
                case "Twitch":
                    Twitch.OnLoad();
                    break;
                /*
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

        #endregion
    }
}