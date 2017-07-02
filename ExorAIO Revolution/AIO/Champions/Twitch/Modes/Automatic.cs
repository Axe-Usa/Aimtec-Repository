
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Prediction.Health;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Twitch
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public static void Automatic()
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
                    HealthPrediction.Implementation.GetPrediction(UtilityClass.Player, 1000 + Game.Ping) <= 0)
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
                            t.GetBuffCount("twitchdeadlyvenom") >= MenuClass.Spells["e"]["logical"].As<MenuSliderBool>().Value))
                    {
                        SpellClass.E.Cast();
                    }
                }

                /// <summary>
                ///     The Automatic JungleSteal E Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["junglesteal"].As<MenuBool>().Value)
                {
                    foreach (var minion in UtilityClass.GetLargeJungleMinionsTargets().Concat(UtilityClass.GetLegendaryJungleMinionsTargets()))
                    {
                        if (IsPerfectExpungeTarget(minion) &&
                            minion.Health < GetTotalExpungeDamage(minion) &&
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