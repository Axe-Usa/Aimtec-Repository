#pragma warning disable 1587
namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;
    using Aimtec.SDK.Prediction;
    using Aimtec.SDK.Prediction.Skillshots;

    using Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Vayne
    {
        /// <summary>
        ///     Called on post attack.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public static void Jungleclear(object sender, PostAttackEventArgs args)
        {
            var jungleTarget = (Obj_AI_Minion)Orbwalker.Implementation.GetTarget();
            if (!UtilityClass.GetGenericJungleMinionsTargets().Contains(jungleTarget))
            {
                return;
            }

            /// <summary>
            ///     The E JungleClear Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.E.Slot, MenuClass.Spells["e"]["jungleclear"]) &&
                MenuClass.Spells["e"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                if (!UtilityClass.GetLegendaryJungleMinionsTargets().Contains(jungleTarget))
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
                    var firstPrediction = Prediction.Implementation.GetPrediction(firstPredictionInput);
                    var secondPrediction = Prediction.Implementation.GetPrediction(secondPredictionInput);

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
            ///     The Q JungleClear Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.ManaPercent() >
                    ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Spells["q"]["jungleclear"]) &&
                MenuClass.Spells["q"]["jungleclear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.Cast(Game.CursorPos);
            }
        }
    }
}
