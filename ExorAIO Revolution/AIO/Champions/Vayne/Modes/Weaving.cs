
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
    internal partial class Vayne
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
            ///     The Q Weaving Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
            {
                if (heroTarget.GetRealBuffCount("vaynesilvereddebuff") != 1 &&
                    MenuClass.Spells["q"]["customization"]["wstacks"].As<MenuBool>().Enabled)
                {
                    return;
                }

                if (UtilityClass.Player.Distance(Game.CursorPos) <= UtilityClass.Player.AttackRange &&
                    MenuClass.Spells["q"]["customization"]["onlyqifmouseoutaarange"].As<MenuBool>().Enabled)
                {
                    return;
                }

                var posAfterQ = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, 300f);
                var qRangeCheck = MenuClass.Spells["q"]["customization"]["qrangecheck"];
                if (qRangeCheck != null)
                {
                    if (qRangeCheck.As<MenuBool>().Enabled &&
                        qRangeCheck.As<MenuSliderBool>().Value <
                            posAfterQ.CountEnemyHeroesInRange(UtilityClass.Player.AttackRange))
                    {
                        return;
                    }
                }

                if (posAfterQ.Distance(heroTarget) > UtilityClass.Player.AttackRange &&
                    MenuClass.Spells["q"]["customization"]["noqoutaarange"].As<MenuBool>().Enabled)
                {
                    return;
                }

                SpellClass.Q.Cast(Game.CursorPos);
            }
        }

        #endregion
    }
}