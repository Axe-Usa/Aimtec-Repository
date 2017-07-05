
#pragma warning disable 1587

namespace AIO.Champions
{
    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Caitlyn
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on post attack.
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
            ///     The E Combo Logic.
            /// </summary>
            if (SpellClass.E.Ready &&
                !Invulnerable.Check(heroTarget) &&
                !heroTarget.HasBuff("caitlynyordletrapinternal") &&
                MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
            {
                if (SpellClass.W.Ready &&
                    MenuClass.Spells["w"]["triplecombo"].As<MenuBool>().Enabled)
                {
                    if (heroTarget.IsValidTarget(SpellClass.W.Range - 50f))
                    {
                        SpellClass.E.Cast(heroTarget);
                    }
                }
                else
                {
                    if (heroTarget.IsValidTarget(SpellClass.E.Range - 100f))
                    {
                        SpellClass.E.Cast(heroTarget);
                    }
                }
            }
        }

        #endregion
    }
}