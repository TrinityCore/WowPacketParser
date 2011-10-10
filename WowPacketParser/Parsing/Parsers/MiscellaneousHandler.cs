using System;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class MiscellaneousParsers
    {
        [Parser(Opcode.SMSG_FEATURE_SYSTEM_STATUS)]
        public static void HandleFeatureSystemStatus(Packet packet)
        {
            packet.ReadByte("Unk Byte");
            packet.ReadBoolean("Enable Voice Chat");
        }

        [Parser(Opcode.CMSG_REALM_SPLIT)]
        public static void HandleClientRealmSplit(Packet packet)
        {
            packet.ReadInt32("Unk Int32");
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            packet.ReadInt32("Unk Int32");
            packet.ReadEnum<RealmSplitState>("Split State", TypeCode.Int32);
            packet.ReadCString("Unk String");
        }

        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing(Packet packet)
        {
            packet.ReadInt32("Ping");
            packet.ReadInt32("Latency");
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

        [Parser(Opcode.SMSG_TIME_SYNC_REQ)]
        public static void HandleTimeSyncReq(Packet packet)
        {
            packet.ReadInt32("Count");
        }

        [Parser(Opcode.SMSG_LEARNED_DANCE_MOVES)]
        public static void HandleLearnedDanceMoves(Packet packet)
        {
            packet.ReadInt32("Dance Move Id");
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

            if (packet.GetOpcode() == Opcode.SMSG_PLAY_OBJECT_SOUND)
                packet.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            packet.ReadEnum<WeatherState>("State", TypeCode.Int32);
            packet.ReadSingle("Grade");
            packet.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_LEVELUP_INFO)]
        public static void HandleLevelUp(Packet packet)
        {
            var level = packet.ReadInt32("Level");
            packet.ReadInt32("Health");
            for (var i = 0; i < 6; i++)
                packet.ReadInt32("Power " + (PowerType)i);

            for (var i = 0; i < 5; i++)
                packet.ReadInt32("StatType " + (StatType)i);

            SessionHandler.LoggedInCharacter.Level = level;
        }

        [Parser(Opcode.CMSG_TUTORIAL_FLAG)]
        public static void HandleTutorialFlag(Packet packet)
        {
            var flag = packet.ReadInt32();
            Console.WriteLine("Flag: 0x" + flag.ToString("X8"));
        }

        [Parser(Opcode.SMSG_TUTORIAL_FLAGS)]
        public static void HandleTutorialFlags(Packet packet)
        {
            for (var i = 0; i < 8; i++)
            {
                var flag = packet.ReadInt32();
                Console.WriteLine("Flags " + i + ": 0x" + flag.ToString("X8"));
            }
        }

        [Parser(Opcode.CMSG_AREATRIGGER)]
        public static void HandleClientAreaTrigger(Packet packet)
        {
            packet.ReadInt32("Area Trigger Id");
        }

        [Parser(Opcode.SMSG_PRE_RESURRECT)]
        public static void HandlePreRessurect(Packet packet)
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
            packet.ReadUInt32("Area Id");
            packet.ReadUInt32("Experience");
        }

        [Parser(Opcode.SMSG_START_MIRROR_TIMER)]
        public static void HandleStartMirrorTimer(Packet packet)
        {
            packet.ReadEnum<MirrorTimerType>("Timer Type", TypeCode.UInt32);
            packet.ReadUInt32("Current Value");
            packet.ReadUInt32("Max Value");
            packet.ReadUInt32("Regen");
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
            Console.WriteLine("Map Id: " + Extensions.MapLine(packet.ReadInt32()));
            packet.ReadVector3("Position");
        }

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
        [Parser(Opcode.CMSG_LFD_PLAYER_LOCK_INFO_REQUEST)]
        [Parser(Opcode.CMSG_LFD_PARTY_LOCK_INFO_REQUEST)]
        [Parser(Opcode.CMSG_REQUEST_RAID_INFO)]
        [Parser(Opcode.CMSG_GMTICKET_GETTICKET)]
        [Parser(Opcode.CMSG_BATTLEFIELD_STATUS)]
        [Parser(Opcode.CMSG_LFG_GET_STATUS)]
        [Parser(Opcode.SMSG_LFG_DISABLED)]
        [Parser(Opcode.CMSG_CHANNEL_VOICE_ON)]
        [Parser(Opcode.SMSG_COMSAT_CONNECT_FAIL)]
        [Parser(Opcode.SMSG_COMSAT_RECONNECT_TRY)]
        [Parser(Opcode.SMSG_COMSAT_DISCONNECT)]
        [Parser(Opcode.SMSG_VOICESESSION_FULL)]
        [Parser(Opcode.SMSG_DEBUG_SERVER_GEO)] // Was unknown
        [Parser(Opcode.SMSG_FORCE_SEND_QUEUED_PACKETS)]
        [Parser(Opcode.SMSG_GOSSIP_COMPLETE)]
        [Parser(Opcode.SMSG_CALENDAR_CLEAR_PENDING_ACTION)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }
    }
}
