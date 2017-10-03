// ReSharper disable ArrangeMethodOrOperatorBody

using Aimtec;
using Aimtec.SDK.Extensions;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class MissFortune
    {
        #region Public Methods and Operators

        /// <returns>
        ///     Returns the NetworkId of the passive target.
        /// </returns>
        public int LoveTapTargetNetworkId = 0;

        /// <returns>
        ///     Returns true if the player has the AA Fourth Shot, else false.
        /// </returns>
        public bool IsUltimateShooting()
        {
            return UtilityClass.Player.HasBuff("missfortunebulletsound");
        }

        /// <returns>
        ///     Returns the passive damage multiplier against minions.
        /// </returns>
        public double GetMinionLoveTapDamageMultiplier()
        {
            return
                UtilityClass.Player.TotalAttackDamage * (UtilityClass.Player.Level < 4
                                                                ? 0.25 :
                                                            UtilityClass.Player.Level < 7
                                                                ? 0.3 :
                                                            UtilityClass.Player.Level < 9
                                                                ? 0.35 :
                                                            UtilityClass.Player.Level < 11
                                                                ? 0.4 :
                                                            UtilityClass.Player.Level < 13
                                                                ? 0.45
                                                                : 0.5);
        }

        /// <returns>
        ///     Returns the passive damage multiplier against heroes.
        /// </returns>
        public float GetHeroLoveTapDamageMultiplier()
        {
            return (float)GetMinionLoveTapDamageMultiplier() * 2;
        }

        /// <returns>
        ///     Gets the Q Cone on a determined target unit.
        /// </returns>
        public Geometry.Sector QCone(Obj_AI_Base target)
        {
            var targetPos = target.Position;
            var range = SpellClass.Q2.Range - SpellClass.Q.Range - UtilityClass.Player.BoundingRadius;
            var dir = (targetPos - UtilityClass.Player.Position).Normalized();
            var spot = targetPos + dir * range;

            return new Geometry.Sector(targetPos, spot, SpellClass.Q2.Width, range);
        }

        #endregion
    }
}