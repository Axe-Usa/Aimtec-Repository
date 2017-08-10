
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
    internal partial class Lucian
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

            switch (MenuClass.Spells["pattern"].As<MenuList>().Value)
            {
                case 0:
                case 2:
                    /// <summary>
                    ///     The E Combo Logic.
                    /// </summary>
                    if (SpellClass.E.Ready &&
                        MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
                    {
                        switch (MenuClass.Spells["e"]["mode"].As<MenuList>().Value)
                        {
                            case 0:
                                Vector3 point;
                                if (UtilityClass.Player.IsUnderEnemyTurret() ||
                                    UtilityClass.Player.Distance(Game.CursorPos) < UtilityClass.Player.AttackRange)
                                {
                                    point = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, UtilityClass.Player.BoundingRadius);
                                }
                                else
                                {
                                    point = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, 475f);
                                }

                                SpellClass.E.Cast(point);
                                return;

                            case 1:
                                SpellClass.E.Cast(UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, 425f));
                                return;

                            case 2:
                                SpellClass.E.Cast(UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, UtilityClass.Player.BoundingRadius));
                                return;
                        }
                    }
                    break;

                case 1:
                    /// <summary>
                    ///     The Q Combo Logic.
                    /// </summary>
                    if (SpellClass.Q.Ready &&
                        heroTarget.IsValidTarget(SpellClass.Q.Range) &&
                        MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
                    {
                        SpellClass.Q.CastOnUnit(heroTarget);
                        return;
                    }
                    break;

                case 3:
                    /// <summary>
                    ///     The W Combo Logic.
                    /// </summary>
                    if (SpellClass.W.Ready &&
                        heroTarget.IsValidTarget(SpellClass.W.Range) &&
                        MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
                    {
                        SpellClass.W.Cast(heroTarget);
                        return;
                    }
                    break;
            }

            switch (MenuClass.Spells["pattern"].As<MenuList>().Value)
            {
                case 0:
                    /// <summary>
                    ///     The Q Combo Logic.
                    /// </summary>
                    if (SpellClass.Q.Ready &&
                        heroTarget.IsValidTarget(SpellClass.Q.Range) &&
                        MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
                    {
                        SpellClass.Q.CastOnUnit(heroTarget);
                        return;
                    }
                    break;

                case 1:
                case 3:
                    /// <summary>
                    ///     The E Combo Logic.
                    /// </summary>
                    if (SpellClass.E.Ready &&
                        MenuClass.Spells["e"]["combo"].As<MenuBool>().Enabled)
                    {
                        switch (MenuClass.Spells["e"]["mode"].As<MenuList>().Value)
                        {
                            case 0:
                                Vector3 point;
                                if (UtilityClass.Player.IsUnderEnemyTurret() ||
                                    UtilityClass.Player.Distance(Game.CursorPos) < UtilityClass.Player.AttackRange)
                                {
                                    point = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, UtilityClass.Player.BoundingRadius);
                                }
                                else
                                {
                                    point = UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, 475f);
                                }

                                SpellClass.E.Cast(point);
                                return;

                            case 1:
                                SpellClass.E.Cast(UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, 425f));
                                return;

                            case 2:
                                SpellClass.E.Cast(UtilityClass.Player.ServerPosition.Extend(Game.CursorPos, UtilityClass.Player.BoundingRadius));
                                return;
                        }
                    }
                    break;

                case 2:
                    /// <summary>
                    ///     The W Combo Logic.
                    /// </summary>
                    if (SpellClass.W.Ready &&
                        heroTarget.IsValidTarget(SpellClass.W.Range) &&
                        MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
                    {
                        SpellClass.W.Cast(heroTarget);
                        return;
                    }
                    break;
            }

            switch (MenuClass.Spells["pattern"].As<MenuList>().Value)
            {
                case 0:
                case 1:
                    /// <summary>
                    ///     The W Combo Logic.
                    /// </summary>
                    if (SpellClass.W.Ready &&
                        heroTarget.IsValidTarget(SpellClass.W.Range) &&
                        MenuClass.Spells["w"]["combo"].As<MenuBool>().Enabled)
                    {
                        SpellClass.W.Cast(heroTarget);
                    }
                    break;

                case 2:
                case 3:
                    /// <summary>
                    ///     The Q Combo Logic.
                    /// </summary>
                    if (SpellClass.Q.Ready &&
                        heroTarget.IsValidTarget(SpellClass.Q.Range) &&
                        MenuClass.Spells["q"]["combo"].As<MenuBool>().Enabled)
                    {
                        SpellClass.Q.CastOnUnit(heroTarget);
                    }
                    break;
            }
        }

        #endregion
    }
}