
using Aimtec.SDK.Orbwalking;
using Aimtec.SDK.Util.Cache;

namespace AimDev
{
    using System;
    using System.Drawing;
    using System.Linq;

    using Aimtec;
    using Aimtec.SDK.Extensions;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;

    internal static class Program
    {
        #region Static Fields

        public static Menu Menu;

        #endregion

        #region Methods

        /// <summary>
        ///     Loads the menus.
        /// </summary>
        private static void InitMenu()
        {
            Menu = new Menu("aimdev", "AimDev", true);
            {
                Menu.Add(new MenuSlider("range", "Max object dist from cursor", 400, 50, 2000));
                Menu.Add(new MenuBool("antiafk", "Anti-AFK Mode"));
            }
            Menu.Attach();
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
            Game.OnNotifyAway += OnNotifyAway;
        }

        /// <summary>
        ///     Called on afk warning.
        /// </summary>
        /// <param name="args">The <see cref="GameNotifyAwayEventArgs" /> instance containing the event data.</param>
        private static void OnNotifyAway(GameNotifyAwayEventArgs args)
        {
            if (Menu["antiafk"].Enabled)
            {
                args.IgnoreDialog = true;
                Orbwalker.Implementation.Move(ObjectManager.GetLocalPlayer().ServerPosition);
            }
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
            if (sender.IsMe)
            {
                //var player = ObjectManager.GetLocalPlayer();
                //var basicData = player.BasicAttack;
                var spellData = args.SpellData;

                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("Name: "                      + spellData.Name);
                Console.WriteLine("Width: "                     + spellData.LineWidth);
                Console.WriteLine("TargettingType: "            + spellData.TargettingType);
                Console.WriteLine("CastRange: "                 + spellData.CastRange);
                Console.WriteLine("CastRadius: "                + spellData.CastRadius);
                Console.WriteLine("CastRangeDisplayOverride: "  + spellData.CastRangeDisplayOverride);
                Console.WriteLine("CastRadiusSecondary: "       + spellData.CastRadiusSecondary);
                Console.WriteLine("SpellTotalTime: "            + spellData.SpellTotalTime);
                Console.WriteLine("CanMoveWhileChanneling: "    + spellData.CanMoveWhileChanneling);
                Console.WriteLine("SpellCastTime: "             + spellData.SpellCastTime);
                Console.WriteLine("MissileSpeed: "              + spellData.MissileSpeed);
                Console.WriteLine("CastType: "                  + spellData.CastType);
                Console.WriteLine("CastFrame: "                 + spellData.CastFrame);
                Console.WriteLine("----------------------------------------------");
            }
        }

