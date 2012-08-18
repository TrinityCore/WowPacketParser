using System;

namespace PacketDumper.Enums
{
    [Flags]
    public enum SQLOutputFlags
    {
        None               = 0x00000,
        GameObjectTemplate = 0x00001,
        GameObjectSpawns   = 0x00002,
        QuestTemplate      = 0x00004,
        QuestPOI           = 0x00008,
        CreatureTemplate   = 0x00010,
        CreatureSpawns     = 0x00020,
        NpcTrainer         = 0x00040,
        NpcVendor          = 0x00080,
        NpcText            = 0x00100,
        Loot               = 0x00200,
        Gossip             = 0x00400,
        PageText           = 0x00800,
        StartInformation   = 0x01000,
        SniffData          = 0x02000,
        SniffDataOpcodes   = 0x04000,
        ObjectNames        = 0x08000,
        CreatureEquip      = 0x10000,
        CreatureMovement   = 0x20000,
    }
}
