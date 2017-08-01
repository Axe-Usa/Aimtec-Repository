
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
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
            {
                if (GameObjects.EnemyHeroes.Any(t =>
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        t.IsValidTarget(SpellClass.W.Width-25f, false, false, this.BallPosition)))
                {
                    SpellClass.W.Cast();
                }
            }

            /// <summary>
            ///     The E Combo Logics.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                /// <summary>
                ///     The E Combo to Allies Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
                {
                    var bestAllies = GameObjects.AllyHeroes
                        .Where(t =>
                            MenuClass.Spells["e"]["combowhitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled &&
                            t.IsValidTarget(SpellClass.E.Range, true))
                        .OrderBy(o => o.Health);
                    foreach (var ally in bestAllies)
                    {
                        var allyToBallRectangle = new Geometry.Rectangle(
                            (Vector2)ally.ServerPosition,
                            (Vector2)ally.ServerPosition.Extend(this.BallPosition, ally.Distance(this.BallPosition) + 30f),
                            SpellClass.E.Width/2);

                        if (GameObjects.EnemyHeroes.Any(t =>
                                t.IsValidTarget() &&
                                !Invulnerable.Check(t, DamageType.Magical) &&
                                allyToBallRectangle.IsInside((Vector2)t.ServerPosition)))
                        {
                            SpellClass.E.CastOnUnit(ally);
                            return;
                        }
                    }
                }

                /// <summary>
                ///     The E to re-gain Ball Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
                {
                    var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range);
                    if (bestTarget != null &&
                        !Invulnerable.Check(bestTarget, DamageType.Magical) &&
                        bestTarget.Distance(this.BallPosition) >= bestTarget.Distance(UtilityClass.Player) + 100f)
                    {
                        SpellClass.E.CastOnUnit(UtilityClass.Player);
                        return;
                    }
                }
            }

            /// <summary>
            ///     The Combo Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical) &&
                    bestTarget.Distance(this.BallPosition) < bestTarget.Distance(UtilityClass.Player) + 100f)
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
                MenuClass.Spells["w"]["customization"]["speedw"].As<MenuBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range + 300f);
                if (bestTarget.IsValidTarget(SpellClass.Q.Range) &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical))
                {
                    if (UtilityClass.Player.Distance(this.BallPosition) < SpellClass.W.Width-50f)
                    {
                        SpellClass.W.Cast();
                    }
                }
            }
        }

        #endregion
    }
}