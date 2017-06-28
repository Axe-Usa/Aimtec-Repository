
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Drawing;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class Xayah
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the menus.
        /// </summary>
        public static void Drawings()
        {
            /// <summary>
            ///     Loads the Q drawing.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Drawings["q"].As<MenuBool>().Enabled)
            {
                RenderManager.RenderCircle(UtilityClass.Player.Position, SpellClass.Q.Range, 100, Color.LightGreen);
            }

            /// <summary>
            ///     Loads the Feather linking drawing.
            /// </summary>
            if (!UtilityClass.Player.SpellBook.GetSpell(SpellSlot.E).State.HasFlag(SpellState.Cooldown) &&
                MenuClass.Drawings["feathers"].As<MenuBool>().Enabled)
            {
                foreach (var feather in GameObjects.AllyMinions.Where(m => m.IsValid && m.Name == "Feather"))
                {
                    Geometry.Rectangle hitbox = new Geometry.Rectangle((Vector2)UtilityClass.Player.Position, (Vector2)feather.Position, SpellClass.Q.Width);
                    Geometry.Util.DrawLineInWorld(UtilityClass.Player.Position, feather.Position, 1, GameObjects.EnemyHeroes.Any(h => !hitbox.IsOutside((Vector2)h.Position)) ? Color.Blue : Color.OrangeRed);
                }
            }

            /// <summary>
            ///     Loads the R drawing.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Drawings["r"].As<MenuBool>().Enabled)
            {
                RenderManager.RenderCircle(UtilityClass.Player.Position, SpellClass.R.Range, 100, Color.Red);
            }
        }

        #endregion
    }
}