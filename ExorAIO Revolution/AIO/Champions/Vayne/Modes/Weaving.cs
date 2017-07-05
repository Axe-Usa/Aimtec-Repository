
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
                if (MenuClass.Miscellaneous["wstacks"].As<MenuBool>().Enabled
                    && heroTarget.GetBuffCount("vaynesilvereddebuff") != 1)
                {
                    return;
                }

                if (!MenuClass.Miscellaneous["alwaysq"].As<MenuBool>().Enabled)
                {
                    var posAfterQ = UtilityClass.Player.Position.Extend(Game.CursorPos, 300f);
                    if (posAfterQ.CountEnemyHeroesInRange(1000f) < 3 &&
                        UtilityClass.Player.Distance(Game.CursorPos) > UtilityClass.Player.GetFullAttackRange(heroTarget))
                    {
                        SpellClass.Q.Cast(Game.CursorPos);
                    }
                }
                else
                {
                    SpellClass.Q.Cast(Game.CursorPos);
                }
            }
        }

        #endregion
    }
}