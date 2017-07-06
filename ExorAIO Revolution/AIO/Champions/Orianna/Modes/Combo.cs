
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
    internal partial class Orianna
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                GameObjects.EnemyHeroes.Any(t =>
                    !Invulnerable.Check(t, DamageType.Magical) &&
                    t.IsValidTarget(SpellClass.W.Width - t.BoundingRadius, false, this.BallPosition)) &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Value)
            {
                SpellClass.W.Cast();
            }

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                GameObjects.EnemyHeroes.Count(t =>
                    !Invulnerable.Check(t, DamageType.Magical) &&
                    t.IsValidTarget(SpellClass.R.Width - t.BoundingRadius, false, this.BallPosition)) >= MenuClass.Spells["r"]["combo"].As<MenuSliderBool>().Value &&
                MenuClass.Spells["r"]["combo"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.R.Cast();
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Value)
            {
                var bestAllies = GameObjects.AllyHeroes.Where(t => !t.IsMe && t.IsValidTarget(SpellClass.E.Range, true)).OrderBy(o => o.Health);
                foreach (var ally in bestAllies)
                {
                    var allyToBallRectangle = new Geometry.Rectangle(
                            (Vector2)ally.Position,
                            (Vector2)ally.Position.Extend(this.BallPosition, ally.Distance(this.BallPosition) + 30f),
                            SpellClass.E.Width);

                    if (GameObjects.EnemyHeroes.Any(t =>
                            t.IsValidTarget() &&
                            !allyToBallRectangle.IsOutside((Vector2)t.Position) &&
                            !Invulnerable.Check(t, DamageType.Magical)))
                    {
                        SpellClass.E.CastOnUnit(ally);
                        return;
                    }
                }

                var playerToBallRectangle = new Geometry.Rectangle(
                        (Vector2)UtilityClass.Player.Position,
                        (Vector2)UtilityClass.Player.Position.Extend(this.BallPosition, UtilityClass.Player.Distance(this.BallPosition) + 30f),
                        SpellClass.E.Width);

                if (GameObjects.EnemyHeroes.Any(t =>
                        t.IsValidTarget() &&
                        !playerToBallRectangle.IsOutside((Vector2)t.Position) &&
                        !Invulnerable.Check(t, DamageType.Magical)))
                {
                    SpellClass.E.CastOnUnit(UtilityClass.Player);
                    return;
                }
            }

            var bestTarget = Extensions.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Magical))
            {
                return;
            }

            /// <summary>
            ///     The Combo Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                bestTarget.IsValidTarget(SpellClass.Q.Range) &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Value)
            {
                if (bestTarget.Distance(this.BallPosition) > bestTarget.Distance(UtilityClass.Player) + 100f)
                {
                    if (SpellClass.E.Ready &&
                        MenuClass.Spells["e"]["combo"].As<MenuBool>().Value)
                    {
                        SpellClass.E.CastOnUnit(UtilityClass.Player);
                        return;
                    }
                }
                else
                {
                    SpellClass.Q.Cast(bestTarget);
                    return;
                }
            }

            /// <summary>
            ///     The Speedup W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                !UtilityClass.Player.HasBuff("orianahaste") &&
                UtilityClass.Player.HasBuff("orianaghostself") &&
                !bestTarget.IsValidTarget(SpellClass.Q.Range) &&
                bestTarget.IsValidTarget(SpellClass.Q.Range + 300f) &&
                MenuClass.Spells["w"]["customization"]["speedw"].As<MenuBool>().Value)
            {
                SpellClass.W.Cast();
            }
        }

        #endregion
    }
}