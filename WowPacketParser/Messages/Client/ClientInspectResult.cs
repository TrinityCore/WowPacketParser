using System.Collections.Generic;
using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
