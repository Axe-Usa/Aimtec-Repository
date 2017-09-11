namespace AIO.Utilities
{
    #region

    using Aimtec;
    using Aimtec.SDK.Menu;
    using Aimtec.SDK.Menu.Components;
    using Aimtec.SDK.Extensions;

    using System;
    using System.Collections.Generic;
    using System.Linq;

    #endregion

    public delegate void OnGapcloserEvent(Obj_AI_Hero target, GapcloserArgs args);

    public enum GapSpellType
    {
        Dash = 0,
        SkillShot = 1,
        Targeted = 2
    }

    internal struct SpellData
    {
        public string ChampionName { get; set; }
        public string SpellName { get; set; }
        public SpellSlot Slot { get; set; }
        public GapSpellType SpellType { get; set; }
    }

    public class GapcloserArgs
    {
        internal Obj_AI_Hero Unit { get; set; }
        public SpellSlot Slot { get; set; }
        public AttackableUnit Target { get; set; }
        public string SpellName { get; set; }
        public GapSpellType Type { get; set; }
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
        public int StartTick { get; set; }
        public int EndTick { get; set; }
        public int DurationTick { get; set; }
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
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Ahri

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Ahri",
                    Slot = SpellSlot.R,
                    SpellName = "ahritumble",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Akali

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Akali",
                    Slot = SpellSlot.R,
                    SpellName = "akalishadowdance",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Alistar

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Alistar",
                    Slot = SpellSlot.W,
                    SpellName = "headbutt",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Azir

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Azir",
                    Slot = SpellSlot.E,
                    SpellName = "azire",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Caitlyn

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Caitlyn",
                    Slot = SpellSlot.E,
                    SpellName = "caitlynentrapment",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Camille

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Camille",
                    Slot = SpellSlot.E,
                    SpellName = "camillee",
                    SpellType = GapSpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Camille",
                    Slot = SpellSlot.E,
                    SpellName = "camilleedash2",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Corki

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Corki",
                    Slot = SpellSlot.W,
                    SpellName = "carpetbomb",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Diana

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Diana",
                    Slot = SpellSlot.R,
                    SpellName = "dianateleport",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Ekko

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Ekko",
                    Slot = SpellSlot.E,
                    SpellName = "ekkoeattack",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Elise

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Elise",
                    Slot = SpellSlot.Q,
                    SpellName = "elisespiderqcast",
                    SpellType = GapSpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Elise",
                    Slot = SpellSlot.E,
                    SpellName = "elisespideredescent",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Ezreal

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Ezreal",
                    Slot = SpellSlot.E,
                    SpellName = "ezrealarcaneshift",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Fiora

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Fiora",
                    Slot = SpellSlot.Q,
                    SpellName = "fioraq",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Fizz

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Fizz",
                    Slot = SpellSlot.Q,
                    SpellName = "fizzpiercingstrike",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Galio

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Galio",
                    Slot = SpellSlot.E,
                    SpellName = "galioe",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Gnar

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Gnar",
                    Slot = SpellSlot.E,
                    SpellName = "gnarbige",
                    SpellType = GapSpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Gnar",
                    Slot = SpellSlot.E,
                    SpellName = "gnare",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Gragas

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Gragas",
                    Slot = SpellSlot.E,
                    SpellName = "gragase",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Graves

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Graves",
                    Slot = SpellSlot.E,
                    SpellName = "gravesmove",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Hecarim

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Hecarim",
                    Slot = SpellSlot.R,
                    SpellName = "hecarimult",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Illaoi

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Illaoi",
                    Slot = SpellSlot.W,
                    SpellName = "illaoiwattack",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Irelia

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Irelia",
                    Slot = SpellSlot.Q,
                    SpellName = "ireliagatotsu",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region JarvanIV

            Spells.Add(
                new SpellData
                {
                    ChampionName = "JarvanIV",
                    Slot = SpellSlot.Q,
                    SpellName = "jarvanivdragonstrike",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Jax

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Jax",
                    Slot = SpellSlot.Q,
                    SpellName = "jaxleapstrike",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Jayce

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Jayce",
                    Slot = SpellSlot.Q,
                    SpellName = "jaycetotheskies",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Kassadin

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Kassadin",
                    Slot = SpellSlot.R,
                    SpellName = "riftwalk",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Katarina

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Katarina",
                    Slot = SpellSlot.E,
                    SpellName = "katarinae",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Kayn

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Kayn",
                    Slot = SpellSlot.Q,
                    SpellName = "kaynq",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Khazix

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Khazix",
                    Slot = SpellSlot.E,
                    SpellName = "khazixe",
                    SpellType = GapSpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Khazix",
                    Slot = SpellSlot.E,
                    SpellName = "khazixelong",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Kindred

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Kindred",
                    Slot = SpellSlot.Q,
                    SpellName = "kindredq",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Leblanc

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Leblanc",
                    Slot = SpellSlot.W,
                    SpellName = "leblancslide",
                    SpellType = GapSpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Leblanc",
                    Slot = SpellSlot.W,
                    SpellName = "leblancslidem",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region LeeSin

            Spells.Add(
                new SpellData
                {
                    ChampionName = "LeeSin",
                    Slot = SpellSlot.Q,
                    SpellName = "blindmonkqtwo",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Leona

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Leona",
                    Slot = SpellSlot.E,
                    SpellName = "leonazenithblade",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Lucian

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Lucian",
                    Slot = SpellSlot.E,
                    SpellName = "luciane",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Malphite

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Malphite",
                    Slot = SpellSlot.R,
                    SpellName = "ufslash",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region MasterYi

            Spells.Add(
                new SpellData
                {
                    ChampionName = "MasterYi",
                    Slot = SpellSlot.Q,
                    SpellName = "alphastrike",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region MonkeyKing

            Spells.Add(
                new SpellData
                {
                    ChampionName = "MonkeyKing",
                    Slot = SpellSlot.E,
                    SpellName = "monkeykingnimbus",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Nautilus

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Nautilus",
                    Slot = SpellSlot.Q,
                    SpellName = "nautilusq",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Nidalee

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Nidalee",
                    Slot = SpellSlot.W,
                    SpellName = "pounce",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Pantheon

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Pantheon",
                    Slot = SpellSlot.W,
                    SpellName = "pantheon_leapbash",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Poppy

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Poppy",
                    Slot = SpellSlot.E,
                    SpellName = "poppyheroiccharge",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Quinn

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Quinn",
                    Slot = SpellSlot.E,
                    SpellName = "quinne",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Rakan

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Rakan",
                    Slot = SpellSlot.W,
                    SpellName = "rakanw",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region RekSai

            Spells.Add(
                new SpellData
                {
                    ChampionName = "RekSai",
                    Slot = SpellSlot.E,
                    SpellName = "reksaieburrowed",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Renekton

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Renekton",
                    Slot = SpellSlot.E,
                    SpellName = "renektonsliceanddice",
                    SpellType = GapSpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Renekton",
                    Slot = SpellSlot.E,
                    SpellName = "renektonpreexecute",
                    SpellType = GapSpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Renekton",
                    Slot = SpellSlot.E,
                    SpellName = "renektonsuperexecute",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Rengar

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Rengar",
                    Slot = SpellSlot.Unknown,
                    SpellName = "rengarpassivebuffdash",
                    SpellType = GapSpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Rengar",
                    Slot = SpellSlot.Unknown,
                    SpellName = "rengarpassivebuffdashaadummy",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Riven

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Riven",
                    Slot = SpellSlot.Q,
                    SpellName = "riventricleave",
                    SpellType = GapSpellType.SkillShot
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Riven",
                    Slot = SpellSlot.E,
                    SpellName = "rivenfeint",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Sejuani

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Sejuani",
                    Slot = SpellSlot.Q,
                    SpellName = "sejuaniarcticassault",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Shen

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Shen",
                    Slot = SpellSlot.E,
                    SpellName = "shene",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Shyvana

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Shyvana",
                    Slot = SpellSlot.R,
                    SpellName = "shyvanatransformcast",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Talon

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Talon",
                    Slot = SpellSlot.Q,
                    SpellName = "talonq",
                    SpellType = GapSpellType.Targeted
                });

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Talon",
                    Slot = SpellSlot.E,
                    SpellName = "taloncutthroat",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Tristana

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Tristana",
                    Slot = SpellSlot.W,
                    SpellName = "rocketjump",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Tryndamere

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Tryndamere",
                    Slot = SpellSlot.E,
                    SpellName = "slashcast",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Vi

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Vi",
                    Slot = SpellSlot.Q,
                    SpellName = "viq",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Vayne

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Vayne",
                    Slot = SpellSlot.Q,
                    SpellName = "vaynetumble",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Warwick

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Warwick",
                    Slot = SpellSlot.R,
                    SpellName = "warwickr",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region XinZhao

            Spells.Add(
                new SpellData
                {
                    ChampionName = "XinZhao",
                    Slot = SpellSlot.E,
                    SpellName = "xenzhaosweep",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Yasuo

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Yasuo",
                    Slot = SpellSlot.E,
                    SpellName = "yasuodashwrapper",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Zac

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Zac",
                    Slot = SpellSlot.E,
                    SpellName = "zace",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion

            #region Zed

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Zed",
                    Slot = SpellSlot.R,
                    SpellName = "zedr",
                    SpellType = GapSpellType.Targeted
                });

            #endregion

            #region Ziggs

            Spells.Add(
                new SpellData
                {
                    ChampionName = "Ziggs",
                    Slot = SpellSlot.W,
                    SpellName = "ziggswtoggle",
                    SpellType = GapSpellType.SkillShot
                });

            #endregion
        }

        public static void Attach(Menu mainMenu, string menuName)
        {
            if (ObjectManager.Get<Obj_AI_Hero>().Any(h => h.IsEnemy))
            {
                Menu = new Menu("Gapcloser", menuName)
                {
                    new MenuBool("GapcloserEnabled", "Enabled"),
                    new MenuSeperator("GapcloserSeperator1")
                };
                mainMenu.Add(Menu);

                foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(x => x.IsEnemy))
                {
                    var heroMenu = new Menu("Gapcloser" + enemy.ChampionName.ToLower(), enemy.ChampionName)
                    {
                        new MenuBool("Gapcloser" + enemy.ChampionName.ToLower() + "Enabled", "Enabled"),
                    };
                    Menu.Add(heroMenu);

                    foreach (var spell in Spells.Where(x => x.ChampionName == enemy.ChampionName))
                    {
                        heroMenu.Add(new MenuBool("Gapcloser" + enemy.ChampionName.ToLower() + "." + spell.SpellName.ToLower(), "Slot: " + spell.Slot + " (" + spell.SpellName + ")"));
                    }
                }

                Game.OnUpdate += OnUpdate;
                Obj_AI_Base.OnProcessSpellCast += OnProcessSpellCast;
                Obj_AI_Base.OnNewPath += OnNewPath;
            }
            else
            {
                mainMenu.Add(new MenuSeperator("separator5", "No enemies found, no need for an Anti-Gapcloser Menu"));
            }
        }

        private static void OnNewPath(Obj_AI_Base sender, Obj_AI_BaseNewPathEventArgs args)
        {
            if (sender == null || sender.Type != GameObjectType.obj_AI_Hero || !sender.IsEnemy)
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
                var gapSender = sender as Obj_AI_Hero;
                if (gapSender == null)
                {
                    return;
                }

                Gapclosers[sender.NetworkId].Unit = gapSender;
                Gapclosers[sender.NetworkId].Slot = SpellSlot.Unknown;
                Gapclosers[sender.NetworkId].Type = GapSpellType.Dash;
                Gapclosers[sender.NetworkId].SpellName = sender.UnitSkinName + "_Dash";
                Gapclosers[sender.NetworkId].StartPosition = sender.ServerPosition;
                Gapclosers[sender.NetworkId].EndPosition = args.Path.Last();
                Gapclosers[sender.NetworkId].StartTick = Game.TickCount;
                Gapclosers[sender.NetworkId].EndTick =
                    (int)
                    (Gapclosers[sender.NetworkId].EndPosition.DistanceSqr(Gapclosers[sender.NetworkId].StartPosition) /
                     args.Speed * args.Speed * 1000) + Gapclosers[sender.NetworkId].StartTick;
                Gapclosers[sender.NetworkId].DurationTick = Gapclosers[sender.NetworkId].EndTick - Gapclosers[sender.NetworkId].StartTick;
            }
        }

        private static void OnUpdate()
        {
            if (Gapclosers.Values.Any(x => Game.TickCount - x.StartTick >= 750))
            {
                Gapclosers.Clear();
            }

            var option = Menu["GapcloserEnabled"].As<MenuBool>();
            if (OnGapcloser == null || option == null || !option.Enabled)
            {
                return;
            }

            foreach (var args in Gapclosers.Where(x =>
                x.Value.Unit.IsValidTarget() &&
                Menu["Gapcloser" + x.Value.Unit.ChampionName.ToLower()] != null &&
                Menu["Gapcloser" + x.Value.Unit.ChampionName.ToLower()]["Gapcloser" + x.Value.Unit.ChampionName.ToLower() + "Enabled"].As<MenuBool>().Enabled))
            {
                switch (args.Value.Type)
                {
                    case GapSpellType.SkillShot:
                    case GapSpellType.Dash:
                    case GapSpellType.Targeted:
                        OnGapcloser(args.Value.Unit, args.Value);
                        break;
                }
            }
        }

        private static void OnProcessSpellCast(Obj_AI_Base sender, Obj_AI_BaseMissileClientDataEventArgs args)
        {
            if (sender == null || !sender.IsValid || !sender.IsEnemy || string.IsNullOrEmpty(args.SpellData.Name))
            {
                return;
            }

            var argsName = args.SpellData.Name.ToLower();
            if (argsName.Contains("attack") || argsName.Contains("crit"))
            {
                return;
            }

            var gapSender = sender as Obj_AI_Hero;
            if (gapSender == null)
            {
                return;
            }

            if (Spells.All(x =>
                    !string.Equals(x.SpellName, args.SpellData.Name, StringComparison.CurrentCultureIgnoreCase)) ||
                    !Menu["Gapcloser" + sender.UnitSkinName.ToLower()]["Gapcloser" + sender.UnitSkinName.ToLower() + "." + args.SpellData.Name.ToLower()].As<MenuBool>().Enabled)
            {
                return;
            }

            if (!Gapclosers.ContainsKey(sender.NetworkId))
            {
                Gapclosers.Add(sender.NetworkId, new GapcloserArgs());
            }

            Gapclosers[sender.NetworkId].Unit = gapSender;
            Gapclosers[sender.NetworkId].Slot = args.SpellSlot;
            Gapclosers[sender.NetworkId].Target = (AttackableUnit)args.Target;
            Gapclosers[sender.NetworkId].Type = args.Target != null ? GapSpellType.Targeted : GapSpellType.SkillShot;
            Gapclosers[sender.NetworkId].SpellName = args.SpellData.Name;
            Gapclosers[sender.NetworkId].StartPosition = args.Start;
            Gapclosers[sender.NetworkId].EndPosition = args.End;
            Gapclosers[sender.NetworkId].StartTick = Game.TickCount;
        }
    }
}