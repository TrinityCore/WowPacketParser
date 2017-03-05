using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_4_8_18291.Parsers
{
    public static class CorpseHandler
    {
        [Parser(Opcode.SMSG_CORPSE_RECLAIM_DELAY)]
        public static void HandleCorpseReclaimDelay(Packet packet)
        {
            var hasDelay = !packet.Translator.ReadBit("hasDelay");
            if (hasDelay)
                packet.Translator.ReadInt32("Delay");
        }

        [Parser(Opcode.SMSG_DEATH_RELEASE_LOC)]
        public static void HandleDeathReleaseLoc(Packet packet)
        {
            packet.Translator.ReadInt32<MapId>("Map Id");
            var pos = new Vector3();
            pos.Y = packet.Translator.ReadSingle();
            pos.X = packet.Translator.ReadSingle();
            pos.Z = packet.Translator.ReadSingle();

            packet.AddValue("Position", pos);
        }

        [Parser(Opcode.SMSG_CORPSE_QUERY_RESPONSE)]
        public static void HandleCorpseQueryResponse(Packet packet)
        {
            var pos = new Vector3();
            var guid = new byte[8];

            guid[0] = packet.Translator.ReadBit();
            guid[3] = packet.Translator.ReadBit();
            guid[2] = packet.Translator.ReadBit();
            packet.Translator.ReadBit("Corpse Found");
            guid[5] = packet.Translator.ReadBit();
            guid[4] = packet.Translator.ReadBit();
            guid[1] = packet.Translator.ReadBit();
            guid[7] = packet.Translator.ReadBit();
            guid[6] = packet.Translator.ReadBit();

            packet.Translator.ReadXORByte(guid, 5);
            pos.Z = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 1);
            packet.Translator.ReadInt32<MapId>("Map ID");
            packet.Translator.ReadXORByte(guid, 6);
            packet.Translator.ReadXORByte(guid, 4);
            pos.X = packet.Translator.ReadSingle();
            packet.Translator.ReadXORByte(guid, 3);
            packet.Translator.ReadXORByte(guid, 7);
            packet.Translator.ReadXORByte(guid, 2);
            packet.Translator.ReadXORByte(guid, 0);
            packet.Translator.ReadInt32<MapId>("Corpse Map ID");
            pos.Y = packet.Translator.ReadSingle();

            packet.AddValue("Position", pos);
            packet.Translator.WriteGuid("Corpse Low GUID", guid);
        }
    }
}
