
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
    internal partial class Syndra
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Combo()
        {
            /// <summary>
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range - 100f);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical) &&
                    MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
                {
                    SpellClass.Q.Cast(bestTarget);
                }
            }

            /// <summary>
            ///     The E Logics.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(t => MenuClass.Spells["e"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled))
                {
                    /// <summary>
                    ///     The Normal E Combo Logic.
                    /// </summary>
                    if (MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
                    {
                        foreach (var sphere in this.DarkSpheres)
                        {
                            if (sphere.Key != this.HoldedSphere?.NetworkId &&
                                this.CanSphereHitUnit(target, sphere))
                            {
                                SpellClass.E.Cast(target);
                                this.SelectedDarkSphereNetworkId = sphere.Key;
                            }
                        }
                    }

                    /// <summary>
                    ///     The E Out of range catch Logic.
                    /// </summary>
                    if (SpellClass.Q.Ready &&
                        !target.IsValidTarget(SpellClass.Q.Range) &&
                        target.IsValidTarget(1100f+SpellClass.Q.Width-150f))
                    {
                        var targetPred = SpellClass.Q.GetPrediction(target);
                        if (targetPred != null)
                        {
                            if (!SpellClass.W.Ready ||!target.IsValidTarget(SpellClass.W.Range) || this.IsHoldingForceOfWillObject())
                            {
                                var qPosition = UtilityClass.Player.ServerPosition.Extend(targetPred.CastPosition, SpellClass.Q.Range - 75f);
                                switch (MenuClass.Spells["e"]["catchmode"].As<MenuList>().Value)
                                {
                                    case 0:
                                        SpellClass.E.Cast(target);
                                        SpellClass.Q.Cast(qPosition);
                                        break;
                                    case 1:
                                        SpellClass.Q.Cast(qPosition);
                                        SpellClass.E.Cast(target);
                                        break;
                                }
                            }
                        }
                    }
                }
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
            {
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.W.Range+200f);
                if (bestTarget == null ||
                    Invulnerable.Check(bestTarget, DamageType.Magical) ||
                    !MenuClass.Spells["w"]["whitelist"][bestTarget.ChampionName.ToLower()].As<MenuBool>().Enabled)
                {
                    return;
                }

                if (!SpellClass.E.Ready &&
                    !this.IsHoldingForceOfWillObject())
                {
                    var obj = this.ForceOfWillObject();
                    if (obj != null &&
                        obj.Distance(UtilityClass.Player) < SpellClass.W.Range)
                    {
                        SpellClass.W.CastOnUnit(obj);
                    }
                }
                else
                {
                    if (bestTarget.IsValidTarget(SpellClass.W.Range-100f))
                    {
                        SpellClass.W.Cast(bestTarget);
                    }
                }
            }
        }

        #endregion
    }
}