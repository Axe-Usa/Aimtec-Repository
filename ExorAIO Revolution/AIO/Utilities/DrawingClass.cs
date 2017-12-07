// ReSharper disable ArrangeMethodOrOperatorBody


using System.Collections.Generic;
using Aimtec;

#pragma warning disable 1587

namespace AIO.Utilities
{
    /// <summary>
    ///     The Drawing class.
    /// </summary>
    internal static class DrawingClass
    {
        #region Static Fields

        /// <summary>
        ///     A list of the names of the champions who have a different healthbar type.
        /// </summary>
        public static readonly List<string> SpecialChampions = new List<string> { "Annie", "Jhin", "Corki" };

        /// <summary>
        ///     The default enemy HP bar height offset.
        /// </summary>
        public static int SHeight = 8;

        /// <summary>
        ///     The default enemy HP bar width offset.
        /// </summary>
        public static int SWidth = 103;

        /// <summary>
        ///     The jungle HP bar offset list.
        /// </summary>
        internal static readonly List<JungleHpBarOffset> JungleHpBarOffsetList = new List<JungleHpBarOffset>
                                                                                     {
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Dragon_Air", Width = 140, Height = 4, XOffset = 12, YOffset = 24 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Dragon_Fire", Width = 140, Height = 4, XOffset = 12, YOffset = 24 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Dragon_Water", Width = 140, Height = 4, XOffset = 12, YOffset = 24 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Dragon_Earth", Width = 140, Height = 4, XOffset = 12, YOffset = 24 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Dragon_Elder", Width = 140, Height = 4, XOffset = 12, YOffset = 24 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Baron", Width = 190, Height = 10, XOffset = 16, YOffset = 24 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_RiftHerald", Width = 139, Height = 6, XOffset = 12, YOffset = 22 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Red", Width = 139, Height = 4, XOffset = 12, YOffset = 24 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Blue", Width = 139, Height = 4, XOffset = 12, YOffset = 24 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Gromp", Width = 86, Height = 2, XOffset = 1, YOffset = 7 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "Sru_Crab", Width = 61, Height = 2, XOffset = 1, YOffset = 5 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Krug", Width = 79, Height = 2, XOffset = 1, YOffset = 7 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Razorbeak", Width = 74, Height = 2, XOffset = 1, YOffset = 7 },
                                                                                         new JungleHpBarOffset { UnitSkinName = "SRU_Murkwolf", Width = 74, Height = 2, XOffset = 1, YOffset = 7 }
                                                                                     };

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     The default enemy HP bar x offset.
        /// </summary>
        public static int SxOffset(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 33 : 30;
        }

        /// <summary>
        ///     The default enemy HP bar y offset.
        /// </summary>
        public static int SyOffset(Obj_AI_Hero target)
        {
            return SpecialChampions.Contains(target.ChampionName) ? 7 : 2;
        }

        #endregion

        /// <summary>
        ///     The jungle HP bar offset.
        /// </summary>
        internal class JungleHpBarOffset
        {
            #region Fields

            /// <summary>
            ///     The height.
            /// </summary>
            internal int Height;

            /// <summary>
            ///     The name.
            /// </summary>
            internal string UnitSkinName;

            /// <summary>
            ///     The width.
            /// </summary>
            internal int Width;

            /// <summary>
            ///     The XOffset.
            /// </summary>
            internal int XOffset;

            /// <summary>
            ///     The YOffset.
            /// </summary>
            internal int YOffset;

            #endregion
        }
    }
}