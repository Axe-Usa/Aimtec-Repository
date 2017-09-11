﻿
using System.Drawing;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using AIO.Utilities;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal partial class Darius
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
            if (SpellClass.Q.Ready &&
                MenuClass.Drawings["q"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.Q.Range, 30, Color.LightGreen);
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
            if (SpellClass.R.Ready)
            {
                if (MenuClass.Drawings["r"].As<MenuBool>().Enabled)
                {
                    Render.Circle(UtilityClass.Player.Position, SpellClass.R.Range, 30, Color.Red);
                }

                /// <summary>
                ///     Loads the R damage to healthbar.
                /// </summary>
                if (MenuClass.Drawings["rdmg"].As<MenuBool>().Enabled)
                {
                    Extensions.GetEnemyHeroesTargets()
                        .Where(h => h.IsValidTarget() && !Invulnerable.Check(h))
                        .ToList()
                        .ForEach(
                            target =>
                            {
                                var width = DrawingClass.SWidth;
                                var height = DrawingClass.SHeight;
                                var xOffset = DrawingClass.SxOffset(target);
                                var yOffset = DrawingClass.SyOffset(target);

                                var barPos = target.FloatingHealthBarPosition;
                                barPos.X += xOffset;
                                barPos.Y += yOffset;

                                var drawEndXPos = barPos.X + width * (target.HealthPercent() / 100);
                                var drawStartXPos = (float)(barPos.X + (target.GetRealHealth() > GetPerfectNoxianGuillotineDamage(target)
                                                                ? width * ((target.GetRealHealth() - GetPerfectNoxianGuillotineDamage(target)) / target.MaxHealth * 100 / 100)
                                                                : 0));

                                Render.Line(drawStartXPos, barPos.Y, drawEndXPos, barPos.Y, height, true, target.GetRealHealth() < GetPerfectNoxianGuillotineDamage(target) ? Color.Blue : Color.Orange);
                                Render.Line(drawStartXPos, barPos.Y, drawStartXPos, barPos.Y + height + 1, 1, true, Color.Lime);
                            });
                }
            }
        }

        #endregion
    }
}