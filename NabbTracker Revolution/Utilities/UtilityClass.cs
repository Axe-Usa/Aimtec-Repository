namespace NabbTracker
{
    using System.Collections.Generic;
    using System.Drawing;

    using Aimtec;

    /// <summary>
    ///     The Utility class.
    /// </summary>
    internal class UtilityClass
    {
        #region Static Fields

        /// <summary>
        ///     Gets the Player.
        /// </summary>
        public static Obj_AI_Hero Player = ObjectManager.GetLocalPlayer();

        /// <summary>
        ///     A list of the names of the champions who have a different healthbar type.
        /// </summary>
        public static readonly List<string> SpecialChampions = new List<string> { "Annie", "Jhin" };

        /// <summary>
        ///     Gets the spellslots.
        /// </summary>
        public static SpellSlot[] SpellSlots =
        {
            SpellSlot.Q,
            SpellSlot.W,
            SpellSlot.E,
            SpellSlot.R
        };

        /// <summary>
        ///     Gets the summoner spellslots.
        /// </summary>
        public static SpellSlot[] SummonerSpellSlots =
        {
            SpellSlot.Summoner1,
            SpellSlot.Summoner2
        };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The Exp Healthbars X coordinate adjustment.
        /// </summary>
        public static int ExpXAdjustment(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 77 : 85;
        }

        /// <summary>
        ///     The Spells Healthbars Y coordinate adjustment.
        /// </summary>
        public static int ExpYAdjustment(Obj_AI_Hero target)
        {
            if (SpecialChampions.Contains(target.ChampionName))
            {
                return MenuClass.Miscellaneous["name"].Enabled ? -62 : -45;
            }

            return target.IsMe
                       ? MenuClass.Miscellaneous["name"].Enabled ? -56 : -37
                       : MenuClass.Miscellaneous["name"].Enabled ? -48 : -29;
        }

        /// <summary>
        ///     The Spells Healthbars X coordinate adjustment.
        /// </summary>
        public static int SpellXAdjustment(Obj_AI_Hero target)
        {
            if (SpecialChampions.Contains(target.ChampionName))
            {
                return target.IsMe ? 34 : 17;
            }

            return target.IsMe ? 55 : 10;
        }

        /// <summary>
        ///     The Spells Healthbars Y coordinate adjustment.
        /// </summary>
        public static int SpellYAdjustment(Obj_AI_Hero target)
        {
            if (SpecialChampions.Contains(target.ChampionName))
            {
                return 25;
            }

            return target.IsMe ? 25 : 35;
        }

        /// <summary>
        ///     The Healthbars X coordinate adjustment.
        /// </summary>
        public static int SummonerSpellXAdjustment(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 2 : 10;
        }

        /// <summary>
        ///     The Healthbars Y coordinate adjustment.
        /// </summary>
        public static int SummonerSpellYAdjustment(Obj_AI_Hero target)
        {
            if (SpecialChampions.Contains(target.ChampionName))
            {
                return MenuClass.Miscellaneous["name"].Enabled ? -32 : -14;
            }

            return target.IsMe
                 ? MenuClass.Miscellaneous["name"].Enabled ? -24 : -6
                 : MenuClass.Miscellaneous["name"].Enabled ? -20 : -4;
        }

        /// <summary>
        ///     The cooldown of a determined spell of a determined unit.
        /// </summary>
        public static string GetUnitSpellCooldown(Obj_AI_Hero unit, int spell)
        {
            var unitSpell = unit.SpellBook.GetSpell(SpellSlots[spell]);
            var cooldownRemaining = unitSpell.CooldownEnd - Game.ClockTime;
            if (cooldownRemaining > 0)
            {
                return ((int)cooldownRemaining).ToString();
            }

            if (unit.IsMe)
            {
                if (unitSpell.State == SpellState.Surpressed ||
                    unitSpell.State == SpellState.Disabled ||
                    unitSpell.State == SpellState.Unknown)
                {
                    return "X";
                }
            }

            return SpellSlots[spell].ToString();
        }

        /// <summary>
        ///     The cooldown of a determined summoner spell of a determined unit.
        /// </summary>
        public static string GetUnitSummonerSpellCooldown(Obj_AI_Hero unit, int summonerSpell)
        {
            var cooldownRemaining = unit.SpellBook.GetSpell(SummonerSpellSlots[summonerSpell]).CooldownEnd - Game.ClockTime;
            if (cooldownRemaining > 0)
            {
                return ((int)cooldownRemaining).ToString();
            }

            return "READY";
        }

        /// <summary>
        ///     Gets the fixed name for reach summonerspell in the game.
        /// </summary>
        public static string GetUnitSummonerSpellFixedName(Obj_AI_Hero unit, int summonerSpell)
        {
            switch (unit.SpellBook.GetSpell(SummonerSpellSlots[summonerSpell]).Name.ToLower())
            {
                case "summonerflash":        return "Flash";
                case "summonerdot":          return "Ignite";
                case "summonerheal":         return "Heal";
                case "summonerteleport":     return "Teleport";
                case "summonerexhaust":      return "Exhaust";
                case "summonerhaste":        return "Ghost";
                case "summonerbarrier":      return "Barrier";
                case "summonerboost":        return "Cleanse";
                case "summonermana":         return "Clarity";
                case "summonerclairvoyance": return "Clairvoyance";
                case "summonersnowball":     return "Mark";
                default:
                    return "Smite";
            }
        }

        /// <summary>
        ///     The color of the string, based on determined events.
        /// </summary>
        public static Color GetUnitSpellStateColor(Obj_AI_Hero unit, int spell)
        {
            var unitSpell = unit.SpellBook.GetSpell(SpellSlots[spell]);
            if (unitSpell.State.HasFlag(SpellState.NotLearned))
            {
                return Color.Gray;
            }

            if (unit.IsMe && unitSpell.State.HasFlag(SpellState.Surpressed) ||
                unitSpell.State.HasFlag(SpellState.Disabled))
            {
                return Color.Purple;
            }

            if (unitSpell.State.HasFlag(SpellState.NoMana))
            {
                return Color.Cyan;
            }

            if (unitSpell.State.HasFlag(SpellState.Cooldown))
            {
                var unitSpellCooldown = unitSpell.CooldownEnd - Game.ClockTime;
                if (unitSpellCooldown <= 5)
                {
                    return Color.Red;
                }

                return Color.Yellow;
            }

            if (unitSpell.State.HasFlag(SpellState.Ready))
            {
                return Color.LightGreen;
            }

            return Color.Black;
        }

        /// <summary>
        ///     The color of the string, based on determined events.
        /// </summary>
        public static Color GetUnitSummonerSpellStateColor(Obj_AI_Hero unit, int summonerSpell)
        {
            var unitSummonerSpell = unit.SpellBook.GetSpell(SummonerSpellSlots[summonerSpell]);
            if (unit.IsMe && unitSummonerSpell.State.HasFlag(SpellState.Surpressed) ||
                unitSummonerSpell.State.HasFlag(SpellState.Disabled))
            {
                return Color.Purple;
            }

            if (unitSummonerSpell.State.HasFlag(SpellState.Cooldown))
            {
                var unitSpellCooldown = unitSummonerSpell.CooldownEnd - Game.ClockTime;
                if (unitSpellCooldown <= 5)
                {
                    return Color.Red;
                }

                return Color.Yellow;
            }

            if (unitSummonerSpell.State.HasFlag(SpellState.Ready))
            {
                return Color.LightGreen;
            }

            return Color.Black;
        }

        #endregion
    }
}