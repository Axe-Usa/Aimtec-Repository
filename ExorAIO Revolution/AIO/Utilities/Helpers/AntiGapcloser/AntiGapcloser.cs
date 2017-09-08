using System;
using System.Collections.Generic;
using System.Linq;
using Aimtec;
using Aimtec.SDK.Extensions;
using Aimtec.SDK.Menu;
using Aimtec.SDK.Menu.Components;

namespace AIO.Utilities
{
    #region

    #endregion

    public delegate void OnGapcloserEvent(Obj_AI_Hero target, GapcloserArgs args);

    public enum AntiGapcloserType
    {
        Melee = 0,
        Dash = 1,
        SkillShot = 2,
        Targeted = 3
    }

    internal struct SpellData
    {
        public string ChampionName { get; set; }
        public string SpellName { get; set; }
        public SpellSlot Slot { get; set; }
        public AntiGapcloserType SpellType { get; set; }
    }

    public class GapcloserArgs
    {
        internal Obj_AI_Hero Unit { get; set; }
        public SpellSlot Slot { get; set; }
        public string SpellName { get; set; }
        public AntiGapcloserType Type { get; set; }
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
        public int StartTick { get; set; }
        public int EndTick { get; set; }
        public int DurationTick { get; set; }
        public bool HaveShield { get; set; }
    }

    public static class Gapcloser
    {
        public static event OnGapcloserEvent OnGapcloser;

        public static Dictionary<int, GapcloserArgs> Gapclosers = new Dictionary<int, GapcloserArgs>();
        internal static List<SpellData> Spells = new List<SpellData>();

        public static Menu Menu;

        static Gapcloser()
        {
            Initialize();
        }

