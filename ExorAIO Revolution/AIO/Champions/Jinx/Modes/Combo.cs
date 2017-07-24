
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

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
        public void Combo(object sender, PreAttackEventArgs args)
        {
            var heroTarget = args.Target as Obj_AI_Hero;
            if (heroTarget == null)
            {
                return;
            }

            /// <summary>
            ///     The Fishbones to PowPow Logic.
            /// </summary>
            if (this.IsUsingFishBones())
            {
                if (SpellClass.Q.Ready &&
                    MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
                {
                    if (heroTarget.IsValidTarget(SpellClass.Q.Range) &&
                        MenuClass.Spells["q"]["customization"]["minenemies"].As<MenuSliderBool>().Value >
                            heroTarget.CountEnemyHeroesInRange(MenuClass.Spells["q"]["customization"]["splashrange"].Value))
                    {
                        SpellClass.Q.Cast();
                    }
                }
            }
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The PowPow to Fishbones Logic.
            /// </summary>
            if (!this.IsUsingFishBones())
            {
                if (SpellClass.Q.Ready &&
                    MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
                {
                    var target = ImplementationClass.IOrbwalker.GetOrbwalkingTarget();
                    if (!Extensions.GetEnemyHeroesTargetsInRange(SpellClass.Q.Range).Any() &&
                        Extensions.GetEnemyHeroesTargetsInRange(SpellClass.Q2.Range+200f).Any() ||
                        MenuClass.Spells["q"]["customization"]["minenemies"].As<MenuSliderBool>().Value <=
                            target?.CountEnemyHeroesInRange(MenuClass.Spells["q"]["customization"]["splashrange"].Value))
                    {
                        SpellClass.Q.Cast();
                    }
                }
            }

            /// <summary>
            ///     The E AoE Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["aoe"] != null &&
                MenuClass.Spells["e"]["aoe"].As<MenuSliderBool>().Enabled)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(h => h.IsValidTarget(SpellClass.E.Range) && !Invulnerable.Check(h)))
                {
                    if (GameObjects.EnemyHeroes.Count(
                            h2 =>
                                !Invulnerable.Check(h2) &&
                                h2.IsValidTarget(SpellClass.E.Range) &&
                                h2.Distance(target) < SpellClass.E.Width*3) >= MenuClass.Spells["e"]["aoe"].As<MenuSliderBool>().Value)
                    {
                        SpellClass.E.Cast(target.ServerPosition);
                    }
                }
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                !UtilityClass.Player.IsUnderEnemyTurret())
            {
                if (UtilityClass.Player.CountEnemyHeroesInRange(SpellClass.Q2.Range) >
                        MenuClass.Spells["w"]["customization"]["wsafetycheck"].As<MenuSliderBool>().Value &&
                    MenuClass.Spells["w"]["customization"]["wsafetycheck"].As<MenuSliderBool>().Enabled)
                {
                    return;
                }

                var target = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.W.Range);
                if (target != null &&
                    target.IsValidTarget() &&
                    !Invulnerable.Check(target))
                {
                    switch (MenuClass.Spells["w"]["mode"].As<MenuList>().Value)
                    {
                        case 0:
                            if (target.Distance(UtilityClass.Player) >= SpellClass.Q.Range * 1.1)
                            {
                                SpellClass.W.Cast(target);
                            }
                            break;
                        case 1:
                            if (target.Distance(UtilityClass.Player) >= SpellClass.Q2.Range * 1.1)
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