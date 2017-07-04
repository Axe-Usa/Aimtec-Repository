
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Jinx
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on orbwalker action.
        /// </summary>
        /// <param name="sender">The object.</param>
        /// <param name="args">The <see cref="PreAttackEventArgs" /> instance containing the event data.</param>
        public static void Combo(object sender, PreAttackEventArgs args)
        {
            var heroTarget = args.Target as Obj_AI_Hero;
            if (heroTarget == null)
            {
                return;
            }

            /// <summary>
            ///     The Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["combo"].As<MenuSliderBool>().Enabled)
            {
                float splashRange = MenuClass.Spells["q"]["customization"]["splashrange"].Value;
                var minSplashRangeEnemies = MenuClass.Spells["q"]["combo"].As<MenuSliderBool>().Value;

                if (UtilityClass.Player.HasBuff("JinxQ"))
                {
                    if (heroTarget.IsValidTarget(SpellClass.Q.Range) &&
                        heroTarget.CountEnemyHeroesInRange(splashRange) < minSplashRangeEnemies)
                    {
                        SpellClass.Q.Cast();
                    }
                }
                else
                {
                    if (!heroTarget.IsValidTarget(SpellClass.Q.Range) &&
                        heroTarget.IsValidTarget(SpellClass.Q2.Range))
                    {
                        SpellClass.Q.Cast();
                        UtilityClass.IOrbwalker.ForceTarget(heroTarget);
                    }
                }
            }
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Combo()
        {
            /// <summary>
            ///     The Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["combo"].As<MenuSliderBool>().Enabled)
            {
                if (!UtilityClass.Player.HasBuff("JinxQ"))
                {
                    if (!Extensions.GetEnemyHeroesTargetsInRange(SpellClass.Q.Range).Any() &&
                        Extensions.GetEnemyHeroesTargetsInRange(SpellClass.Q2.Range).Any())
                    {
                        SpellClass.Q.Cast();
                    }
                }
            }

            /// <summary>
            ///     The E AoE Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["aoe"].As<MenuSliderBool>().Enabled)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(h => h.IsValidTarget(SpellClass.E.Range) && !Invulnerable.Check(h)))
                {
                    if (GameObjects.EnemyHeroes.Count(h2 =>
                            !Invulnerable.Check(h2) &&
                            h2.IsValidTarget(SpellClass.E.Range) &&
                            h2.Distance(target) < SpellClass.E.Width) >= MenuClass.Spells["e"]["aoe"].As<MenuSliderBool>().Value)
                    {
                        SpellClass.E.Cast(target.Position);
                    }
                }
            }

            var bestTarget = Extensions.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Physical))
            {
                return;
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                !UtilityClass.Player.IsUnderEnemyTurret())
            {
                if (MenuClass.Miscellaneous["wsafetycheck"].As<MenuSliderBool>().Enabled &&
                    UtilityClass.Player.CountEnemyHeroesInRange(SpellClass.Q2.Range) < MenuClass.Miscellaneous["wsafetycheck"].As<MenuSliderBool>().Value)
                {
                    var target = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q2.Range);
                    switch (MenuClass.Spells["w"]["combo"].As<MenuList>().Value)
                    {
                        case 0:
                            if (target.Distance(UtilityClass.Player) >= SpellClass.Q.Range * 0.9)
                            {
                                SpellClass.W.Cast(target);
                            }
                            break;
                        case 1:
                            if (target.Distance(UtilityClass.Player) >= SpellClass.Q2.Range * 0.9)
                            {
                                SpellClass.W.Cast(target);
                            }
                            break;
                    }
                }
            }
        }

        #endregion
    }
}