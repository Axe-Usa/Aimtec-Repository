// ReSharper disable LoopCanBeConvertedToQuery
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;

    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Olaf
    {
        #region Fields

        /// <summary>
        ///     Gets the axes.
        /// </summary>
        public Dictionary<int, Vector3> Axes = new Dictionary<int, Vector3>();

        /// <summary>
        ///     Reloads the DarkSpheres.
        /// </summary>
        public void ReloadAxes()
        {
            foreach (var axe in ObjectManager.Get<GameObject>().Where(o => o != null && o.IsValid))
            {
                if (axe.IsValid)
                {
                    if (this.Axes.Any(o => o.Key == axe.NetworkId))
                    {
                        this.Axes.Remove(axe.NetworkId);
                    }
                }

                switch (axe.Name)
                {
                    case "Olaf_Base_Q_Axe_Ally.troy":
                        this.Axes.Add(axe.NetworkId, axe.Position);
                        break;
                }
            }
        }

        #endregion
    }
}