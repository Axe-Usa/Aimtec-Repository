
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Drawing;

    using Aimtec;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Util;

    using AIO.Utilities;

    using Geometry = AIO.Utilities.Geometry;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class Olaf
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the menus.
        /// </summary>
        public void Drawings()
        {
            /// <summary>
            ///     Loads the Q range drawing.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Drawings["q"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.Q.Range, 30, Color.LightGreen);
            }

            /// <summary>
            ///     Loads the Q path drawing.
            /// </summary>
            if (MenuClass.Drawings["qpath"].As<MenuBool>().Enabled)
            {
                foreach (var axe in this.Axes)
                {
                    var axeRectangle = new Geometry.Rectangle((Vector2)UtilityClass.Player.Position, (Vector2)axe.Value, 1);
                    Render.Circle(axe.Value, 30f, 30, Color.Yellow);
                    axeRectangle.Draw(Color.Yellow);
                }
            }
        }

        #endregion
    }
}