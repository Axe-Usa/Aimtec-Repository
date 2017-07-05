
#pragma warning disable 1587
namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;
    using Aimtec.SDK.Prediction;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Vayne
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on post attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public void Jungleclear(object sender, PostAttackEventArgs args)
        {
            var jungleTarget = (Obj_AI_Minion)UtilityClass.IOrbwalker.GetTarget();
            if (!Extensions.GetGenericJungleMinionsTargets().Contains(jungleTarget))
            {
                return;
            }

            /// <summary>
            ///     The E Jungleclear Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["Jungleclear"]) &&
                MenuClass.Spells["e"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                if (!Extensions.GetLegendaryJungleMinionsTargets().Contains(jungleTarget))
                {
                    var playerPos = UtilityClass.Player.Position;
                    var firstPredictionInput = new PredictionInput
                                                   {
                                                       Delay = SpellClass.E.Delay,
                                                       Radius = SpellClass.E.Width,
                                                       From = UtilityClass.Player.Position,
                                                       Range = SpellClass.E.Range,
                                                       SkillType = SpellClass.E.Type,
                                                       Speed = SpellClass.E.Speed,
                                                       Target = jungleTarget,
                                                       Unit = UtilityClass.Player
                                                   };
                    var secondPredictionInput = new PredictionInput
                                                    {
                                                        Delay = SpellClass.E2.Delay,
                                                        Radius = SpellClass.E2.Width,
                                                        From = UtilityClass.Player.Position,
                                                        Range = SpellClass.E2.Range,
                                                        SkillType = SpellClass.E2.Type,
                                                        Speed = SpellClass.E2.Speed,
                                                        Target = jungleTarget,
                                                        Unit = UtilityClass.Player
                                                    };
                    var firstPrediction = UtilityClass.IPrediction.GetPrediction(firstPredictionInput);
                    var secondPrediction = UtilityClass.IPrediction.GetPrediction(secondPredictionInput);

                    for (var i = 1; i < 10; i++)
                    {
                        var firstPredictionFirstPoint = NavMesh.WorldToCell(playerPos.Extend(firstPrediction.PredictedPosition, i * 42)).Flags;
                        var secondPredictionFirstPoint = NavMesh.WorldToCell(playerPos.Extend(secondPrediction.PredictedPosition, i * 42)).Flags;
                        var firstPredictionSecondPoint = NavMesh.WorldToCell(playerPos.Extend(firstPrediction.PredictedPosition, i * 45)).Flags;
                        var secondPredictionSecondPoint = NavMesh.WorldToCell(playerPos.Extend(secondPrediction.PredictedPosition, i * 45)).Flags;
                        if (firstPredictionFirstPoint.HasFlag(NavCellFlags.Wall) &&
                            secondPredictionFirstPoint.HasFlag(NavCellFlags.Wall) &&
                            firstPredictionSecondPoint.HasFlag(NavCellFlags.Wall) &&
                            secondPredictionSecondPoint.HasFlag(NavCellFlags.Wall))
                        {
                            SpellClass.E.Cast(jungleTarget);
                        }
                    }
                }
            }

            /// <summary>
            ///     The Q Jungleclear Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent()
                    > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["Jungleclear"]) &&
                MenuClass.Spells["q"]["Jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.Cast(Game.CursorPos);
            }
        }

        #endregion
    }
}