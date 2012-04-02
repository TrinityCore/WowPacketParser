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
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            CurrentAreaId = packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area Id");

            var numFields = packet.ReadInt16("Field Count");
            for (var i = 0; i < numFields; i++)
                ReadWorldStateBlock(ref packet);
        }

        public static void ReadWorldStateBlock(ref Packet packet)
        {
            var field = packet.ReadInt32();
            var val = packet.ReadInt32();
            packet.WriteLine("Field: {0} - Value: {1}", field, val);
        }

        [Parser(Opcode.SMSG_UPDATE_WORLD_STATE)]
        public static void HandleUpdateWorldState(Packet packet)
        {
            ReadWorldStateBlock(ref packet);

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadByte("Unk byte");
        }

        [Parser(Opcode.SMSG_WORLD_STATE_UI_TIMER_UPDATE)]
        public static void HandleUpdateUITimer(Packet packet)
        {
            packet.ReadTime("Time");
        }
    }
}
