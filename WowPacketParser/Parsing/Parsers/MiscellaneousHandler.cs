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
            var unknown = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unknown);

            var voiceChat = packet.ReadBoolean();
            Console.WriteLine("Voice Chat On: " + voiceChat);
        }

        [Parser(Opcode.CMSG_REALM_SPLIT)]
        public static void HandleClientRealmSplit(Packet packet)
        {
            var unk = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unk);
        }

        [Parser(Opcode.SMSG_REALM_SPLIT)]
        public static void HandleServerRealmSplit(Packet packet)
        {
            var unk = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + unk);

            var state = (RealmSplitState)packet.ReadInt32();
            Console.WriteLine("Split State: " + state);

            var unkDate = packet.ReadCString();
            Console.WriteLine("Unk String: " + unkDate);
        }

        [Parser(Opcode.CMSG_PING)]
        public static void HandleClientPing(Packet packet)
        {
            var ping = packet.ReadInt32();
            Console.WriteLine("Ping: " + ping);

            var latency = packet.ReadInt32();
            Console.WriteLine("Latency: " + latency);
        }

        [Parser(Opcode.SMSG_PONG)]
        public static void HandleServerPong(Packet packet)
        {
            var ping = packet.ReadInt32();
            Console.WriteLine("Ping: " + ping);
        }

        [Parser(Opcode.SMSG_CLIENTCACHE_VERSION)]
        public static void HandleClientCacheVersion(Packet packet)
        {
            var version = packet.ReadInt32();
            Console.WriteLine("Version: " + version);
        }

        [Parser(Opcode.SMSG_TIME_SYNC_REQ)]
        public static void HandleTimeSyncReq(Packet packet)
        {
            var gameTime = packet.ReadInt32();
            Console.WriteLine("Unk Int32: " + gameTime);
        }

        [Parser(Opcode.SMSG_LEARNED_DANCE_MOVES)]
        public static void HandleLearnedDanceMoves(Packet packet)
        {
            var unk64 = packet.ReadInt64();
            Console.WriteLine("Dance Move ID: " + unk64);
        }

        [Parser(Opcode.SMSG_TRIGGER_CINEMATIC)]
        [Parser(Opcode.SMSG_TRIGGER_MOVIE)]
        public static void HandleTriggerSequence(Packet packet)
        {
            var id = packet.ReadInt32();
            Console.WriteLine("Sequence ID: " + id);
        }

        [Parser(Opcode.SMSG_PLAY_SOUND)]
        [Parser(Opcode.SMSG_PLAY_MUSIC)]
        [Parser(Opcode.SMSG_PLAY_OBJECT_SOUND)]
        public static void HandleSoundMessages(Packet packet)
        {
            var soundId = packet.ReadInt32();
            Console.WriteLine("Sound ID: " + soundId);

            if (packet.GetOpcode() != Opcode.SMSG_PLAY_OBJECT_SOUND)
                return;

            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.SMSG_WEATHER)]
        public static void HandleWeatherStatus(Packet packet)
        {
            var state = (WeatherState)packet.ReadInt32();
            Console.WriteLine("State: " + state);

            var grade = packet.ReadSingle();
            Console.WriteLine("Grade: " + grade);

            var unkByte = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unkByte);
        }

        [Parser(Opcode.SMSG_LEVELUP_INFO)]
        public static void HandleLevelUp(Packet packet)
        {
            var level = packet.ReadInt32();
            Console.WriteLine("Level: " + level);

            var health = packet.ReadInt32();
            Console.WriteLine("Health: " + health);

            for (var i = 0; i < 6; i++)
            {
                var power = packet.ReadInt32();
                Console.WriteLine((PowerType)i + " Value: " + power);
            }

            for (var i = 0; i < 5; i++)
            {
                var stat = packet.ReadInt32();
                Console.WriteLine((StatType)i + " Value: " + stat);
            }

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
            var id = packet.ReadInt32();
            Console.WriteLine("Area Trigger ID: " + id);
        }

        [Parser(Opcode.SMSG_PRE_RESURRECT)]
        public static void HandlePreRessurect(Packet packet)
        {
            var guid = packet.ReadPackedGuid();
            Console.WriteLine("GUID: " + guid);
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
        [Parser(Opcode.CMSG_MEETINGSTONE_INFO)]
        [Parser(Opcode.SMSG_LFG_DISABLED)]
        [Parser(Opcode.SMSG_QUESTLOG_FULL)]
        [Parser(Opcode.CMSG_CHANNEL_VOICE_ON)]
        [Parser(Opcode.SMSG_COMSAT_CONNECT_FAIL)]
        [Parser(Opcode.SMSG_COMSAT_RECONNECT_TRY)]
        [Parser(Opcode.SMSG_COMSAT_DISCONNECT)]
        [Parser(Opcode.SMSG_VOICESESSION_FULL)]
        [Parser(Opcode.SMSG_DEBUG_SERVER_GEO)] // Was unknown
        [Parser(Opcode.SMSG_FORCE_SEND_QUEUED_PACKETS)]
        [Parser(Opcode.SMSG_GOSSIP_COMPLETE)]
        public static void HandleZeroLengthPackets(Packet packet)
        {
        }
    }
}
