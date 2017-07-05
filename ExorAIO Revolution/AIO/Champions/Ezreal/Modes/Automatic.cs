
#pragma warning disable 1587

namespace AIO.Champions
{
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Orbwalking;

    using AIO.Utilities;

    /// <summary>
    ///     The champion class.
    /// </summary>
    internal partial class Ezreal
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Automatic()
        {
            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Tear Stacking Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                UtilityClass.Player.HasTearLikeItem() &&
                !Extensions.GetEnemyLaneMinionsTargets().Any() &&
                UtilityClass.IOrbwalker.Mode == OrbwalkingMode.None &&
                UtilityClass.Player.CountEnemyHeroesInRange(1500) == 0 &&
                UtilityClass.Player.ManaPercent()
                > ManaManager.GetNeededMana(SpellClass.Q.Slot, MenuClass.Miscellaneous["tear"]) &&
                MenuClass.Miscellaneous["tear"].As<MenuSliderBool>().Enabled)
            {
                SpellClass.Q.Cast(Game.CursorPos);
            }

            /// <summary>
            ///     The Semi-Automatic R Logic.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["bool"].As<MenuBool>().Enabled &&
                MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
            {
                var bestTarget = GameObjects.EnemyHeroes
                    .Where(
                        t =>
                            t.IsValidTarget(2000f) &&
                            !Invulnerable.Check(t, DamageType.Magical, false) &&
                            MenuClass.Spells["r"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled)
                    .OrderBy(o => o.Health)
                    .FirstOrDefault();
                if (bestTarget != null)
                {
                    SpellClass.R.Cast(bestTarget);
                }
            }

            if (ObjectManager.GetLocalPlayer().TotalAttackDamage < ObjectManager.GetLocalPlayer().TotalAbilityDamage)
            {
                return;
            }

            var target = UtilityClass.IOrbwalker.GetTarget();

            /// <summary>
            ///     Initializes the orbwalkingmodes.
            /// </summary>
            switch (UtilityClass.IOrbwalker.Mode)
            {
                case OrbwalkingMode.Combo:
                    if (!(target is Obj_AI_Hero))
                    {
                        return;
                    }
                    break;

                case OrbwalkingMode.Laneclear:
                    if (!target.IsBuilding())
                    {
                        return;
                    }
                    break;

                default:
                    if (!GameObjects.Jungle.Contains(target) && !(target is Obj_AI_Hero) && !target.IsBuilding())
                    {
                        return;
                    }
                    break;
            }

            /// <summary>
            ///     The Automatic W Logic.
            /// </summary>
            if (SpellClass.W.Ready &&
                UtilityClass.Player.ManaPercent()
                > ManaManager.GetNeededMana(SpellClass.W.Slot, MenuClass.Spells["w"]["logical"]) &&
                MenuClass.Spells["w"]["logical"].As<MenuSliderBool>().Enabled)
            {
                foreach (var ally in GameObjects.AllyHeroes.Where(a => !a.IsMe && a.SpellBook.IsAutoAttacking && a.IsValidTarget(SpellClass.W.Range, true)))
                {
                    SpellClass.W.Cast(ally);
                }
            }
        }

        #endregion
    }
}