
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Drawing;

    using Aimtec;
    using Aimtec.SDK.Menu.Components;

    using Utilities;


    /// <summary>
    ///     The prediction drawings class.
    /// </summary>
    internal partial class Orianna
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Loads the range drawings.
        /// </summary>
        public static void Drawings()
        {
            /// <summary>
            ///     Loads the Ball drawing.
            /// </summary>
            if (MenuClass.Drawings["ball"].As<MenuBool>().Value)
            {
                RenderManager.RenderCircle(BallPosition, SpellClass.Q.Width, 100, Color.Blue);
            }

            /// <summary>
            ///     Loads the Q drawing.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Drawings["q"].As<MenuBool>().Value)
            {
                RenderManager.RenderCircle(UtilityClass.Player.Position, SpellClass.Q.Range, 100, Color.LightGreen);
            }

            /// <summary>
            ///     Loads the W width drawing.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Drawings["ballw"].As<MenuBool>().Value)
            {
                RenderManager.RenderCircle(BallPosition, SpellClass.W.Width, 100, Color.Purple);
            }

            /// <summary>
            ///     Loads the E drawing.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Drawings["e"].As<MenuBool>().Value)
            {
                RenderManager.RenderCircle(UtilityClass.Player.Position, SpellClass.Q.Range, 100, Color.Cyan);
            }

            /// <summary>
            ///     Loads the R width drawing.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Drawings["ballr"].As<MenuBool>().Value)
            {
                RenderManager.RenderCircle(BallPosition, SpellClass.R.Width, 100, Color.Red);
            }
        }

        #endregion
    }
}