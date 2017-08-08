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
    internal class AttackRangeTracker
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the AttackRange Tracker.
        /// </summary>
        public static void Initialize()
        {
            foreach (var hero in ObjectManager.Get<Obj_AI_Hero>().Where(a =>
                !a.IsMe &&
                !a.IsDead &&
                a.IsVisible &&
                    (a.IsEnemy && MenuClass.AttackRangeTracker["enemies"].As<MenuBool>().Enabled ||
                    a.IsAlly && MenuClass.AttackRangeTracker["allies"].As<MenuBool>().Enabled)))
            {
                var attackRange = hero.AttackRange;
                Render.Circle(hero.ServerPosition, attackRange, 30, UtilityClass.Player.Distance(hero) < attackRange
                    ? Colors.GetRealColor(Color.Red)
                    : Colors.GetRealColor(Color.Yellow));
            }
        }

        #endregion
    }
}