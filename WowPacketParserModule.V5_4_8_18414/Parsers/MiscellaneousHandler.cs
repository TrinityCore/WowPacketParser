using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;
using WowPacketParser.Parsing;
using WowPacketParserModule.V5_4_8_18414.Enums;
using CoreParsers = WowPacketParser.Parsing.Parsers;

namespace WowPacketParserModule.V5_4_8_18414.Parsers
{
    public static class MiscellaneousHandler
    {
        [Parser(Opcode.CMSG_ADD_FRIEND)]
        public static void HandleAddFriend(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_ADD_IGNORE)]
        public static void HandleAddIgnore(Packet packet)
        {
            packet.ReadCString("Str");
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            packet.ReadInt32("unk");
            packet.ReadBit("unkb");
            packet.ReadBit("unkb2");
        }

        [Parser(Opcode.CMSG_BUY_BANK_SLOT)]
        public static void HandleBuyBankSlot(Packet packet)
        {
            var guid = packet.StartBitStream(7, 6, 1, 3, 2, 0, 4, 5);
            packet.ParseBitStream(guid, 3, 5, 1, 6, 7, 2, 0, 4);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_DEL_FRIEND)]
        public static void HandleDelFriend(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_DEL_IGNORE)]
        public static void HandleDelIgnore(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_INSPECT)]
        public static void HandleInspect(Packet packet)
        {
            packet.ReadToEnd();
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
            packet.ReadUInt32("Disconnect Reason");
            // 4 is inability for client to decrypt RSA
            // 3 is not receiving "WORLD OF WARCRAFT CONNECTION - SERVER TO CLIENT"
            // 11 is sent on receiving opcode 0x140 with some specific data
        }

        [Parser(Opcode.CMSG_MINIMAP_PING)]
        public static void HandleMinimapPing(Packet packet)
        {
            packet.ReadSingle("Y");
            packet.ReadSingle("X");
            packet.ReadByte("byte24");
        }

        [Parser(Opcode.CMSG_OPENING_CINEMATIC)]
        public static void HandleOpeningCinematic(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing(Packet packet)
        {
            packet.ReadUInt32("Latency");
            packet.ReadUInt32("Ping");
        }

        [Parser(Opcode.CMSG_QUESTGIVER_STATUS_MULTIPLE_QUERY)]
        public static void HandleQuestgiverStatusMultipleQuery(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : SMSG_SPLINE_MOVE_SET_RUN_SPEED");
                var guid = packet.StartBitStream(3, 0, 1, 4, 7, 5, 6, 2);
                packet.ParseBitStream(guid, 4);
                packet.ReadSingle("Speed"); // 24
                packet.ParseBitStream(guid, 1, 5, 3, 7, 6, 2, 0);
                packet.WriteGuid("Guid", guid);
            }
        }

        [Parser(Opcode.CMSG_RAID_READY_CHECK)]
       public static void HandleRaidReadyCheck(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : SMSG_MOVE_SET_SWIM_SPEED"); // sub_C8CBBE
                var guid = packet.StartBitStream(5, 0, 6, 3, 7, 2, 4, 1);
                packet.ReadInt32("MCounter"); // 28
                packet.ParseBitStream(guid, 1, 3);
                packet.ReadSingle("Speed"); // 24
                packet.ParseBitStream(guid, 6, 7, 0, 5, 2, 4);
                packet.WriteGuid("Guid", guid);
            }
        }

        [Parser(Opcode.CMSG_RAID_READY_CHECK_CONFIRM)]
        public static void HandleRaidReadyCheckConfirm(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_RANDOM_ROLL)]

        public static void HandleRandomRoll(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadToEnd();
            }
            else
            {
                packet.WriteLine("              : SMSG_???");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_REALM_SPLIT)]
        public static void HandleClientRealmSplit(Packet packet)
        {
        }

        [Parser(Opcode.CMSG_RECLAIM_CORPSE)]
        public static void HandleClientReclaimCorpse(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_REPOP_REQUEST)]
        public static void HandleClientRepopRequest(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_RETURN_TO_GRAVEYARD)]
        public static void HandleClientreturnToGraveyard(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SET_CONTACT_NOTES)]
        public static void HandleSetContactNotes(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SET_DUNGEON_DIFFICULTY)]
        public static void HandleSetDungeonDifficulty(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SET_PVP)]
        public static void HandleSetPVP(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SET_SELECTION)]
        public static void HandleSetSelection(Packet packet)
        {
            var guid = packet.StartBitStream(7, 6, 5, 4, 3, 2, 1, 0);
            packet.ParseBitStream(guid, 0, 7, 3, 5, 1, 4, 6, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_SET_TITLE)]
        public static void HandleSetTitle(Packet packet)
        {
            packet.ReadInt32("Title");
        }

        [Parser(Opcode.CMSG_SPELLCLICK)]
        public static void HandleSpellClick(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SUBMIT_BUG)]
        public static void HandleSubmitBug(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SUBMIT_COMPLAIN)]
        public static void HandleSubmitComplain(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_SUGGESTION_SUBMIT)]
        public static void HandleSuggestionSubmit(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESP)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadUInt32("MCounter");
                packet.ReadUInt32("Client Ticks");
            }
            else
            {
                packet.WriteLine("              : SMSG_SPELL_CATEGORY_COOLDOWN");
                packet.ReadToEnd();
            }

        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESP_FAILED)]
        public static void HandleTimeSyncRespFailed(Packet packet)
        {
            packet.ReadUInt32("Unk Uint32");
        }

        [Parser(Opcode.CMSG_UNK_0068)] // sub_669815
        public static void HandleUnk0068(Packet packet)
        {
            packet.ReadInt32("unk16");
            packet.ReadInt32("unk20");
            var len = packet.ReadInt32("Len");
            var pkt = packet.Inflate(len, 4096);
            packet.WriteLine(pkt.ReadCString());
            packet.ReadBits("unk24", 3);
            packet.ResetBitReader();
        }

        [Parser(Opcode.CMSG_UNK_006B)]
        public static void HandleUnk006B(Packet packet)
        {
            packet.ReadByte("unk17");
            packet.ReadBit("unk16");
            packet.ResetBitReader();
        }

        [Parser(Opcode.CMSG_UNK_0087)]
        public static void HandleUnk0087(Packet packet)
        {
            var val = packet.ReadBit("unkb");
            if (val != 0)
                packet.ReadInt32("unk");
        }

        [Parser(Opcode.CMSG_UNK_00A7)]
        public static void HandleUnk00A7(Packet packet)
        {
            packet.ReadByte("unk");
        }

        [Parser(Opcode.CMSG_UNK_0264)]
        public static void HandleUnk0264(Packet packet)
        {
            packet.ReadInt16("unk16");
        }

        [Parser(Opcode.CMSG_UNK_0292)]
        public static void HandleUnk0292(Packet packet)
        {
            packet.ReadInt32("unk20");
        }

        [Parser(Opcode.CMSG_UNK_0265)]
        public static void HandleUnk0265(Packet packet)
        {
            packet.ReadBit("unkb");
        }

        [Parser(Opcode.CMSG_UNK_02C4)]
        public static void HandleUnk02C4(Packet packet)
        {
            packet.ReadBit("unkb");
            packet.ReadWoWString("Str", packet.ReadBits(6));
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.CMSG_UNK_09F0)]
        public static void HandleUnk09F0(Packet packet)
        {
            packet.ReadBit("unk");
            var guid = packet.StartBitStream(3, 0, 2, 1, 5, 4, 7, 6);
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 3, 4, 5, 2, 7, 0, 1, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNK_0CF0)]
        public static void HandleUnk0CF0(Packet packet)
        {
            var count = packet.ReadBits("count", 22);
            for (var i = 0; i < count; i++)
                packet.ReadInt32("unk24", i);
        }

        [Parser(Opcode.CMSG_UNK_115B)]
        public static void HandleUnk115B(Packet packet)
        {
            packet.ReadBit("unkb");
            packet.ResetBitReader();
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.CMSG_UNK_1258)] // sub_68B545
        public static void HandleUnk1258(Packet packet)
        {
            var guid = packet.StartBitStream(1, 0, 3, 2, 7, 4, 5, 6);
            packet.ResetBitReader();
            packet.ParseBitStream(guid, 3, 7, 5, 1, 0, 6, 4, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.CMSG_UNK_15A9)]
        public static void HandleUnk15A9(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadBit("unk16");
                packet.ReadBit("unk17");
                packet.ResetBitReader();
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_15A9");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.CMSG_UNK_1841)]
        public static void HandleUnk1841(Packet packet)
        {
            packet.ReadByte("unk");
        }

        [Parser(Opcode.CMSG_UNK_19C2)]
        public static void HandleUnk19C2(Packet packet)
        {
            packet.ReadBit("unkb");
        }

        [Parser(Opcode.CMSG_UNK_1D8A)]
        public static void HandleUnk1D8A(Packet packet)
        {
            packet.ReadBits("unk", 3);
        }

        [Parser(Opcode.CMSG_UNK_1DAE)]
        public static void HandleUnk1DAE(Packet packet)
        {
            for (var i = 0; i < 50; i++)
                packet.ReadInt32("unk20", i);
            packet.ReadInt32("unk16");
        }

        [Parser(Opcode.SMSG_CLIENTCACHE_VERSION)]
        public static void HandleClientCacheVersion(Packet packet)
        {
            packet.ReadUInt32("Version");
        }

        [Parser(Opcode.SMSG_CORPSE_RECLAIM_DELAY)]
        public static void HandleCorpseReclaimDelay(Packet packet)
        {
            var hasData = !packet.ReadBit("!hasData");
            if (hasData)
                packet.ReadInt32("Data");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadInt32("unk48");
            packet.ReadInt32("unk80");
            packet.ReadInt32("unk56");
            packet.ReadByte("unk53");
            packet.ReadInt32("unk20");
            packet.ReadBit("unk45");
            packet.ReadBit("unk47");
            packet.ReadBit("unk52");
            packet.ReadBit("unk17");
            packet.ReadBit("unk16");
            packet.ReadBit("unk44");
            packet.ReadBit("unk76");
            var unk72 = packet.ReadBit("unk72");
            packet.ReadBit("unk46");
            var unk40 = packet.ReadBit("unk40");
            if (unk72)
            {
                packet.ReadInt32("unk60");
                packet.ReadInt32("unk64");
                packet.ReadInt32("unk68");
            }
            if (unk40)
            {
                packet.ReadInt32("unk36");
                packet.ReadInt32("unk32");
                packet.ReadInt32("unk24");
                packet.ReadInt32("unk28");
            }
        }

        [Parser(Opcode.SMSG_GOSSIP_COMPLETE)]
        public static void HandleGossipComplete(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_HOTFIX_INFO)]
        public static void HandleHotfixInfo(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_MINIMAP_PING)]
        public static void HandleSMimimapPing(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        public static void HandlePlaySound(Packet packet)
        {
            var guid = packet.StartBitStream(2, 3, 7, 6, 0, 5, 4, 1);
            packet.ReadInt32("Sound");
            packet.ParseBitStream(guid, 3, 2, 4, 7, 5, 0, 6, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_PLAYED_TIME)]
        public static void HandlePlayedTime(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadInt32("unk16");
                packet.ReadInt32("unk20");
                packet.ReadBit("unk24");
            }
            else
            {
                packet.WriteLine("              : CMSG_UNK_11E2");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.SMSG_PLAYERBOUND)]
        public static void HandlePlayerBound(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong(Packet packet)
        {
            packet.ReadUInt32("Ping");
        }

        [Parser(Opcode.SMSG_RAID_READY_CHECK)]
        [Parser(Opcode.SMSG_RAID_READY_CHECK_COMPLETED)]
        [Parser(Opcode.SMSG_RAID_READY_CHECK_CONFIRM)]
        [Parser(Opcode.SMSG_RANDOM_ROLL)]
        [Parser(Opcode.SMSG_RANDOMIZE_CHAR_NAME)]
        [Parser(Opcode.SMSG_TRIGGER_CINEMATIC)]
        [Parser(Opcode.SMSG_TRIGGER_MOVIE)]
        public static void HandleRaidreadycheck(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SET_FACTION_ATWAR)]
        public static void HandleSetFactionAtWar(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_SET_FACTION_STANDING)]
        public static void HandleSetFactionStanding(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_TIME_SYNC_REQ)]
        public static void HandleTimeSyncReq(Packet packet)
        {
            packet.ReadUInt32("MCounter");
        }

        [Parser(Opcode.SMSG_SET_TIMEZONE_INFORMATION)]
        public static void HandleServerTimezone(Packet packet)
        {
            var Location2Lenght = packet.ReadBits(7);
            var Location1Lenght = packet.ReadBits(7);

            packet.ReadWoWString("Timezone Location1", Location1Lenght);
            packet.ReadWoWString("Timezone Location2", Location2Lenght);
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void Handleweather(Packet packet)
        {
            packet.ReadInt32("unk24");
            packet.ReadSingle("unk16");
            packet.ReadBit("unk20");
        }

        [Parser(Opcode.SMSG_WORLD_SERVER_INFO)]
        public static void HandleWorldServerInfo(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                var unk52 = packet.ReadBit("unk52");
                var unk40 = packet.ReadBit("unk40");
                var unk20 = packet.ReadBit("unk20");
                var unk28 = packet.ReadBit("unk28");

                if (unk40)
                    packet.ReadInt32("unk36");

                packet.ReadByte("unk32");
                packet.ReadInt32("unk176");
                packet.ReadInt32("unk224");

                if (unk20)
                    packet.ReadInt32("unk64");

                if (unk52)
                    packet.ReadInt32("unk192");

                if (unk28)
                    packet.ReadInt32("unk96");
            }
            else
            {
                packet.WriteLine("              : CMSG_NULL_0082");
            }
        }

        [Parser(Opcode.SMSG_UNK_001F)]
        public static void HandleUnk001F(Packet packet)
        {
            //var guid = packet.StartBitStream(4, 7, 1, 0, 5, 3, 2);
            var guid = new byte[8];
            guid[4] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            var bit80 = packet.ReadBit("Byte80");
            guid[6] = packet.ReadBit();
            packet.ReadBit("Byte16");
            if (!bit80)
                packet.ReadInt32("Dword80");

            packet.ParseBitStream(guid, 5, 6, 7, 3, 4, 0, 2, 1);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0050)]
        public static void HandleUnk0050(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            for (var i = 0 ; i < count; i++)
                packet.ReadBit("unk40", i);
            for (var i = 0 ; i < count; i++)
            {
                packet.ReadInt32("unk84", i);
                packet.ReadInt32("unk36", i);
                packet.ReadInt32("unk68", i);
                packet.ReadInt32("unk52", i);
                packet.ReadInt32("unk20", i);
            }
        }

        [Parser(Opcode.SMSG_UNK_0063)] // sub_C89A3D
        public static void HandleUnk0063(Packet packet)
        {
            var guid = packet.StartBitStream(6, 4, 2, 7, 1, 3, 0, 5);
            packet.ParseBitStream(guid, 2, 7, 5, 1, 4, 6, 0, 3);
            packet.WriteGuid("Guid", guid);
            packet.ReadSingle("unk");
        }

        [Parser(Opcode.SMSG_UNK_00A3)]
        public static void HandleUnk00A3(Packet packet)
        {
            packet.ReadInt32("Dword4");
        }

        [Parser(Opcode.SMSG_UNK_00AB)]
        public static void HandleUnk00AB(Packet packet)
        {
            var count = packet.ReadBits("count", 21);
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("unk20", i);
                packet.ReadInt32("unk24", i);
            }
        }

        [Parser(Opcode.SMSG_UNK_00F9)]
        public static void HandleUnk00F9(Packet packet)
        {
            var count = packet.ReadBits("count", 21);
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("unk20", i);
                packet.ReadByte("unk24", i);
            }
        }

        [Parser(Opcode.SMSG_UNK_01D2)] // sub_C697B7
        public static void HandleUnk01D2(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid2[3] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            var unk48 = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid[7] = packet.ReadBit();

            if (unk48)
            {
                packet.ReadInt32("unk56");
                packet.ReadInt32("unk52");
            }

            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid, 4);

            packet.ReadInt32("unk60");

            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 1);

            packet.ReadInt32("unk32");

            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid2, 4);

            packet.ReadInt32("unk16");

            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 1);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_01E1)] // sub_C8B308
        public static void HandleUnk01E1(Packet packet)
        {
            var guid = packet.StartBitStream(1, 5, 2, 0, 3, 6, 4, 7);
            packet.ParseBitStream(guid, 2, 7, 1, 3, 5, 0, 4, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0250)] // sub_C95075
        public static void HandleUnk0250(Packet packet)
        {
            var guid = new byte[8];

            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[1] = packet.ReadBit();

            var unk20 = !packet.ReadBit("skip unk20");

            guid[3] = packet.ReadBit();

            var unk16 = packet.ReadBits("unk16", 2);

            guid[2] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();

            packet.ReadSingle("Speed");
            if (unk20)
                packet.ReadInt32("unk20");

            packet.ParseBitStream(guid, 3, 2, 5, 6);

            packet.ReadInt32("MCounter");
            packet.ReadSingle("unk32");
            packet.ParseBitStream(guid, 7, 1, 4, 0);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_036D)]
        public static void HandleUnk036D(Packet packet)
        {
            var count = packet.ReadBits("count", 21);
            var cnt = new uint[count];
            for (var i = 0; i < count; i++)
                cnt[i] = packet.ReadBits("unk36", 22, i);
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("unk20", i);
                for (var j = 0; j < cnt[i]; j++)
                    packet.ReadInt32("unk56", i, j);
            }
        }

        [Parser(Opcode.SMSG_UNK_042A)] // pair CMSG_NULL_06E4
        public static void HandleUnk042A(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                var count = packet.ReadBits("count", 22);
                packet.ReadBit("unk");
                for (var i = 0; i < count; i++)
                    packet.ReadInt32("unk6", i);
            }
            else
            {
                packet.WriteLine("              : CMSG_LEAVE_CHANNEL");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.SMSG_UNK_043F)]
        public static void HandleUnk043F(Packet packet)
        {
            packet.ReadInt32("Dword8");
            packet.ReadBits("unk", 19);
        }

        [Parser(Opcode.SMSG_UNK_0562)]
        public static void HandleUnk0562(Packet packet)
        {
            packet.ReadSingle("unk40");
            packet.ReadSingle("unk32");
            packet.ReadSingle("unk24");
            packet.ReadInt32("MCounter");
            packet.ReadSingle("unk28");

            var guid = packet.StartBitStream(2, 0, 7, 1, 4, 6, 5, 3);
            packet.ParseBitStream(guid, 6, 0, 7, 5, 4, 3, 1, 2);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_05F3)]
        public static void HandleUnk05F3(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];
            guid2[1] = packet.ReadBit(); // 81
            guid[2] = packet.ReadBit(); // 34
            var unk72 = packet.ReadBit("unk72"); // 72
            guid[6] = packet.ReadBit(); // 38
            guid2[3] = packet.ReadBit(); // 83
            guid[4] = packet.ReadBit(); // 36
            guid2[2] = packet.ReadBit(); // 82
            guid2[5] = packet.ReadBit(); // 85
            guid2[6] = packet.ReadBit(); // 86
            guid[3] = packet.ReadBit(); // 35
            guid2[0] = packet.ReadBit(); // 80
            guid[5] = packet.ReadBit(); // 37
            guid[1] = packet.ReadBit(); // 33
            guid[0] = packet.ReadBit(); // 32
            guid2[7] = packet.ReadBit(); // 87
            guid2[4] = packet.ReadBit(); // 84
            guid[7] = packet.ReadBit(); // 39

            if (unk72)
            {
                var unk56 = packet.ReadBits("unk56", 21); // 56
                packet.ReadInt32("unk48"); // 48
                for ( var i = 0; i < unk56; i++ )
                {
                    packet.ReadInt32("unk64", i); // 64
                    packet.ReadInt32("unk60", i); // 60
                }
                packet.ReadInt32("unk44"); // 44
                packet.ReadInt32("unk52"); // 52
            }

            packet.ReadXORByte(guid2, 2); // 82
            packet.ReadXORByte(guid, 6); // 38
            packet.ReadXORByte(guid2, 6); // 86
            packet.ReadXORByte(guid2, 4); // 84
            packet.ReadXORByte(guid, 3); // 35
            packet.ReadXORByte(guid2, 7); // 87
            packet.ReadInt32("unk40"); // 40
            packet.ReadXORByte(guid, 4); // 36
            packet.ReadXORByte(guid2, 1); // 81
            packet.ReadInt32("unk24"); // 24
            packet.ReadXORByte(guid, 7); // 39
            packet.ReadInt32("unk16"); // 16
            packet.ReadInt32("unk20"); // 20
            packet.ReadXORByte(guid2, 5); // 85
            packet.ReadXORByte(guid, 5); // 37
            packet.ReadXORByte(guid2, 0); // 80
            packet.ReadXORByte(guid, 1); // 33
            packet.ReadXORByte(guid, 0); // 32
            packet.ReadXORByte(guid, 2); // 34
            packet.ReadInt32("unk88"); // 88
            packet.ReadXORByte(guid2, 3); // 83

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_0632)]
        public static void HandleUnk0632(Packet packet)
        {
            var guid = new byte[8];
            guid[5] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            var count = packet.ReadBits("Count", 21);
            var guid1 = new byte[count][];
            for (var i = 0; i < count; i++)
            {
                guid1[i] = packet.StartBitStream(2, 3, 6, 5, 1, 4, 0, 7);
            }

            guid[2] = packet.ReadBit();

            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid1[i], 6, 7, 0, 1, 2, 5, 3, 4);
                packet.WriteGuid("Guid", guid1[i], i);
                packet.ReadInt32("Int20", i);
            }

            packet.ParseBitStream(guid, 1, 4, 2, 3, 5, 6, 0, 7);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0728)] // sub_C8CED2
        public static void HandleUnk0728(Packet packet)
        {
            var guid = packet.StartBitStream(3, 7, 2, 4, 5, 6, 0, 1);
            packet.ParseBitStream(guid, 2, 4, 5, 7, 1, 0, 3, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0868)]
        public static void HandleUnk0868(Packet packet)
        {
            var guid = packet.StartBitStream(0, 4, 1, 6, 7, 2, 5, 3);
            packet.ParseBitStream(guid, 7, 6, 0, 1, 2, 4);
            packet.ReadInt32("Count");
            packet.ParseBitStream(guid, 5, 3);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_089B)]
        public static void HandleUnk089B(Packet packet)
        {
            var cnt20 = packet.ReadBits("cnt20", 20);
            var guid = new byte[cnt20][];
            var unk12 = new bool[cnt20];
            var unk20 = new bool[cnt20];
            for (var i = 0; i < cnt20; i++)
            {
                guid[i] = new byte[8];
                guid[i][3] = packet.ReadBit();
                unk12[i] = packet.ReadBit("unk12", i);
                guid[i][2] = packet.ReadBit();
                unk20[i] = packet.ReadBit("unk20", i);
                guid[i][6] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
            }

            for (var i = 0; i < cnt20; i++)
            {
                packet.ReadInt32("unk52", i);
                packet.ParseBitStream(guid[i], 5, 4, 6, 1);
                packet.ReadByte("unk56");
                packet.ParseBitStream(guid[i], 0);
                packet.ReadSingle("unk24");
                if (unk20[i])
                    packet.ReadInt32("unk88");
                packet.ReadInt32("unk176");
                packet.ParseBitStream(guid[i], 3, 2);
                if (unk12[i])
                    packet.ReadInt32("unk56");
                packet.ParseBitStream(guid[i], 7);
            }
            packet.ReadSingle("unk16");
        }

        [Parser(Opcode.SMSG_UNK_0987)]
        public static void HandleUnk0987(Packet packet)
        {
            packet.ReadToEnd();
        }

        [Parser(Opcode.SMSG_UNK_09D3)]
        public static void HandleUnk09D3(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                var count = packet.ReadBits("count", 22);
                var cnt = new uint[count];
                for (var i = 0; i < count; i++)
                    cnt[i] = packet.ReadBits("cnt", 21, i);
                for (var i = 0; i < count; i++)
                {
                    for (var j = 0; j < cnt[i]; j++)
                    {
                        packet.ReadSingle("unk28", i, j);
                        packet.ReadByte("unk32", i, j);
                    }
                    packet.ReadByte("unk20", i);
                }
            }
            else
            {
                packet.WriteLine("              : CMSG_UNK_???");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.SMSG_UNK_0A03)]
        public static void HandleUnk0A03(Packet packet)
        {
            packet.ReadInt32("Count");
            var guid = packet.StartBitStream(4, 3, 7, 6, 5, 1, 0, 2);
            packet.ParseBitStream(guid, 4, 7, 1, 2, 6, 0, 3, 5);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0A0B)]
        public static void HandleUnk0A0B(Packet packet)
        {
            for (var i = 0; i < 256; i++)
                packet.ReadBit("Byte16", i);
        }

        [Parser(Opcode.SMSG_UNK_0A3F)]
        public static void HandleUnk0A3F(Packet packet)
        {
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.SMSG_UNK_0A8B)]
        public static void HandleUnk0A8B(Packet packet)
        {
            var hasData = !packet.ReadBit("!hasData");
            var count = packet.ReadBits("count", 24);
            packet.ReadInt32("unk20");
            packet.ReadByte("unk40");
            if (hasData)
            {
                packet.ReadInt32("unk16");
            }
            for (var i = 0; i < count; i++)
            {
                packet.ReadByte("unkb28", i);
            }
            packet.ReadByte("unk41");
        }

        [Parser(Opcode.SMSG_UNK_0A9E)]
        public static void HandleUnk0A9E(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var guid = new byte[count][];
            var guid2 = new byte[count][];
            var cnt = new uint[count];
            for (var i = 0; i < count; i++)
            {
                guid[i] = new byte[8];
                guid2[i] = new byte[8];

                guid[i][0] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();

                cnt[i] = packet.ReadBits("cnt", 4, i);

                guid2[i][4] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
                guid2[i][0] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                guid2[i][3] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                guid[i][3] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
            }
            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid[i], 2);
                packet.ParseBitStream(guid2[i], 2, 4);
                packet.ParseBitStream(guid[i], 4);
                packet.ParseBitStream(guid2[i], 6, 0);
                packet.ReadInt32("unk276", i);
                packet.ParseBitStream(guid2[i], 1, 7);
                packet.ReadInt32("unksub132", i);
                packet.ParseBitStream(guid[i], 7, 6, 5, 1);
                packet.ParseBitStream(guid2[i], 3);
                packet.ReadInt32("unk260", i);
                packet.ParseBitStream(guid2[i], 5);
                packet.ParseBitStream(guid[i], 3, 0);
                packet.ReadInt32("unk20", i);
                packet.WriteGuid("Guid", guid[i], i);
                packet.WriteGuid("Guid2", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNK_0AAE)]
        public static void HandleUnk0AAE(Packet packet)
        {
            packet.ReadBits("unk32", 3);
            var guid = packet.StartBitStream(5, 1, 3, 7, 0, 4, 2, 6);
            packet.ParseBitStream(guid, 3, 1, 5);
            packet.ReadInt32("unk96");
            var count = packet.ReadInt32("count");
            var pkt = packet.Inflate(count, 4096);
            packet.WriteLine(pkt.ReadCString());
            packet.ParseBitStream(guid, 7, 4, 0, 6, 2);
            packet.ReadInt32("unk112");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_0C44)]
        public static void HandleUnk0C44(Packet packet)
        {
            packet.ReadInt32("unk1");
            packet.ReadInt32("unk2");
        }

        [Parser(Opcode.SMSG_UNK_0CBE)] // sub_714C42
        public static void HandleUnk0CBE(Packet packet)
        {
            var count1 = packet.ReadBits("count1", 24);
            packet.ReadBit("unk16");
            var count2 = packet.ReadBits("count36", 24);
            var guid2 = new byte[count2][];
            for (var i = 0; i < count2; i++)
            {
                guid2[i] = new byte[8];
                guid2[i] = packet.StartBitStream(4, 7, 3, 2, 6, 0, 5, 1);
            }
            var guid3 = new byte[count1][];
            for (var i = 0; i < count1; i++)
            {
                guid3[i] = new byte[8];
                guid3[i] = packet.StartBitStream(5, 4, 1, 7, 0, 6, 2, 3);
            }
            var count3 = packet.ReadBits("count3", 24);
            var guid4 = new byte[count3][];
            for (var i = 0; i < count3; i++)
            {
                guid4[i] = new byte[8];
                guid4[i] = packet.StartBitStream(1, 4, 3, 6, 2, 0, 7, 5);
            }
            var count4 = packet.ReadBits("count4", 20);
            var guid5 = new byte[count4][];
            for (var i = 0; i < count4; i++)
            {
                guid5[i] = new byte[8];
                guid5[i] = packet.StartBitStream(5, 3, 7, 4, 2, 0, 6, 1);
            }
            var count5 = packet.ReadBits("count5", 20);
            var guid6 = new byte[count5][];
            for (var i = 0; i < count5; i++)
            {
                guid6[i] = new byte[8];
                guid6[i] = packet.StartBitStream(3, 5, 2, 6, 4, 0, 1, 7);
            }
            for (var i = 0; i < count5; i++)
            {
                packet.ParseBitStream(guid6[i], 5, 2);
                packet.ReadSingle("unk72", i);
                packet.ReadSingle("unk64", i);
                packet.ParseBitStream(guid6[i], 1, 4, 6, 0);
                packet.ReadInt32("unk136", i);
                packet.ReadSingle("unk68", i);
                packet.ParseBitStream(guid6[i], 3, 7);
                packet.WriteGuid("Guid6", guid6[i], i);
            }
            for (var i = 0; i < count1; i++)
            {
                packet.ParseBitStream(guid3[i], 4, 0, 6, 7, 5, 3, 1, 2);
                packet.WriteGuid("Guid3", guid3[i], i);
            }
            for (var i = 0; i < count4; i++)
            {
                packet.ParseBitStream(guid5[i], 2, 5);
                packet.ReadSingle("unk104", i);
                packet.ReadInt32("unk108", i);
                packet.ReadSingle("unk100", i);
                packet.ParseBitStream(guid5[i], 1);
                packet.ReadSingle("unk96", i);
                packet.ParseBitStream(guid5[i], 6, 7, 4, 3, 0);
                packet.WriteGuid("Guid5", guid5[i], i);
            }
            for (var i = 0; i < count3; i++)
            {
                packet.ParseBitStream(guid4[i], 7, 1, 0, 6, 2, 3, 4, 5);
                packet.WriteGuid("Guid4", guid4[i], i);
            }
            for (var i = 0; i < count2; i++)
            {
                packet.ParseBitStream(guid2[i], 6, 2, 5, 0, 3, 4, 1, 7);
                packet.WriteGuid("Guid2", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNK_0CF2)]
        public static void HandleUnk0CF2(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];


            guid[7] = packet.ReadBit(); // 23
            guid2[0] = packet.ReadBit(); // 80
            guid2[7] = packet.ReadBit(); // 87
            guid[1] = packet.ReadBit(); // 17

            var count = packet.ReadBits("cnt", 21); // 64
            // 12*4 = count == 0
            guid[0] = packet.ReadBit(); // 16

            var unk1 = new Int32[count];
            var unk2 = new Int32[count];
            var unk3 = new Int32[count];
            var unk4 = new Int32[count];

            var hasUnk1 = new bool[count];
            var hasUnk2 = new bool[count];
            var hasUnk3 = new bool[count];
            var hasUnk4 = new bool[count];

            var cnt36 = 0u;

            for ( var i = 0; i < count; i++ )
            {
                hasUnk1[i] = !packet.ReadBit("not unk1", i); // ! 64*4+8
                hasUnk3[i] = !packet.ReadBit("not unk3", i); // ! 64*4+16
                packet.ReadBit("unk24", i); // 64+24
                hasUnk4[i] = !packet.ReadBit("not unk4", i); // ! 64*4+20
                hasUnk2[i] = !packet.ReadBit("not unk2", i); // ! 64*4+12
            }
            guid[5] = packet.ReadBit(); // 21
            guid[3] = packet.ReadBit(); // 19
            guid2[1] = packet.ReadBit(); // 81
            guid[2] = packet.ReadBit(); // 18
            guid2[6] = packet.ReadBit(); // 86
            guid2[3] = packet.ReadBit(); // 83
            guid2[4] = packet.ReadBit(); // 84
            var unk52 = packet.ReadBit("unk52"); // 52
            guid2[2] = packet.ReadBit(); // 82
            guid[6] = packet.ReadBit(); // 22
            guid2[5] = packet.ReadBit(); // 85

            if (unk52)
            {
                cnt36 = packet.ReadBits("cnt36", 21);
            }
            guid[4] = packet.ReadBit(); // 20

            for (var i = 0; i < count; i++)
            {
                if (hasUnk1[i])
                    unk1[i] = packet.ReadInt32("unk1", i);
                packet.ReadInt32("unk_4", i); // 4
                packet.ReadInt32("unk_64", i); // 64*4
                if (hasUnk4[i])
                    unk4[i] = packet.ReadInt32("unk4", i);
                if (hasUnk3[i])
                    unk3[i] = packet.ReadInt32("unk3", i);
                if (hasUnk2[i])
                    unk2[i] = packet.ReadInt32("unk2", i);
            }
            packet.ReadXORByte(guid2, 5); // 85
            packet.ReadXORByte(guid2, 3); // 83
            packet.ReadXORByte(guid, 4); // 20
            packet.ReadInt32("unk56");
            packet.ReadXORByte(guid, 6); // 22
            packet.ReadXORByte(guid2, 7); // 87
            packet.ReadXORByte(guid2, 1); // 81
            if (unk52)
            {
                for ( var i = 0; i < cnt36; i++ )
                {
                    packet.ReadInt32("unk44", i);
                    packet.ReadInt32("unk48", i);
                }
                packet.ReadInt32("unk28");
                packet.ReadInt32("unk24");
                packet.ReadInt32("unk32");
            }
            packet.ReadXORByte(guid, 5); // 21
            packet.ReadXORByte(guid2, 0); // 80
            packet.ReadXORByte(guid, 1); // 17
            packet.ReadXORByte(guid, 7); // 23
            packet.ReadXORByte(guid2, 4); // 84
            packet.ReadXORByte(guid, 3); // 19
            packet.ReadXORByte(guid2, 2); // 82
            packet.ReadXORByte(guid, 0); // 16
            packet.ReadXORByte(guid, 2); // 18
            packet.ReadXORByte(guid2, 6); // 86

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_0D51)]
        public static void HandleUnk0D51(Packet packet)
        {
            var count = packet.ReadBits("count", 22);
            for (var i = 0; i < count; i++)
                packet.ReadInt32("unk20", i);
        }

        [Parser(Opcode.SMSG_UNK_0D79)] // sub_C76759
        public static void HandleUnk0D79(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                var guid = new byte[8];
                var guid2 = new byte[8];

                guid[7] = packet.ReadBit(); // 63
                guid[3] = packet.ReadBit(); // 59
                guid2[1] = packet.ReadBit(); // 65
                guid[4] = packet.ReadBit(); // 60
                guid[2] = packet.ReadBit(); // 58
                guid2[3] = packet.ReadBit(); // 67
                guid[5] = packet.ReadBit(); // 61
                var unk44 = packet.ReadBit(); // 44
                guid2[7] = packet.ReadBit(); // 71
                guid2[0] = packet.ReadBit(); // 64
                guid2[2] = packet.ReadBit(); // 66

                var count28 = 0u;
                if (unk44)
                    count28 = packet.ReadBits("count28", 21);
                guid2[4] = packet.ReadBit(); // 68
                guid2[6] = packet.ReadBit(); // 70
                guid[6] = packet.ReadBit(); // 62
                guid[1] = packet.ReadBit(); // 57
                guid[0] = packet.ReadBit(); // 56
                guid2[5] = packet.ReadBit(); // 69

                packet.ReadXORByte(guid, 0); // 56
                packet.ReadXORByte(guid2, 5); // 69
                packet.ReadXORByte(guid, 6); // 62

                if (unk44)
                {
                    packet.ReadInt32("unk20");
                    for (var i = 0; i < count28; i++)
                    {
                        packet.ReadInt32("unk32", i);
                        packet.ReadInt32("unk36", i);
                    }
                    packet.ReadInt32("unk24");
                    packet.ReadInt32("unk16");
                }
                packet.ReadXORByte(guid2, 6); // 70
                packet.ReadXORByte(guid, 2); // 58
                packet.ReadXORByte(guid2, 0); // 64
                packet.ReadXORByte(guid, 1); // 57
                packet.ReadInt32("unk48");
                packet.ReadXORByte(guid, 4); // 60
                packet.ReadXORByte(guid2, 1); // 65
                packet.ReadXORByte(guid2, 7); // 71
                packet.ReadXORByte(guid, 5); // 61
                packet.ReadXORByte(guid2, 2); // 66
                packet.ReadXORByte(guid2, 3); // 67
                packet.ReadXORByte(guid, 7); // 63
                packet.ReadXORByte(guid2, 4); // 68
                packet.ReadXORByte(guid, 3); // 59

                packet.ReadInt32("unk72"); // 72
                packet.ReadInt32("unk52"); // 52

                packet.WriteGuid("Guid", guid);
                packet.WriteGuid("Guid2", guid2);
            }
            else
            {
                packet.WriteLine("              : CMSG_GUILD_DEL_RANK");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.SMSG_UNK_0E9B)]
        public static void HandleUnk0E9B(Packet packet)
        {
            var guid = packet.StartBitStream(4, 6, 2, 3, 7, 1, 5, 0);
            packet.ParseBitStream(guid, 3, 6, 2);
            packet.ReadUInt32("Int72");
            packet.ReadUInt32("Int76");
            packet.ParseBitStream(guid, 5, 1);
            packet.ReadInt32("Int28");
            packet.ParseBitStream(guid, 4);
            packet.ReadUInt32("Int24");
            packet.ReadUInt32("Int80");
            packet.ParseBitStream(guid, 7, 0);
            packet.WriteGuid("Guid", guid);
            packet.ReadUInt64("QW16");
        }

        [Parser(Opcode.SMSG_UNK_0EAB)]
        public static void HandleUnk0EAB(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.ReadInt32("unk24");
                packet.ReadSingle("unk32");
                packet.ReadSingle("unk28");
                packet.ReadInt32("unk16");
                packet.ReadSingle("unk");
            }
            else
            {
                packet.WriteLine("              : CMSG_MESSAGECHAT_AFK");
                packet.ReadToEnd();
            }
        }

        [Parser(Opcode.SMSG_UNK_108B)]
        public static void HandleUnk108B(Packet packet)
        {
            var guid = packet.StartBitStream(0, 2, 4, 1, 7, 3, 6, 5);
            packet.ParseBitStream(guid, 0, 2, 4, 5, 7, 3, 1, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_10F1)]
        public static void HandleUnk10F1(Packet packet)
        {
            var count = packet.ReadBits("count", 21);
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("unk24", i);
                packet.ReadByte("unk20", i);
                packet.ReadInt32("unk28", i);
            }
        }

        [Parser(Opcode.SMSG_UNK_10F2)]
        public static void HandleUnk10F2(Packet packet)
        {
            var count = packet.ReadBits("count", 22);
            var cnt = new uint[count];
            for (var i = 0; i < count; i++)
                cnt[i] = packet.ReadBits("cnt", 21, i);
            for (var i = 0; i < count; i++)
            {
                packet.ReadByte("unk20");
                for (var j = 0; j < cnt[i]; j++)
                {
                    packet.ReadSingle("unk28", i, j);
                    packet.ReadByte("unk32", i, j);
                }
            }
        }

        [Parser(Opcode.SMSG_UNK_10F9)]
        public static void HandleUnk10F9(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];
            var guid3 = new byte[8];

            var unk68 = false;
            var unk256 = false;
            var unk224 = false;

            guid[7] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var unk72 = packet.ReadBit("unk72");
            if (unk72)
            {
                guid2[6] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                guid2[0] = packet.ReadBit();
                unk68 = !packet.ReadBit("!unk68");
                guid2[3] = packet.ReadBit();
                guid2[7] = packet.ReadBit();
                guid2[5] = packet.ReadBit();
                guid2[1] = packet.ReadBit();
                guid2[2] = packet.ReadBit();
                unk256 = !packet.ReadBit("!unk256");
                unk224 = !packet.ReadBit("!unk224");
                guid3 = packet.StartBitStream(4, 5, 1, 7, 0, 2, 3, 6);
            }
            guid[3] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            var unk32 = packet.ReadBit("unk32");
            if (unk72)
            {
                packet.ParseBitStream(guid3, 4, 6, 1, 0, 7, 3, 2, 5);
                packet.WriteGuid("Guid3", guid3);
                if (unk68)
                    packet.ReadByte("unk68");
                packet.ParseBitStream(guid2, 4, 5, 1, 3);
                if (unk256)
                    packet.ReadInt32("unk256");
                packet.ParseBitStream(guid2, 6, 7, 2, 0);
                packet.WriteGuid("Guid2", guid2);
            }
            if (unk32)
            {
                packet.ReadInt32("unk112");
                packet.ReadInt32("unk96");
            }
            packet.ParseBitStream(guid, 6, 7, 3, 1, 0);
            packet.ReadInt32("unk160");
            packet.ParseBitStream(guid, 5, 4, 2);
            packet.ReadInt32("unk144");
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_11E3)]
        public static void HandleUnk111E3(Packet packet)
        {
            var guid = packet.StartBitStream(4, 2, 6, 5, 1, 3, 0, 7);
            packet.ParseBitStream(guid, 5, 7);
            packet.ReadInt32("unk24");
            packet.ParseBitStream(guid, 1, 0, 6);
            packet.ReadInt32("unk28");
            packet.ParseBitStream(guid, 4, 2, 3);
            packet.ReadInt32("unk32");

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_121E)]
        public static void HandleUnk121E(Packet packet)
        {
            packet.ReadBit("Bit in Byte16");
            packet.ReadBit("Bit in Byte18");
            packet.ReadBit("Bit in Byte17");
        }

        [Parser(Opcode.SMSG_UNK_1227)]
        public static void HandleUnk1227(Packet packet)
        {
            var count = packet.ReadBits("count", 24);
            var guid = new byte[count][];
            for (var i = 0; i < count; i++)
            {
                guid[i] = new byte[8];
                guid[i] = packet.StartBitStream(1, 6, 0, 4, 5, 7, 2, 3);
            }
            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid[i], 5, 2, 3, 0, 6, 1, 4, 7);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNK_129A)]
        public static void HandleUnk129A(Packet packet)
        {
            var count = packet.ReadBits("Count", 22);
            packet.ReadBit("Byte16");
            for (var i = 0; i < count; i++)
                packet.ReadUInt32("Dword24", i);
        }

        [Parser(Opcode.SMSG_UNK_129B)]
        public static void HandleUnk129B(Packet packet)
        {
            var hasData = packet.ReadBit("HasData");
            if (hasData)
            {
                var len1 = packet.ReadBits("len6", 11);
                var len2 = packet.ReadBits("len2042", 10);
                packet.ReadInt32("unk5");
                packet.ReadByte("unk2040");
                packet.ReadByte("unk2041");
                packet.ReadByte("unk2025");
                packet.ReadInt32("unk509");
                packet.ReadWoWString("str6", len1);
                packet.ReadInt32("unk767");
                packet.ReadInt32("unk507");
                packet.ReadInt32("unk508");
                packet.ReadWoWString("str2042", len2);
            }
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.SMSG_UNK_14AE)]
        public static void HandleUnk14AE(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid2[4] = packet.ReadBit();

            var count32 = packet.ReadBits("cnt32", 21);
            var guids = new byte[count32][];

            for (var i = 0; i < count32; i++)
            {
                guids[i] = new byte[8];
                guids[i][6] = packet.ReadBit();
                guids[i][1] = packet.ReadBit();
                guids[i][0] = packet.ReadBit();
                guids[i][2] = packet.ReadBit();
                guids[i][7] = packet.ReadBit();
                guids[i][4] = packet.ReadBit();
                guids[i][3] = packet.ReadBit();
                guids[i][5] = packet.ReadBit();
            }

            guid2[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid[2] = packet.ReadBit();

            packet.ReadXORByte(guid2, 4);

            for (var i = 0; i < count32; i++)
            {
                packet.ParseBitStream(guids[i], 6);
                packet.ReadInt32("unk8", i);
                packet.ParseBitStream(guids[i], 4, 0, 3, 5, 2, 1, 7);

                packet.WriteGuid("Guids", guids[i], i);
            }
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid, 5);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_1570)]
        public static void HandleUnk1570(Packet packet)
        {
            var guid = packet.StartBitStream(5, 1, 4, 0, 7, 3, 2, 6); // 32
            var count = packet.ReadBits("count", 23); // 16
            var guid2 = new byte[count][];
            var unk1 = new byte[count];
            for ( var i = 0; i < count; i++ )
            {
                guid2[i] = packet.StartBitStream(0, 1, 6, 2, 5, 3, 4, 7); // 20*4 + 24*i
                unk1[i] = packet.ReadBit("unk1", i);
            }
            for (var i = 0; i < count; i++)
            {
                packet.ReadByte("unk88", i); // 20*4+8
                packet.ParseBitStream(guid2[i], 7, 5, 0, 6, 3, 2);
                if (unk1[i]!=0)
                {
                    packet.ReadSingle("unks1", i);
                    packet.ReadSingle("unks2", i);
                }
                packet.ParseBitStream(guid2[i], 1, 4);

                packet.WriteGuid("Guid2", guid2[i], i);
            }
            packet.ParseBitStream(guid, 6, 4, 2, 0, 1);
            packet.ReadInt32("unk40"); // 40*4
            packet.ParseBitStream(guid, 3, 7, 5);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1613)]
        public static void HandleUnk1613(Packet packet)
        {
            var guid = packet.StartBitStream(3, 0, 4, 7, 2, 1, 6, 5);
            var count = packet.ReadBits("count", 19);
            var cnt = new uint[count];
            for (var i = 0; i < count; i++)
            {
                cnt[i] = packet.ReadBits("unk192", 8, i);
            }
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("unk144", i);
                packet.ReadInt32("unk176", i);
                packet.ReadByte("unk261", i);
                packet.ReadInt32("unk160", i);
                packet.ReadWoWString("str", cnt[i], i);
                packet.ReadInt32("unk128", i);
                packet.ReadInt32("unk112", i);
            }
            packet.ParseBitStream(guid, 3, 5, 7, 2, 0, 4, 1, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_16BF)]
        public static void HandleUnk16BF(Packet packet)
        {
            var count = packet.ReadBits("Count", 20); // 16
            var guid = new byte[count][];

            for ( var i = 0; i < count; i++ )
            {
                guid[i] = new byte[8];

                guid[i][1] = packet.ReadBit(); // 29
                guid[i][2] = packet.ReadBit(); // 30
                guid[i][6] = packet.ReadBit(); // 34
                packet.ReadBit("unk37", i); // 37
                guid[i][0] = packet.ReadBit(); // 28
                guid[i][5] = packet.ReadBit(); // 33
                guid[i][4] = packet.ReadBit(); // 32
                packet.ReadBit("unk36", i); // 36
                guid[i][7] = packet.ReadBit(); // 35
                guid[i][3] = packet.ReadBit(); // 31
            }
            for ( var i = 0; i < count; i++ )
            {
                packet.ParseBitStream(guid[i], 7, 6, 4, 2, 0);
                packet.ReadInt32("unk40", i); // 40
                packet.ReadInt32("unk44", i); // 44
                packet.ParseBitStream(guid[i], 1);
                packet.ReadInt32("unk20", i); // 20
                packet.ReadInt32("unk24", i); // 24
                packet.ParseBitStream(guid[i], 3, 5);

                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNK_180A)]
        public static void HandleUnk180A(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var guid = new byte[count][];
            var guid2 = new byte[count][];
            var cnt = new uint[count];
            for (var i = 0; i < count; i++)
            {
                guid[i] = new byte[8];
                guid2[i] = new byte[8];

                guid[i][3] = packet.ReadBit();
                guid2[i][3] = packet.ReadBit();
                guid2[i][6] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
                guid2[i][7] = packet.ReadBit();
                guid[i][1] = packet.ReadBit();
                guid[i][5] = packet.ReadBit();
                guid2[i][2] = packet.ReadBit();
                guid2[i][1] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                guid2[i][4] = packet.ReadBit();
                guid2[i][0] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid2[i][5] = packet.ReadBit();
                guid[i][4] = packet.ReadBit();
                cnt[i] = packet.ReadBits("cnt", 4, i);
                guid[i][6] = packet.ReadBit();
            }
            var cnt16 = packet.ReadBits("CNT16", 20);
            var guid3 = new byte[cnt16][];
            for (var i = 0; i < cnt16; i++)
            {
                guid3[i] = new byte[8];
                guid3[i] = packet.StartBitStream(0, 7, 1, 5, 2, 4, 6, 3);
            }
            for (var i = 0; i < cnt16; i++)
            {
                packet.ReadInt32("unk20", i);
                packet.ReadInt32("unk208", i);
                packet.ParseBitStream(guid3[i], 5, 7);
                packet.ReadInt32("unk212", i);
                packet.ReadInt32("unksub", i);
                packet.ParseBitStream(guid3[i], 0, 4, 1, 6, 2, 3);
                packet.WriteGuid("guid3", guid3[i], i);
            }
            for (var i = 0; i < count; i++)
            {
                packet.ParseBitStream(guid[i], 7);
                packet.ReadInt32("unk292", i);
                packet.ParseBitStream(guid[i], 6);
                packet.ParseBitStream(guid2[i], 1);
                packet.ReadInt32("unk36", i);
                packet.ParseBitStream(guid[i], 4);
                packet.ParseBitStream(guid2[i], 0, 4, 6);
                packet.ParseBitStream(guid[i], 1, 5);
                packet.ParseBitStream(guid2[i], 7, 2);
                packet.ParseBitStream(guid[i], 2, 0);
                packet.ParseBitStream(guid2[i], 3);
                packet.ReadInt32("unk236", i);
                packet.ParseBitStream(guid[i], 3);
                packet.ParseBitStream(guid2[i], 5);

                packet.ReadInt32("unkSub", i);

                packet.WriteGuid("Guid", guid[i], i);
                packet.WriteGuid("Guid2", guid2[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNK_188F)]
        public static void HandleUnk188F(Packet packet)
        {
            packet.ReadBits("unk16", 2);
            packet.ReadInt32("unk20");
        }

        [Parser(Opcode.SMSG_UNK_189E)]
        public static void HandleUnk189E(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[4] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid[2] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid[0] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            var unk40 = packet.ReadBits("unk40", 4);
            guid2[5] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid2[0] = packet.ReadBit();

            packet.ReadXORByte(guid2, 7);

            packet.ReadInt32("unk80");
            packet.ReadInt32("unk16");

            packet.ReadXORByte(guid, 7);

            packet.ReadInt32("unk76");

            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid2, 3);
            packet.ReadInt32("unk");
            packet.ReadXORByte(guid, 0);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 6);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_18BA)]
        public static void HandleUnk18BA(Packet packet)
        {
            packet.ReadInt32("unk20");
            packet.ReadBits("unk16", 2);
        }

        [Parser(Opcode.SMSG_UNK_18C3)]
        public static void HandleUnk18C3(Packet packet)
        {
            var guid = new byte[8];
            var guid2 = new byte[8];

            guid[2] = packet.ReadBit();
            guid2[2] = packet.ReadBit();
            guid[4] = packet.ReadBit();
            guid[3] = packet.ReadBit();
            guid2[4] = packet.ReadBit();
            guid2[0] = packet.ReadBit();
            guid[7] = packet.ReadBit();
            guid2[6] = packet.ReadBit();
            guid2[7] = packet.ReadBit();
            guid[6] = packet.ReadBit();
            guid[5] = packet.ReadBit();
            guid2[1] = packet.ReadBit();
            guid[1] = packet.ReadBit();
            guid2[5] = packet.ReadBit();
            guid2[3] = packet.ReadBit();
            guid[0] = packet.ReadBit();

            packet.ReadXORByte(guid, 6);
            packet.ReadXORByte(guid2, 4);
            packet.ReadXORByte(guid, 4);
            packet.ReadXORByte(guid, 5);
            packet.ReadXORByte(guid, 3);
            packet.ReadXORByte(guid, 2);
            packet.ReadXORByte(guid, 1);
            packet.ReadXORByte(guid2, 1);
            packet.ReadXORByte(guid2, 0);
            packet.ReadXORByte(guid2, 3);
            packet.ReadXORByte(guid2, 5);
            packet.ReadXORByte(guid2, 7);
            packet.ReadXORByte(guid2, 6);
            packet.ReadXORByte(guid, 7);
            packet.ReadXORByte(guid2, 2);
            packet.ReadXORByte(guid, 0);

            packet.WriteGuid("Guid", guid);
            packet.WriteGuid("Guid2", guid2);
        }

        [Parser(Opcode.SMSG_UNK_18E2)]
        public static void HandleUnk18E2(Packet packet)
        {
            var count = packet.ReadBits("count", 19);
            var guid = new byte[count][];
            var guid2 = new byte[count][][];
            var len68 = new uint[count];
            var len580 = new uint[count];
            for (var i = 0; i < count; i++)
            {
                guid[i] = new byte[8];

                guid[i][4] = packet.ReadBit("unk", i);
                guid2[i] = new byte[20][];
                for (var j = 0; j < 19; j++)
                {
                    guid2[i][j] = new byte[8];
                    guid2[i][j] = packet.StartBitStream(3, 5, 7, 2, 6, 0, 4, 1);
                }
                guid[i][5] = packet.ReadBit();
                len580[i] = packet.ReadBits("unk580", 9, i);
                guid[i][1] = packet.ReadBit();
                guid[i][7] = packet.ReadBit();
                len68[i] = packet.ReadBits("unk68", 8, i);
                guid[i][3] = packet.ReadBit();
                guid[i][2] = packet.ReadBit();
                guid[i][6] = packet.ReadBit();
                guid[i][0] = packet.ReadBit();
            }
            for (var i = 0; i < count; i++)
            {
                for (var j = 0; j < 19; j++)
                {
                    packet.ParseBitStream(guid2[i][j], 2, 3, 7, 1, 6, 5, 0, 4);
                    packet.WriteGuid("Guid2", guid2[i][j], i, j);
                }
                packet.ParseBitStream(guid[i], 7);
                packet.ReadInt32("unk52", i);
                packet.ReadWoWString("str", len68[i], i);
                packet.ParseBitStream(guid[i], 2, 6, 0, 3, 1, 5, 4);
                packet.ReadWoWString("str2", len580[i], i);
                packet.WriteGuid("Guid", guid[i], i);
            }
        }

        [Parser(Opcode.SMSG_UNK_1E0F)]
        public static void HandleUnk1E0F(Packet packet)
        {
            var guid = packet.StartBitStream(1, 3, 0, 4, 6, 7, 5, 2);
            packet.ParseBitStream(guid, 7, 6, 2, 5, 0, 4, 1, 3);

            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1E12)]
        public static void HandleUnk1E12(Packet packet)
        {
            var guid = packet.StartBitStream(5, 4, 1, 3, 0, 2, 6, 7);
            packet.ParseBitStream(guid, 0, 1, 3, 7, 2, 4, 5);
            packet.ReadInt16("unk48");
            packet.ParseBitStream(guid, 6);
            packet.WriteGuid("Guid", guid);
        }

        [Parser(Opcode.SMSG_UNK_1E9B)]
        public static void HandleUnk1E9B(Packet packet)
        {
            packet.ReadUInt32("Dword8");
            packet.ReadUInt32("Dword5");
            packet.ReadUInt32("Dword6");
            packet.ReadUInt32("Dword7");
            packet.ReadBit("Bit in Byte16");
        }

        [Parser(Opcode.CMSG_UNK_10A2)]
        public static void HandleUnk10A2(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.ReadInt32("unk");
            }
            else
            {
                packet.WriteLine("              : SMSG_UNK_10A2");
                var guid = new byte[8];
                var guid2 = new byte[8];

                guid[4] = packet.ReadBit();
                guid[0] = packet.ReadBit();
                guid2[3] = packet.ReadBit();
                guid[3] = packet.ReadBit();
                guid2[2] = packet.ReadBit();
                guid2[6] = packet.ReadBit();
                guid2[7] = packet.ReadBit();
                guid[1] = packet.ReadBit();
                guid2[4] = packet.ReadBit();
                guid[6] = packet.ReadBit();
                guid[5] = packet.ReadBit();
                guid2[0] = packet.ReadBit();
                guid[2] = packet.ReadBit();
                guid2[5] = packet.ReadBit();
                guid2[1] = packet.ReadBit();
                guid[7] = packet.ReadBit();

                packet.ReadInt32("unk32");

                packet.ReadXORByte(guid2, 4);
                packet.ReadXORByte(guid2, 2);
                packet.ReadXORByte(guid, 5);
                packet.ReadXORByte(guid, 4);
                packet.ReadXORByte(guid2, 6);
                packet.ReadXORByte(guid, 1);
                packet.ReadXORByte(guid2, 0);
                packet.ReadXORByte(guid2, 1);
                packet.ReadXORByte(guid, 6);
                packet.ReadXORByte(guid, 2);
                packet.ReadXORByte(guid2, 7);
                packet.ReadXORByte(guid, 0);
                packet.ReadXORByte(guid, 3);
                packet.ReadXORByte(guid, 7);
                packet.ReadXORByte(guid2, 3);
                packet.ReadXORByte(guid2, 5);

                packet.ReadInt32("unk36");

                packet.WriteGuid("Guid", guid);
                packet.WriteGuid("Guid2", guid2);
            }
        }

        [Parser(Opcode.CMSG_UNK_0247)]
        [Parser(Opcode.CMSG_UNK_044E)]
        [Parser(Opcode.CMSG_UNK_0656)]
        [Parser(Opcode.CMSG_UNK_08C0)]
        [Parser(Opcode.CMSG_UNK_1446)]
        [Parser(Opcode.CMSG_UNK_144D)]
        public static void HandleUnk1446(Packet packet)
        {
            packet.ReadInt32("unk");
        }

        [Parser(Opcode.CMSG_UNK_14E3)]
        public static void HandleUnk14E3(Packet packet)
        {
            packet.ReadInt64("unk");
        }

        [Parser(Opcode.CMSG_UNK_03E4)]
        public static void HandleUnk03E4(Packet packet)
        {
            packet.ReadInt32("unk1");
            packet.ReadInt32("unk2");
        }

        [Parser(Opcode.CMSG_ATTACKSTOP)]
        [Parser(Opcode.CMSG_GET_TIMEZONE_INFORMATION)]
        [Parser(Opcode.CMSG_WORLD_STATE_UI_TIMER_UPDATE)]
        [Parser(Opcode.MSG_MOVE_WORLDPORT_ACK)]  //0
        [Parser(Opcode.CMSG_NULL_0023)]
        [Parser(Opcode.CMSG_NULL_0060)]
        [Parser(Opcode.CMSG_NULL_0141)]
        [Parser(Opcode.CMSG_NULL_01C0)]
        [Parser(Opcode.CMSG_NULL_029F)]
        [Parser(Opcode.CMSG_NULL_02D6)]
        [Parser(Opcode.CMSG_NULL_02DA)]
        [Parser(Opcode.CMSG_NULL_032D)]
        [Parser(Opcode.CMSG_NULL_033D)]
        [Parser(Opcode.CMSG_NULL_0365)]
        [Parser(Opcode.CMSG_NULL_0374)]
        [Parser(Opcode.CMSG_NULL_03C4)]
        [Parser(Opcode.CMSG_NULL_0558)]
        [Parser(Opcode.CMSG_NULL_05E1)]
        [Parser(Opcode.CMSG_NULL_0640)]
        [Parser(Opcode.CMSG_NULL_0644)]
        [Parser(Opcode.CMSG_NULL_06D4)]
        [Parser(Opcode.CMSG_NULL_06E4)]
        [Parser(Opcode.CMSG_NULL_06F5)]
        [Parser(Opcode.CMSG_NULL_077B)]
        [Parser(Opcode.CMSG_NULL_0813)]
        [Parser(Opcode.CMSG_NULL_0826)]
        [Parser(Opcode.CMSG_NULL_0A22)]
        [Parser(Opcode.CMSG_NULL_0A23)]
        [Parser(Opcode.CMSG_NULL_0A82)]
        [Parser(Opcode.CMSG_NULL_0A87)]
        [Parser(Opcode.CMSG_NULL_0C62)]
        [Parser(Opcode.CMSG_NULL_0DE0)]
        [Parser(Opcode.CMSG_NULL_1124)]
        [Parser(Opcode.CMSG_NULL_1203)]
        [Parser(Opcode.CMSG_NULL_1207)]
        [Parser(Opcode.CMSG_NULL_1272)]
        [Parser(Opcode.CMSG_NULL_135B)]
        [Parser(Opcode.CMSG_NULL_1452)]
        [Parser(Opcode.CMSG_NULL_147B)]
        [Parser(Opcode.CMSG_NULL_14DB)]
        [Parser(Opcode.CMSG_NULL_14E0)]
        [Parser(Opcode.CMSG_NULL_15A8)]
        [Parser(Opcode.CMSG_NULL_15E2)]
        [Parser(Opcode.CMSG_NULL_18A2)]
        [Parser(Opcode.CMSG_NULL_1A23)]
        [Parser(Opcode.CMSG_NULL_1A87)]
        [Parser(Opcode.CMSG_NULL_1C45)]
        [Parser(Opcode.CMSG_NULL_1C5A)]
        [Parser(Opcode.CMSG_NULL_1CE3)]
        [Parser(Opcode.CMSG_NULL_1D61)]
        [Parser(Opcode.CMSG_NULL_1DC3)]
        [Parser(Opcode.CMSG_NULL_1F34)]
        [Parser(Opcode.CMSG_NULL_1F89)]
        [Parser(Opcode.CMSG_NULL_1F8E)]
        [Parser(Opcode.CMSG_NULL_1F9E)]
        [Parser(Opcode.CMSG_NULL_1FBE)]
        public static void HandleCNullMisc(Packet packet)
        {
            if (packet.Direction == Direction.ServerToClient)
            {
                packet.WriteLine("              : SMSG_???");
                packet.ReadToEnd();
            }
            else
            {
                //packet.ReadToEnd();
            }
        }

        [Parser(Opcode.SMSG_NULL_04BB)]
        [Parser(Opcode.SMSG_NULL_0C59)]
        [Parser(Opcode.SMSG_NULL_0C9A)]
        [Parser(Opcode.SMSG_NULL_0E2B)]
        [Parser(Opcode.SMSG_NULL_0E8B)]
        [Parser(Opcode.SMSG_NULL_0FE1)]
        [Parser(Opcode.SMSG_NULL_141B)]
        [Parser(Opcode.SMSG_NULL_1A2A)]
        public static void HandleSNullMisc(Packet packet)
        {
            if (packet.Direction == Direction.ClientToServer)
            {
                packet.WriteLine("              : CMSG_???");
                packet.ReadToEnd();
            }
            else
            {
                //packet.ReadToEnd();
            }
        }
    }
}
