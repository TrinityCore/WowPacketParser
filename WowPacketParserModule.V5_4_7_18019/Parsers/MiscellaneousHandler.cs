using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V5_4_7_18019.Enums;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_7_18019.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.CMSG_INSPECT)]
        public static void HandleInspect(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                var guid = packet.StartBitStream(5, 0, 7, 4, 6, 2, 1, 3);
                packet.ParseBitStream(guid, 5, 6, 3, 4, 0, 1, 7, 2);

                packet.WriteGuid("Guid", guid);
            }
            else packet.Opcode = (int)Opcode.CMSG_INSPECT;
        }

        [Parser(Opcode.CMSG_LOAD_SCREEN)]
        public static void HandleClientEnterWorld(Packet packet)
        {
            var mapId = packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map Id");
            packet.ReadBit("Loading");

            CoreParsers.MovementHandler.CurrentMapId = (uint)mapId;

            packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.CMSG_LOG_DISCONNECT)]
        public static void HandleLogDisconnect(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadUInt32("Disconnect Reason");
                // 4 is inability for client to decrypt RSA
                // 3 is not receiving "WORLD OF WARCRAFT CONNECTION - SERVER TO CLIENT"
                // 11 is sent on receiving opcode 0x140 with some specific data
            }
            else
            {
                packet.WriteLine("              : SMSG_GUILD_COMMAND_RESULT");
                packet.Opcode = (int)Opcode.SMSG_GUILD_COMMAND_RESULT;
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing(Packet packet)
        {
            packet.ReadUInt32("Latency");
            packet.ReadUInt32("Ping");
        }

        [Parser(Opcode.CMSG_REALM_SPLIT)]
        public static void HandleClientRealmSplit(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESP)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            packet.ReadUInt32("Counter");
            packet.ReadUInt32("Client Ticks");
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.StartBitStream(3, 5, 6, 7, 2, 4, 1, 0);
            packet.ParseBitStream(guid, 5, 0, 4, 3, 1, 7, 2, 6);

            packet.WriteGuid("Target Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_TITLE)]
        public static void HandleSetTitle(Packet packet)
        {
            packet.ReadInt32("TitleID");
        }

        [Parser(Opcode.SMSG_CLIENTCACHE_VERSION)]
        public static void HandleClientCacheVersion(Packet packet)
        {
            packet.ReadUInt32("Version");
        }

        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong(Packet packet)
        {
            packet.ReadUInt32("Ping");
        }

        [Parser(Opcode.SMSG_SERVER_TIMEZONE)]
        public static void HandleServerTimezone(Packet packet)
        {
            var Location2Lenght = packet.ReadBits(7);
            var Location1Lenght = packet.ReadBits(7);

            packet.ReadWoWString("Timezone Location1", Location1Lenght);
            packet.ReadWoWString("Timezone Location2", Location2Lenght);
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus434(Packet packet)
        {
            packet.ReadByte("Complain System Status");
            packet.ReadInt32("Scroll of Resurrections Remaining");
            packet.ReadInt32("Scroll of Resurrections Per Day");
            packet.ReadInt32("Unused Int32");
            packet.ReadInt32("Unused Int32");
            packet.ReadBit("HasTravelPass");
            var parentalControl = packet.ReadBit("ParentalControl");
            packet.ReadBit("InGameShop");
            packet.ReadBit("RecruitAFrend");
            var feedback = packet.ReadBit("FeedbackSystem");
            packet.ReadBit("unk1");
            packet.ReadBit("IsVoiceChatAllowedByServer");
            packet.ReadBit("InGameShopStatus");
            packet.ReadBit("Scroll of Resurrection Enabled");
            packet.ReadBit("InGameShopParentalControl");

            if (feedback)
            {
                packet.ReadInt32("Unk5");
                packet.ReadInt32("Unk6");
                packet.ReadInt32("Unk7");
                packet.ReadInt32("Unk8");
            }

            if (parentalControl)
            {
                packet.ReadInt32("Unk9");
                packet.ReadInt32("Unk10");
                packet.ReadInt32("Unk11");
            }
        }

        [Parser(Opcode.SMSG_GOSSIP_COMPLETE)]
        public static void HandleGossipComplete(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_0130)]
        public static void HandleUnk0130(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_022F)]
        public static void HandleUnk022F(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_04AB)]
        public static void HandleUnk04AB(Packet packet)
        {
            packet.ReadUInt32("Dword");
        }

        [Parser(Opcode.SMSG_UNK_0851)]
        public static void HandleUnk0851(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_10E3)]
        public static void HandleUnk10E3(Packet packet)
        {
            packet.ReadUInt32("Dword8");
            packet.ReadUInt32("Dword6");
            packet.ReadUInt32("Dword5");
            packet.ReadUInt32("Dword7");
            packet.ReadBit("Bit in Byte16");
        }

        [Parser(Opcode.SMSG_UNK_12D8)]
        public static void HandleUnk12D8(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_12F9)]
        public static void HandleUnk12F9(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_1609)]
        public static void HandleUnk1609(Packet packet)
        {
            packet.ReadBit("Bit in Byte17");
            packet.ReadBit("Bit in Byte18");
            packet.ReadBit("Bit in Byte16");
        }

        [Parser(Opcode.SMSG_UNK_1725)]
        public static void HandleUnk1725(Packet packet)
        {
            packet.AsHex();
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_1D13)]
        public static void HandleUnk1D13(Packet packet)
        {
            packet.ReadBit("Bit in Byte20");
            packet.ReadUInt32("Dword24");
            packet.ReadUInt32("Dword16");
        }
    }
}
