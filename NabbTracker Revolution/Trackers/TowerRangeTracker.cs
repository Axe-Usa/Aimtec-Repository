namespace NabbTracker
{
    using System.Drawing;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal class TowerRangeTracker
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the Experience Tracker.
        /// </summary>
        public static void Initialize()
        {
            var player = ObjectManager.GetLocalPlayer();
            foreach (var tower in
                ObjectManager.Get<Obj_AI_Turret>().Where(
                    e => !e.IsDead && e.IsVisible &&
                         (e.IsEnemy && MenuClass.TowerRangeTracker["enemies"].As<MenuBool>().Value ||
                          e.IsAlly && MenuClass.TowerRangeTracker["allies"].As<MenuBool>().Value)))
            {
                var towerAutoAttackRange = 775f + tower.BoundingRadius + player.BoundingRadius - 10f;
                Render.Circle(tower.ServerPosition, towerAutoAttackRange, 30, player.Distance(tower) > towerAutoAttackRange
                    ? Colors.GetRealColor(Color.LightGreen)
                    : Colors.GetRealColor(Color.Red));
            }
        }

        #endregion
    }
}