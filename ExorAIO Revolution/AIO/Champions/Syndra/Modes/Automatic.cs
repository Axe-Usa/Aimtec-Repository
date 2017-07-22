
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Prediction.Skillshots;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Syndra
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Automatic()
        {
            SpellClass.E.Width = UtilityClass.GetAngleByDegrees(UtilityClass.Player.SpellBook.GetSpell(SpellSlot.E).Level < 5 ? 40 : 60);
            SpellClass.R.Range = UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).Level < 3 ? 675f : 750f;

            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic E Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                SpellClass.E.Ready &&
                MenuClass.Spells["e"]["logical"].As<MenuBool>().Enabled)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsImmobile() &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        t.IsValidTarget(SpellClass.E.Range+SpellClass.Q.Range)))
                {
                    if (target.IsValidTarget(SpellClass.Q.Range))
                    {
                        SpellClass.Q.Cast(target);
                        SpellClass.E.Cast(target);
                    }
                    else
                    {
                        SpellClass.E.Cast(target.Position);
                        SpellClass.Q.Cast(
                            UtilityClass.Player.Position.Extend(
                                Prediction.GetPrediction
                                    (SpellClass.Q.GetPredictionInput(target)).CastPosition,
                                    SpellClass.Q.Range));
                    }
                }
            }
        }

        #endregion
    }
}