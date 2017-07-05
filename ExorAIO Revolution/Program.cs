
#pragma warning disable 1587
namespace AIO
{
    using System;

    using Aimtec.SDK.Events;

    using AIO.Utilities;

    internal class Program
    {
        #region Methods

        /// <summary>
        ///     The entry point of the application.
        /// </summary>
        private static void Main()
        {
            GameEvents.GameStart += OnStart;
        }

        /// <summary>
        ///     Event which triggers on game start.
        /// </summary>
        private static void OnStart()
        {
            General.Menu();
            General.Methods();

            Bootstrap.LoadChampion();
            Console.WriteLine("ExorAIO: Revolution - " + UtilityClass.Player.ChampionName + (Bools.IsChampionSupported ? " Loaded." : " Not supported."));
        }

        #endregion
    }
}