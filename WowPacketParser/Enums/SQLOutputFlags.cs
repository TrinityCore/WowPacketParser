using System;

namespace WowPacketParser.Enums
{
    [Flags]
    enum SQLOutputFlags
    {
        None               = 0,
        GameObjectTemplate = 0x0001,
        GameObjectSpawns   = 0x0002,
        QuestTemplate      = 0x0004,
        QuestPOI           = 0x0008,
        CreatureTemplate   = 0x0010,
        CreatureSpawns     = 0x0020,
        NpcTrainer         = 0x0040,
        NpcVendor          = 0x0080,
        NpcText            = 0x0100,
        Loot               = 0x0200,
        Gossip             = 0x0400,
        PageText           = 0x0800,
        StartInformation   = 0x1000,
        SniffData          = 0x2000,
        SniffDataOpcodes   = 0x4000,
    }
}
