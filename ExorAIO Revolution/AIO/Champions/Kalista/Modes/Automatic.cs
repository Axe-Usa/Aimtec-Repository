
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;
    using Aimtec.SDK.Prediction.Health;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Kalista
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public static void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                SoulBound.IsValidTarget(SpellClass.R.Range) &&
                SoulBound.CountEnemyHeroesInRange(800f) > 0 &&
                HealthPrediction.Implementation.GetPrediction(SoulBound, 250 + Game.Ping) <= SoulBound.MaxHealth / 4 &&
                MenuClass.Spells["r"]["lifesaver"].As<MenuBool>().Enabled)
            {
                SpellClass.R.Cast();
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                !UtilityClass.Player.IsRecalling() &&
                !UtilityClass.Player.IsUnderEnemyTurret() &&
                Orbwalker.Implementation.Mode == OrbwalkingMode.None &&
                UtilityClass.Player.CountEnemyHeroesInRange(1500f) == 0 &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["logical"]) &&
                MenuClass.Spells["w"]["logical"].As<MenuSliderBool>().Enabled)
            {
                foreach (var loc in Locations.Where(l =>
                    UtilityClass.Player.Distance(l) <= SpellClass.W.Range &&
                    !ObjectManager.Get<Obj_AI_Minion>().Any(m => m.Distance(l) <= 1000f && m.UnitSkinName.Equals("KalistaSpawn"))))
                {
                    SpellClass.W.Cast(loc);
                }
            }

            /// <summary>
            ///     The Automatic E Logics.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                /// <summary>
                ///     The E Before death Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["ondeath"].As<MenuBool>().Enabled &&
                    HealthPrediction.Implementation.GetPrediction(UtilityClass.Player, 1000 + Game.Ping) <= 0)
                {
                    SpellClass.E.Cast();
                }

                var validMinions = UtilityClass.GetAllGenericMinionsTargets().Where(m => IsPerfectRendTarget(m) && m.GetRealHealth() < GetTotalRendDamage(m));
                var validTargets = GameObjects.EnemyHeroes.Where(IsPerfectRendTarget);

                var rendableHeroes = validTargets as IList<Obj_AI_Hero> ?? validTargets.ToList();
                var rendableMinions = validMinions as IList<Obj_AI_Minion> ?? validMinions.ToList();

                /// <summary>
                ///     The E Minion Harass Logic.
                /// </summary>
                if (rendableHeroes.Any() && rendableMinions.Any() &&
                    MenuClass.Spells["e"]["harass"].As<MenuBool>().Enabled)
                {
                    SpellClass.E.Cast();
                }

                /// <summary>
                ///     The E JungleClear Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["junglesteal"].As<MenuBool>().Enabled)
                {
                    foreach (var minion in UtilityClass.GetLargeJungleMinionsTargets().Concat(UtilityClass.GetLegendaryJungleMinionsTargets()))
                    {
                        if (IsPerfectRendTarget(minion) &&
                            minion.Health < GetTotalRendDamage(minion) &&
                            MenuClass.WhiteList[minion.Name].As<MenuBool>().Enabled)
                        {
                            SpellClass.E.Cast();
                        }
                    }
                }
            }
        }

        #endregion
    }
}