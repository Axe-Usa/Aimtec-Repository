
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ahri
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["logical"].As<MenuBool>().Enabled)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !t.Name.Equals("Target Dummy") &&
                        t.IsValidTarget(SpellClass.Q.Range) &&
                        !t.ActionState.HasFlag(ActionState.CanMove)))
                {
                    SpellClass.Q.Cast(target);
                }
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["logical"].As<MenuBool>().Enabled)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !t.Name.Equals("Target Dummy") &&
                        t.IsValidTarget(SpellClass.E.Range) &&
                        !t.ActionState.HasFlag(ActionState.CanMove)))
                {
                    SpellClass.E.Cast(target);
                }
            }
        }

        #endregion
    }
}