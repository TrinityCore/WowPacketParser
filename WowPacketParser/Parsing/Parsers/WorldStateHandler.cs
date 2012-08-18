using System;
using PacketParser.Enums;
using PacketParser.Misc;
using PacketParser.DataStructures;
using PacketParser.Processing;

namespace PacketParser.Parsing.Parsers
{
    public static class WorldStateHandler
    {
        [Parser(Opcode.SMSG_INIT_WORLD_STATES)]
        public static void HandleInitWorldStates(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map ID");
            packet.ReadEntryWithName<Int32>(StoreNameType.Zone, "Zone Id");
            PacketFileProcessor.Current.GetProcessor<SessionStore>().CurrentAreaId = packet.ReadEntryWithName<Int32>(StoreNameType.Area, "Area Id");

            var numFields = packet.ReadInt16("Field Count");
            packet.StoreBeginList("WorldStateFields");
            for (var i = 0; i < numFields; i++)
                ReadWorldStateBlock(ref packet, i);
            packet.StoreEndList();
        }

        public static void ReadWorldStateBlock(ref Packet packet, params int[] values)
        {
            packet.StoreBeginObj("WorldStateBlock", values);
            packet.ReadInt32("Field");
            packet.ReadInt32("Value");
            packet.StoreEndObj();
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
