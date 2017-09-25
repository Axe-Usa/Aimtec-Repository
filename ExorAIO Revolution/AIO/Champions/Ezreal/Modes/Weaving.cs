
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu.Components;
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Prediction.Skillshots;
using AIO.Utilities;
using Prediction = AIO.Utilities.Prediction;

#pragma warning disable 1587

namespace AIO.Champions
{
    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ezreal
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="PostAttackEventArgs" /> instance containing the event data.</param>
        public void Weaving(object sender, PostAttackEventArgs args)
        {
            var heroTarget = args.Target as Obj_AI_Hero;
            if (heroTarget == null)
            {
                return;
            }

            /// <summary>
            ///     The Q Weaving Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                var output = Prediction.GetPrediction(SpellClass.Q, heroTarget);
                if (output?.HitChance >= HitChance.Low)
                {
                    SpellClass.Q.Cast(output.CastPosition);
                    return;
                }
            }

            /// <summary>
            ///     The W Weaving Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
            {
                var buffMenu = MenuClass.Spells["w"]["buff"];
                if (buffMenu != null)
                {
                    if (UtilityClass.Player.TotalAbilityDamage < GetMinimumApForApMode() &&
                        UtilityClass.Player.ManaPercent()
                        > ManaManager.GetNeededMana(SpellClass.W.Slot, buffMenu["logical"]) &&
                        buffMenu["logical"].As<MenuSliderBool>().Enabled &&
                        GameObjects.AllyHeroes.Any(a =>
                            !a.IsMe &&
                            a.IsValidTarget(SpellClass.W.Range, true) &&
                            buffMenu["allywhitelist"][a.ChampionName.ToLower()].As<MenuBool>().Enabled))
                    {
                        return;
                    }
                }

                var output = Prediction.GetPrediction(SpellClass.W, heroTarget);
                if (output?.HitChance >= HitChance.Low)
                {
                    SpellClass.W.Cast(output.CastPosition);
                }
            }
        }

        #endregion
    }
}