namespace AimDev
{
    using System;
    using System.Drawing;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Util;

    internal static class Program
    {
        #region Static Fields

        private static Menu config;

        private static int lastMovementTick;

        #endregion

        #region Methods

        /// <summary>
        ///     Loads the menus.
        /// </summary>
        private static void InitMenu()
        {
            config = new Menu("aimdev", "AimDev", true);
            {
                config.Add(new MenuSlider("range", "Max object dist from cursor", 400, 100, 2000));
                config.Add(new MenuBool("antiafk", "Anti-AFK"));
                config.Add(new MenuKeyBind("masteries", "Show Masteries", KeyCode.L, KeybindType.Press));
            }
            config.Attach();
        }

        /// <summary>
        ///     The entry point of the application.
        /// </summary>
        private static void Main()
        {
            InitMenu();
            Game.OnUpdate += OnUpdate;
            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            Obj_AI_Base.OnProcessAutoAttack += OnProcessAutoAttack;
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        private static void OnProcessAutoAttack(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (sender is Obj_AI_Hero)
            {
                Console.WriteLine("Autoattack Name: " + args.SpellData.Name);
            }
        }

        /// <summary>
        ///     Called on do-cast.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="Obj_AI_BaseMissileClientDataEventArgs" /> instance containing the event data.</param>
        private static void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (sender is Obj_AI_Hero)
            {
                Console.WriteLine("Name: " + args.SpellData.Name);
            }
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        private static void OnUpdate()
        {
            if (config["antiafk"].Enabled && Game.TickCount - lastMovementTick > 140000)
            {
                ObjectManager.GetLocalPlayer().IssueOrder(OrderType.MoveTo, ObjectManager.GetLocalPlayer().Position - 5);
                lastMovementTick = Game.TickCount;
            }

            if (config["masteries"].Enabled)
            {
                var masteries = ObjectManager.GetLocalPlayer().Masteries;
                for (var i = 0; i < masteries.Length; i++)
                {
                    var mastery = masteries[i];
                    if (mastery != null)
                    {
                        var text = "You have " + mastery.Points + " points in mastery #" + mastery.Id + " from page " + mastery.Page;
                        Render.Text(95 - text.Length, 150 - masteries.Length + i * 10, Color.OrangeRed, text);
                    }
                }
            }
            else
            {
                foreach (var obj in ObjectManager.Get<GameObject>().Where(o => o.Distance(Game.CursorPos) < config["range"].Value))
                {
                    Vector2 screenPosition;
                    Render.WorldToScreen(obj.Position, out screenPosition);

                    Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 20), Color.OrangeRed, obj.Type.ToString());
                    Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 30), Color.OrangeRed, "NetworkID: " + obj.NetworkId);
                    Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 40), Color.OrangeRed, screenPosition.ToString());

                    switch (obj.Type)
                    {
                        case GameObjectType.obj_AI_Hero:
                            Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 50), Color.OrangeRed, ((Obj_AI_Hero)obj).ChampionName);
                            break;

                        case GameObjectType.obj_AI_Minion:
                            var objAiMinion = obj as Obj_AI_Minion;
                            if (objAiMinion != null)
                            {
                                Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 50), Color.OrangeRed, "Name: " + objAiMinion.Name);
                                Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 60), Color.OrangeRed, "UnitSkinName: " + objAiMinion.UnitSkinName);

                                var buffs = objAiMinion.Buffs.Where(b => b.IsActive && b.IsValid && b.Name != "No Script").ToArray();
                                Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 80), Color.Yellow, "Buffs:");
                                Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 90), Color.Yellow, "------");

                                for (var i = 0; i < buffs.Length; i++)
                                {
                                    Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 100 + 10 * i), Color.OrangeRed, objAiMinion.GetBuffCount(buffs[i].Name) + "x " + buffs[i].Name);
                                }
                            }
                            break;

                        case GameObjectType.obj_AI_Turret:
                            var objAiTurret = obj as Obj_AI_Turret;
                            if (objAiTurret != null)
                            {
                                Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 50), Color.OrangeRed, objAiTurret.Name);
                            }
                            break;

                        case GameObjectType.obj_AI_Base:
                            var objAiBase = obj as Obj_AI_Base;
                            if (objAiBase != null)
                            {
                                Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 50), Color.OrangeRed, "Health: " + objAiBase.Health + "/" + objAiBase.MaxHealth + "(" + objAiBase.HealthPercent() + "%)");
                            }
                            break;

                        default:
                            Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 50), Color.OrangeRed, obj.Name);
                            break;
                    }

                    var aiHero = obj as Obj_AI_Hero;
                    if (aiHero != null)
                    {
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 80), Color.Yellow, "Spells:");
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 90), Color.Yellow, "-------");
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 100), Color.OrangeRed, "(Q): " + aiHero.SpellBook.GetSpell(SpellSlot.Q).Name);
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 110), Color.OrangeRed, "(W): " + aiHero.SpellBook.GetSpell(SpellSlot.W).Name);
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 120), Color.OrangeRed, "(E): " + aiHero.SpellBook.GetSpell(SpellSlot.E).Name);
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 130), Color.OrangeRed, "(R): " + aiHero.SpellBook.GetSpell(SpellSlot.R).Name);

                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 150), Color.Yellow, "SummonerSpells:");
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 160), Color.Yellow, "-------");
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 170), Color.OrangeRed, "(D): " + aiHero.SpellBook.GetSpell(SpellSlot.Summoner1).Name);
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 180), Color.OrangeRed, "(F): " + aiHero.SpellBook.GetSpell(SpellSlot.Summoner2).Name);

                        var buffs = aiHero.Buffs.Where(b => b.IsActive && b.IsValid && b.Name != "No Script").ToArray();
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 200), Color.Yellow, "Buffs:");
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 210), Color.Yellow, "------");

                        for (var i = 0; i < buffs.Length; i++)
                        {
                            Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 221 + 10 * i), Color.OrangeRed, aiHero.GetBuffCount(buffs[i].Name) + "x " + buffs[i].Name);
                        }
                    }

                    if (obj is MissileClient)
                    {
                        var missile = obj as MissileClient;
                        Render.Text(new Vector2(screenPosition.X, screenPosition.Y + 50), Color.OrangeRed, "Missile Speed: " + missile.SpellData.MissileSpeed);
                    }
                }
            }
        }

        #endregion
    }
}