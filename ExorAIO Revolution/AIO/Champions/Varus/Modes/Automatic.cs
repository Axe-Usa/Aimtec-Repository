
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
    internal partial class Varus
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            ImplementationClass.IOrbwalker.AttackingEnabled = !SpellClass.Q.IsCharging;

            if (SpellClass.Q.IsCharging)
            {
                SpellClass.Q.Range = 925+UtilityClass.Player.BoundingRadius + 7*SpellClass.Q.ChargePercent;
            }

            /// <summary>
            ///     The Automatic R Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["aoe"] != null &&
                MenuClass.Spells["r"]["aoe"].As<MenuSliderBool>().Enabled)
            {
                var bestTarget = GameObjects.EnemyHeroes.FirstOrDefault(t =>
                    t.IsValidTarget(SpellClass.R.Range) &&
                    !Invulnerable.Check(t, DamageType.Magical, false) &&
                    GameObjects.EnemyHeroes.Count(t2 =>
                        t2.Distance(t) <= 550f &&
                        !Invulnerable.Check(t2, DamageType.Magical, false)) >= MenuClass.Spells["r"]["aoe"].As<MenuSliderBool>().Value);

                if (bestTarget != null)
                {
                    SpellClass.R.Cast(bestTarget);
                }
            }

            /// <summary>
            ///     The Semi-Automatic R Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["bool"].As<MenuBool>().Enabled &&
                MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
            {
                var bestTarget = GameObjects.EnemyHeroes.Where(t =>
                        t.IsValidTarget(SpellClass.R.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false) &&
                        MenuClass.Spells["r"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled)
                    .MinBy(o => o.GetRealHealth());
                if (bestTarget != null)
                {
                    SpellClass.R.Cast(bestTarget);
                }
            }
        }

        #endregion
    }
}