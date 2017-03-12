using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInspectResult
    {
        public List<CliInspectItemData> Items;
        public int ClassID;
        public List<ushort> Talents;
        public List<ushort> Glyphs;
        public CliInspectGuildData? GuildData; // Optional
        public ulong InspecteeGUID;
        public int SpecializationID;
    }
}
