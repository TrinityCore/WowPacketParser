using WowPacketParser.Enums;
using WowPacketParser.Misc;

namespace WowPacketParser.Parsing.Parsers
{
    public static class VoiceChatHandler
    {
        [Parser(Opcode.CMSG_VOICE_SESSION_ENABLE)]
        public static void HandleVoiceSessionEnable(Packet packet)
        {
            packet.Translator.ReadBool("Voice Enabled");
            packet.Translator.ReadBool("Microphone Enabled");
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_ROSTER_UPDATE)]
        public static void HandleVoiceRosterUpdate(Packet packet)
        {
            packet.Translator.ReadGuid("Group GUID");
            packet.Translator.ReadInt16("Channel ID");
            packet.Translator.ReadByte("Channel Type"); // 0: channel, 2: party
            packet.Translator.ReadCString("Channel Name");
            packet.Translator.ReadBytes("Encryption Key", 16);
            packet.Translator.ReadIPAddress("IP");
            packet.Translator.ReadInt16("Voice Server Port");

            var count = packet.Translator.ReadByte("Player Count");

            packet.Translator.ReadGuid("Leader GUID");

            var leaderFlags1 = packet.Translator.ReadByte();
            packet.AddValue("Leader Flags 1", "0x" + leaderFlags1.ToString("X2"));

            var leaderFlags2 = packet.Translator.ReadByte();
            packet.AddValue("Leader Flags 2", "0x" + leaderFlags2.ToString("X2"));

            for (var i = 0; i < count - 1; i++)
            {
                packet.Translator.ReadGuid("Player GUID");
                packet.Translator.ReadByte("Index");
                var flags1 = packet.Translator.ReadByte();
                packet.AddValue("Flags 1", "0x" + flags1.ToString("X2"));
                var flags2 = packet.Translator.ReadByte();
                packet.AddValue("Flags 2", "0x" + flags2.ToString("X2"));
            }
        }

        [Parser(Opcode.SMSG_VOICE_SESSION_LEAVE)]
        public static void HandleVoiceLeave(Packet packet)
        {
            packet.Translator.ReadGuid("Player GUID");
            packet.Translator.ReadGuid("Group GUID");
        }

        [Parser(Opcode.SMSG_VOICE_SET_TALKER_MUTED)]
        public static void HandleSetTalkerMuted(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
            packet.Translator.ReadByte("Unk Byte");
        }

        [Parser(Opcode.SMSG_VOICE_PARENTAL_CONTROLS)]
        public static void HandleVoiceParentalControls(Packet packet)
        {
            packet.Translator.ReadBool("Disable All");
            packet.Translator.ReadBool("Disable Microphone");
        }

        [Parser(Opcode.SMSG_AVAILABLE_VOICE_CHANNEL)]
        public static void HandleAvailableVoiceChannel(Packet packet)
        {
            packet.Translator.ReadGuid("Group GUID");
            packet.Translator.ReadByte("Channel Type");
            packet.Translator.ReadCString("Channel Name");
            packet.Translator.ReadGuid("Player GUID");
        }

        [Parser(Opcode.CMSG_SET_ACTIVE_VOICE_CHANNEL)]
        public static void HandleSetActiveVoiceChannel(Packet packet)
        {
            packet.Translator.ReadInt32("Channel ID");
            packet.Translator.ReadCString("Channel Name");
        }

        [Parser(Opcode.CMSG_VOICE_ADD_IGNORE)]
        public static void HandleAddVoiceIgnore(Packet packet)
        {
            packet.Translator.ReadCString("Name");
        }

        [Parser(Opcode.CMSG_VOICE_DEL_IGNORE)]
        public static void HandleDelVoiceIgnore(Packet packet)
        {
            packet.Translator.ReadGuid("GUID");
        }

        [Parser(Opcode.SMSG_VOICE_CHAT_STATUS)]
        public static void HandleVoiceStatus(Packet packet)
        {
            packet.Translator.ReadByte("Status");
        }
    }
}
