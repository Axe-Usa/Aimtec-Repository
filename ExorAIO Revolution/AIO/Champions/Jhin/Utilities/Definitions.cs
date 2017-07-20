// ReSharper disable ArrangeMethodOrOperatorBody


#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;

    using AIO.Utilities;

    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Jhin
    {
        #region Fields

        /// <summary>
        ///     The args End.
        /// </summary>
        public Vector3 End = Vector3.Zero;

        /// <returns>
        ///     The Jhin's ultimate shot count.
        /// </returns>
        public int UltimateShotsCount;

        #endregion

        #region Public Methods and Operators

        /// <returns>
        ///     Returns true if the player has the AA Fourth Shot, else false.
        /// </returns>
        public bool HasFourthShot()
        {
            return UtilityClass.Player.HasBuff("jhinpassiveattackbuff");
        }

        /// <returns>
        ///     Returns true if the player has the Ultimate Fourth Shot, else false.
        /// </returns>
        public bool HasUltimateFourthShot()
        {
            return this.UltimateShotsCount == 3;
        }

        /// <returns>
        ///     Returns true if the player is reloading, else false.
        /// </returns>
        public bool IsReloading()
        {
            return UtilityClass.Player.HasBuff("JhinPassiveReload");
        }

        /// <returns>
        ///     Returns true if the player has the AA Fourth Shot, else false.
        /// </returns>
        public bool IsUltimateShooting()
        {
            return UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).Name.Equals("JhinRShot");
        }

        /// <summary>
        ///     The Ultimate Cone.
        /// </summary>
        public Geometry.Sector UltimateCone()
        {
            return new Geometry.Sector(
                (Vector2)UtilityClass.Player.Position.Extend(this.End, -UtilityClass.Player.BoundingRadius * 3),
                (Vector2)this.End,
                UtilityClass.GetAngleByDegrees(55f),
                SpellClass.R.Range);
        }

        #endregion
    }
}