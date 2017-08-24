
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
    internal partial class Anivia
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["combo"].As<MenuBool>().Enabled)
            {
                switch (UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).ToggleState)
                {
                    case 1:
                        var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.R.Range);
                        if (bestTarget != null &&
                            !Invulnerable.Check(bestTarget, DamageType.Magical))
                        {
                            SpellClass.R.Cast(bestTarget);
                        }
                        break;
                    case 2:
                        if (UtilityClass.Player.InFountain())
                        {
                            return;
                        }

                        if (this.GlacialStorm != null &&
                            !GameObjects.EnemyHeroes.Any(t =>
                                !Invulnerable.Check(t, DamageType.Magical) &&
                                t.IsValidTarget(SpellClass.R.Width, false, true, this.GlacialStorm.Position)))
                        {
                            SpellClass.R.Cast();
                        }
                        break;
                }
            }

            /// <summary>
            ///     The Wall Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Value)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.W.Range);
                if (bestTarget != null)
                {
                    var targetPos = SpellClass.W.GetPrediction(bestTarget).CastPosition;
                    var castPosition =
                        UtilityClass.Player.ServerPosition.Extend(
                            targetPos,
                            UtilityClass.Player.Distance(targetPos) + bestTarget.BoundingRadius*2);
                    if (UtilityClass.Player.Distance(castPosition) <= SpellClass.W.Range)
                    {
                        SpellClass.W.Cast(castPosition);
                    }
                }
            }

            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                switch (UtilityClass.Player.SpellBook.GetSpell(SpellSlot.Q).ToggleState)
                {
                    case 1:
                        var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range);
                        if (bestTarget != null &&
                            !Invulnerable.Check(bestTarget, DamageType.Magical))
                        {
                            SpellClass.Q.Cast(bestTarget);
                        }
                        break;
                    case 2:
                        if (this.FlashFrost != null &&
                            GameObjects.EnemyHeroes.Any(t =>
                                !Invulnerable.Check(t, DamageType.Magical) &&
                                t.IsValidTarget(SpellClass.Q.Width, false, true, this.FlashFrost.Position)))
                        {
                            SpellClass.Q.Cast();
                        }
                        break;
                }
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.E.Range);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical))
                {
                    switch (MenuClass.Spells["e"]["customization"]["emodes"]["combo"].As<MenuList>().Value)
                    {
                        case 0:
                            if (this.IsChilled(bestTarget))
                            {
                                SpellClass.E.CastOnUnit(bestTarget);
                            }
                            break;
                        case 1:
                            SpellClass.E.CastOnUnit(bestTarget);
                            break;
                    }
                }
            }
        }

        #endregion
    }
}