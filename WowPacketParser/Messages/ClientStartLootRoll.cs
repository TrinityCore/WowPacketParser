using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientStartLootRoll
    {
        public uint RollTime;
        public byte Method;
        public LootItem Item;
        public int MapID;
        public ulong LootObj;
        public byte ValidRolls;
    }
}
