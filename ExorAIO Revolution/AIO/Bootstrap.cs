
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
                    Activator.CreateInstance(type);
                }
            }
            catch (Exception e)
            {
                switch (e)
                {
                    case TargetInvocationException _:
                        Console.WriteLine("ExorAIO: Error occurred while trying to load " + UtilityClass.Player.ChampionName);
                        Console.WriteLine(e);
                        break;
                    case TypeLoadException _:
                        for (var i = 1; i < 10; i++)
                        {
                            Console.WriteLine($"ExorAIO: Champion {UtilityClass.Player.ChampionName} is NOT supported yet.");
                        }
                        Bools.IsChampionSupported = false;
                        break;
                }
            }
        }

        #endregion
    }
}