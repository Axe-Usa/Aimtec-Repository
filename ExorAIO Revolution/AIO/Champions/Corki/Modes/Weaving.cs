
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
    internal partial class Corki
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
            ///     The Q Combo Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                if (!heroTarget.IsValidTarget() ||
                    Invulnerable.Check(heroTarget, DamageType.Magical) ||
                    heroTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(heroTarget)))
                {
                    return;
                }

                SpellClass.Q.Cast(heroTarget);
            }

            /// <summary>
            ///     The R Combo Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["combo"].As<MenuBool>().Enabled)
            {
                if (!heroTarget.IsValidTarget() ||
                    Invulnerable.Check(heroTarget, DamageType.Magical) ||
                    heroTarget.IsValidTarget(UtilityClass.Player.GetFullAttackRange(heroTarget)))
                {
                    return;
                }

                SpellClass.R.Cast(heroTarget);
            }
        }

        #endregion
    }
}