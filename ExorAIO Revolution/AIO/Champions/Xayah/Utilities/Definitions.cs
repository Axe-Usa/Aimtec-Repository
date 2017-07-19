// ReSharper disable ArrangeMethodOrOperatorBody


#pragma warning disable 1587

namespace AIO.Champions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;

    using Utilities;

    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Xayah
    {
        #region Fields

        /// <summary>
        ///     Returns the Feathers.
        /// </summary>
        public Dictionary<int, Vector3> Feathers = new Dictionary<int, Vector3>();

        /// <summary>
        ///     Returns true, if the spell should get stopped.
        /// </summary>
        public bool Interrupt = true;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Returns true if the target unit can be hit by any feather.
        /// </summary>
        public bool CanFeathersHitUnit(Obj_AI_Base unit)
        {
            return this.CountFeathersHitOnUnit(unit) > 0;
        }

        /// <summary>
        ///     Returns the number of feathers which can hit the target unit.
        /// </summary>
        public int CountFeathersHitOnUnit(Obj_AI_Base unit)
        {
            var hit = 0;
            foreach (var feather in this.Feathers)
            {
                var playerToFeatherRectangle = new Geometry.Rectangle((Vector2)UtilityClass.Player.ServerPosition, (Vector2)feather.Value, SpellClass.Q.Width);
                if (playerToFeatherRectangle.IsInside((Vector2)unit.ServerPosition))
                {
                    hit++;
                }
            }

            return hit;
        }

        /// <summary>
        ///     Returns the number of minions killable by E Cast.
        /// </summary>
        public int CountFeathersKillableMinions()
        {
            return Extensions.GetAllGenericMinionsTargets().Count(m => this.GetPerfectFeatherDamage(m, this.CountFeathersHitOnUnit(m)) >= m.Health);
        }

        /// <summary>
        ///     Gets the real Damage the E spell would deal to a determined enemy unit.
        /// </summary>
        public double GetPerfectFeatherDamage(Obj_AI_Base unit, int feathers)
        {
            double damage = 0;
            double multiplier = 1;
            for (var cycle = 1; cycle < feathers-1; cycle++)
            {
                multiplier -= 0.1 * cycle;
                damage += UtilityClass.Player.GetSpellDamage(unit, SpellSlot.E) * Math.Max(0.1, multiplier);
            }

            return damage;
        }

        /// <summary>
        ///     Returns true if the target is a perfectly valid rend target.
        /// </summary>
        public bool IsPerfectFeatherTarget(Obj_AI_Base unit)
        {
            if (unit.IsValidTarget() &&
                this.CanFeathersHitUnit(unit))
            {
                switch (unit.Type)
                {
                    case GameObjectType.obj_AI_Minion:
                        return true;

                    case GameObjectType.obj_AI_Hero:
                        return !Invulnerable.Check((Obj_AI_Hero)unit);
                }
            }

            return false;
        }

        /// <summary>
        ///     Reloads the Feathers.
        /// </summary>
        public void ReloadFeathers()
        {
            foreach (var feather in ObjectManager.Get<GameObject>().Where(o => o != null && o.IsValid))
            {
                switch (feather.Name)
                {
                    case "Xayah_Base_Passive_Dagger_Mark8s.troy":
                        this.Feathers.Add(feather.NetworkId, feather.Position);
                        break;
                }
            }
        }

        #endregion
    }
}