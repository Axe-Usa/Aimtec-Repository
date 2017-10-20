
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
    internal partial class Twitch
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
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
                ///     The Automatic Enemy E Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["logical"].As<MenuSliderBool>().Enabled)
                {
                    if (GameObjects.EnemyHeroes.Any(t =>
                            !Invulnerable.Check(t) &&
                            t.IsValidTarget(SpellClass.E.Range) &&
                            t.GetRealBuffCount("twitchdeadlyvenom") >=
                                MenuClass.Spells["e"]["logical"].As<MenuSliderBool>().Value))
                    {
                        SpellClass.E.Cast();
                    }
                }

                /// <summary>
                ///     The Automatic JungleSteal E Logic.
                /// </summary>
                if (UtilityClass.Player.Level >=
                        MenuClass.Spells["e"]["junglesteal"].As<MenuSliderBool>().Value &&
                    MenuClass.Spells["e"]["junglesteal"].As<MenuSliderBool>().Enabled)
                {
                    /// <summary>
                    ///     The E Jungleclear Logic.
                    /// </summary>
                    if (UtilityClass.Player.Level >=
                            MenuClass.Spells["e"]["junglesteal"].As<MenuSliderBool>().Value &&
                        MenuClass.Spells["e"]["junglesteal"].As<MenuSliderBool>().Enabled)
                    {
                        foreach (var minion in Extensions.GetGenericJungleMinionsTargets().Where(m =>
                            IsPerfectExpungeTarget(m) &&
                            m.GetRealHealth() <= GetTotalExpungeDamage(m)))
                        {
                            if (UtilityClass.JungleList.Contains(minion.UnitSkinName) &&
                                MenuClass.Spells["e"]["whitelist"][minion.UnitSkinName].As<MenuBool>().Enabled)
                            {
                                SpellClass.E.Cast();
                            }

                            if (!UtilityClass.JungleList.Contains(minion.UnitSkinName) &&
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