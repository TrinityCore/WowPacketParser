using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class InstanceHandler
    {
        [Parser(Opcode.SMSG_RAID_INSTANCE_INFO)]
        public static void HandleRaidInstanceInfo(Packet packet)
        {
            var counter = packet.ReadInt32("Counter");
            for (var i = 0; i < counter; ++i)
            {
                packet.ReadEntry<Int32>(StoreNameType.Map, "Map ID", i);
                packet.ReadEnum<MapDifficulty>("Map Difficulty", TypeCode.UInt32, i);
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_6a_13623))
                    packet.ReadUInt32("Heroic", i);
                //packet.ReadGuid("Instance GUID", i);
                packet.ReadUInt64("InstanceGUID");
                packet.ReadBoolean("Expired", i);
                packet.ReadBoolean("Extended", i);
                packet.ReadUInt32("Reset Time", i);
                packet.ReadUInt32("Completed Encounters Mask", i);
            }
        }
    }
}