        private static void Initialize()
        {
            #region Aatrox

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Aatrox",
                    Slot = SpellSlot.Q,
                    SpellName = "aatroxq",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Ahri

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Ahri",
                    Slot = SpellSlot.R,
                    SpellName = "ahritumble",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Akali

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Akali",
                    Slot = SpellSlot.R,
                    SpellName = "akalishadowdance",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Alistar

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Alistar",
                    Slot = SpellSlot.W,
                    SpellName = "headbutt",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Azir

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Azir",
                    Slot = SpellSlot.E,
                    SpellName = "azire",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Caitlyn

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Caitlyn",
                    Slot = SpellSlot.E,
                    SpellName = "caitlynentrapment",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Camille

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Camille",
                    Slot = SpellSlot.E,
                    SpellName = "camillee",
                    SpellType = AntiGapcloserType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Camille",
                    Slot = SpellSlot.E,
                    SpellName = "camilleedash2",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Corki

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Corki",
                    Slot = SpellSlot.W,
                    SpellName = "carpetbomb",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Diana

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Diana",
                    Slot = SpellSlot.R,
                    SpellName = "dianateleport",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Ekko

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Ekko",
                    Slot = SpellSlot.E,
                    SpellName = "ekkoeattack",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Elise

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Elise",
                    Slot = SpellSlot.Q,
                    SpellName = "elisespiderqcast",
                    SpellType = AntiGapcloserType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Elise",
                    Slot = SpellSlot.E,
                    SpellName = "elisespideredescent",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Ezreal

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Ezreal",
                    Slot = SpellSlot.E,
                    SpellName = "ezrealarcaneshift",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Fiora

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Fiora",
                    Slot = SpellSlot.Q,
                    SpellName = "fioraq",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Fizz

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Fizz",
                    Slot = SpellSlot.Q,
                    SpellName = "fizzpiercingstrike",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Galio

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Galio",
                    Slot = SpellSlot.E,
                    SpellName = "galioe",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Gnar

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Gnar",
                    Slot = SpellSlot.E,
                    SpellName = "gnarbige",
                    SpellType = AntiGapcloserType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Gnar",
                    Slot = SpellSlot.E,
                    SpellName = "gnare",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Gragas

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Gragas",
                    Slot = SpellSlot.E,
                    SpellName = "gragase",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Graves

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Graves",
                    Slot = SpellSlot.E,
                    SpellName = "gravesmove",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Hecarim

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Hecarim",
                    Slot = SpellSlot.R,
                    SpellName = "hecarimult",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Illaoi

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Illaoi",
                    Slot = SpellSlot.W,
                    SpellName = "illaoiwattack",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Irelia

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Irelia",
                    Slot = SpellSlot.Q,
                    SpellName = "ireliagatotsu",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region JarvanIV

            Spells.Add(
                new SpellData
                {
                    ChampionName = "JarvanIV",
                    Slot = SpellSlot.Q,
                    SpellName = "jarvanivdragonstrike",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Jax

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Jax",
                    Slot = SpellSlot.Q,
                    SpellName = "jaxleapstrike",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Jayce

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Jayce",
                    Slot = SpellSlot.Q,
                    SpellName = "jaycetotheskies",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Kassadin

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Kassadin",
                    Slot = SpellSlot.R,
                    SpellName = "riftwalk",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Katarina

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Katarina",
                    Slot = SpellSlot.E,
                    SpellName = "katarinae",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Kayn

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Kayn",
                    Slot = SpellSlot.Q,
                    SpellName = "kaynq",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Khazix

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Khazix",
                    Slot = SpellSlot.E,
                    SpellName = "khazixe",
                    SpellType = AntiGapcloserType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Khazix",
                    Slot = SpellSlot.E,
                    SpellName = "khazixelong",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Kindred

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Kindred",
                    Slot = SpellSlot.Q,
                    SpellName = "kindredq",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Leblanc

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Leblanc",
                    Slot = SpellSlot.W,
                    SpellName = "leblancslide",
                    SpellType = AntiGapcloserType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Leblanc",
                    Slot = SpellSlot.W,
                    SpellName = "leblancslidem",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region LeeSin

            Spells.Add(
                new SpellData
                {
                    ChampionName = "LeeSin",
                    Slot = SpellSlot.Q,
                    SpellName = "blindmonkqtwo",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Leona

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Leona",
                    Slot = SpellSlot.E,
                    SpellName = "leonazenithblade",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Lucian

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Lucian",
                    Slot = SpellSlot.E,
                    SpellName = "luciane",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Malphite

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Malphite",
                    Slot = SpellSlot.R,
                    SpellName = "ufslash",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region MasterYi

            Spells.Add(
                new SpellData
                {
                    ChampionName = "MasterYi",
                    Slot = SpellSlot.Q,
                    SpellName = "alphastrike",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region MonkeyKing

            Spells.Add(
                new SpellData
                {
                    ChampionName = "MonkeyKing",
                    Slot = SpellSlot.E,
                    SpellName = "monkeykingnimbus",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Nautilus

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Nautilus",
                    Slot = SpellSlot.Q,
                    SpellName = "nautilusq",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Nidalee

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Nidalee",
                    Slot = SpellSlot.W,
                    SpellName = "pounce",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Pantheon

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Pantheon",
                    Slot = SpellSlot.W,
                    SpellName = "pantheon_leapbash",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Poppy

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Poppy",
                    Slot = SpellSlot.E,
                    SpellName = "poppyheroiccharge",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Quinn

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Quinn",
                    Slot = SpellSlot.E,
                    SpellName = "quinne",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Rakan

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Rakan",
                    Slot = SpellSlot.W,
                    SpellName = "rakanw",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region RekSai

            Spells.Add(
                new SpellData
                {
                    ChampionName = "RekSai",
                    Slot = SpellSlot.E,
                    SpellName = "reksaieburrowed",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Renekton

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Renekton",
                    Slot = SpellSlot.E,
                    SpellName = "renektonsliceanddice",
                    SpellType = AntiGapcloserType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Renekton",
                    Slot = SpellSlot.E,
                    SpellName = "renektonpreexecute",
                    SpellType = AntiGapcloserType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Renekton",
                    Slot = SpellSlot.E,
                    SpellName = "renektonsuperexecute",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Rengar

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Rengar",
                    Slot = SpellSlot.Unknown,
                    SpellName = "rengarpassivebuffdash",
                    SpellType = AntiGapcloserType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Rengar",
                    Slot = SpellSlot.Unknown,
                    SpellName = "rengarpassivebuffdashaadummy",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Riven

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Riven",
                    Slot = SpellSlot.Q,
                    SpellName = "riventricleave",
                    SpellType = AntiGapcloserType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Riven",
                    Slot = SpellSlot.E,
                    SpellName = "rivenfeint",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Sejuani

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Sejuani",
                    Slot = SpellSlot.Q,
                    SpellName = "sejuaniarcticassault",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Shen

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Shen",
                    Slot = SpellSlot.E,
                    SpellName = "shene",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Shyvana

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Shyvana",
                    Slot = SpellSlot.R,
                    SpellName = "shyvanatransformcast",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Talon

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Talon",
                    Slot = SpellSlot.Q,
                    SpellName = "talonq",
                    SpellType = AntiGapcloserType.Targeted
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Talon",
                    Slot = SpellSlot.E,
                    SpellName = "taloncutthroat",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Tristana

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Tristana",
                    Slot = SpellSlot.W,
                    SpellName = "rocketjump",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Tryndamere

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Tryndamere",
                    Slot = SpellSlot.E,
                    SpellName = "slashcast",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Vi

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Vi",
                    Slot = SpellSlot.Q,
                    SpellName = "viq",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Vayne

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Vayne",
                    Slot = SpellSlot.Q,
                    SpellName = "vaynetumble",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Warwick

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Warwick",
                    Slot = SpellSlot.R,
                    SpellName = "warwickr",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region XinZhao

            Spells.Add(
                new SpellData
                {
                    ChampionName = "XinZhao",
                    Slot = SpellSlot.E,
                    SpellName = "xenzhaosweep",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Yasuo

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Yasuo",
                    Slot = SpellSlot.E,
                    SpellName = "yasuodashwrapper",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Zac

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Zac",
                    Slot = SpellSlot.E,
                    SpellName = "zace",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion

            #region Zed

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Zed",
                    Slot = SpellSlot.R,
                    SpellName = "zedr",
                    SpellType = AntiGapcloserType.Targeted
                });

            #endregion

            #region Ziggs

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Ziggs",
                    Slot = SpellSlot.W,
                    SpellName = "ziggswtoggle",
                    SpellType = AntiGapcloserType.SkillShot
                });

            #endregion
        }

        public static void Attach(Menu mainMenu, string menuName)
        {
            var menu = new Menu("Gapcloser", menuName);
            if (ObjectManager.Get<Obj_AI_Hero>().All(x => !x.IsEnemy))
            {
                menu.Add(new MenuSeperator("separator", "No enemies found, no need for an Anti-Gapcloser Menu."));
            }
            else
            {
                menu.Add(new MenuBool("GapcloserEnabled", "Enabled"));
                menu.Add(new MenuSeperator("GapcloserSeperator1"));

                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(x => x.IsEnemy))
                {
                    var name = "Gapcloser" + enemy.ChampionName.ToLower();
                    var heroMenu = new Menu(name, enemy.ChampionName)
                    {
                        new MenuBool(name + "Enabled", "Enabled"),
                        new MenuSlider(name + "Distance", "If Target Distance To Player <= x", 550, 1, 700),
                        new MenuSlider(name + "HPercent", "When Player HealthPercent <= x%", 100, 1)
                    };
                    menu.Add(heroMenu);

                    foreach (var spell in Spells.Where(x => x.ChampionName == enemy.ChampionName))
                    {
                        heroMenu.Add(new MenuBool(name + "." + spell.SpellName.ToLower(), "Anti Slot: " + spell.Slot + "(" + spell.SpellName + ")"));
                    }
                }
            }
            mainMenu.Add(menu);

            Game.OnUpdate += OnUpdate;
            //GameObject.OnCreate += OnCreate;

            Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
            Obj_AI_Base.OnNewPath += OnNewPath;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once UnusedParameter.Local
        private static void OnCreate(GameObject sender)
        {
            //special dash (like rengar, khazix, ziggs)
        }

        private static void OnNewPath(Obj_AI_Base sender, Obj_AI_BaseNewPathEventArgs args)
        {
            if (sender == null || !sender.IsEnemy || sender.Type != GameObjectType.obj_AI_Hero)
            {
                return;
            }

            if (sender.UnitSkinName == "Vi" || sender.UnitSkinName == "Sion" || sender.UnitSkinName == "Kayn" || sender.UnitSkinName == "Fizz")
            {
                // Vi R
                // Sion R
                // Kayn R
                // Fizz E
                return;
            }

            if (!Gapclosers.ContainsKey(sender.NetworkId))
            {
                Gapclosers.Add(sender.NetworkId, new GapcloserArgs());
            }

            if (args.IsDash)
            {
                Gapclosers[sender.NetworkId].Unit = (Obj_AI_Hero)sender;
                Gapclosers[sender.NetworkId].Slot = SpellSlot.Unknown;
                Gapclosers[sender.NetworkId].Type = AntiGapcloserType.Dash;
                Gapclosers[sender.NetworkId].SpellName = sender.UnitSkinName + "_Dash";
                Gapclosers[sender.NetworkId].StartPosition = sender.ServerPosition;
                Gapclosers[sender.NetworkId].EndPosition = args.Path.Last();
                Gapclosers[sender.NetworkId].StartTick = Game.TickCount;
                Gapclosers[sender.NetworkId].EndTick =
                    (int)
                    (Gapclosers[sender.NetworkId].EndPosition.DistanceSqr(Gapclosers[sender.NetworkId].StartPosition) /
                     args.Speed * args.Speed * 1000) + Gapclosers[sender.NetworkId].StartTick;
                Gapclosers[sender.NetworkId].DurationTick = Gapclosers[sender.NetworkId].EndTick - Gapclosers[sender.NetworkId].StartTick;
                Gapclosers[sender.NetworkId].HaveShield = Invulnerable.Check((Obj_AI_Hero)sender, DamageType.Magical, false);
            }
        }

        private static void OnUpdate()
        {
            if (Gapclosers.Values.Any(x => Game.TickCount - x.StartTick > 900 + Game.Ping))
            {
                Gapclosers.Clear();
            }

            if (OnGapcloser == null || Menu["GapcloserEnabled"].As<MenuBool>() == null || !Menu["GapcloserEnabled"].As<MenuBool>().Enabled)
            {
                return;
            }

            foreach (var args in Gapclosers.Where(x =>
                x.Value.Unit.IsValidTarget() &&
                Menu["Gapcloser" + x.Value.Unit.ChampionName.ToLower()].As<Menu>() != null &&
                Menu["Gapcloser" + x.Value.Unit.ChampionName.ToLower()].As<Menu>()["Gapcloser" + x.Value.Unit.ChampionName.ToLower() + "Enabled"].As<MenuBool>().Enabled))
            {
                var championName = args.Value.Unit.ChampionName.ToLower();
                var menu = Menu["Gapcloser" + championName].As<Menu>();
                var distance = menu["Gapcloser" + championName + "Distance"].As<MenuSlider>().Value;
                var healthPercent = menu["Gapcloser" + championName + "HPercent"].As<MenuSlider>().Value;

                if (ObjectManager.GetLocalPlayer().HealthPercent() <= healthPercent)
                {
                    switch (args.Value.Type)
                    {
                        case AntiGapcloserType.SkillShot:
                        case AntiGapcloserType.Dash:
                            if (args.Value.Unit.ServerPosition.DistanceSqr(ObjectManager.GetLocalPlayer().ServerPosition) <= distance * distance)
                            {
                                OnGapcloser(args.Value.Unit, args.Value);
                            }
                            break;
                    }

                    OnGapcloser(args.Value.Unit, args.Value);
                }
            }
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (sender == null ||
                !sender.IsEnemy ||
                sender.Type != GameObjectType.obj_AI_Hero ||
                string.IsNullOrEmpty(args.SpellData.Name))
            {
                return;
            }

            if (Spells.All(x => !string.Equals(x.SpellName, args.SpellData.Name, StringComparison.CurrentCultureIgnoreCase)) ||
                !Menu["Gapcloser" + sender.UnitSkinName.ToLower()].As<Menu>()["Gapcloser" + sender.UnitSkinName.ToLower() + "." + args.SpellData.Name.ToLower()].As<MenuBool>().Enabled)
            {
                return;
            }

            if (!Gapclosers.ContainsKey(sender.NetworkId))
            {
                Gapclosers.Add(sender.NetworkId, new GapcloserArgs());
            }

            Gapclosers[sender.NetworkId].Unit = (Obj_AI_Hero)sender;
            Gapclosers[sender.NetworkId].Slot = args.SpellSlot;
            Gapclosers[sender.NetworkId].Type = args.Target != null ? AntiGapcloserType.Targeted : AntiGapcloserType.SkillShot;
            Gapclosers[sender.NetworkId].SpellName = args.SpellData.Name;
            Gapclosers[sender.NetworkId].StartPosition = args.Start;
            Gapclosers[sender.NetworkId].EndPosition = args.End;
            Gapclosers[sender.NetworkId].StartTick = Game.TickCount;
            Gapclosers[sender.NetworkId].HaveShield = Invulnerable.Check((Obj_AI_Hero)sender, DamageType.Magical, false);
        }
    }
}