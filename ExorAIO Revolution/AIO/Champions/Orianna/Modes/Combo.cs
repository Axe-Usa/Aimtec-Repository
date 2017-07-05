
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
                GameObjects.EnemyHeroes.Any(
                    t =>
                        !Invulnerable.Check(t, DamageType.Magical) &&
                        t.IsValidTarget(SpellClass.W.Width, false, this.BallPosition)) &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Value)
            {
                SpellClass.W.Cast();
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.Mana - UtilityClass.Player.SpellBook.GetSpell(SpellSlot.E).Cost
                > UtilityClass.Player.SpellBook.GetSpell(SpellSlot.Q).Cost + UtilityClass.Player.SpellBook.GetSpell(SpellSlot.W).Cost &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Value)
            {
                foreach (var ally in GameObjects.AllyHeroes
                    .Where(
                        t =>
                            t.IsValidTarget() &&
                            !Invulnerable.Check(t, DamageType.Magical))
                    .OrderBy(o => o.Health))
                {
                    var polygon = new Geometry.Rectangle((Vector2)ally.Position, (Vector2)ally.Position.Extend(this.BallPosition, ally.Distance(this.BallPosition)), SpellClass.E.Width);
                    if (GameObjects.EnemyHeroes.Any(
                        t =>
                            t.IsValidTarget() &&
                            !polygon.IsOutside((Vector2)t.Position) &&
                            !Invulnerable.Check(t, DamageType.Magical, false)))
                    {
                        SpellClass.E.CastOnUnit(ally);
                    }
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
                    }
                }
                else
                {
                    SpellClass.Q.Cast(bestTarget);
                }
            }
            else
            {
                if (SpellClass.W.Ready &&
                    bestTarget.IsValidTarget(SpellClass.Q.Range + 300f) &&
                    MenuClass.Miscellaneous["speedw"].As<MenuBool>().Value)
                {
                    SpellClass.W.Cast();
                }
            }
        }

        #endregion
    }
}