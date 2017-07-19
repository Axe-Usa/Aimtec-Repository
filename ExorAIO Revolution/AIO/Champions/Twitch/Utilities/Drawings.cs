// ReSharper disable MergeConditionalExpression


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
    internal partial class Twitch
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the menus.
        /// </summary>
        public void Drawings()
        {
            /// <summary>
            ///     Loads the Q duration drawing.
            /// </summary>
            if (MenuClass.Drawings["qduration"].As<MenuBool>().Enabled)
            {
                var buff = UtilityClass.Player.GetBuff("TwitchHideInShadows");
                if (buff != null)
                {
                    Render.Circle(UtilityClass.Player.Position, (buff.EndTime - Game.ClockTime) * UtilityClass.Player.MoveSpeed, 30, Color.Green);
                }
                else
                {
                    var qDuration = new[] { 10, 11, 12, 13, 14 }[UtilityClass.Player.SpellBook.GetSpell(SpellSlot.Q).Level - 1];
                    Render.Circle(UtilityClass.Player.Position, qDuration * UtilityClass.Player.MoveSpeed, 30, Color.Green);
                    Geometry.DrawCircleOnMinimap(UtilityClass.Player.Position, qDuration * UtilityClass.Player.MoveSpeed, Color.White);
                }
            }

            /// <summary>
            ///     Loads the W drawing.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Drawings["w"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.W.Range, 30, Color.Purple);
            }

            /// <summary>
            ///     Loads the E drawing.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                if (MenuClass.Drawings["e"].As<MenuBool>().Enabled)
                {
                    Render.Circle(UtilityClass.Player.Position, SpellClass.E.Range, 30, Color.Cyan);
                }

                /// <summary>
                ///     Loads the E damage to healthbar.
                /// </summary>
                if (MenuClass.Drawings["edmg"].As<MenuBool>().Enabled)
                {
                    ObjectManager.Get<Obj_AI_Base>()
                        .Where(h => this.IsPerfectExpungeTarget(h) && (h is Obj_AI_Hero || UtilityClass.JungleList.Contains(h.UnitSkinName)))
                        .ToList()
                        .ForEach(
                            unit =>
                                {
                                    var heroUnit = unit as Obj_AI_Hero;
                                    var jungleList = UtilityClass.JungleList;
                                    var mobOffset = DrawingClass.JungleHpBarOffsetList.FirstOrDefault(x => x.UnitSkinName.Equals(unit.UnitSkinName));

                                    int width;
                                    if (jungleList.Contains(unit.UnitSkinName))
                                    {
                                        width = mobOffset != null ? mobOffset.Width : DrawingClass.SWidth;
                                    }
                                    else
                                    {
                                        width = DrawingClass.SWidth;
                                    }

                                    int height;
                                    if (jungleList.Contains(unit.UnitSkinName))
                                    {
                                        height = mobOffset != null ? mobOffset.Height : DrawingClass.SHeight;
                                    }
                                    else
                                    {
                                        height = DrawingClass.SHeight;
                                    }

                                    int xOffset;
                                    if (jungleList.Contains(unit.UnitSkinName))
                                    {
                                        xOffset = mobOffset != null ? mobOffset.XOffset : DrawingClass.SxOffset(heroUnit);
                                    }
                                    else
                                    {
                                        xOffset = DrawingClass.SxOffset(heroUnit);
                                    }

                                    int yOffset;
                                    if (jungleList.Contains(unit.UnitSkinName))
                                    {
                                        yOffset = mobOffset != null ? mobOffset.YOffset : DrawingClass.SyOffset(heroUnit);
                                    }
                                    else
                                    {
                                        yOffset = DrawingClass.SyOffset(heroUnit);
                                    }

                                    var barPos = unit.FloatingHealthBarPosition;
                                    barPos.X += xOffset;
                                    barPos.Y += yOffset;

                                    var drawEndXPos = barPos.X + width * (unit.HealthPercent() / 100);
                                    var drawStartXPos = (float)(barPos.X + (unit.GetRealHealth() > this.GetTotalExpungeDamage(unit)
                                                                                ? width * ((unit.GetRealHealth() - this.GetTotalExpungeDamage(unit)) / unit.MaxHealth * 100 / 100)
                                                                                : 0));

                                    Render.Line(drawStartXPos, barPos.Y, drawEndXPos, barPos.Y, height, true, unit.GetRealHealth() < this.GetTotalExpungeDamage(unit) ? Color.Blue : Color.Orange);
                                    Render.Line(drawStartXPos, barPos.Y, drawStartXPos, barPos.Y + height + 1, 1, true, Color.Lime);
                                });
                }
            }

            /// <summary>
            ///     Loads the R drawing.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Drawings["r"].As<MenuBool>().Enabled)
            {
                Render.Circle(UtilityClass.Player.Position, SpellClass.R.Range, 30, Color.Red);
            }
        }

        #endregion
    }
}