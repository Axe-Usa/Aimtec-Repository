
using System;
using System.Reflection;
using AIO.Utilities;

#pragma warning disable 1587
namespace AIO
{
    /// <summary>
    ///     The bootstrap class.
    /// </summary>
    internal static class Bootstrap
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Tries to load the champion which is being currently played.
        /// </summary>
        public static void LoadChampion()
        {
            try
            {
                var pluginName = "AIO.Champions." + UtilityClass.Player.ChampionName;
                var type = Type.GetType(pluginName, true);
                if (type != null)
                {
                    Console.WriteLine("Loading new instance of " + type.Name);
                    Activator.CreateInstance(type);
                }
            }
            catch (Exception e)
            {
                if (e is TargetInvocationException)
                {
                    Console.WriteLine("Error occurred while trying to load " + UtilityClass.Player.ChampionName);
                    Console.WriteLine(e);
                }
                if (e is TypeLoadException)
                {
                    Console.WriteLine("Champion not supported " + UtilityClass.Player.ChampionName);
                    Bools.IsChampionSupported = false;
                }
            }
        }

        #endregion
    }
}