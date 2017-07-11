
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
                        t.IsValidTarget(SpellClass.W.Width-50f, false, false, this.BallPosition)))
                {
                    SpellClass.W.Cast();
                }
            }

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["combo"].As<MenuSliderBool>().Enabled)
            {
                var countValidTargets = GameObjects.EnemyHeroes.Count(t =>
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        t.IsValidTarget(SpellClass.R.Width-50f, false, false, this.BallPosition));

                if (countValidTargets >= MenuClass.Spells["r"]["combo"].As<MenuSliderBool>().Value)
                {
                    SpellClass.R.Cast();
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
                if (MenuClass.Spells["e"]["comboallies"].As<MenuBool>().Enabled)
                {
                    var bestAllies = GameObjects.AllyHeroes.Where(t => !t.IsMe && t.IsValidTarget(SpellClass.E.Range, true)).OrderBy(o => o.Health);
                    foreach (var ally in bestAllies.Where(a => MenuClass.Spells["e"]["combowhitelist"][a.ChampionName.ToLower()].As<MenuBool>().Enabled))
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
                }

                /// <summary>
                ///     The E Combo to Orianna Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
                {
                    var playerToBallRectangle = new Geometry.Rectangle(
                        (Vector2)UtilityClass.Player.Position,
                        (Vector2)UtilityClass.Player.Position.Extend(this.BallPosition, UtilityClass.Player.Distance(this.BallPosition) + 30f),
                        SpellClass.E.Width);

                    if (GameObjects.EnemyHeroes.Any(
                        t =>
                            t.IsValidTarget() &&
                            !playerToBallRectangle.IsOutside((Vector2)t.Position) &&
                            !Invulnerable.Check(t, DamageType.Magical)))
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
                if (bestTarget.IsValidTarget() &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical))
                {
                    if (bestTarget.Distance(this.BallPosition) > bestTarget.Distance(UtilityClass.Player) + 100f)
                    {
                        if (SpellClass.E.Ready &&
                            MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
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