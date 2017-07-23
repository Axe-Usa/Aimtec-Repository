
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
    internal partial class Jhin
    {
        #region Public Methods and Operators

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        public void Automatic()
        {
            ImplementationClass.IOrbwalker.MovingEnabled = !this.IsUltimateShooting();
            ImplementationClass.IOrbwalker.AttackingEnabled = !this.IsUltimateShooting();

            if (UtilityClass.Player.IsRecalling())
            {
                return;
            }

            /// <summary>
            ///     The Automatic W Logic. 
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["logical"].As<MenuBool>().Enabled)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsImmobile() &&
                        t.HasBuff("jhinespotteddebuff") &&
                        t.IsValidTarget(SpellClass.W.Range)))
                {
                    SpellClass.W.Cast(target.ServerPosition);
                }
            }

            /// <summary>
            ///     The Automatic E Logic. 
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["logical"].As<MenuBool>().Enabled)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsImmobile() &&
                        t.Distance(UtilityClass.Player) < SpellClass.E.Range))
                {
                    SpellClass.E.Cast(target.ServerPosition);
                }
            }

            /// <summary>
            ///     The Automatic E on Teleport Logic. 
            /// </summary>
            if (SpellClass.E.Ready &&
                MenuClass.Spells["e"]["teleport"].As<MenuBool>().Enabled)
            {
                foreach (var minion in ObjectManager.Get<Obj_AI_Minion>().Where(
                    m =>
                        m.IsEnemy &&
                        m.Distance(UtilityClass.Player) <= SpellClass.E.Range &&
                        m.Buffs.Any(b => b.IsValid && b.IsActive && b.Name.Equals("teleport_target"))))
                {
                    SpellClass.E.Cast(minion.ServerPosition);
                }
            }
        }

        #endregion
    }
}