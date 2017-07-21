namespace AIO.Utilities
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;

    /// <summary>
    ///     The Bools class.
    /// </summary>
    internal static class Bools
    {
        #region Static Fields

        /// <returns>
        ///     true if the champion is supported by the AIO; otherwise, false.
        /// </returns>
        public static bool IsChampionSupported = true;

        #endregion

        #region Public Methods and Operators

        /// <returns>
        ///     true if an unit has a Sheen-Like buff; otherwise, false.
        /// </returns>
        public static bool HasSheenLikeBuff(this Obj_AI_Hero unit)
        {
            var sheenLikeBuffNames = new[] { "sheen", "LichBane", "dianaarcready", "ItemFrozenFist", "sonapassiveattack", "AkaliTwinDisciplines" };
            return sheenLikeBuffNames.Any(b => UtilityClass.Player.HasBuff(b));
        }

        /// <summary>
        ///     Gets a value indicating whether a determined hero has a stackable item.
        /// </summary>
        public static bool HasTearLikeItem(this Obj_AI_Hero unit)
        {

            return
                unit.HasItem(ItemId.Manamune) ||
                unit.HasItem(ItemId.ArchangelsStaff) ||
                unit.HasItem(ItemId.TearoftheGoddess) ||
                unit.HasItem(ItemId.ManamuneQuickCharge) ||
                unit.HasItem(ItemId.ArchangelsStaffQuickCharge) ||
                unit.HasItem(ItemId.TearoftheGoddessQuickCharge);
        }

        /// <returns>
        ///     true if an unit is a Building; otherwise, false.
        /// </returns>
        public static bool IsBuilding(this AttackableUnit unit)
        {
            switch (unit.Type)
            {
                case GameObjectType.obj_AI_Turret:
                case GameObjectType.obj_BarracksDampener:
                case GameObjectType.obj_HQ:
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Gets a value indicating whether a determined champion can move or not.
        /// </summary>
        public static bool IsImmobile(this Obj_AI_Base target)
        {
            if (target.Name.Equals("Target Dummy"))
            {
                return false;
            }

            if (target.Buffs.Any(
                buff =>
                    buff.Type == BuffType.Stun ||
                    buff.Type == BuffType.Fear ||
                    buff.Type == BuffType.Flee ||
                    buff.Type == BuffType.Snare ||
                    buff.Type == BuffType.Taunt ||
                    buff.Type == BuffType.Charm ||
                    buff.Type == BuffType.Knockup ||
                    buff.Type == BuffType.Suppression))
            {
                return true;
            }

            switch (target.ActionState)
            {
                case ActionState.Asleep:
                case ActionState.Feared:
                case ActionState.Charmed:
                case ActionState.Fleeing:
                case ActionState.Surpressed:
                    return true;
            }

            return false;
        }

        /// <summary>
        ///     Checks whether the unit should preserve the sheen buff.
        /// </summary>
        public static bool ShouldPreserveSheen(this Obj_AI_Hero source)
        {
            return source.ActionState.HasFlag(ActionState.CanAttack);
        }

        /// <returns>
        ///     true if the sender is a hero, a turret or an important jungle monster; otherwise, false.
        /// </returns>
        public static bool ShouldShieldAgainstSender(Obj_AI_Base sender)
        {
            return
                GameObjects.EnemyHeroes.Contains(sender) ||
                GameObjects.EnemyTurrets.Contains(sender) ||
                Extensions.GetGenericJungleMinionsTargets().Contains(sender);
        }

        #endregion
    }
}