
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Kalista
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Automatic()
        {
            var passiveObject = ObjectManager.Get<GameObject>().FirstOrDefault(o => o.IsValid && o.Name == "Kalista_Base_P_LinkIcon.troy");
            if (passiveObject != null)
            {
                SoulBound = GameObjects.AllyHeroes
                    .Where(a => !a.IsMe)
                    .MinBy(o => o.Distance(passiveObject));
            }

            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The R Logics.
            /// </summary>
            if (SpellClass.R.Ready &&
                SoulBound != null)
            {
                /// <summary>
                ///     The Offensive R Logics.
                /// </summary>
                foreach (var target in GameObjects.EnemyHeroes
                    .Where(t => t.Distance(UtilityClass.Player.ServerPosition) > UtilityClass.Player.GetFullAttackRange(t)))
                {
                    if (RLogics.ContainsKey(SoulBound.ChampionName))
                    {
                        var option = RLogics.FirstOrDefault(k => k.Key == SoulBound.ChampionName);
                        var buffName = option.Value.Item1;
                        var menuOption = option.Value.Item2;

                        if (target.HasBuff(buffName) &&
                            MenuClass.Spells["r"][menuOption].As<MenuBool>().Enabled)
                        {
                            SpellClass.R.Cast();
                        }
                    }
                }

                /// <summary>
                ///     The Lifesaver R Logic.
                /// </summary>
                if (SoulBound.CountEnemyHeroesInRange(800f) > 0 &&
                    SoulBound.HealthPercent() <=
                        MenuClass.Spells["r"]["lifesaver"].As<MenuSliderBool>().Value &&
                    MenuClass.Spells["r"]["lifesaver"].As<MenuSliderBool>().Enabled)
                {
                    SpellClass.R.Cast();
                }
            }

            /// <summary>
            ///     The Spot W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                !UtilityClass.Player.IsUnderEnemyTurret() &&
                ImplementationClass.IOrbwalker.Mode == OrbwalkingMode.None &&
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
                    ImplementationClass.IHealthPrediction.GetPrediction(UtilityClass.Player, 1000 + Game.Ping) <= 0)
                {
                    SpellClass.E.Cast();
                }

                /// <summary>
                ///     The E Minion Harass Logic.
                /// </summary>
                if (GameObjects.EnemyHeroes.Any(IsPerfectRendTarget) &&
                    Extensions.GetEnemyLaneMinionsTargetsInRange(SpellClass.E.Range).Any(m => IsPerfectRendTarget(m) && m.GetRealHealth() <= GetTotalRendDamage(m)) &&
                    MenuClass.Spells["e"]["harass"].As<MenuBool>().Enabled)
                {
                    SpellClass.E.Cast();
                }

                /// <summary>
                ///     The E Jungleclear Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["junglesteal"].As<MenuBool>().Enabled)
                {
                    foreach (var minion in Extensions.GetGenericJungleMinionsTargets().Where(IsPerfectRendTarget))
                    {
                        if (minion.GetRealHealth() <= GetTotalRendDamage(minion))
                        {
                            if (UtilityClass.JungleList.Contains(minion.UnitSkinName) &&
                                MenuClass.Spells["e"]["whitelist"][minion.UnitSkinName].As<MenuBool>().Enabled)
                            {
                                SpellClass.E.Cast();
                            }
                            else if (!UtilityClass.JungleList.Contains(minion.UnitSkinName) &&
                                     MenuClass.General["junglesmall"].As<MenuBool>().Enabled)
                            {
                                SpellClass.E.Cast();
                            }
                        }
                    }
                }
            }
        }

        #endregion
    }
}