
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
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
            if (BallPosition == null)
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                !UtilityClass.Player.GetSpell(SpellSlot.W).State.HasFlag(SpellState.Surpressed) &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
            {
                if (GameObjects.EnemyHeroes.Any(t =>
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        t.IsValidTarget(SpellClass.W.Width - t.BoundingRadius - SpellClass.W.Delay * t.BoundingRadius, false, false, (Vector3)BallPosition)))
                {
                    SpellClass.W.Cast();
                }
            }

            /// <summary>
            ///     The E Logics.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                /// <summary>
                ///     The E Engager Logic.
                /// </summary>
                if (MenuClass.Spells["r"]["aoe"] != null &&
                    MenuClass.Spells["r"]["aoe"].As<MenuSliderBool>().Enabled &&
                    MenuClass.Spells["e"]["engager"].As<MenuBool>().Enabled)
                {
                    var bestAllies = GameObjects.AllyHeroes
                        .Where(a =>
                            !a.IsMe &&
                            a.IsValidTarget(SpellClass.E.Range, true) &&
                            MenuClass.Spells["e"]["engagerswhitelist"][a.ChampionName.ToLower()].As<MenuBool>().Enabled);

                    var bestAlly = bestAllies
                        .FirstOrDefault(a =>
                            GameObjects.EnemyHeroes.Count(t =>
                                !Invulnerable.Check(t, DamageType.Magical, false) &&
                                t.IsValidTarget(SpellClass.R.Width - t.BoundingRadius - SpellClass.R.Delay * t.BoundingRadius, false, false, a.ServerPosition)) >= MenuClass.Spells["r"]["aoe"].As<MenuSliderBool>().Value);

                    if (bestAlly != null)
                    {
                        SpellClass.E.CastOnUnit(bestAlly);
                    }
                }

                /// <summary>
                ///     The E Combo Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
                {
                    var bestAllies = GameObjects.AllyHeroes
                        .Where(a =>
                            a.IsValidTarget(SpellClass.E.Range, true) &&
                            MenuClass.Spells["e"]["combowhitelist"][a.ChampionName.ToLower()].As<MenuBool>().Enabled)
                        .OrderBy(o => o.GetRealHealth());

                    foreach (var ally in bestAllies)
                    {
                        var allyToBallRectangle = new Geometry.Rectangle(
                            ally.ServerPosition,
                            ally.ServerPosition.Extend((Vector3)BallPosition, ally.Distance((Vector3)BallPosition) + 30f),
                            SpellClass.E.Width);

                        if (GameObjects.EnemyHeroes.Any(
                            t =>
                                t.IsValidTarget() &&
                                !Invulnerable.Check(t, DamageType.Magical) &&
                                allyToBallRectangle.IsInside((Vector2)t.ServerPosition)))
                        {
                            SpellClass.E.CastOnUnit(ally);
                            return;
                        }
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
                if (bestTarget != null)
                {
                    if (SpellClass.E.Ready &&
                        bestTarget.Distance((Vector3)BallPosition) >=
                            bestTarget.Distance(UtilityClass.Player) + 100f &&
                        MenuClass.E2["gaine"].As<MenuBool>().Enabled)
                    {
                        SpellClass.E.CastOnUnit(UtilityClass.Player);
                        return;
                    }

                    SpellClass.Q.GetPredictionInput(bestTarget).From = (Vector3)BallPosition;
                    SpellClass.Q.Cast(SpellClass.Q.GetPrediction(bestTarget).CastPosition);
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
                    if (UtilityClass.Player.Distance((Vector3)BallPosition) < SpellClass.W.Width)
                    {
                        SpellClass.W.Cast();
                    }
                }
            }
        }

        #endregion
    }
}