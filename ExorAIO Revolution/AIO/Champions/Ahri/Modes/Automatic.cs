
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ahri
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["logical"].As<MenuBool>().Value)
            {
                var range = SpellClass.E.Range;
                var target = GameObjects.EnemyHeroes
                                        .Where(
                                            t =>
                                                t.IsImmobile() &&
                                                !Invulnerable.Check(t) &&
                                                t.IsValidTarget(range))
                                        .OrderBy(o => UtilityClass.ITargetSelector.GetOrderedTargets(range)).FirstOrDefault();
                if (target != null)
                {
                    SpellClass.E.Cast(target);
                }
            }
        }

        #endregion
    }
}