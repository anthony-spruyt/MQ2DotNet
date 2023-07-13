using JetBrains.Annotations;
using System;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// BuildType.h
    /// </summary>
    [PublicAPI]
    [Flags]
    public enum Expansion
    {
        /// <summary>
        /// No Expansion
        /// </summary>
        CLASSIC = 0,
        /// <summary>
        /// The Ruins of Kunark
        /// </summary>
        ROK = 1 << 0,
        /// <summary>
        /// The Scars of Velious
        /// </summary>
        SOV = 1 << 1,
        /// <summary>
        /// The Shadows of Luclin
        /// </summary>
        SOL = 1 << 2,
        /// <summary>
        /// The Planes of Power
        /// </summary>
        POP = 1 << 3,
        /// <summary>
        /// The Legacy of Ykesha
        /// </summary>
        LOY = 1 << 4,
        /// <summary>
        /// Lost Dungeons of Norrath
        /// </summary>
        LDON = 1 << 5,
        /// <summary>
        /// Gates of Discord
        /// </summary>
        GOD = 1 << 6,
        /// <summary>
        /// Omens of War
        /// </summary>
        OOW = 1 << 7,
        /// <summary>
        /// Dragons of Norrath
        /// </summary>
        DON = 1 << 8,
        /// <summary>
        /// Depths of Darkhollow
        /// </summary>
        DODH = 1 << 9,
        /// <summary>
        /// Prophecy of Ro
        /// </summary>
        POR = 1 << 10,
        /// <summary>
        /// The Serpent's Spine
        /// </summary>
        TSS = 1 << 11,
        /// <summary>
        /// The Buried Sea
        /// </summary>
        TBS = 1 << 12,
        /// <summary>
        /// Secrets of Faydwer
        /// </summary>
        SOF = 1 << 13,
        /// <summary>
        /// Seeds of Destruction
        /// </summary>
        SOD = 1 << 14,
        /// <summary>
        /// Underfoot
        /// </summary>
        UF = 1 << 15,
        /// <summary>
        /// House of Thule
        /// </summary>
        HOT = 1 << 16,
        /// <summary>
        /// Veil of Alaris
        /// </summary>
        VOA = 1 << 17,
        /// <summary>
        /// Rain of Fear
        /// </summary>
        ROF = 1 << 18,
        /// <summary>
        /// Call of the Forsaken
        /// </summary>
        COTF = 1 << 19,
        /// <summary>
        /// The Darkened Sea
        /// </summary>
        TDS = 1 << 20,
        /// <summary>
        /// The Broken Mirror
        /// </summary>
        TBM = 1 << 21,
        /// <summary>
        /// Empires of Kunark
        /// </summary>
        EOK = 1 << 22,
        /// <summary>
        /// Ring of Scale
        /// </summary>
        ROS = 1 << 23,
        /// <summary>
        /// The Burning Lands
        /// </summary>
        TBL = 1 << 24,
        /// <summary>
        /// Torment of Velious
        /// </summary>
        TOV = 1 << 25,
        /// <summary>
        /// Claws of Veeshan
        /// </summary>
        COV = 1 << 26,
        /// <summary>
        /// Terror of Luclin
        /// </summary>
        TOL = 1 << 27,
        /// <summary>
        /// Night of Shadows
        /// </summary>
        NOS = 1 << 28,
    }
}
