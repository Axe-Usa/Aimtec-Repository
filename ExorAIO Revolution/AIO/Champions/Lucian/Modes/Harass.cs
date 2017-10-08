
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
    internal partial class Lucian
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Harass()
        {
            /// <summary>
            ///     The Extended Q Mixed Harass Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q2"]["mixed"]) &&
                MenuClass.Spells["q2"]["mixed"].As<MenuSliderBool>().Enabled)
            {
                var target = Extensions.GetBestEnemyHeroesTargetsInRange(SpellClass.Q2.Range)
                    .MinBy(t =>
                        !Invulnerable.Check(t) &&
                        MenuClass.Spells["q2"]["whitelist"][t.ChampionName.ToLower()].Enabled);
                if (target != null)
                {
                    foreach (var minion in Extensions.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range))
                    {
                        var polygon = QRectangle(minion);
                        if (minion != target &&
                            polygon.IsInside((Vector2)target.ServerPosition) &&
                            polygon.IsInside((Vector2)SpellClass.Q2.GetPrediction(target).CastPosition))
                        {
                            SpellClass.Q.CastOnUnit(minion);
                        }
                    }
                }
            }
        }

        #endregion
    }
}