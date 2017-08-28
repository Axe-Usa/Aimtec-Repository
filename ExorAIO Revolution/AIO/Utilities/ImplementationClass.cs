// ReSharper disable ArrangeMethodOrOperatorBody
// ReSharper disable InconsistentNaming


using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Prediction.Health;
using Aimtec.SDK.Prediction.Skillshots;
using Aimtec.SDK.TargetSelector;

#pragma warning disable 1587

namespace AIO.Utilities
{
    /// <summary>
    ///     The Implementation class.
    /// </summary>
    internal static class ImplementationClass
    {
        #region Static Fields

        /// <summary>
        ///     Gets the HealthPrediction implementation.
        /// </summary>
        public static IHealthPrediction IHealthPrediction => HealthPrediction.Implementation;

        /// <summary>
        ///     Gets the Orbwalker implementation.
        /// </summary>
        public static IOrbwalker IOrbwalker => Orbwalker.Implementation;

        /// <summary>
        ///     Gets the Prediction implementation.
        /// </summary>
        public static Prediction Prediction => Prediction.Instance;

        /// <summary>
        ///     Gets the TargetSelector implementation.
        /// </summary>
        public static ITargetSelector ITargetSelector => TargetSelector.Implementation;

        #endregion
    }
}