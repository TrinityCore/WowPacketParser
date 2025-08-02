using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;

namespace WowPacketParserModule.V5_5_0_61735.Parsers
{
    public static class SessionHandler
    {
        public static void ReadVirtualRealmNameInfo(Packet packet, params object[] indexes)
        {
            packet.ResetBitReader();
            packet.ReadBit("IsLocal", indexes);
            packet.ReadBit("IsHiddenFromPlayers", indexes);

            var actualNameLength = packet.ReadBits(8);
            var normalizedNameLength = packet.ReadBits(8);

            packet.ReadWoWString("RealmNameActual", actualNameLength, indexes);
            packet.ReadWoWString("RealmNameNormalized", normalizedNameLength, indexes);
        }

        public static void ReadVirtualRealmInfo(Packet packet, params object[] indexes)
        {
            packet.ReadUInt32("RealmAddress", indexes);
            ReadVirtualRealmNameInfo(packet, indexes, "RealmNameInfo");
        }

        [Parser(Opcode.SMSG_REALM_QUERY_RESPONSE)]
        public static void HandleRealmQueryResponse(Packet packet)
        {
            packet.ReadUInt32("VirtualRealmAddress");

            var state = packet.ReadByte("LookupState");
            if (state == 0)
            {
                packet.ResetBitReader();

                packet.ReadBit("IsLocal");
                packet.ReadBit("Unk bit");

                var bits2 = packet.ReadBits(9);
                var bits258 = packet.ReadBits(9);
                packet.ReadBit();

                packet.ReadWoWString("RealmNameActual", bits2);
                packet.ReadWoWString("RealmNameNormalized", bits258);
            }
        }

        [Parser(Opcode.SMSG_MOTD)]
        public static void HandleMessageOfTheDay(Packet packet)
        {
            var lineCount = packet.ReadBits("Line Count", 4);

            packet.ResetBitReader();
            for (var i = 0; i < lineCount; i++)
            {
                var lineLength = (int)packet.ReadBits(7);
                packet.ResetBitReader();
                packet.ReadWoWString("Line", lineLength, i);
            }
        }

        [Parser(Opcode.SMSG_WAIT_QUEUE_UPDATE)]
        public static void HandleWaitQueueUpdate(Packet packet)
        {
            packet.ReadInt32("WaitCount");
            packet.ReadInt32("WaitTime");
            packet.ReadByte("AllowedFactionGroupForCharacterCreate");
            packet.ReadBit("HasFCM");
            packet.ReadBit("CanCreateOnlyIfExisting");
        }

        [Parser(Opcode.SMSG_SUSPEND_TOKEN)]
        [Parser(Opcode.SMSG_RESUME_TOKEN)]
        public static void HandleResumeTokenPacket(Packet packet)
        {
            packet.ReadUInt32("Sequence");
            packet.ReadBits("Reason", 2);
        }

        [Parser(Opcode.SMSG_WAIT_QUEUE_FINISH)]
        public static void HandleSessionZero(Packet packet)
        {
        }
    }
}
