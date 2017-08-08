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
        ///     Initializes the TowerRange Tracker.
        /// </summary>
        public static void Initialize()
        {
            var player = ObjectManager.GetLocalPlayer();
            foreach (var tower in ObjectManager.Get<Obj_AI_Turret>().Where(e =>
                !e.IsDead &&
                e.IsVisible &&
                    (e.IsEnemy && MenuClass.TowerRangeTracker["enemies"].As<MenuBool>().Enabled ||
                    e.IsAlly && MenuClass.TowerRangeTracker["allies"].As<MenuBool>().Enabled)))
            {
                var towerAutoAttackRange = 775f + tower.BoundingRadius + player.BoundingRadius - 10f;
                Render.Circle(tower.ServerPosition, towerAutoAttackRange, 30, tower.IsEnemy && player.Distance(tower) <= towerAutoAttackRange
                    ? Colors.GetRealColor(Color.Red)
                    : Colors.GetRealColor(Color.LightGreen));
            }
        }

        #endregion
    }
}