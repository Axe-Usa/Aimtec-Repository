
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
    internal partial class Jhin
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                !GameObjects.Player.IsUnderEnemyTurret() &&
                MenuClass.Spells["w"]["logical"].As<MenuBool>().Value)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.HasBuff("jhinespotteddebuff") &&
                        t.IsValidTarget(SpellClass.W.Range - 150f) &&
                        !Invulnerable.Check(t, DamageType.Physical, false) &&
                        MenuClass.Spells["w"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Value))
                {
                    if (MenuClass.Spells["w"]["customization"]["onlyslowed"].As<MenuBool>().Value)
                    {
                        if (target.HasBuffOfType(BuffType.Slow))
                        {
                            SpellClass.W.Cast(target);
                        }
                    }
                    else
                    {
                        SpellClass.W.Cast(target);
                    }
                }
            }

            var bestTarget = Extensions.GetBestEnemyHeroTarget();
            if (!bestTarget.IsValidTarget() ||
                Invulnerable.Check(bestTarget, DamageType.Magical, false))
            {
                return;
            }

            /// <summary>
            ///     The Q Combo on Reload Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                this.IsReloading() &&
                bestTarget.IsValidTarget(SpellClass.Q.Range) &&
                MenuClass.Spells["q"]["customization"]["comboonreload"].As<MenuBool>().Enabled)
            {
                SpellClass.Q.CastOnUnit(bestTarget);
            }

            /// <summary>
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                bestTarget.IsValidTarget(SpellClass.W.Range) &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
            {
                if (MenuClass.Spells["e"]["customization"]["comboonreload"].As<MenuBool>().Enabled)
                {
                    if (this.IsReloading())
                    {
                        SpellClass.E.Cast(bestTarget);
                    }
                }
                else
                {
                    SpellClass.E.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}