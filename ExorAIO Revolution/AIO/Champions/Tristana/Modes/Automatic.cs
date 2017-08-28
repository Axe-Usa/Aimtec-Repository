
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The logics class.
    /// </summary>
    internal partial class Tristana
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Automatic()
        {
            SpellClass.E.Range = UtilityClass.Player.AttackRange;
            SpellClass.R.Range = UtilityClass.Player.AttackRange;

            /// <summary>
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["bool"].As<MenuBool>().Enabled &&
                MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
            {
                var bestTarget = GameObjects.EnemyHeroes.Where(t =>
                        t.IsValidTarget(SpellClass.R.Range) &&
                        !Invulnerable.Check(t, DamageType.Magical, false))
                    .MinBy(o => o.Distance(UtilityClass.Player));
                if (bestTarget != null)
                {
                    SpellClass.R.CastOnUnit(bestTarget);
                }
            }
        }

        #endregion
    }
}