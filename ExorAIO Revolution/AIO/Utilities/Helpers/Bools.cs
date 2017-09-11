using System.Linq;
using Aimtec;
using Aimtec.SDK.Damage;
using Aimtec.SDK.Events;
using Aimtec.SDK.Extensions;

namespace AIO.Utilities
{
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

        /// <summary>
        ///     Returns if the name is an auto attack
        /// </summary>
        /// <param name="name">Name of spell</param>
        /// <returns>The <see cref="bool" /></returns>
        public static bool IsAutoAttack(string name)
        {
            name = name.ToLower();
            return name.Contains("attack") && !UtilityClass.NoAttacks.Contains(name) || UtilityClass.Attacks.Contains(name);
        }

        /// <returns>
        ///     true if a determined cell has a Wall flag, else, false.
        /// </returns>
        public static bool IsWall(this Vector3 pos, bool includeBuildings = false)
        {
            var point = NavMesh.WorldToCell(pos).Flags;
            return point.HasFlag(NavCellFlags.Wall) || includeBuildings && point.HasFlag(NavCellFlags.Building);
        }

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

        /// <summary>
        ///     Returns true if there is a Wall between X pos and Y pos.
        /// </summary>
        public static bool AnyWallInBetween(Vector2 startPos, Vector2 endPos)
        {
            for (var i = 0; i < startPos.Distance(endPos); i+=5)
            {
                var point = NavMesh.WorldToCell((Vector3)startPos.Extend(endPos, i));
                if (point.Flags.HasFlag(NavCellFlags.Wall) ||
                    point.Flags.HasFlag(NavCellFlags.Building))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Returns true if a determined buff is a Hard CC Buff.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static bool IsHardCC(this Buff buff)
        {
            return
                buff.Type == BuffType.Stun ||
                buff.Type == BuffType.Fear ||
                buff.Type == BuffType.Flee ||
                buff.Type == BuffType.Snare ||
                buff.Type == BuffType.Taunt ||
                buff.Type == BuffType.Charm ||
                buff.Type == BuffType.Knockup ||
                buff.Type == BuffType.Suppression;
        }

        /// <summary>
        ///     Gets a value indicating whether a determined champion can move or not.
        /// </summary>
        public static bool IsImmobile(this Obj_AI_Base target)
        {
            if (target.IsDead ||
                target.IsDashing() ||
                target.Name.Equals("Target Dummy") ||
                target.HasBuffOfType(BuffType.Knockback))
            {
                return false;
            }

            if (target.ValidActiveBuffs().Any(buff => buff.IsHardCC()))
            {
                return true;
            }

            if (!target.ActionState.HasFlag(ActionState.CanMove))
            {
                return true;
            }

            return false;
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

        /// <summary>
        ///     Returns whether the hero is in fountain range.
        /// </summary>
        /// <param name="hero">The Hero</param>
        /// <returns>Is Hero in fountain range</returns>
        public static bool InFountain(this Obj_AI_Hero hero)
        {
            var heroTeam = hero.Team == GameObjectTeam.Order ? "Order" : "Chaos";
            var fountainTurret = ObjectManager.Get<GameObject>().FirstOrDefault(o => o.IsValid && o.Name == "Turret_" + heroTeam + "TurretShrine");
            if (fountainTurret == null)
            {
                return false;
            }

            return hero.Distance(fountainTurret) < 1300f;
        }

        #endregion
    }
}