        /// <summary>
        ///     Fired when the game is updated.
        /// </summary>
        private static void OnUpdate()
        {
            foreach (var obj in ObjectManager.Get<GameObject>().Where(o => o.Distance(Game.CursorPos) < Menu["range"].Value))
            {
                Render.WorldToScreen(obj.Position, out var screenPosition);

                switch (obj.Type)
                {
                    case GameObjectType.obj_AI_Base:
                        var baseUnit = (Obj_AI_Base)obj;

                        Render.Text("Name: "         + baseUnit.Name,             new Vector2(screenPosition.X,       screenPosition.Y + 20), RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("UnitSkinName: " + baseUnit.UnitSkinName,     new Vector2(screenPosition.X,       screenPosition.Y + 35), RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("Type: "         + baseUnit.Type,             new Vector2(screenPosition.X,       screenPosition.Y + 50), RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("NetworkID: "    + baseUnit.NetworkId,        new Vector2(screenPosition.X,       screenPosition.Y + 65), RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("X: "            + baseUnit.ServerPosition.X, new Vector2(screenPosition.X,       screenPosition.Y + 95), RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("Y: "            + baseUnit.ServerPosition.Z, new Vector2(screenPosition.X + 100, screenPosition.Y + 95), RenderTextFlags.None, Color.OrangeRed);

                        Render.Text("Buffs:", new Vector2(screenPosition.X, screenPosition.Y + 115), RenderTextFlags.None, Color.Yellow);
                        Render.Text("------", new Vector2(screenPosition.X, screenPosition.Y + 130), RenderTextFlags.None, Color.Yellow);

                        var buffs = baseUnit.ValidActiveBuffs().ToArray();
                        for (var i = 0; i < buffs.Length; i++)
                        {
                            Render.Text(baseUnit.GetRealBuffCount(buffs[i].Name) + "x " + buffs[i].Name, new Vector2(screenPosition.X, screenPosition.Y + 115 + 12 * i), RenderTextFlags.None, Color.OrangeRed);
                        }
                        break;

                    case GameObjectType.obj_AI_Hero:
                        var heroUnit = (Obj_AI_Hero)obj;
                         
                        Render.Text("Name: "         + heroUnit.Name,                                                                      new Vector2(screenPosition.X,       screenPosition.Y + 20),  RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("ChampionName: " + heroUnit.ChampionName,                                                              new Vector2(screenPosition.X,       screenPosition.Y + 35),  RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("UnitSkinName: " + heroUnit.UnitSkinName,                                                              new Vector2(screenPosition.X,       screenPosition.Y + 50),  RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("Type: "         + heroUnit.Type,                                                                      new Vector2(screenPosition.X,       screenPosition.Y + 65),  RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("NetworkID: "    + heroUnit.NetworkId,                                                                 new Vector2(screenPosition.X,       screenPosition.Y + 80),  RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("Health: "       + heroUnit.Health + "/" + heroUnit.MaxHealth + "(" + heroUnit.HealthPercent() + "%)", new Vector2(screenPosition.X,       screenPosition.Y + 95),  RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("X: "            + heroUnit.ServerPosition.X,                                                          new Vector2(screenPosition.X,       screenPosition.Y + 110), RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("Y: "            + heroUnit.ServerPosition.Z,                                                          new Vector2(screenPosition.X + 100, screenPosition.Y + 110), RenderTextFlags.None, Color.OrangeRed);

                        Render.Text("Spells:", new Vector2(screenPosition.X, screenPosition.Y + 130), RenderTextFlags.None, Color.Yellow);
                        Render.Text("-------", new Vector2(screenPosition.X, screenPosition.Y + 140), RenderTextFlags.None, Color.Yellow);

                        const int spellLength = 150;
                        var spellSlots = new[] { SpellSlot.Q, SpellSlot.W, SpellSlot.E, SpellSlot.R };
                        for (var i = 0; i < spellSlots.Length; i++)
                        {
                            Render.Text($"({spellSlots[i]}): {heroUnit.SpellBook.GetSpell(spellSlots[i]).Name}",  new Vector2(screenPosition.X,       screenPosition.Y + spellLength + 15 * i), RenderTextFlags.None, Color.OrangeRed);
                            Render.Text($"ToggleState: {heroUnit.SpellBook.GetSpell(spellSlots[i]).ToggleState}", new Vector2(screenPosition.X + 200, screenPosition.Y + spellLength + 15 * i), RenderTextFlags.None, Color.OrangeRed);
                        }

                        Render.Text("SummonerSpells:", new Vector2(screenPosition.X, screenPosition.Y + 220), RenderTextFlags.None, Color.Yellow);
                        Render.Text("---------------", new Vector2(screenPosition.X, screenPosition.Y + 230), RenderTextFlags.None, Color.Yellow);

                        const int summonerSpellLength = 240;
                        var summonerSpellSlots = new[] { SpellSlot.Summoner1, SpellSlot.Summoner2 };
                        for (var i = 0; i < summonerSpellSlots.Length; i++)
                        {
                            Render.Text($"({summonerSpellSlots[i]}): {heroUnit.SpellBook.GetSpell(summonerSpellSlots[i]).Name}", new Vector2(screenPosition.X,       screenPosition.Y + summonerSpellLength + 15 * i), RenderTextFlags.None, Color.OrangeRed);
                            Render.Text($"ToggleState: {heroUnit.SpellBook.GetSpell(summonerSpellSlots[i]).ToggleState}",        new Vector2(screenPosition.X + 200, screenPosition.Y + summonerSpellLength + 15 * i), RenderTextFlags.None, Color.OrangeRed);
                        }

                        Render.Text("Buffs:", new Vector2(screenPosition.X, screenPosition.Y + 280), RenderTextFlags.None, Color.Yellow);
                        Render.Text("------", new Vector2(screenPosition.X, screenPosition.Y + 290), RenderTextFlags.None, Color.Yellow);

                        var heroBuffs = heroUnit.ValidActiveBuffs().ToArray();
                        for (var i = 0; i < heroBuffs.Length; i++)
                        {
                            Render.Text($"{heroUnit.GetRealBuffCount(heroBuffs[i].Name)}x {heroBuffs[i].Name}", new Vector2(screenPosition.X, screenPosition.Y + 300 + 12 * i), RenderTextFlags.None, Color.OrangeRed);
                        }
                        break;

                    case GameObjectType.MissileClient:
                        var missileUnit = (MissileClient)obj;
                        Render.Text("Missile Speed: " + missileUnit.SpellData.MissileSpeed, new Vector2(screenPosition.X, screenPosition.Y + 50), RenderTextFlags.None, Color.OrangeRed);
                        break;

                    default:
                        Render.Text("Name: "         + obj.Name,             new Vector2(screenPosition.X,       screenPosition.Y + 20), RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("Type: "         + obj.Type,             new Vector2(screenPosition.X,       screenPosition.Y + 35), RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("NetworkID: "    + obj.NetworkId,        new Vector2(screenPosition.X,       screenPosition.Y + 50), RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("X: "            + obj.ServerPosition.X, new Vector2(screenPosition.X,       screenPosition.Y + 65), RenderTextFlags.None, Color.OrangeRed);
                        Render.Text("Y: "            + obj.ServerPosition.Z, new Vector2(screenPosition.X + 100, screenPosition.Y + 65), RenderTextFlags.None, Color.OrangeRed);

                        if (obj.Type == GameObjectType.obj_GeneralParticleEmitter)
                        {
                            Console.WriteLine("Name: " + obj.Name);
                        }
                        break;
                }
            }
        }

        #endregion
    }
}