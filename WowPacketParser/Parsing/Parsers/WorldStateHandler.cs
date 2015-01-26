using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class WorldStateHandler
    {
        public static int CurrentAreaId = -1;

        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadEntry<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadEntry<Int32>(StoreNameType.Zone, "Zone Id");
            CurrentAreaId = packet.ReadEntry<Int32>(StoreNameType.Area, "Area Id");

            var numFields = packet.ReadInt16("Field Count");
            for (var i = 0; i < numFields; i++)
                ReadWorldStateBlock(packet, i);
        }

        public static void ReadWorldStateBlock(Packet packet, params object[] indexes)
        {
            var field = packet.ReadInt32();
            var val = packet.ReadInt32();
            packet.AddValue("Field", field + " - Value: " + val, indexes);
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            ReadWorldStateBlock(packet);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadByte("Unk byte");
        }

        [Parser(Opcode.SMSG_UI_TIME)]
        public static void HandleUpdateUITimer(Packet packet)
        {
            packet.ReadTime("Time");
        }
    }
}
