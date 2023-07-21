using System;

namespace MQ2DotNet.EQ
{
    /// <summary>
    /// In game race
    /// TODO: Add other races
    /// </summary>
    [Flags]
    public enum Race
    {
        //Any = -1, // Got these values from src\plugins\bzsrch\MQ2Bzsrch.cpp which is likely not correct, no idea...
        Human = 1,
        //Barbarian = 2,
        //Erudite = 3,
        //WoodElf = 4,
        //HighElf = 5,
        //DarkElf = 6,
        //HalfElf = 7,
        //Dwarf = 8,
        //Troll = 9,
        //Ogre = 10,
        //Halfling = 11,
        //Gnome = 12,
        //Iksar = 13,
        //VahShir = 14,
        //Froglok = 15,
        //Drakkin = 522
    }
}