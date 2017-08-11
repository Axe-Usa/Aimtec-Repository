
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Anivia
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The R Stacking Manager.
            /// </summary>
            if (UtilityClass.Player.InFountain() &&
                UtilityClass.Player.HasTearLikeItem() &&
                UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).ToggleState == 1 &&
                MenuClass.Miscellaneous["tear"].As<MenuBool>().Value)
            {
                SpellClass.R.Cast(Game.CursorPos);
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["logical"].As<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(t =>
                    t.IsImmobile() &&
                    !Invulnerable.Check(t, DamageType.Magical, false)))
                {
                    switch (UtilityClass.Player.SpellBook.GetSpell(SpellSlot.Q).ToggleState)
                    {
                        case 1:
                            if (target.IsValidTarget(SpellClass.Q.Range))
                            {
                                SpellClass.Q.Cast(target);
                            }
                            break;
                        case 2:
                            if (target.IsValidTarget(SpellClass.Q.Width, false, false, this.FlashFrost.Position))
                            {
                                SpellClass.Q.Cast(target);
                            }
                            break;
                    }
                }
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["logical"].As<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(t =>
                    t.IsImmobile() &&
                    t.IsValidTarget(SpellClass.W.Range)))
                {
                    SpellClass.W.Cast(
                        UtilityClass.Player.ServerPosition.Extend(
                            target.ServerPosition,
                            UtilityClass.Player.Distance(target) + target.BoundingRadius/2));
                }
            }
        }

        #endregion
    }
}