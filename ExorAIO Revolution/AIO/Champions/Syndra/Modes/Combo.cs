
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
                    SpellClass.Q.Cast(SpellClass.Q.GetPrediction(bestTarget).CastPosition);
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
                foreach (var target in GameObjects.EnemyHeroes.Where(t =>
                    MenuClass.Spells["e"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled))
                {
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
                                SpellClass.E.Cast(target.ServerPosition);
                                SelectedDarkSphereNetworkId = sphere.Key;
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}