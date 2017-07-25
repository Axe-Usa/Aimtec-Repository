
#pragma warning disable 1587

namespace AIO.Champions
{
    using System;
    using System.Linq;

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
        ///     Fired when the game is updated.
        /// </summary>
        public void Automatic()
        {
            SpellClass.R.Range = 1500f + 500f * UtilityClass.Player.SpellBook.GetSpell(SpellSlot.R).Level;

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
                Console.WriteLine("ready!");
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        t.IsImmobile() &&
                        t.Distance(UtilityClass.Player) < SpellClass.W.Range))
                {
                    Console.WriteLine("ready!?!?!?");
                    UtilityClass.Player.SpellBook.CastSpell(SpellSlot.W, target);
                    UtilityClass.Player.SpellBook.CastSpell(SpellSlot.W, target.Position);
                    UtilityClass.Player.SpellBook.CastSpell(SpellSlot.W, target.ServerPosition);
                }
            }

            /// <summary>
            ///     The Automatic W on Teleport Logic. 
            /// </summary>
            if (SpellClass.W.Ready &&
                MenuClass.Spells["w"]["teleport"].As<MenuBool>().Enabled)
            {
                foreach (var minion in ObjectManager.Get<Obj_AI_Minion>().Where(
                    m =>
                        m.IsEnemy &&
                        m.Distance(UtilityClass.Player) <= SpellClass.W.Range &&
                        m.Buffs.Any(b => b.IsValid && b.IsActive && b.Name.Equals("teleport_target"))))
                {
                    SpellClass.W.Cast(minion.ServerPosition);
                }
            }

            /// <summary>
            ///     The Automatic Q Logic.
            /// </summary>
            if (SpellClass.Q.Ready &&
                ImplementationClass.IOrbwalker.Mode != OrbwalkingMode.None &&
                UtilityClass.Player.CountEnemyHeroesInRange(SpellClass.Q.Range) <= 3 &&
                MenuClass.Spells["q"]["logical"].As<MenuBool>().Enabled)
            {
                foreach (var target in GameObjects.EnemyHeroes.Where(
                    t =>
                        !Invulnerable.Check(t) &&
                        t.IsValidTarget(SpellClass.Q.Range) &&
                        t.HasBuff("caitlynyordletrapdebuff")))
                {
                    SpellClass.Q.Cast(target.ServerPosition);
                }
            }

            /// <summary>
            ///     The Semi-Automatic R Management.
            /// </summary>
            if (SpellClass.R.Ready &&
                MenuClass.Spells["r"]["bool"].As<MenuBool>().Enabled &&
                MenuClass.Spells["r"]["key"].As<MenuKeyBind>().Enabled)
            {
                var bestTarget = GameObjects.EnemyHeroes
                    .Where(
                        t =>
                            !Invulnerable.Check(t) &&
                            t.IsValidTarget(SpellClass.R.Range) &&
                            MenuClass.Spells["r"]["whitelist"][t.ChampionName.ToLower()].As<MenuBool>().Enabled)
                    .OrderBy(o => o.Health)
                    .FirstOrDefault();
                if (bestTarget != null)
                {
                    SpellClass.R.CastOnUnit(bestTarget);
                }
            }
        }

        #endregion
    }
}