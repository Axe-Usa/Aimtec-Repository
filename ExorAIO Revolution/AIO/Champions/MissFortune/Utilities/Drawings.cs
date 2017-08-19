
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Drawing;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class MissFortune
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the menus.
        /// </summary>
        public void Drawings()
        {
            /// <summary>
            ///     Loads the Q drawing.
            /// </summary>
            if (SpellClass.Q.Ready)
            {
                /// <summary>
                ///     Loads the Q drawing.
                /// </summary>
                if (MenuClass.Drawings["q"].As<MenuBool>().Enabled)
                {
                    Render.Circle(UtilityClass.Player.Position, SpellClass.Q.Range, 30, Color.LightGreen);
                }

                /// <summary>
                ///     Loads the Extended Q drawing.
                /// </summary>
                if (MenuClass.Drawings["qcone"].As<MenuBool>().Enabled)
                {
                    var unitsToIterate = Extensions.GetAllGenericUnitTargetsInRange(SpellClass.Q.Range);
                    foreach (var obj in unitsToIterate)
                    {
                        var polygon = this.QCone(obj);
                        foreach (var target in Extensions.GetBestEnemyHeroesTargetsInRange(SpellClass.Q2.Range).Where(t =>
                               !Invulnerable.Check(t) &&
                               t.IsValidTarget(SpellClass.Q2.Range)))
                        {
                            polygon.Draw(
                                polygon.IsInside((Vector2)target.ServerPosition) &&
                                (target.NetworkId == this.LoveTapTargetNetworkId || GameObjects.EnemyMinions.All(m => polygon.IsOutside((Vector2)m.ServerPosition))) &&
                                polygon.IsInside((Vector2)SpellClass.Q.GetPrediction(target).CastPosition)
                                    ? Color.Green
                                    : Color.Red);
                        }
                    }
                }
            }

            /// <summary>
            ///     Loads the E drawing.
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Drawings["e"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.E.Range, 30, Color.Cyan);
            }

            /// <summary>
            ///     Loads the R drawing.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Drawings["r"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.R.Range, 30, Color.Red);
            }

            /// <summary>
            ///     Loads the Passive drawing.
            /// </summary>
            if (MenuClass.Drawings["passivetarget"].As<MenuBool>().Enabled)
            {
                var target = ObjectManager.Get<Obj_AI_Base>().FirstOrDefault(u => u.NetworkId == this.LoveTapTargetNetworkId);
                if (target != null)
                {
                    Render.Circle(target.Position, target.BoundingRadius, 30, Color.Black);
                }
            }
        }

        #endregion
    }
}