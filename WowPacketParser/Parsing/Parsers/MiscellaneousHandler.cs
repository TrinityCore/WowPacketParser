using System;
using WowPacketParser.Enums;
using WowPacketParser.Enums.Version;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class MiscellaneousParsers
    {
        [Parser(Opcode.SMSG_HOTFIX_NOTIFY)]
        public static void HandleHotfixNotify(Packet packet)
        {
            packet.ReadInt32("Unk int32");
            packet.ReadUInt32("Unk int32");
            packet.ReadUInt32("Unk int32");
        }

        [Parser(Opcode.SMSG_HOTFIX_INFO)]
        public static void HandleHotfixInfo(Packet packet)
        {
            var count = packet.ReadInt32("Count");
            for (var i = 0; i < count; i++)
            {
                packet.ReadInt32("Hotfix type"); // Also time?
                packet.ReadTime("Hotfix date");
                packet.ReadInt32("Hotfixed entry");
            }
        }

        [Parser(Opcode.TEST_430_SYNC_PLAYER_MOVE)]
        public static void HandleUnk5(Packet packet)
        {
            packet.ReadVector4("Position");
        }

        [Parser(Opcode.CMSG_SUSPEND_TOKEN)]
        public static void HandleSuspendToken(Packet packet)
        {
            packet.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_SUSPEND_TOKEN_RESPONSE)]
        public static void HandleSuspendTokenResponse(Packet packet)
        {
            packet.ReadBit("Unk");
            packet.ReadUInt32("Count");
        }

        [Parser(Opcode.SMSG_COMPRESSED_MULTIPLE_PACKETS)]
        public static void HandleCompressedMultiplePackets(Packet packet)
        {
            using (var packet2 = packet.Inflate(packet.ReadInt32()))
                 HandleMultiplePackets(packet2);
         }

        [Parser(Opcode.SMSG_MULTIPLE_PACKETS)]
        public static void HandleMultiplePackets(Packet packet)
        {
            // Testing: packet.WriteLine(packet.AsHex());
            packet.WriteLine("{");
            var i = 0;
            while (packet.CanRead())
            {
                var opcode = 0;
                var len = 0;
                byte[] bytes = null;
                if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                {
                    opcode = packet.ReadUInt16();
                    // Why are there so many 0s in some packets? Should we have some check if opcode == 0 here?
                    len = packet.ReadUInt16();
                    bytes = packet.ReadBytes(len);
                }
                else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                {
                    len = packet.ReadUInt16();
                    opcode = packet.ReadUInt16();
                    bytes = packet.ReadBytes(len - 2);
                }

                if (bytes == null || len == 0)
                    continue;

                if (i > 0)
                    packet.WriteLine();

                packet.Write("[{0}] ", i++);

                using (var newpacket = new Packet(bytes, opcode, packet.Time, packet.Direction, packet.Number, packet.Writer, packet.SniffFileInfo))
                    Handler.Parse(newpacket, isMultiple: true);
                //newpacket.DisposePacket();
            }
            packet.WriteLine("}");
        }

        [Parser(Opcode.SMSG_MULTIPLE_PACKETS_2)]
        public static void HandleMultiplePackets2(Packet packet)
        {
            // Testing: packet.WriteLine(packet.AsHex());
            packet.WriteLine("{");
            var i = 0;
            while (packet.CanRead())
            {
                packet.Opcode = packet.ReadUInt16();

                if (i > 0)
                    packet.WriteLine();

                packet.Write("[{0}] ", i++);

                Handler.Parse(packet, isMultiple: true);
            }
            packet.WriteLine("}");
        }

        [Parser(Opcode.SMSG_STOP_DANCE)]
        [Parser(Opcode.SMSG_INVALIDATE_PLAYER)]
        [Parser(Opcode.CMSG_SET_SELECTION)]
        [Parser(Opcode.CMSG_INSPECT)]
        [Parser(Opcode.CMSG_BUY_BANK_SLOT)]
        [Parser(Opcode.CMSG_DEL_FRIEND)]
        [Parser(Opcode.CMSG_DEL_IGNORE)]
        [Parser(Opcode.CMSG_DUEL_ACCEPTED)]
        [Parser(Opcode.CMSG_DUEL_CANCELLED)]
        [Parser(Opcode.SMSG_REFER_A_FRIEND_EXPIRED)]
        [Parser(Opcode.CMSG_PLAYER_VEHICLE_ENTER)]
        public static void HandleReadGuid(Packet packet)
        {
            packet.ReadGuid("GUID");
        }

        [Parser(Opcode.CMSG_GRANT_LEVEL)]
        [Parser(Opcode.CMSG_ACCEPT_LEVEL_GRANT)]
        [Parser(Opcode.SMSG_PROPOSE_LEVEL_GRANT)]
        public static void HandleGrantLevel(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.CMSG_REQUEST_VEHICLE_SWITCH_SEAT)]
        public static void HandleRequestVehicleSwitchSeat(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadByte("Seat");
        }

        [Parser(Opcode.CMSG_CHANGE_SEATS_ON_CONTROLLED_VEHICLE)]
        public static void HandleChangeSeatsOnControlledVehicle(Packet packet)
        {
            packet.ReadPackedGuid("Vehicle GUID");
            // FIXME: 3.3.3a has some data here
            packet.ReadPackedGuid("Accesory GUID");
            packet.ReadByte("Seat");
        }

        [Parser(Opcode.SMSG_CROSSED_INEBRIATION_THRESHOLD)]
        public static void HandleCrossedInerbriationThreshold(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadEnum<DrunkenState>("Drunken State", TypeCode.UInt32);
            packet.ReadEntryWithName<UInt32>(StoreNameType.Item, "Entry");
        }

        [Parser(Opcode.SMSG_BUY_BANK_SLOT_RESULT)]
        public static void HandleBuyBankSlotResult(Packet packet)
        {
            packet.ReadEnum<BankSlotResult>("Result", TypeCode.UInt32);
        }

        [Parser(Opcode.CMSG_ADD_FRIEND)]
        public static void HandleAddFriend(Packet packet)
        {
            packet.ReadCString("Name");
            packet.ReadCString("Note");
        }

        [Parser(Opcode.CMSG_ADD_IGNORE)]
        public static void HandleAddIgnore(Packet packet)
        {
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_SET_CONTACT_NOTES)]
        public static void HandleSetContactNotes(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_BUG)]
        public static void HandleBug(Packet packet)
        {
            packet.ReadUInt32("Suggestion");
            packet.ReadUInt32("Content Lenght");
            packet.ReadCString("Content");
            packet.ReadUInt32("Text Lenght");
            packet.ReadCString("Text");
        }

        [Parser(Opcode.CMSG_SET_ACTION_BUTTON)]
        public static void HandleActionButton(Packet packet)
        {
            packet.ReadByte("Button");
            var data = packet.ReadInt32();
            var type = (ActionButtonType)((data & 0xFF000000) >> 24);
            var action = (data & 0x00FFFFFF);
            packet.WriteLine("Type: " + type + " ID: " + action);
        }

        [Parser(Opcode.SMSG_RESURRECT_REQUEST)]
        public static void HandleResurrectRequest(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadUInt32("Name length");
            packet.ReadCString("Resurrector Name");
            // FIXME: TC states this is "Null terminator" and "Affected by Resurrection sickness?" but i'm not sure
            // All packets have 0 on first one and 95% of second = false. Leaving as Unk till further test
            packet.ReadByte("Unk byte 1");
            packet.ReadByte("Unk byte 2");
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadEntryWithName<Int32>(StoreNameType.Spell, "Spell ID");   // Used only for: <if (Spell ID == 83968 && Unit_HasAura(95223) return 1;>
        }

        [Parser(Opcode.CMSG_RESURRECT_RESPONSE)]
        public static void HandleResurrectResponse(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadBoolean("Accept");
        }

        [Parser(Opcode.CMSG_REPOP_REQUEST)]
        public static void HandleRepopRequest(Packet packet)
        {
            packet.ReadBoolean("Accept");
        }

        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadBoolean("Unk bool");
            packet.ReadBoolean("Enable Voice Chat");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadInt32("Complain System Status");
            else if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_0_14333))
            {
                packet.ReadByte("Complain System Status");
                packet.ReadInt32("Unknown Mail Url Related Value");
            }
        }

        [Parser(Opcode.CMSG_REALM_SPLIT)]
        public static void HandleClientRealmSplit(Packet packet)
        {
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_3_15354))
            {
                var lengt = packet.ReadInt32("String Lenght");
                packet.ReadWoWString("RealmList:",lengt);
            }
            else
            {
                packet.ReadInt32("Unk Int32");
                packet.ReadEnum<RealmSplitState>("Split State", TypeCode.Int32);
                packet.ReadCString("Unk String");
            }
           
        }

        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing(Packet packet)
        {
            packet.ReadInt32("Ping");
            packet.ReadInt32("Ping Count");
        }

        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong(Packet packet)
        {
            packet.ReadInt32("Ping");
        }

        [Parser(Opcode.SMSG_CLIENTCACHE_VERSION)]
        public static void HandleClientCacheVersion(Packet packet)
        {
            packet.ReadInt32("Version");
        }

        [Parser(Opcode.CMSG_MOVE_TIME_SKIPPED)]
        public static void HandleMoveTimeSkipped(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadInt32("Time");
        }

        [Parser(Opcode.SMSG_TIME_SYNC_REQ)]
        public static void HandleTimeSyncReq(Packet packet)
        {
            packet.ReadInt32("Count");
        }

        [Parser(Opcode.SMSG_LEARNED_DANCE_MOVES)]
        public static void HandleLearnedDanceMoves(Packet packet)
        {
            packet.ReadInt32("Dance Move Id");
            packet.ReadInt32("Unk int");
        }

        [Parser(Opcode.SMSG_TRIGGER_CINEMATIC)]
        [Parser(Opcode.SMSG_TRIGGER_MOVIE)]
        public static void HandleTriggerSequence(Packet packet)
        {
            packet.ReadInt32("Sequence Id");
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        [Parser(Opcode.SMSG_PLAY_MUSIC)]
        [Parser(Opcode.SMSG_PLAY_OBJECT_SOUND)]
        public static void HandleSoundMessages(Packet packet)
        {
            packet.ReadInt32("Sound Id");

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_3_0_15005))
                packet.ReadGuid("GUID");

            if (packet.Opcode == Opcodes.GetOpcode(Opcode.SMSG_PLAY_OBJECT_SOUND))
                packet.ReadGuid("GUID 2");
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            packet.ReadEnum<WeatherState>("State", TypeCode.Int32);
            packet.ReadSingle("Grade");
            packet.ReadByte("Unk Byte"); // Type
        }

        [Parser(Opcode.SMSG_LEVELUP_INFO)]
        public static void HandleLevelUp(Packet packet)
        {
            var level = packet.ReadInt32("Level");
            packet.ReadInt32("Health");

            var powerCount = 5;
            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing))
                powerCount = 7;
            if (ClientVersion.AddedInVersion(ClientType.Cataclysm))
                powerCount = 5;

            // TODO: Exclude happiness on Cata
            for (var i = 0; i < powerCount; i++)
                packet.WriteLine("Power " + (PowerType)i + ": " + packet.ReadInt32());

            for (var i = 0; i < 5; i++)
                packet.WriteLine("Stat " + (StatType)i + ": " + packet.ReadInt32());

            if (SessionHandler.LoggedInCharacter != null)
                SessionHandler.LoggedInCharacter.Level = level;
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG)]
        public static void HandleTutorialFlag(Packet packet)
        {
            var flag = packet.ReadInt32();
            packet.WriteLine("Flag: 0x" + flag.ToString("X8"));
        }

        [Parser(Opcode.SMSG_TUTORIAL_FLAGS)]
        public static void HandleTutorialFlags(Packet packet)
        {
            for (var i = 0; i < 8; i++)
            {
                var flag = packet.ReadInt32();
                packet.WriteLine("Flags " + i + ": 0x" + flag.ToString("X8"));
            }
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            packet.ReadInt32("Area Trigger Id");
        }

        [Parser(Opcode.SMSG_PRE_RESURRECT)]
        public static void HandlePreResurrect(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
        }

        [Parser(Opcode.SMSG_FORCE_ANIM)]
        public static void HandleForceAnimation(Packet packet) // It's still unknown until confirmed.
        {
            packet.ReadGuid("GUID");
            packet.ReadCString("Unk String");
        }

        [Parser(Opcode.SMSG_SUSPEND_COMMS)]
        [Parser(Opcode.CMSG_SUSPEND_COMMS_ACK)]
        public static void HandleSuspendCommsPackets(Packet packet)
        {
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.CMSG_SET_ALLOW_LOW_LEVEL_RAID1)]
        [Parser(Opcode.CMSG_SET_ALLOW_LOW_LEVEL_RAID2)]
        public static void HandleLowLevelRaidPackets(Packet packet)
        {
            packet.ReadBoolean("Allow");
        }

        [Parser(Opcode.SMSG_EXPLORATION_EXPERIENCE)]
        public static void HandleExplorationExperience(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Area, "Area ID");
            packet.ReadUInt32("Experience");
        }

        [Parser(Opcode.SMSG_START_MIRROR_TIMER)]
        public static void HandleStartMirrorTimer(Packet packet)
        {
            packet.ReadEnum<MirrorTimerType>("Timer Type", TypeCode.UInt32);
            packet.ReadUInt32("Current Value");
            packet.ReadUInt32("Max Value");
            packet.ReadInt32("Regen");
            packet.ReadByte("Unk Byte");
            packet.ReadUInt32("Spell Id");
        }

        [Parser(Opcode.SMSG_STOP_MIRROR_TIMER)]
        public static void HandleStopMirrorTimer(Packet packet)
        {
            packet.ReadEnum<MirrorTimerType>("Timer Type", TypeCode.UInt32);
        }

        [Parser(Opcode.SMSG_DEATH_RELEASE_LOC)]
        public static void HandleDeathReleaseLoc(Packet packet)
        {
            packet.ReadEntryWithName<Int32>(StoreNameType.Map, "Map Id");
            packet.ReadVector3("Position");
        }

        [Parser(Opcode.CMSG_ZONEUPDATE)]
        [Parser(Opcode.SMSG_ZONE_UNDER_ATTACK)]
        public static void HandleZoneUpdate(Packet packet)
        {
            packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id");
        }

        [Parser(Opcode.SMSG_HEALTH_UPDATE)]
        public static void HandleHealthUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");
            packet.ReadUInt32("Value");
        }

        [Parser(Opcode.SMSG_POWER_UPDATE)]
        public static void HandlePowerUpdate(Packet packet)
        {
            packet.ReadPackedGuid("GUID");

            var count = 1;

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_0_1_13164))
                count = packet.ReadInt32("Count");

            for (var i = 0; i < count; i++)
            {
                packet.ReadEnum<PowerType>("Power type", TypeCode.Byte); // Actually powertype for class
                packet.ReadInt32("Value");
            }
        }

        [Parser(Opcode.CMSG_SET_ACTIONBAR_TOGGLES)]
        public static void HandleSetActionBarToggles(Packet packet)
        {
            packet.ReadByte("Action Bar");
        }

        [Parser(Opcode.CMSG_PLAY_DANCE)]
        public static void HandleClientPlayDance(Packet packet)
        {
            packet.ReadInt32("Unk int32 1");
            packet.ReadInt32("Unk int32 2");
            packet.ReadInt32("Unk int32 3");
        }

        /*
        [Parser(Opcode.SMSG_NOTIFY_DANCE)]
        public static void HandleNotifyDance(Packet packet)
        {
            var flag = packet.ReadEnum<>("Flag", TypeCode.Int32);

            if (flag & 0x8)
            {
                var unk4 = packet.ReadInt32();
                if (unk4 == 1)
                    packet.WriteLine("Error msg = ERR_DANCE_SAVE_FAILED");
                else if (unk4 == 2)
                    packet.WriteLine("Error msg = ERR_DANCE_DELETE_FAILED");
                else if (unk4 == 0)
                    packet.WriteLine("Error msg = ERR_DANCE_CREATE_DUPLICATE");
            }
            else
            {
                packet.ReadInt32("Unk int 1");
                packet.ReadCString("Unk string");
                packet.ReadInt32("Unk int 2");
            }
        }
        */

        [Parser(Opcode.SMSG_PLAY_DANCE)]
        public static void HandleServerPlayDance(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadInt32("Unk int32 1");
            packet.ReadInt32("Unk int32 2");
            packet.ReadInt32("Unk int32 3");
            packet.ReadInt32("Unk int32 4");
        }

        [Parser(Opcode.CMSG_WHO)]
        public static void HandleWhoRequest(Packet packet)
        {
            packet.ReadInt32("Min Level");
            packet.ReadInt32("Max Level");
            packet.ReadCString("Player Name");
            packet.ReadCString("Guild Name");
            packet.ReadInt32("RaceMask");
            packet.ReadInt32("ClassMask");

            var zones = packet.ReadUInt32("Zones count");
            for (var i = 0; i < zones; ++i)
                packet.ReadUInt32("Zone Id", i);

            var patterns = packet.ReadUInt32("Pattern count");
            for (var i = 0; i < patterns; ++i)
                packet.ReadCString("Pattern", i);
        }

        [Parser(Opcode.SMSG_WHO)]
        public static void HandleWho(Packet packet)
        {
            var counter = packet.ReadUInt32("List count");
            packet.ReadUInt32("Online count");

            if (counter == 0)
                return;

            for (var i = 0; i < counter; ++i)
            {
                packet.ReadCString("Name");
                packet.ReadCString("Guild");
                packet.ReadUInt32("Level");
                packet.ReadEnum<Class>("Class", TypeCode.UInt32);
                packet.ReadEnum<Race>("Race", TypeCode.UInt32);
                packet.ReadEnum<Gender>("Gender", TypeCode.Byte);
                packet.ReadEntryWithName<UInt32>(StoreNameType.Zone, "Zone Id");
            }
        }

        [Parser(Opcode.CMSG_TIME_SYNC_RESP)]
        public static void HandleTimeSyncResp(Packet packet)
        {
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // no idea when this was added exactly
            {
                packet.ReadUInt32("Ticks");
                packet.ReadUInt32("Counter");
            }
            else
            {
                packet.ReadUInt32("Counter");
                packet.ReadUInt32("Ticks");
            }
        }

        [Parser(Opcode.SMSG_GAMETIME_SET)]
        public static void HandleGametimeSet(Packet packet)
        {
            packet.ReadUInt32("Unk time");
            packet.ReadUInt32("Unk int32");
        }

        [Parser(Opcode.SMSG_GAMETIME_UPDATE)]
        public static void HandleGametimeUpdate(Packet packet)
        {
            packet.ReadUInt32("Unk time"); // Time online?

            if (ClientVersion.AddedInVersion(ClientType.WrathOfTheLichKing)) // no idea when this was added exactly, doesn't exist in 2.4.0
                packet.ReadUInt32("Unk int32");
        }

        [Parser(Opcode.CMSG_FAR_SIGHT)]
        [Parser(Opcode.SMSG_PLAYER_SKINNED)]
        public static void HandleFarSight(Packet packet)
        {
            packet.ReadBoolean("Apply");
        }

        [Parser(Opcode.SMSG_SERVER_MESSAGE)]
        public static void HandleServerMessage(Packet packet)
        {
            packet.ReadUInt32("Server Message DBC Id");
            packet.ReadCString("Message");
        }

        [Parser(Opcode.MSG_INSPECT_HONOR_STATS)]
        public static void HandleInspectHonorStats(Packet packet)
        {
            packet.ReadGuid("GUID");
            if (packet.Direction == Direction.ClientToServer)
                return;

            packet.ReadByte("Honor Points");
            packet.ReadUInt32("Kills");
            packet.ReadUInt32("Today");
            packet.ReadUInt32("Yesterday");
            packet.ReadUInt32("Life Time Kills");
        }

        [Parser(Opcode.CMSG_LOAD_SCREEN)] // Also named CMSG_LOADING_SCREEN_NOTIFY
        public static void HandleClientEnterWorld(Packet packet)
        {
            packet.WriteLine("Loading: " + (packet.ReadBit() ? "true" : "false")); // Not sure on the meaning
            var mapId = packet.ReadEntryWithName<UInt32>(StoreNameType.Map, "Map");
            MovementHandler.CurrentMapId = (uint) mapId;

            if (mapId >= 0 && mapId < 1000) // Getting some weird results in a couple of packets
                packet.AddSniffData(StoreNameType.Map, mapId, "LOAD_SCREEN");
        }

        [Parser(Opcode.MSG_VERIFY_CONNECTIVITY)]
        public static void HandleServerInfo(Packet packet)
        {
            packet.ReadCString("String");
        }

        [Parser(Opcode.SMSG_OVERRIDE_LIGHT)]
        public static void HandleOverrideLight(Packet packet)
        {
            packet.ReadUInt32("Current Light.dbc entry");
            packet.ReadUInt32("Target Light.dbc entry");
            packet.ReadUInt32("Fade in time");
        }

        [Parser(Opcode.SMSG_PROPOSE_LEVEL_GRANT)]
        public static void HandleProposeLevelGrant(Packet packet)
        {
            packet.ReadPackedGuid("Guid");
        }

        [Parser(Opcode.SMSG_DURABILITY_DAMAGE_DEATH)]
        public static void HandleDurabilityDamageDeath(Packet packet)
        {
            // Confirm
            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545))
                packet.ReadInt32("Durability");
        }

        [Parser(Opcode.SMSG_CAMERA_SHAKE)]
        public static void HandleCameraShake(Packet packet)
        {
            packet.ReadInt32("SpellEffectCameraShakes"); // index from dbc
            packet.ReadInt32("Unknown"); // Sound related
        }

        [Parser(Opcode.SMSG_COMPLAIN_RESULT)]
        public static void HandleComplainResult(Packet packet)
        {
            packet.ReadByte("Unknown1"); // value 1 resets CGChat::m_complaintsSystemStatus in client. (unused?)

            if (ClientVersion.AddedInVersion(ClientVersionBuild.V4_2_2_14545)) // guessing
                packet.ReadByte("Unknown2"); // value 0xC generates a "CalendarError" in client.
        }

        [Parser(Opcode.CMSG_MINIGAME_MOVE)]
        public static void HandleMinigameMove(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("MoveType");
            packet.ReadUInt32("Param");
        }

        [Parser(Opcode.SMSG_MINIGAME_SETUP)]
        public static void HandleMiniGameSetup(Packet packet)
        {
            packet.ReadGuid("GUID");
            packet.ReadByte("unk byte");
            byte unk1 = packet.ReadByte("unk byte");
            packet.ReadGuid("unk guid");
            packet.ReadGuid("unk guid");
            packet.ReadCString("string");
            if (unk1 == 2)
                packet.ReadByte("unk byte");
        }

        [Parser(Opcode.SMSG_MINIGAME_STATE)]
        [Parser(Opcode.SMSG_DUEL_OUTOFBOUNDS)]
        [Parser(Opcode.CMSG_READY_FOR_ACCOUNT_DATA_TIMES)]
        [Parser(Opcode.CMSG_CALENDAR_GET_CALENDAR)]
        [Parser(Opcode.CMSG_CALENDAR_GET_NUM_PENDING)]
        [Parser(Opcode.CMSG_CHAR_ENUM)]
        [Parser(Opcode.CMSG_KEEP_ALIVE)]
        [Parser(Opcode.CMSG_TUTORIAL_RESET)]
        [Parser(Opcode.CMSG_TUTORIAL_CLEAR)]
        [Parser(Opcode.MSG_MOVE_WORLDPORT_ACK)]
        [Parser(Opcode.CMSG_MOUNTSPECIAL_ANIM)]
        [Parser(Opcode.CMSG_QUERY_TIME)]
        [Parser(Opcode.CMSG_PLAYER_LOGOUT)]
        [Parser(Opcode.CMSG_LOGOUT_REQUEST)]
        [Parser(Opcode.CMSG_LOGOUT_CANCEL)]
        [Parser(Opcode.SMSG_LOGOUT_CANCEL_ACK)]
        [Parser(Opcode.CMSG_WORLD_STATE_UI_TIMER_UPDATE)]
        [Parser(Opcode.CMSG_HEARTH_AND_RESURRECT)]
        [Parser(Opcode.CMSG_LFG_PLAYER_LOCK_INFO_REQUEST)]
        [Parser(Opcode.CMSG_LFG_PARTY_LOCK_INFO_REQUEST)]
        [Parser(Opcode.CMSG_REQUEST_RAID_INFO)]
        [Parser(Opcode.CMSG_BATTLEFIELD_STATUS)]
        [Parser(Opcode.CMSG_LFG_GET_STATUS)]
        [Parser(Opcode.SMSG_LFG_DISABLED)]
        [Parser(Opcode.CMSG_CHANNEL_VOICE_ON)]
        [Parser(Opcode.SMSG_COMSAT_CONNECT_FAIL)]
        [Parser(Opcode.SMSG_COMSAT_RECONNECT_TRY)]
        [Parser(Opcode.SMSG_COMSAT_DISCONNECT)]
        [Parser(Opcode.SMSG_VOICESESSION_FULL)] // 61 bytes in 2.4.1
        [Parser(Opcode.SMSG_DEBUG_SERVER_GEO)] // Was unknown
        [Parser(Opcode.SMSG_FORCE_SEND_QUEUED_PACKETS)]
        [Parser(Opcode.SMSG_GOSSIP_COMPLETE)]
        [Parser(Opcode.SMSG_CALENDAR_CLEAR_PENDING_ACTION)]
        [Parser(Opcode.CMSG_LFG_LEAVE)]
        [Parser(Opcode.CMSG_GROUP_DISBAND)]
        [Parser(Opcode.SMSG_CANCEL_COMBAT)]
        [Parser(Opcode.SMSG_DURABILITY_DAMAGE_DEATH)]
        [Parser(Opcode.CMSG_ATTACKSTOP)]
        [Parser(Opcode.SMSG_ATTACKSWING_NOTINRANGE)]
        [Parser(Opcode.SMSG_ATTACKSWING_BADFACING)]
        [Parser(Opcode.SMSG_ATTACKSWING_DEADTARGET)]
        [Parser(Opcode.SMSG_INVALID_PROMOTION_CODE)]
        [Parser(Opcode.SMSG_GROUP_DESTROYED)]
        [Parser(Opcode.SMSG_GROUP_UNINVITE)]
        [Parser(Opcode.CMSG_GROUP_DECLINE)]
        [Parser(Opcode.SMSG_ON_CANCEL_EXPECTED_RIDE_VEHICLE_AURA)]
        [Parser(Opcode.CMSG_CANCEL_AUTO_REPEAT_SPELL)]
        [Parser(Opcode.CMSG_CANCEL_GROWTH_AURA)]
        [Parser(Opcode.CMSG_CANCEL_MOUNT_AURA)]
        [Parser(Opcode.CMSG_COMPLETE_CINEMATIC)]
        [Parser(Opcode.CMSG_NEXT_CINEMATIC_CAMERA)]
        [Parser(Opcode.CMSG_REQUEST_PET_INFO)]
        [Parser(Opcode.CMSG_REQUEST_VEHICLE_EXIT)]
        [Parser(Opcode.CMSG_RESET_INSTANCES)]
        [Parser(Opcode.CMSG_SELF_RES)]
        [Parser(Opcode.MSG_RAID_READY_CHECK_FINISHED)]
        [Parser(Opcode.SMSG_ATTACKSWING_CANT_ATTACK)]
        [Parser(Opcode.SMSG_CORPSE_NOT_IN_INSTANCE)]
        [Parser(Opcode.SMSG_ENABLE_BARBER_SHOP)]
        [Parser(Opcode.SMSG_FISH_NOT_HOOKED)]
        [Parser(Opcode.SMSG_FISH_ESCAPED)]
        [Parser(Opcode.SMSG_SUMMON_CANCEL)]
        [Parser(Opcode.CMSG_MEETINGSTONE_INFO)]
        [Parser(Opcode.CMSG_RETURN_TO_GRAVEYARD)]
        [Parser(Opcode.CMSG_BATTLEFIELD_REQUEST_SCORE_DATA)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }
    }
}
