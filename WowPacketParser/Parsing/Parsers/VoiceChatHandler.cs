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
            packet.ReadBoolean("Voice Enabled");

            packet.ReadBoolean("Microphone Enabled");
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_ROSTER_UPDATE)]
        public static void HandleVoiceRosterUpdate(Packet packet)
        {
            packet.ReadInt64("Voice Channel ID");

            packet.ReadInt16("Channel ID");

            packet.ReadByte("Channel Type"); // 0: channel, 2: party

            packet.ReadCString("Channel Name");

            var key = Encoding.UTF8.GetString(packet.ReadBytes(16));
            Console.WriteLine("Encryption Key: " + key);

            packet.ReadUInt32("Voice Server IP");
            packet.ReadByte("Voice Server Port");

            var count = packet.ReadByte("Player Count");

            packet.ReadGuid("Leader GUID");

            var leaderFlags1 = packet.ReadByte();
            Console.WriteLine("Leader Flags 1: 0x" + leaderFlags1.ToString("X2"));

            var leaderFlags2 = packet.ReadByte();
            Console.WriteLine("Leader Flags 2: 0x" + leaderFlags2.ToString("X2"));

            for (var i = 0; i < count - 1; i++)
            {
                packet.ReadGuid("Player GUID");

                packet.ReadByte("Index");

                var flags1 = packet.ReadByte();
                Console.WriteLine("Flags 1: 0x" + flags1.ToString("X2"));

                var flags2 = packet.ReadByte();
                Console.WriteLine("Flags 2: 0x" + flags2.ToString("X2"));
            }
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_LEAVE)]
        public static void HandleVoiceLeave(Packet packet)
        {
            packet.ReadInt64("Unk Int64 1");

            packet.ReadInt64("Unk Int64 2");
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
