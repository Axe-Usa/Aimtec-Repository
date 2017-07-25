
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
    internal partial class MissFortune
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Automatic()
        {
            var passiveObject = ObjectManager.Get<GameObject>().FirstOrDefault(o => o.IsValid && o.Name == "MissFortune_Base_P_Mark.troy");
            if (passiveObject != null)
            {
                var objAiBase = Extensions.GetAllGenericUnitTargets()
                    .Where(m => m.IsValidTarget())
                    .OrderBy(o => o.Distance(passiveObject))
                    .FirstOrDefault();

                this.LoveTapTargetNetworkId = objAiBase?.NetworkId ?? 0;
            }
            else
            {
                this.LoveTapTargetNetworkId = 0;
            }

            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["bool"].As<MenuBool>().Enabled)
            {
                var bestTarget = GameObjects.EnemyHeroes
                    .Where(t =>
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(SpellClass.R.Range) &&
                        MenuClass.Spells["r"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled)
                    .OrderBy(o => o.CountEnemyHeroesInRange(300f))
                    .FirstOrDefault();
                if (bestTarget != null)
                {
                    if (!this.IsUltimateShooting() &&
                        MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
                    {
                        if (SpellClass.E.Ready)
                        {
                            SpellClass.E.Cast(bestTarget.ServerPosition);
                        }

                        SpellClass.R.Cast(bestTarget.ServerPosition);
                    }
                    else if (this.IsUltimateShooting() &&
                         !MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
                    {
                        UtilityClass.Player.IssueOrder(OrderType.MoveTo, Game.CursorPos);
                    }
                }
            }
        }

        #endregion
    }
}