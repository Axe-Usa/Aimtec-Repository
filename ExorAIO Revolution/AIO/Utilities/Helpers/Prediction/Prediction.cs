namespace AIO.Utilities
{
    using System;
    using System.Collections.Generic;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Prediction.Collision;
    using Aimtec.SDK.Prediction.Skillshots;

    using Spell = Aimtec.SDK.Spell;

    internal static class Prediction
    {
        internal static PredictionOutput GetPrediction(Spell spell, Obj_AI_Hero unit)
        {
            var result = new PredictionOutput();

            var input = spell.GetPredictionInput(unit);
            if (input == null || !input.Unit.IsValidTarget())
            {
                return result;
            }

            if (input.Unit.IsMoving)
            {
                return GetMovementPrediction(spell, input);
            }

            return spell.GetPrediction(unit);
        }

        private static PredictionOutput GetMovementPrediction(Spell spell, PredictionInput input)
        {
            var unitPosition = Vector3.Zero;

            var paths = input.Unit.Path;

            for (var i = 0; i < paths.Length - 1; i++)
            {
                var previousPath = paths[i];
                var currentPath = paths[i + 1];
                var remainingLength = input.Unit.ServerPosition.Distance(currentPath);

                var direction = (currentPath - previousPath).Normalized();
                var velocity = direction * input.Unit.MoveSpeed;

                unitPosition = input.Unit.ServerPosition + velocity * input.Delay;

                var toUnit = (unitPosition - input.From).Normalized();
                var cosTheta = Vector3.Dot(direction, toUnit);
                var calculation = 1 - Math.Abs(cosTheta);
                var castDirection = cosTheta > 0 ? -direction : direction;

                unitPosition = unitPosition + castDirection * input.Unit.BoundingRadius;
                unitPosition = unitPosition + castDirection * (calculation * (input.Radius * 0.5f));

                var unitDistance = input.From.Distance(unitPosition);

                var a = Vector3.Dot(velocity, velocity) - (Math.Abs(input.Speed - float.MaxValue) < 0
                            ? float.MaxValue
                            : (float)Math.Pow(input.Speed, 2));

                var b = 2 * unitDistance * input.Unit.MoveSpeed * (Math.Abs(cosTheta) <= 0 ? 0.1f : cosTheta);
                var c = (float)Math.Pow(unitDistance, 2);

                var discriminant = b * b - 4f * a * c;
                if (discriminant < 0)
                {
                    return new PredictionOutput { HitChance = HitChance.OutOfRange };
                }

                var impactTime = 2f * c / ((float)Math.Sqrt(discriminant) - b);
                if (impactTime < 0)
                {
                    return new PredictionOutput { HitChance = HitChance.None };
                }

                if (remainingLength / input.Unit.MoveSpeed < impactTime)
                {
                    unitPosition = currentPath;
                    break;
                }

                unitPosition = input.Unit.ServerPosition + velocity * impactTime;

                var checkPosition = unitPosition + direction * input.Delay + castDirection * (input.Unit.BoundingRadius + input.Radius);
                if (input.From.Distance(checkPosition) > input.Range)
                {
                    return new PredictionOutput { HitChance = HitChance.OutOfRange };
                }
            }

            var collisionObjects = Collision.GetCollision(
                    new List<Vector3> { input.Unit.ServerPosition, unitPosition },
                    input);

            return new PredictionOutput
            {
                UnitPosition = unitPosition,
                CastPosition = unitPosition,
                CollisionObjects = collisionObjects,
                HitChance = collisionObjects.Count >= 1
                    ? HitChance.Collision
                    : spell.HitChance
            };
        }
    }
}