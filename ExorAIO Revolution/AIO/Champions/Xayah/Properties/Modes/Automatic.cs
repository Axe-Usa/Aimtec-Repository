
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Xayah
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
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["bool"].As<MenuBool>().Enabled &&
                MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
            {
                var bestTarget = GameObjects.EnemyHeroes
                    .Where(t =>
                        t.IsValidTarget(SpellClass.R.Range) &&
                        !Invulnerable.Check(t, DamageType.Physical, false) &&
                        MenuClass.Spells["r"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled)
                    .OrderBy(o => o.Health)
                    .FirstOrDefault();
                if (bestTarget != null)
                {
                    SpellClass.R.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}