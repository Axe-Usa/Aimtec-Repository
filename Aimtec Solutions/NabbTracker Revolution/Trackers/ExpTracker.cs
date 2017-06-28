namespace NabbTracker
{
    using System;
    using System.Drawing;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal class ExpTracker
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the Experience Tracker.
        /// </summary>
        public static void Initialize()
        {
            foreach (var unit in
                ObjectManager.Get<Obj_AI_Hero>().Where(e => Math.Abs(e.FloatingHealthBarPosition.X) > 0 && !e.IsDead && e.IsVisible &&
                    (e.IsMe && MenuClass.ExpTracker["me"].As<MenuBool>().Value ||
                    e.IsEnemy && MenuClass.ExpTracker["enemies"].As<MenuBool>().Value ||
                    e.IsAlly && !e.IsMe && MenuClass.ExpTracker["allies"].As<MenuBool>().Value))
                )
            {
                if (unit.Name.Equals("Target Dummy"))
                {
                    return;
                }

                var xOffset = (int)unit.FloatingHealthBarPosition.X + UtilityClass.ExpXAdjustment(unit);
                var yOffset = (int)unit.FloatingHealthBarPosition.Y + UtilityClass.ExpYAdjustment(unit);

                var actualExp = unit.Exp;
                if (unit.Level > 1)
                {
                    actualExp -= (280 + 80 + 100 * unit.Level) / 2 * (unit.Level - 1);
                }

                var neededExp = 180 + 100 * unit.Level;
                var expPercent = (int)(actualExp / neededExp * 100);
                if (unit.Level < 18 || UtilityClass.Player.HasBuff("AwesomeBuff") && unit.Level < 30)
                {
                    RenderManager.RenderLine(xOffset - 76, yOffset + 20, xOffset + 56, yOffset + 20, 7, true,Colors.GetRealColor(Color.Purple));

                    if (expPercent > 0)
                    {
                        RenderManager.RenderLine(xOffset - 76, yOffset + 20, xOffset - 76 + (float)(1.32 * expPercent), yOffset + 20, 7, true, Colors.GetRealColor(Color.Red));
                    }
                    RenderManager.RenderText(xOffset - 13, yOffset + 17, Colors.GetRealColor(Color.Yellow), expPercent > 0 ? expPercent + "%" : "0%");
                }
            }
        }

        #endregion
    }
}