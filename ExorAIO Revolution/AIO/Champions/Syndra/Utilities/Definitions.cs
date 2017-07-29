﻿// ReSharper disable ArrangeMethodOrOperatorBody


// ReSharper disable LoopCanBeConvertedToQuery
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Collections.Generic;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Damage;
    using Aimtec.SDK.Extensions;

    using Utilities;

    /// <summary>
    ///     The definitions class.
    /// </summary>
    internal partial class Syndra
    {
        #region Fields

        /// <summary>
        ///     Returns the HoldedSphere.
        /// </summary>
        public GameObject HoldedSphere;

        /// <summary>
        ///     Returns the DarkSpheres.
        /// </summary>
        public Dictionary<int, Vector3> DarkSpheres = new Dictionary<int, Vector3>();

        /// <summary>
        ///     Returns the Selected DarkSphere's NetworkID.
        /// </summary>
        public int SelectedDarkSphereNetworkId = 0;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Returns true if the 'obj' object is a DarkSphere, else false.
        /// </summary>
        public bool IsDarkSphere(GameObject obj)
        {
            return obj.Name == "Syndra_Base_Q_idle.troy" || obj.Name == "Syndra_Base_Q_Lv5_idle.troy";
        }

        /// <summary>
        ///     Returns true if Syndra is currently holding a Force of Will object, else false.
        /// </summary>
        public bool IsHoldingForceOfWillObject()
        {
            return UtilityClass.Player.SpellBook.GetSpell(SpellSlot.W).ToggleState == 2;
        }

        /// <summary>
        ///     Gets the best Force of Will object.
        /// </summary>
        public GameObject ForceOfWillObject()
        {
            var possibleTarget1 = GameObjects.JungleLarge.FirstOrDefault(m => m.IsValidSpellTarget(SpellClass.W.Range));
            if (possibleTarget1 != null)
            {
                return possibleTarget1;
            }

            var possibleTarget2 = GameObjects.EnemyMinions.FirstOrDefault(m => m.IsValidSpellTarget(SpellClass.W.Range));
            if (possibleTarget2 != null)
            {
                return possibleTarget2;
            }

            var possibleTarget3 = ObjectManager.Get<GameObject>().FirstOrDefault(o =>
                    o.IsValid &&
                    this.IsDarkSphere(o) &&
                    o.NetworkId != this.SelectedDarkSphereNetworkId &&
                    o.Distance(UtilityClass.Player.ServerPosition) <= SpellClass.W.Range);
            if (possibleTarget3 != null)
            {
                return possibleTarget3;
            }

            return null;
        }

        /// <summary>
        ///     Returns true if the target unit can be hit by any sphere.
        /// </summary>
        public bool CanSpheresHitUnit(Obj_AI_Base unit)
        {
            foreach (var sphere in this.DarkSpheres)
            {
                var targetPos = (Vector2)unit.ServerPosition;
                if (this.DarkSphereScatterRectangle(sphere).IsInside(targetPos) &&
                    UtilityClass.Player.Distance(sphere.Value) < SpellClass.E.Range)
                {
                    switch (unit.Type)
                    {
                        case GameObjectType.obj_AI_Minion:
                            return true;
                        case GameObjectType.obj_AI_Hero:
                            return !Invulnerable.Check((Obj_AI_Hero)unit, DamageType.Magical, false);
                    }
                }
            }

            return false;
        }

        /// <summary>
        ///     Returns true if the target unit can be hit by a determined sphere.
        /// </summary>
        public bool CanSphereHitUnit(Obj_AI_Base unit, KeyValuePair<int, Vector3> sphere)
        {
            var targetPos = (Vector2)unit.ServerPosition;
            if (this.DarkSphereScatterRectangle(sphere).IsInside(targetPos) &&
                UtilityClass.Player.Distance(sphere.Value) < SpellClass.E.Range)
            {
                switch (unit.Type)
                {
                    case GameObjectType.obj_AI_Minion:
                        return true;
                    case GameObjectType.obj_AI_Hero:
                        return !Invulnerable.Check((Obj_AI_Hero)unit, DamageType.Magical, false);
                }
            }

            return false;
        }

        /// <summary>
        ///     Gets the real Damage the R spell would deal to a determined enemy hero.
        /// </summary>
        public double GetPerfectUnleashedPowerDamage(Obj_AI_Hero target)
        {
            var player = UtilityClass.Player;
            var singleSphereDamage = player.GetSpellDamage(target, SpellSlot.R) / 3;
            return singleSphereDamage * player.SpellBook.GetSpell(SpellSlot.R).Ammo;
        }

        /// <summary>
        ///     Returns true if the target is a perfectly valid stunnable target.
        /// </summary>
        public bool IsPerfectSphereTarget(Obj_AI_Base unit)
        {
            if (unit.IsValidTarget() &&
                this.CanSpheresHitUnit(unit))
            {
                switch (unit.Type)
                {
                    case GameObjectType.obj_AI_Minion:
                        return true;

                    case GameObjectType.obj_AI_Hero:
                        return !Invulnerable.Check((Obj_AI_Hero)unit, DamageType.Magical, false);
                }
            }

            return false;
        }

        /// <summary>
        ///     The Sphere Scatter Rectangle.
        /// </summary>
        public Geometry.Rectangle DarkSphereScatterRectangle(KeyValuePair<int, Vector3> sphere)
        {
            return new Geometry.Rectangle(
                           (Vector2)sphere.Value.Extend(UtilityClass.Player.Position, SpellClass.Q.Width*2),
                           (Vector2)sphere.Value.Extend(UtilityClass.Player.Position, -1100f-SpellClass.Q.Width/2+UtilityClass.Player.Distance(sphere.Value)),
                           SpellClass.Q.Width - 100f);
        }

        /// <summary>
        ///     The Scatter the Weak Cone.
        /// </summary>
        public Geometry.Sector ScatterTheWeakCone(Vector2 targetPos)
        {
            return new Geometry.Sector(
                        (Vector2)UtilityClass.Player.Position,
                        targetPos,
                        SpellClass.E.Width,
                        SpellClass.E.Range - 50f);
        }

        /// <summary>
        ///     Reloads the DarkSpheres.
        /// </summary>
        public void ReloadDarkSpheres()
        {
            foreach (var sphere in ObjectManager.Get<GameObject>().Where(o => o != null && o.IsValid))
            {
                if (sphere.IsValid)
                {
                    if (this.DarkSpheres.Any(o => o.Key == sphere.NetworkId))
                    {
                        this.DarkSpheres.Remove(sphere.NetworkId);
                    }
                }

                switch (sphere.Name)
                {
                    case "Syndra_Base_Q_idle.troy":
                    case "Syndra_Base_Q_Lv5_idle.troy":
                        this.DarkSpheres.Add(sphere.NetworkId, sphere.Position);
                        break;
                }
            }
        }

        #endregion
    }
}