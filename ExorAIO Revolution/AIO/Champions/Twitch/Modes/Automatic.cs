
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Twitch
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Called on tick update.
        /// </summary>
        public void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic E Logics.
            /// </summary>
            if (SpellClass.E.Ready)
            {
                /// <summary>
                ///     The E Before death Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["ondeath"].As<MenuBool>().Enabled &&
                    ImplementationClass.IHealthPrediction.GetPrediction(UtilityClass.Player, 1000 + Game.Ping) <= 0)
                {
                    SpellClass.E.Cast();
                }

                /// <summary>
                ///     The Automatic Enemy E Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["logical"].As<MenuSliderBool>().Enabled)
                {
                    if (GameObjects.EnemyHeroes.Any(
                        t =>
                            !Invulnerable.Check(t) &&
                            t.IsValidTarget(SpellClass.E.Range) &&
                            t.GetRealBuffCount("twitchdeadlyvenom") >= MenuClass.Spells["e"]["logical"].As<MenuSliderBool>().Value))
                    {
                        SpellClass.E.Cast();
                    }
                }

                /// <summary>
                ///     The Automatic JungleSteal E Logic.
                /// </summary>
                if (MenuClass.Spells["e"]["junglesteal"].As<MenuBool>().Enabled)
                {
                    foreach (var minion in ObjectManager.Get<Obj_AI_Minion>().Where(m => UtilityClass.JungleList.Contains(m.UnitSkinName)))
                    {
                        if (this.IsPerfectExpungeTarget(minion) &&
                            minion.Health <= this.GetTotalExpungeDamage(minion) &&
                            MenuClass.Spells["e"]["whitelist"][minion.UnitSkinName].As<MenuBool>().Enabled)
                        {
                            SpellClass.E.Cast();
                        }
                    }
                }
            }
        }

        #endregion
    }
}