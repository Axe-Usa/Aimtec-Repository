
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ashe
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
            ///     The Semi-Automatic R Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["bool"].As<MenuBool>().Value &&
                MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
            {
                var target = GameObjects.EnemyHeroes
                    .Where(t =>
                        t.IsValidTarget(SpellClass.R.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        MenuClass.Spells["r"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Value)
                    .OrderBy(o => o.Health)
                    .FirstOrDefault();
                if (target != null)
                {
                    if (SpellClass.E.Ready &&
                        MenuClass.Spells["e"]["logical"].As<MenuBool>().Value)
                    {
                        SpellClass.E.Cast(target);
                    }

                    SpellClass.R.Cast(target);
                }
            }
        }

        #endregion
    }
}