using System;
using System.Drawing;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Menu.Components;

namespace NabbTracker
{
    /// <summary>
    ///     The drawings class.
    /// </summary>
    internal class SpellTracker
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Initializes the SpellTracker.
        /// </summary>
        public static void Initialize()
        {
            foreach (var unit in
                ObjectManager.Get<Obj_AI_Hero>().Where(
                    e => Math.Abs(e.FloatingHealthBarPosition.X) > 0 && !e.IsDead && e.IsVisible &&
                         (e.IsMe && MenuClass.SpellTracker["me"].As<MenuBool>().Value ||
                          e.IsEnemy && MenuClass.SpellTracker["enemies"].As<MenuBool>().Value ||
                          e.IsAlly && !e.IsMe && MenuClass.SpellTracker["allies"].As<MenuBool>().Value))
            )
            {
                if (unit.Name.Equals("Target Dummy"))
                {
                    return;
                }

                for (var spell = 0; spell < UtilityClass.SpellSlots.Length; spell++)
                {
                    var xSpellOffset = (int)unit.FloatingHealthBarPosition.X + UtilityClass.SpellXAdjustment(unit) + spell * 25;
                    var ySpellOffset = (int)unit.FloatingHealthBarPosition.Y + UtilityClass.SpellYAdjustment(unit);
                    var spellColor = UtilityClass.GetUnitSpellStateColor(unit, spell);
                    var spellCooldown = UtilityClass.GetUnitSpellCooldown(unit, spell);

                    Render.Text(spellCooldown, new Vector2(xSpellOffset, ySpellOffset), RenderTextFlags.None, Colors.GetRealColor(spellColor));

                    for (var level = 0; level <= unit.SpellBook.GetSpell(UtilityClass.SpellSlots[spell]).Level - 1; level++)
                    {
                        var xLevelOffset = xSpellOffset + level * 3 - 4;
                        var yLevelOffset = ySpellOffset + 4;

                        Render.Text(".", new Vector2(xLevelOffset, yLevelOffset), RenderTextFlags.None, Color.White);
                    }
                }

                for (var summonerSpell = 0; summonerSpell < UtilityClass.SummonerSpellSlots.Length; summonerSpell++)
                {
                    var xSummonerSpellOffset = (int)unit.FloatingHealthBarPosition.X-20 + UtilityClass.SummonerSpellXAdjustment(unit) + summonerSpell * 100;
                    var ySummonerSpellOffset = (int)unit.FloatingHealthBarPosition.Y + UtilityClass.SummonerSpellYAdjustment(unit);
                    var summonerSpellColor = UtilityClass.GetUnitSummonerSpellStateColor(unit, summonerSpell);
                    var summonerSpellCooldown = UtilityClass.GetUnitSummonerSpellFixedName(unit, summonerSpell) + ": " + UtilityClass.GetUnitSummonerSpellCooldown(unit, summonerSpell);

                    Render.Text(summonerSpellCooldown, new Vector2(xSummonerSpellOffset, ySummonerSpellOffset), RenderTextFlags.None, Colors.GetRealColor(summonerSpellColor));
                }
            }
        }

        #endregion
    }
}