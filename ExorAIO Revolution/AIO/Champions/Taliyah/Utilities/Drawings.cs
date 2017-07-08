
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Drawing;

    using Aimtec;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class Taliyah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes drawings.
        /// </summary>
        public void Drawings()
        {
            /// <summary>
            ///     Loads the Q drawing.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Drawings["q"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.Q.Range, 100, Color.LightGreen);
            }

            /// <summary>
            ///     Loads the W drawing.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Drawings["w"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.W.Range, 100, Color.Purple);
            }

            /// <summary>
            ///     Loads the E drawing.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Drawings["e"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.E.Range, 100, Color.Cyan);
            }

            /// <summary>
            ///     Loads the R drawing.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Drawings["r"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.R.Range, 100, Color.Red);
            }

            /// <summary>
            ///     Loads the WorkedGrounds drawing.
            /// </summary>
            if (MenuClass.Drawings["grounds"].As<MenuBool>().Enabled)
            {
                foreach (var ground in this.WorkedGrounds)
                {
                    Render.Circle(ground.Value, 412.5f, 100, Color.Brown);
                }
            }

            /// <summary>
            ///     Loads the MineFields drawing.
            /// </summary>
            if (MenuClass.Drawings["boulders"].As<MenuBool>().Enabled)
            {
                foreach (var boulder in this.MineField)
                {
                    Render.Circle(boulder.Value, 30f, 100, Color.Brown);
                }
            }
        }

        /// <summary>
        ///     Initializes the minimap drawings.
        /// </summary>
        public void MinimapDrawings()
        {
            /// <summary>
            ///     Loads the R minimap drawing.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Drawings["rmm"].As<MenuBool>().Enabled)
            {
                Geometry.DrawCircleOnMinimap(UtilityClass.Player.Position, SpellClass.R.Range, Color.White);
            }
        }

        #endregion
    }
}