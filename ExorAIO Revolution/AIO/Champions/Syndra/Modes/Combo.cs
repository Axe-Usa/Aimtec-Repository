
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
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
                var bestTarget = Extensions.GetBestEnemyHeroTargetInRange(SpellClass.Q.Range);
                if (bestTarget != null &&
                    !Invulnerable.Check(bestTarget, DamageType.Magical) &&
                    MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
                {
                    SpellClass.Q.Cast(bestTarget);
                }
            }

            /// <summary>
            ///     The W Combo Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
            {
                InitializeWLogic(false);
            }

            /// <summary>
            ///     The E Logics.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(t => MenuClass.Spells["e"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled))
                {
                    var targetPos = target.ServerPosition;

                    /// <summary>
                    ///     The Normal E Combo Logic.
                    /// </summary>
                    if (HoldedSphere != null &&
                        target.IsValidTarget(SpellClass.E.Range))
                    {
                        foreach (var sphere in DarkSpheres)
                        {
                            if (CanSphereHitUnit(target, sphere) &&
                                HoldedSphere?.NetworkId != sphere.Key)
                            {
                                SpellClass.E.Cast(targetPos);
                                SelectedDarkSphereNetworkId = sphere.Key;
                            }
                        }
                    }
                    else
                    {
                        /// <summary>
                        ///     The E Out of range catch Logic.
                        /// </summary>
                        if (SpellClass.Q.Ready &&
                            target.IsValidTarget(1100f) &&
                            !target.IsValidTarget(SpellClass.Q.Range))
                        {
                            if (!SpellClass.W.Ready ||
                                !target.IsValidTarget(SpellClass.W.Range))
                            {
                                var qPosition = UtilityClass.Player.ServerPosition.Extend(targetPos, SpellClass.Q.Range - 125f);
                                switch (MenuClass.Spells["e"]["catchmode"].As<MenuList>().Value)
                                {
                                    case 0:
                                        SpellClass.E.Cast(targetPos);
                                        UtilityClass.LastTick = Game.TickCount;
                                        SpellClass.Q.Cast(qPosition);
                                        break;
                                    case 1:
                                        SpellClass.Q.Cast(qPosition);
                                        SpellClass.E.Cast(targetPos);
                                        break;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}