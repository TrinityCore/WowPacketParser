using System;
using System.Text;
using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class VoiceChatHandler
    {
        [Parser(Opcode.CMSG_VOICE_SESSION_ENABLE)]
        public static void HandleVoiceSessionEnable(Packet packet)
        {
            var voiceEnabled = packet.ReadBoolean();
            Console.WriteLine("Voice Enabled: " + voiceEnabled);

            var micEnabled = packet.ReadByte();
            Console.WriteLine("Microphone Enabled. " + micEnabled);
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_ROSTER_UPDATE)]
        public static void HandleVoiceRosterUpdate(Packet packet)
        {
            var unk64 = packet.ReadInt64();
            Console.WriteLine("Unk Int64: " + unk64);

            var chanId = packet.ReadInt16();
            Console.WriteLine("Channel ID: " + chanId);

            var chanName = packet.ReadCString();
            Console.WriteLine("Channel Name: " + chanName);

            var key = Encoding.UTF8.GetString(packet.ReadBytes(16));
            Console.WriteLine("Encryption Key: " + key);

            var ip = packet.ReadInt32();
            Console.WriteLine("Voice Server IP: " + ip);

            var count = packet.ReadByte();
            Console.WriteLine("Player Count: " + count);

            var leaderGuid = packet.ReadGuid();
            Console.WriteLine("Leader GUID: " + leaderGuid);

            var leaderFlags = packet.ReadByte();
            Console.WriteLine("Leader Flags: 0x" + leaderFlags.ToString("X2"));

            var unk = packet.ReadByte();
            Console.WriteLine("Unk Byte 1: " + unk);

            for (var i = 0; i < count - 1; i++)
            {
                var guid = packet.ReadGuid();
                Console.WriteLine("Player GUID: " + guid);

                var idx = packet.ReadByte();
                Console.WriteLine("Index: " + idx);

                var flags = packet.ReadByte();
                Console.WriteLine("Flags: 0x" + flags.ToString("X2"));

                var unk2 = packet.ReadByte();
                Console.WriteLine("Unk Byte 2: " + unk2);
            }
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_LEAVE)]
        public static void HandleVoiceLeave(Packet packet)
        {
            var unk1 = packet.ReadInt64();
            Console.WriteLine("Unk Int64 1: " + unk1);

            var unk2 = packet.ReadInt64();
            Console.WriteLine("Unk Int64 2: " + unk2);
        }

        [Parser(Opcode.SMSG_VOICE_SET_TALKER_MUTED)]
        public static void HandleSetTalkerMuted(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var unk = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unk);
        }

        [Parser(Opcode.SMSG_VOICE_PARENTAL_CONTROLS)]
        public static void HandleVoiceParentalControls(Packet packet)
        {
            var disableAll = packet.ReadBoolean();
            Console.WriteLine("Disable All: " + disableAll);

            var disableMic = packet.ReadBoolean();
            Console.WriteLine("Disable Microphone: " + disableMic);
        }

        [Parser(Opcode.SMSG_AVAILABLE_VOICE_CHANNEL)]
        public static void HandleAvailableVoiceChannel(Packet packet)
        {
            var unk = packet.ReadInt64();
            Console.WriteLine("Unk Int64 1: " + unk);

            var type = packet.ReadByte();
            Console.WriteLine("Channel Type: " + type);

            var name = packet.ReadCString();
            Console.WriteLine("Channel Name: " + name);

            var unk2 = packet.ReadInt64();
            Console.WriteLine("Unk Int64 2: " + unk2);
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_VOICE_CHANNEL)]        
        public static void HandleSetActiveVoiceChannel(Packet packet)
        {
            var chanId = packet.ReadInt32();
            Console.WriteLine("Channel ID: " + chanId);

            var name = packet.ReadCString();
            Console.WriteLine("Channel Name: " + name);
        }

        [Parser(Opcode.SMSG_VOICE_SET_TALKER_MUTED)]
        public static void HandleTalkerMuted(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);

            var unk = packet.ReadByte();
            Console.WriteLine("Unk Byte: " + unk);
        }

        [Parser(Opcode.CMSG_ADD_VOICE_IGNORE)]
        public static void HandleAddVoiceIgnore(Packet packet)
        {
            var name = packet.ReadCString();
            Console.WriteLine("Name: " + name);
        }

        [Parser(Opcode.CMSG_DEL_VOICE_IGNORE)]
        public static void HandleDelVoiceIgnore(Packet packet)
        {
            var guid = packet.ReadGuid();
            Console.WriteLine("GUID: " + guid);
        }

        [Parser(Opcode.SMSG_VOICE_CHAT_STATUS)]
        public static void HandleVoiceStatus(Packet packet)
        {
            var status = packet.ReadByte();
            Console.WriteLine("Status: " + status);
        }
    }
}
