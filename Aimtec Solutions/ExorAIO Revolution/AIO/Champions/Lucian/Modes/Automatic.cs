
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Util;

    using AIO.Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public static void Automatic()
        {
            var bestHero = UtilityClass.GetBestEnemyHeroTarget();

            /// <summary>
            ///     The Automatic R Orbwalking.
            /// </summary>
            if (MenuClass.Spells["r"]["bool"].As<MenuBool>().Enabled &&
                MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
            {
                DelayAction.Queue((int)(100 + Game.Ping / 2f),
                    () =>
                        {
                            UtilityClass.Player.IssueOrder(OrderType.MoveTo, Game.CursorPos);
                        });
            }

            /// <summary>
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["bool"].As<MenuBool>().Enabled &&
                MenuClass.Spells["r"]["whitelist"][bestHero.ChampionName.ToLower()].As<MenuBool>().Enabled)
            {
                if (bestHero.IsValidTarget(SpellClass.R.Range) &&
                    !UtilityClass.Player.HasBuff("LucianR") &&
                    MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
                {
                    SpellClass.W.Cast(bestHero);
                    SpellClass.R.Cast(bestHero);
                }
                else if (UtilityClass.Player.HasBuff("LucianR") &&
                         !MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
                {
                    SpellClass.R.Cast(bestHero);
                }
            }
        }

        #endregion
    }
}