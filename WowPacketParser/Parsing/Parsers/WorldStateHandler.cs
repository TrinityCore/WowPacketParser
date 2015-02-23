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
            packet.ReadInt32<MapId>("Map ID");
            packet.ReadInt32<ZoneId>("Zone Id");
            CurrentAreaId = packet.ReadInt32<AreaId>("Area Id");

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
        public static void HandleUITimer(Packet packet)
        {
            packet.ReadTime("Time");
        }
    }
}
